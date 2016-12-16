////-----------------------------------------------------------------------
// <copyright file="Login.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>05/03/2012</date>
// <summary>no summary</summary>
// <project>LifeSpring<project>
////********************************History********************************
//     Date    Add/Modified by     Method   Summary
//
////-----------------------------------------------------------------------

#region Namespaces
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using DhiiLifeSpring.AppInfrastructure;
using DhiiLifeSpring.BusinessEntities;
using DhiiLifeSpring.BusinessLayer;
using DhiiLifeSpring.Security;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Net;
using System.Collections.Generic;
#endregion

namespace DhiiLifeSpring
{
    public partial class Login : BasePage
    {
        #region User Defined Methods

        private void GetIpAddress()
        {
            string strIpAddress;
            strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIpAddress == null)
            {
                strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            Session["ISPIpAddress"] = strIpAddress;
        }
        protected long Log(Int64? LogId, String Message)
        {
            return new BL_Logs().InsertUpdateEntryLog(LogId, Session["UserID"] == null ? "Anonymous" : Convert.ToString(Session["UserID"]), Session["ISPIpAddress"] == null ? "" : Session["ISPIpAddress"].ToString(), Message, null);
        }

        void BindBranch()
        {
            ddlBranch.Items.Clear();
            DataSet dsfacility = new BL_Facility().GetFacilities();
            DataView dvFacility = new DataView();
            if (dsfacility.Tables.Count > 0 && dsfacility != null && dsfacility.Tables[0].Rows.Count > 0)
            {
                dvFacility = dsfacility.Tables[0].DefaultView;

                dvFacility.RowFilter = "Status= true ";
                ddlBranch.DataSource = dvFacility;
                ddlBranch.DataTextField = "FacilityName";
                ddlBranch.DataValueField = "FacilityId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, "Select");
            }
            else
            {
                ddlBranch.Items.Insert(0, "Select");
            }
        }

        #endregion

        #region Page Event Handlers

        protected void Page_Init(object Sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtUserName.Focus();
                    BindBranch();
                    PnlLogin.Enabled = true;
                    GetIpAddress();
                }
            }
            catch (Exception ex)
            {
                if (!(ex is System.Threading.ThreadAbortException))
                    WriteLogItem(ex.Message, ex.StackTrace, Session["UserID"] == null ? "Anonymous" : Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                lblMsg.Text = string.Empty;
                ddlBranch.SelectedIndex = 0;
                txtUserName.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Session["UserID"] == null ? "Anonymous" : Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                int count = Convert.ToInt32(Session["LoginCount"]);
                if (count > Convert.ToInt32(ConfigurationManager.AppSettings["LoginCount"]))
                {
                    lblMsg.Text = "Your no. of attempts exceeded maximum attempts to login";
                    return;
                }
                if (txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
                {
                    lblMsg.Text = "Required userName and password";
                    return;
                }
                Session["UserId"] = null;
                BE_Login beLogin = new BE_Login();
                beLogin.UserName = txtUserName.Text.Trim();
                beLogin.Password = Cryptography.Encrypt(txtPassword.Text);
                beLogin.FailityID = Convert.ToInt32(ddlBranch.SelectedValue);
                BL_Login blLogin = new BL_Login();
                DataSet dsIsvalidUser = blLogin.ValidateUser(beLogin);
                if (dsIsvalidUser != null && dsIsvalidUser.Tables.Count > 0 && dsIsvalidUser.Tables[0].Rows.Count > 0)
                {
                    DataRow drIsValidUser = dsIsvalidUser.Tables[0].Rows[0];
                    if (Convert.ToBoolean(drIsValidUser["Active"]))
                    {
                        FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, txtUserName.Text, DateTime.Now, DateTime.Now.AddMinutes(Session.Timeout), false, ConfigurationManager.AppSettings["GroupName"]);
                        String cookiestr = FormsAuthentication.Encrypt(tkt);
                        HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                        ck.Expires = tkt.Expiration;
                        ck.Path = FormsAuthentication.FormsCookiePath;
                        Response.Cookies.Add(ck);

                        Session["GroupId"] = 1;
                        Session["GroupName"] = ConfigurationManager.AppSettings["GroupName"];
                        Session["DBConnectionString"] = null;
                        Session["UserID"] = txtUserName.Text;
                        Session["UserName"] = txtUserName.Text;
                        Session["EntryLogId"] = Log(null, "Login by the user");
                        Session["FacilityId"] = drIsValidUser["FacilityId"].ToString().ToUpper();
                        Session["FacilityName"] = drIsValidUser["FacilityName"].ToString().ToUpper();
                        Session["EmpId"] = drIsValidUser["EmpId"];
                        Session["EmpName"] = drIsValidUser["EmpName"].ToString().ToUpper();
                        Session["RoleName"] = drIsValidUser["RoleName"].ToString().ToUpper();
                        Session["RoleId"] = drIsValidUser["RoleId"].ToString().ToUpper();
                        Session["IsAdmin"] = drIsValidUser["IsAdmin"];
                        Session["LoginCount"] = 0;
                        Session["CLINIC_ID"] = drIsValidUser["FacilityId"];
                        String returnUrl = Request["ReturnUrl"];
                        Session["Menu"] = new BL_Login().GetMenuDatset(txtUserName.Text.Trim(), false, 0);
                        if (returnUrl == null)
                            Response.Redirect("~/Application/Common/DashBoard.aspx", true);
                        else
                            Response.Redirect(returnUrl, true);
                    }
                    else
                    {
                        Session["EntryLogId"] = Log(null, "Loged in by the inactive user");
                        lblMsg.Text = "Your account is inactive, please check your email";
                    }
                }
                else
                {
                    beLogin.UserName = txtUserName.Text.Trim();
                    beLogin.Password = null;
                    dsIsvalidUser = blLogin.ValidateUser(beLogin);
                    if (dsIsvalidUser != null && dsIsvalidUser.Tables.Count > 0 && dsIsvalidUser.Tables[0].Rows.Count > 0)
                    {
                        lblMsg.Text = "Invalid password";
                        txtPassword.Focus();
                    }
                    else
                    {
                        lblMsg.Text = "Invalid username";
                        txtUserName.Text = string.Empty;
                        txtUserName.Focus();
                    }
                    Session["GroupId"] = null;
                    Session["GroupName"] = null;
                    Session["DBConnectionString"] = null;
                    Session["EntryLogId"] = null;
                    Session["FacilityId"] = null;
                    Session["FacilityName"] = null;
                    Session["UserID"] = null;
                    Session["UserName"] = null;
                    Session["EmpId"] = null;
                    Session["EmpName"] = null;
                    Session["RoleName"] = null;
                    Session["IsAdmin"] = null;
                    Session["AdminEmail"] = null;
                    Session["Menu"] = null;
                    Session["LoginCount"] = count + 1;
                }
            }
            catch (Exception ex)
            {
                if (!(ex is System.Threading.ThreadAbortException))
                    WriteLogItem(ex.Message, ex.StackTrace, Session["UserID"] == null ? "Anonymous" : Convert.ToString(Session["UserID"]));
            }
        }

        #endregion
    }
}