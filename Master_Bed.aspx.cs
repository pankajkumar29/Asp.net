////-----------------------------------------------------------------------
// <copyright file="Master_Bed.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
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
    public partial class Master_Bed : BasePage
    {
        #region User Defined Methods
        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlBranch.Enabled = true;
            ddlWard.SelectedIndex = 0;
            txtBed.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }
        /// <summary>
        /// This Method is used to Bind Facilities
        /// </summary>
        public void BindFacilities()
        {
            ddlBranch.Items.Clear();
            ddlSearchBranch.Items.Clear();
            DataSet dsFacilities = new BL_Facility().GetFacilities();
            if (dsFacilities.Tables.Count > 0 && dsFacilities != null && dsFacilities.Tables[0].Rows.Count > 0)
            {
                DataView dvFacilities = dsFacilities.Tables[0].DefaultView;
                ddlBranch.DataSource = dvFacilities;
                ddlBranch.DataTextField = "FacilityName";
                ddlBranch.DataValueField = "FacilityId";
                ddlBranch.DataBind();

                ddlSearchBranch.DataSource = dvFacilities;
                ddlSearchBranch.DataTextField = "FacilityName";
                ddlSearchBranch.DataValueField = "FacilityId";
                ddlSearchBranch.DataBind();
            }
            ddlBranch.Items.Insert(0, "Select");
            ddlSearchBranch.Items.Insert(0, "Branch");
            ddlWard.Items.Clear();
            ddlWard.Items.Insert(0, "Select");
        }
        /// <summary>
        /// This Method is used to Bind Wards
        /// </summary>
        /// <param name="WardId">This Parameter Belongs to BindWards</param>
        public void BindWards(Int32? WardId)
        {
            ddlWard.Items.Clear();
            DataSet dsWards = new BL_Ward().GetWard(Convert.ToInt32(ddlBranch.SelectedValue.Trim().ToUpper()), null, null, null);
            if (dsWards.Tables.Count > 0 && dsWards != null && dsWards.Tables[0].Rows.Count > 0)
            {
                DataView dvWards = dsWards.Tables[0].DefaultView;
                if (hfBedId.Value == "")
                {
                    dvWards.RowFilter = "Status = true";
                }
                if ((hfBedId.Value != string.Empty) && (WardId != null))
                {
                    dvWards.RowFilter = "Status = true or WardId = " + WardId;
                }
                ddlWard.DataSource = dvWards;
                ddlWard.DataTextField = "Ward";
                ddlWard.DataValueField = "WardId";
                ddlWard.DataBind();
            }
            ddlWard.Items.Insert(0, "Select");
        }
        /// <summary>
        /// This Method is used to Bind Bed
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindBed</param>
        private void BindBed(bool openOnEmpty)
        {
            DataSet dsBeds = null;
            if (ddlSearchBranch.SelectedIndex > 0 && ddlStatus.SelectedIndex > 0)
                dsBeds = new BL_Bed().GetBed(Convert.ToInt32(ddlSearchBranch.SelectedValue.Trim().ToUpper()), null, null, txtSearch.Text.Trim().ToUpper(), null, Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else if (ddlSearchBranch.SelectedIndex > 0 && ddlStatus.SelectedIndex == 0)
                dsBeds = new BL_Bed().GetBed(Convert.ToInt32(ddlSearchBranch.SelectedValue.Trim().ToUpper()), null, null, txtSearch.Text.Trim().ToUpper(), null, null);
            else if (ddlSearchBranch.SelectedIndex == 0 && ddlStatus.SelectedIndex > 0)
                dsBeds = new BL_Bed().GetBed(null, null, null, txtSearch.Text.Trim().ToUpper(), null, Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsBeds = new BL_Bed().GetBed(null, null, null, txtSearch.Text.Trim().ToUpper(), null, null);
            Cache["Bed"] = dsBeds;
            if (dsBeds != null && dsBeds.Tables.Count > 0 && dsBeds.Tables[0].Rows.Count > 0)
            {
                gvBed.DataSource = dsBeds;
                gvBed.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsBeds.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvBed.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvBed.DataSource = null;
                gvBed.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }
        /// <summary>
        /// This Method is used to Add Bed
        /// </summary>
        private void AddBed()
        {
            modlpopup.Show();
            DataSet dsBed = new BL_Bed().GetBed(null, null, null, null, null, null);
            DataView dvBed = dsBed.Tables[0].DefaultView;
            DataRow[] rows = dsBed.Tables[0].Select("FacilityId = " + ddlBranch.SelectedValue.Trim().ToUpper() + "AND Bed = '" + txtBed.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Bed already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtBed.Text = string.Empty;
                txtBed.Focus();
                return;
            }
            BE_Bed beBed = new BE_Bed();
            beBed.FacilityId = Convert.ToInt32(ddlBranch.SelectedValue.Trim().ToUpper());
            beBed.BedId = 0;
            beBed.WardId = Convert.ToInt32(ddlWard.SelectedValue.Trim().ToUpper());
            beBed.Bed = txtBed.Text.Trim().ToUpper();
            beBed.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beBed.Status = true;
                beBed.Reason = string.Empty;
            }
            else
            {
                beBed.Status = false;
                beBed.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Bed().InsertUpdateBed(beBed);
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
            BindBed(false);
            ddlBranch.Focus();
        }
        /// <summary>
        /// This Method is used to Update Bed
        /// </summary>
        private void UpdateBed()
        {
            modlpopup.Show();
            DataSet dsBed = new BL_Bed().GetBed(null, null, null, null, null, null);
            DataView dvBed = dsBed.Tables[0].DefaultView;
            DataRow[] rows = dsBed.Tables[0].Select("BedId <> " + Convert.ToInt32(hfBedId.Value) + "AND FacilityId = " + ddlBranch.SelectedValue.Trim().ToUpper() + "AND Bed ='" + txtBed.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Bed already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtBed.Text = string.Empty;
                txtBed.Focus();
                return;
            }
            BE_Bed beBed = new BE_Bed();
            beBed.FacilityId = Convert.ToInt32(ddlBranch.SelectedValue.Trim().ToUpper());
            beBed.BedId = Convert.ToInt32(hfBedId.Value.Trim().ToUpper());
            beBed.WardId = Convert.ToInt32(ddlWard.SelectedValue.Trim().ToUpper());
            beBed.Bed = txtBed.Text.Trim().ToUpper();
            beBed.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beBed.Status = true;
                beBed.Reason = string.Empty;
            }
            else
            {
                beBed.Status = false;
                beBed.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Bed().InsertUpdateBed(beBed);
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
            BindBed(false);
            txtBed.Focus();
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
                    txtBed.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindFacilities();
                        BindBed(true);
                        btnAddBed.Focus();
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
                BindBed(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvBed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvBed);
                    }
                    else
                    {
                        Image sortImage = new Image();
                        sortImage.ImageUrl = "~/Images/UpArrow.png";
                        sortImage.Width = 10;
                        sortImage.Height = 10;
                        sortImage.AlternateText = "Ascending Order";
                        e.Row.Cells[3].Controls.Add(sortImage);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvBed_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsBed = new BL_Bed().GetBed(null, Convert.ToInt32(e.CommandArgument.ToString()), null, null, null, null);
                    if (dsBed != null && dsBed.Tables.Count > 0 && dsBed.Tables[0].Rows.Count > 0)
                    {
                        BindFacilities();
                        ddlBranch.SelectedValue = dsBed.Tables[0].Rows[0]["FacilityId"].ToString().ToUpper();
                        ddlBranch.Enabled = false;
                        int WardId = Convert.ToInt32(dsBed.Tables[0].Rows[0]["WardId"].ToString());
                        BindWards(WardId);
                        ddlWard.SelectedValue = WardId.ToString().ToUpper();
                        hfBedId.Value = e.CommandArgument.ToString().ToUpper();
                        txtBed.Text = dsBed.Tables[0].Rows[0]["Bed"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsBed.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsBed.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        ddlBranch.Enabled = false;
                        ddlWard.Enabled = false;
                        txtBed.Enabled = false;
                        modlpopup.Show();
                        txtBed.Focus();

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
                                gvBed.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvBed_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvBed.PageIndex = e.NewPageIndex;
                BindBed(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvBed_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Bed"] as DataSet;
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
                    gvBed.DataSource = dv;
                    gvBed.DataBind();
                    gvBed.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddBed_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                ddlBranch.Focus();
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
                txtBed.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfBedId.Value != string.Empty)
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

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                if (ddlBranch.SelectedIndex > 0)
                {
                    BindWards(null);
                }
                else
                {
                    ddlWard.Items.Clear();
                    ddlWard.Items.Insert(0, "Select");
                }
                ddlBranch.Focus();
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
                    AddBed();
                else if (hbtntext.Value == "Update")
                    UpdateBed();
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
                ddlBranch.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}