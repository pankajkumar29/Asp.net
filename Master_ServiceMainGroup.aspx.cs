////-----------------------------------------------------------------------
// <copyright file="Master_ServiceMainGroup.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>24/04/2012</date>
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
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhiiLifeSpring.AppInfrastructure;
using DhiiLifeSpring.BusinessEntities;
using DhiiLifeSpring.BusinessLayer;
#endregion

namespace DhiiLifeSpring.Application.Masters
{
    public partial class Master_ServiceMainGroup : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtMainGroup.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Main Group
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindMainGroup</param>
        private void BindMainGroup(bool openOnEmpty)
        {
            DataSet dsMainGroup = null;

            if (ddlStatus.SelectedIndex > 0)
                dsMainGroup = new BL_ServiceMainGroup().GetServiceMainGroup(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsMainGroup = new BL_ServiceMainGroup().GetServiceMainGroup(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["MainGroup"] = dsMainGroup;
            if (dsMainGroup != null && dsMainGroup.Tables.Count > 0 && dsMainGroup.Tables[0].Rows.Count > 0)
            {
                gvMainGroup.DataSource = dsMainGroup;
                gvMainGroup.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsMainGroup.Tables[0].Rows.Count.ToString().ToUpper();
                if (ViewState["SortExpression"] != null)
                {
                    if (ViewState["SortDirection"] != null)
                    {
                        if (ViewState["SortDirection"].ToString() == "ASC")
                        {
                            ViewState["SortDirection"] = "DESC";

                        }
                        else
                            ViewState["SortDirection"] = "ASC";
                        gvMainGroup.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }

                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvMainGroup.DataSource = null;
                gvMainGroup.DataBind();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Main Group
        /// </summary>
        private void AddMainGroup()
        {
            modlpopup.Show();
            DataSet dsMainGroup = new BL_ServiceMainGroup().GetServiceMainGroup(null, null, null);
            DataView dvMainGroup = dsMainGroup.Tables[0].DefaultView;
            DataRow[] rows = dsMainGroup.Tables[0].Select("ServiceMainGroup ='" + txtMainGroup.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Service Group already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtMainGroup.Text = string.Empty;
                txtMainGroup.Focus();
                return;
            }
            BE_ServiceMainGroup beMainGroup = new BE_ServiceMainGroup();
            beMainGroup.ServiceMainGroupId = 0;
            beMainGroup.ServiceMainGroup = txtMainGroup.Text.Trim().ToUpper();
            beMainGroup.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beMainGroup.Status = true;
                beMainGroup.Reason = string.Empty;
            }
            else
            {
                beMainGroup.Status = false;
                beMainGroup.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_ServiceMainGroup().InsertUpdateServiceMainGroup(beMainGroup);
            if (result > 0)
            {
                lblMsg.Text = "Added successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Failed to add details";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            rbtnStatus.SelectedIndex = 0;
            txtMainGroup.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
            btnAdd.Text = "Add";
            btnAdd.ToolTip = "Add";
            hbtntext.Value = "Add";
            BindMainGroup(false);
            txtMainGroup.Focus();
        }

        /// <summary>
        /// This Method is used to Update Main Group
        /// </summary>
        private void UpdateMainGroup()
        {
            modlpopup.Show();
            DataSet dsMainGroup = new BL_ServiceMainGroup().GetServiceMainGroup(null, null, null);
            DataView dvMainGroup = dsMainGroup.Tables[0].DefaultView;
            DataRow[] rows = dsMainGroup.Tables[0].Select("ServiceMainGroupId <> " + Convert.ToInt32(hfMainGroupId.Value) + "AND ServiceMainGroup ='" + txtMainGroup.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Service Main Group already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtMainGroup.Text = string.Empty;
                txtMainGroup.Focus();
                return;
            }
            BE_ServiceMainGroup beMainGroup = new BE_ServiceMainGroup();
            beMainGroup.ServiceMainGroupId = Convert.ToInt32(hfMainGroupId.Value.Trim().ToUpper());
            beMainGroup.ServiceMainGroup = txtMainGroup.Text.Trim().ToUpper();
            beMainGroup.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beMainGroup.Status = true;
                beMainGroup.Reason = string.Empty;
            }
            else
            {
                beMainGroup.Status = false;
                beMainGroup.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_ServiceMainGroup().InsertUpdateServiceMainGroup(beMainGroup);
            if (result > 0)
            {
                lblMsg.Text = "Updated successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Failed to update details";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            BindMainGroup(false);
            txtMainGroup.Focus();
        }

        #endregion

        #region Page Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UserID"] == null || (Session["UserID"] != null && Convert.ToString(Session["UserID"]) == string.Empty))
                    Response.Redirect(ConfigurationManager.AppSettings["LoginUrl"]);
                else
                {
                    lblMsg.Text = string.Empty;
                    txtMainGroup.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindMainGroup(true);
                        btnAddMainGroup.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(ex is System.Threading.ThreadAbortException))
                    WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindMainGroup(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvMainGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvMainGroup);
                    }
                    else
                    {
                        Image sortImage = new Image();
                        sortImage.ImageUrl = "~/Images/UpArrow.png";
                        sortImage.Width = 10;
                        sortImage.Height = 10;
                        sortImage.AlternateText = "Ascending Order";
                        e.Row.Cells[1].Controls.Add(sortImage);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvMainGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMainGroup.PageIndex = e.NewPageIndex;
                BindMainGroup(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvMainGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsMainGroup = new BL_ServiceMainGroup().GetServiceMainGroup(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsMainGroup != null && dsMainGroup.Tables.Count > 0 && dsMainGroup.Tables[0].Rows.Count > 0)
                    {
                        hfMainGroupId.Value = e.CommandArgument.ToString().ToUpper();
                        txtMainGroup.Text = dsMainGroup.Tables[0].Rows[0]["ServiceMainGroup"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsMainGroup.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsMainGroup.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtMainGroup.Focus();

                        if (ViewState["SortExpression"] != null)
                        {
                            if (ViewState["SortDirection"] != null)
                            {
                                if (ViewState["SortDirection"].ToString() == "ASC")
                                {
                                    ViewState["SortDirection"] = "DESC";
                                }
                                else
                                    ViewState["SortDirection"] = "ASC";
                                gvMainGroup.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvMainGroup_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["MainGroup"] as DataSet;
                DataView dv = new DataView();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    dv = new DataView(ds.Tables[0]);

                String SortExpresssion = e.SortExpression;
                String SortDirection;
                if (ViewState["SortDirection"].ToString() == "ASC")
                    SortDirection = "DESC";
                else
                    SortDirection = "ASC";
                ViewState["SortDirection"] = SortDirection;
                ViewState["SortExpression"] = SortExpresssion;

                if (null != SortExpresssion && string.Empty != SortExpresssion)
                {
                    String strSort = String.Format("{0} {1}", SortExpresssion, SortDirection);
                    dv = new DataView(dv.ToTable(), string.Empty, strSort, DataViewRowState.CurrentRows);
                    gvMainGroup.DataSource = dv;
                    gvMainGroup.DataBind();
                    gvMainGroup.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddMainGroup_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                modlpopup.Show();
                txtMainGroup.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void rbtnStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                txtMainGroup.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfMainGroupId.Value != string.Empty)
                    {
                        txtReason.Focus();
                    }
                }
                else
                {
                    txtReason.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (hbtntext.Value == "Add")
                    AddMainGroup();
                else if (hbtntext.Value == "Update")
                    UpdateMainGroup();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                modlpopup.Show();
                txtMainGroup.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}