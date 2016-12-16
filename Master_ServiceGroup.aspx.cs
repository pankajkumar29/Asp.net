////-----------------------------------------------------------------------
// <copyright file="Master_ServiceGroup.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\kirankumarj</author>
// <email>kiran.kumarj@dhii.in</email>
// <date>12/03/2012</date>
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
    public partial class Master_ServiceGroup : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            ddlMainGroup.SelectedIndex = 0;
            txtServiceGroup.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Main Groups
        /// </summary>
        /// <param name="MainGroupId">This Parameter Belongs to BindMainGroups</param>
        private void BindMainGroups(Int32? MainGroupId)
        {
            ddlMainGroup.Items.Clear();
            DataSet dsMainGroups = new BL_ServiceMainGroup().GetServiceMainGroup(null, null, null);
            if (dsMainGroups.Tables.Count > 0 && dsMainGroups != null && dsMainGroups.Tables[0].Rows.Count > 0)
            {
                DataView dvMainGroups = dsMainGroups.Tables[0].DefaultView;
                if (hfServiceGroupId.Value == "")
                {
                    dvMainGroups.RowFilter = "Status = true";
                }
                if ((hfServiceGroupId.Value != string.Empty) && (MainGroupId != null))
                {
                    dvMainGroups.RowFilter = "Status = true or ServiceMainGroupId = " + MainGroupId;
                }
                ddlMainGroup.DataSource = dsMainGroups;
                ddlMainGroup.DataTextField = "ServiceMainGroup";
                ddlMainGroup.DataValueField = "ServiceMainGroupId";
                ddlMainGroup.DataBind();
            }
            ddlMainGroup.Items.Insert(0, "Select");
        }

        /// <summary>
        /// This Method is used to Bind Service Group
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindServiceGroup</param>
        private void BindServiceGroup(bool openOnEmpty)
        {
            DataSet dsServiceGroup = null;

            if (ddlStatus.SelectedIndex > 0)
                dsServiceGroup = new BL_ServiceGroup().GetServiceGroup(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsServiceGroup = new BL_ServiceGroup().GetServiceGroup(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["ServiceGroup"] = dsServiceGroup;
            if (dsServiceGroup != null && dsServiceGroup.Tables.Count > 0 && dsServiceGroup.Tables[0].Rows.Count > 0)
            {
                gvServiceGroup.DataSource = dsServiceGroup;
                gvServiceGroup.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsServiceGroup.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvServiceGroup.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }

                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvServiceGroup.DataSource = null;
                gvServiceGroup.DataBind();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Service Group
        /// </summary>
        private void AddServiceGroup()
        {
            modlpopup.Show();
            DataSet dsServiceGroup = new BL_ServiceGroup().GetServiceGroup(null, null, null);
            DataView dvServiceGroup = dsServiceGroup.Tables[0].DefaultView;
            DataRow[] rows = dsServiceGroup.Tables[0].Select("ServiceGroup ='" + txtServiceGroup.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Service Group already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtServiceGroup.Text = string.Empty;
                txtServiceGroup.Focus();
                return;
            }
            BE_ServiceGroup beServiceGroup = new BE_ServiceGroup();
            beServiceGroup.ServiceGroupId = 0;
            beServiceGroup.ServiceMainGroupId = Convert.ToInt32(ddlMainGroup.SelectedValue.Trim().ToUpper());
            beServiceGroup.ServiceGroup = txtServiceGroup.Text.Trim().ToUpper();
            beServiceGroup.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beServiceGroup.Status = true;
                beServiceGroup.Reason = string.Empty;
            }
            else
            {
                beServiceGroup.Status = false;
                beServiceGroup.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_ServiceGroup().InsertUpdateServiceGroup(beServiceGroup);
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
            ClearControls();
            btnAdd.Text = "Add";
            btnAdd.ToolTip = "Add";
            hbtntext.Value = "Add";
            BindServiceGroup(false);
            ddlMainGroup.Focus();
        }

        /// <summary>
        /// This Method is used to Update Service Group
        /// </summary>
        private void UpdateServiceGroup()
        {
            modlpopup.Show();
            DataSet dsServiceGroup = new BL_ServiceGroup().GetServiceGroup(null, null, null);
            DataView dvServiceGroup = dsServiceGroup.Tables[0].DefaultView;
            DataRow[] rows = dsServiceGroup.Tables[0].Select("ServiceGroupId <> " + Convert.ToInt32(hfServiceGroupId.Value) + "AND ServiceGroup ='" + txtServiceGroup.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Service Group already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtServiceGroup.Text = string.Empty;
                txtServiceGroup.Focus();
                return;
            }
            BE_ServiceGroup beServiceGroup = new BE_ServiceGroup();
            beServiceGroup.ServiceGroupId = Convert.ToInt32(hfServiceGroupId.Value.Trim().ToUpper());
            beServiceGroup.ServiceMainGroupId = Convert.ToInt32(ddlMainGroup.SelectedValue.Trim().ToUpper());
            beServiceGroup.ServiceGroup = txtServiceGroup.Text.Trim().ToUpper();
            beServiceGroup.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beServiceGroup.Status = true;
                beServiceGroup.Reason = string.Empty;
            }
            else
            {
                beServiceGroup.Status = false;
                beServiceGroup.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_ServiceGroup().InsertUpdateServiceGroup(beServiceGroup);
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
            BindServiceGroup(false);
            ddlMainGroup.Focus();
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
                    txtServiceGroup.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindMainGroups(null);
                        BindServiceGroup(true);
                        btnAddServiceGroup.Focus();
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
                BindServiceGroup(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvServiceGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvServiceGroup);
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

        protected void gvServiceGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvServiceGroup.PageIndex = e.NewPageIndex;
                BindServiceGroup(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvServiceGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsServiceGroup = new BL_ServiceGroup().GetServiceGroup(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsServiceGroup != null && dsServiceGroup.Tables.Count > 0 && dsServiceGroup.Tables[0].Rows.Count > 0)
                    {
                        hfServiceGroupId.Value = e.CommandArgument.ToString().ToUpper();
                        BindMainGroups(null);
                        ddlMainGroup.SelectedValue = dsServiceGroup.Tables[0].Rows[0]["ServiceMainGroupId"].ToString().ToUpper();
                        txtServiceGroup.Text = dsServiceGroup.Tables[0].Rows[0]["ServiceGroup"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsServiceGroup.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsServiceGroup.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtServiceGroup.Focus();

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
                                gvServiceGroup.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvServiceGroup_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["ServiceGroup"] as DataSet;
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
                    gvServiceGroup.DataSource = dv;
                    gvServiceGroup.DataBind();
                    gvServiceGroup.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddServiceGroup_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                modlpopup.Show();
                ddlMainGroup.Focus();
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
                txtServiceGroup.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfServiceGroupId.Value != string.Empty)
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
                    AddServiceGroup();
                else if (hbtntext.Value == "Update")
                    UpdateServiceGroup();
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
                txtServiceGroup.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}