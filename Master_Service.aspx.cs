////-----------------------------------------------------------------------
// <copyright file="Master_Service.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\kirankumarj</author>
// <email>nkiran.kuamrj@dhii.in</email>
// <date>12/02/2012</date>
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
    public partial class Master_Service : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            ddlServiceGroup.SelectedIndex = 0;
            txtService.Text = string.Empty;
            ChkPackage.Checked = false;
            txtPackageDays.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Service
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindService</param>
        private void BindService(bool openOnEmpty)
        {
            DataSet dsService = null;
            bool? status = null;
            if (ddlStatus.SelectedIndex > 0)
            {
                status = Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper());
            }
            //if (ddlSearchServiceGroup.SelectedIndex == 0)
            //{
            //    Session["ServiceGroup"] = "0";
            //}
            if (Session["ServiceGroup"] == null)
            {
                Session["ServiceGroup"] = "0";
            }
            if (Convert.ToInt32(Session["ServiceGroup"]) > 0)
                dsService = new BL_Service().GetService(null, txtSearch.Text.Trim().ToUpper(), Convert.ToInt32(Session["ServiceGroup"]), status);
            else if (status != null)
                dsService = new BL_Service().GetService(null, txtSearch.Text.Trim().ToUpper(), null, status);
            else
                dsService = new BL_Service().GetService(null, txtSearch.Text.Trim().ToUpper(), null, null);

            Cache["Service"] = dsService;
            if (dsService != null && dsService.Tables.Count > 0 && dsService.Tables[0].Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = dsService.Tables[0].DefaultView;
                dv.RowFilter = "ServiceGroup<>'Consultation'";
                if (dv.Count > 0)
                {

                    gvService.DataSource = dv;
                    gvService.DataBind();
                    lblTotalRecords.Visible = true;
                    lblTotalRecords.Text = "Total Records:" + dv.Count.ToString().ToUpper();
                }
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
                        gvService.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }

                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvService.DataSource = null;
                gvService.DataBind();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Service
        /// </summary>
        private void AddService()
        {
            modlpopup.Show();
            DataSet dsService = new BL_Service().GetService(null, null, null, null);
            DataView dvService = dsService.Tables[0].DefaultView;
            DataRow[] rows = dsService.Tables[0].Select("Service ='" + txtService.Text.Trim().ToUpper() + "' and serviceGroupID=" + Convert.ToInt32(ddlServiceGroup.SelectedValue) + "");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Service already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtService.Text = string.Empty;
                txtService.Focus();
                return;
            }
            BE_Service beService = new BE_Service();
            beService.ServiceId = 0;
            if (ddlServiceGroup.SelectedIndex > 0)
                beService.ServiceGroupId = Convert.ToInt32(ddlServiceGroup.SelectedValue);
            beService.Service = txtService.Text.Trim().ToUpper();
            beService.IsPackage = ChkPackage.Checked;
            if (txtPackageDays.Text != string.Empty)
                beService.PackageDays = Convert.ToInt32(txtPackageDays.Text.Trim());
            else
                beService.PackageDays = null;
            beService.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beService.Status = true;
                beService.Reason = string.Empty;
            }
            else
            {
                beService.Status = false;
                beService.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Service().InsertUpdateService(beService);
            if (result > 0)
            {
                lblMsg.Text = "Service added successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Failed to add service";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClearControls();
            btnAdd.Text = "Add";
            btnAdd.ToolTip = "Add";
            hbtntext.Value = "Add";
            BindService(false);
            ddlServiceGroup.Focus();
        }

        /// <summary>
        /// This Method is used to Update Service
        /// </summary>
        private void UpdateService()
        {
            modlpopup.Show();
            DataSet dsService = new BL_Service().GetService(null, null, null, null);
            DataView dvService = dsService.Tables[0].DefaultView;
            DataRow[] rows = dsService.Tables[0].Select("ServiceId <> " + Convert.ToInt32(hfServiceId.Value) + " AND Service ='" + txtService.Text.Trim().ToUpper() + "' and ServiceGroupId=" + Convert.ToInt32(ddlServiceGroup.SelectedValue) + "");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Service already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtService.Text = string.Empty;
                txtService.Focus();
                return;
            }
            BE_Service beService = new BE_Service();
            beService.ServiceId = Convert.ToInt32(hfServiceId.Value.Trim().ToUpper());
            if (ddlServiceGroup.SelectedIndex > 0)
                beService.ServiceGroupId = Convert.ToInt32(ddlServiceGroup.SelectedValue);
            beService.Service = txtService.Text.Trim().ToUpper();
            beService.IsPackage = ChkPackage.Checked;
            if (txtPackageDays.Text != string.Empty)
                beService.PackageDays = Convert.ToInt32(txtPackageDays.Text.Trim());
            else
                beService.PackageDays = null;
            beService.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beService.Status = true;
                beService.Reason = string.Empty;
            }
            else
            {
                beService.Status = false;
                beService.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Service().InsertUpdateService(beService);
            if (result > 0)
            {
                lblMsg.Text = "Service updated successfully ";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Failed to update service";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            BindService(false);
            ddlServiceGroup.Focus();
        }

        /// <summary>
        /// This Method is used to Bind Service Group
        /// </summary>
        /// <param name="ServiceGroupId">This Parameter Belongs to BindServiceGroup</param>
        void BindServiceGroup(Int32? ServiceGroupId)
        {
            ddlServiceGroup.Items.Clear();
            BL_Service blService = new BL_Service();
            DataSet dsService = blService.GetServiceGroup(null, null, null);
            DataView dvcountries = new DataView();
            if (dsService.Tables.Count > 0 && dsService != null && dsService.Tables[0].Rows.Count > 0)
            {
                if (hfServiceId.Value == "")
                {
                    dvcountries = dsService.Tables[0].DefaultView;
                    dvcountries.RowFilter = "Status=true and ServiceGroup<>'Consultation'";
                }
                if ((hfServiceId.Value != string.Empty) && (ServiceGroupId != null))
                {
                    dvcountries = dsService.Tables[0].DefaultView;
                    dvcountries.RowFilter = "Status=true and ServiceGroup<>'Consultation' or ServiceGroupId=" + ServiceGroupId;
                }
                DataTable dtcountries = dvcountries.ToTable();
                ddlServiceGroup.DataSource = dtcountries;
                ddlServiceGroup.DataTextField = "ServiceGroup";
                ddlServiceGroup.DataValueField = "ServiceGroupId";
                ddlServiceGroup.DataBind();
                ddlServiceGroup.Items.Insert(0, "Select");

                ddlSearchServiceGroup.DataSource = dtcountries;
                ddlSearchServiceGroup.DataTextField = "ServiceGroup";
                ddlSearchServiceGroup.DataValueField = "ServiceGroupId";
                ddlSearchServiceGroup.DataBind();
                ddlSearchServiceGroup.Items.Insert(0, "Service Group");

            }
            else
            {
                ddlServiceGroup.Items.Insert(0, "Select ");
                ddlSearchServiceGroup.Items.Insert(0, "Service Group");
            }
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
                    txtService.Attributes.Add("onkeyup", "return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        Session["ServiceGroup"] = "0";
                        ViewState["SortDirection"] = "ASC";
                        BindService(true);
                        BindServiceGroup(null);
                        btnAddService.Focus();
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
                BindService(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvService);
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

        protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvService.PageIndex = e.NewPageIndex;
                BindService(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvService_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsService = new BL_Service().GetService(Convert.ToInt32(e.CommandArgument.ToString()), null, null, null);
                    if (dsService != null && dsService.Tables.Count > 0 && dsService.Tables[0].Rows.Count > 0)
                    {
                        hfServiceId.Value = e.CommandArgument.ToString().ToUpper();
                        int ServiceGroupId = Convert.ToInt32(dsService.Tables[0].Rows[0]["ServiceGroupId"].ToString());
                        BindServiceGroup(ServiceGroupId);
                        ddlServiceGroup.SelectedValue = dsService.Tables[0].Rows[0]["ServiceGroupId"].ToString().ToUpper();
                        txtService.Text = dsService.Tables[0].Rows[0]["Service"].ToString().ToUpper();
                        ChkPackage.Checked = Convert.ToBoolean(dsService.Tables[0].Rows[0]["IsPackage"].ToString().ToUpper());
                        if (dsService.Tables[0].Rows[0]["PackageDays"].ToString().ToUpper() != null)
                            txtPackageDays.Text = dsService.Tables[0].Rows[0]["PackageDays"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsService.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsService.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        ddlServiceGroup.Focus();

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
                                gvService.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvService_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Service"] as DataSet;
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

                    dv.RowFilter = "ServiceGroup<>'Consultation'";
                    gvService.DataSource = dv;
                    gvService.DataBind();
                    gvService.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                ddlServiceGroup.Focus();
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
                ddlServiceGroup.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfServiceId.Value != string.Empty)
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

        protected void ChkPackage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                ChkPackage.Focus();
                txtPackageDays.Text = string.Empty;
                if (ChkPackage.Checked == true)
                    txtPackageDays.Enabled = true;
                else
                    txtPackageDays.Enabled = false;
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
                    AddService();
                else if (hbtntext.Value == "Update")
                    UpdateService();
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
                txtService.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void ddlSearchServiceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["ServiceGroup"] = "0";
                txtSearch.Text = string.Empty;
                if (ddlSearchServiceGroup.SelectedIndex != 0)
                {
                    Session["ServiceGroup"] = Convert.ToString(ddlSearchServiceGroup.SelectedValue);
                }
                ddlSearchServiceGroup.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Session["UserID"].ToString());
            }
        }

        #endregion
    }
}