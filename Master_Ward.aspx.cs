////-----------------------------------------------------------------------
// <copyright file="Master_Ward.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public partial class Master_Ward : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtWard.Text = string.Empty;
            txtShortCode.Text = string.Empty;
            ddlOrder.SelectedIndex = 0;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Ward
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindWard</param>
        private void BindWard(bool openOnEmpty)
        {
            DataSet dsWards = null;

            if (ddlStatus.SelectedIndex > 0)
                dsWards = new BL_Ward().GetWardCommon(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsWards = new BL_Ward().GetWardCommon(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["Ward"] = dsWards;
            if (dsWards != null && dsWards.Tables.Count > 0 && dsWards.Tables[0].Rows.Count > 0)
            {
                gvWard.DataSource = dsWards;
                gvWard.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsWards.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvWard.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvWard.DataSource = null;
                gvWard.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Ward
        /// </summary>
        private void AddWard()
        {
            modlpopup.Show();
            DataSet dsWard = new BL_Ward().GetWardCommon(null, null, null);
            DataView dvWard = dsWard.Tables[0].DefaultView;
            DataRow[] rows = dsWard.Tables[0].Select("Ward = '" + txtWard.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Ward already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtWard.Text = string.Empty;
                txtWard.Focus();
                return;
            }
            BE_Ward beWard = new BE_Ward();
            beWard.WardId = 0;
            beWard.Ward = txtWard.Text.Trim().ToUpper();
            beWard.ShortCode = txtShortCode.Text.Trim().ToUpper();
            beWard.Ordering = Convert.ToInt32(ddlOrder.SelectedValue.Trim().ToUpper());
            beWard.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beWard.Status = true;
                beWard.Reason = string.Empty;
            }
            else
            {
                beWard.Status = false;
                beWard.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Ward().InsertUpdateWardCommon(beWard);
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
            BindWard(false);
            txtWard.Focus();
        }

        /// <summary>
        /// This Method is used to Update Ward
        /// </summary>
        private void UpdateWard()
        {
            modlpopup.Show();
            DataSet dsWard = new BL_Ward().GetWardCommon(null, null, null);
            DataView dvWard = dsWard.Tables[0].DefaultView;
            DataRow[] rows = dsWard.Tables[0].Select("WardId <> " + Convert.ToInt32(hfWardId.Value) + "AND Ward ='" + txtWard.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Ward already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtWard.Text = string.Empty;
                txtWard.Focus();
                return;
            }
            BE_Ward beWard = new BE_Ward();
            beWard.WardId = Convert.ToInt32(hfWardId.Value.Trim().ToUpper());
            beWard.Ward = txtWard.Text.Trim().ToUpper();
            beWard.ShortCode = txtShortCode.Text.Trim().ToUpper();
            beWard.Ordering = Convert.ToInt32(ddlOrder.SelectedValue.Trim().ToUpper());
            beWard.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beWard.Status = true;
                beWard.Reason = string.Empty;
            }
            else
            {
                beWard.Status = false;
                beWard.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Ward().InsertUpdateWardCommon(beWard);
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
            BindWard(false);
            txtWard.Focus();
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
                    txtWard.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindWard(true);
                        btnAddWard.Focus();
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
                BindWard(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvWard_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvWard);
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

        protected void gvWard_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsWard = new BL_Ward().GetWardCommon(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsWard != null && dsWard.Tables.Count > 0 && dsWard.Tables[0].Rows.Count > 0)
                    {
                        hfWardId.Value = e.CommandArgument.ToString().ToUpper();
                        txtWard.Text = dsWard.Tables[0].Rows[0]["Ward"].ToString().ToUpper();
                        txtShortCode.Text = dsWard.Tables[0].Rows[0]["ShortCode"].ToString().ToUpper();
                        ddlOrder.SelectedValue = dsWard.Tables[0].Rows[0]["Ordering"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsWard.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsWard.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtWard.Focus();

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
                                gvWard.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvWard_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvWard.PageIndex = e.NewPageIndex;
                BindWard(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvWard_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Ward"] as DataSet;
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
                    gvWard.DataSource = dv;
                    gvWard.DataBind();
                    gvWard.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddWard_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                txtWard.Focus();
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
                txtWard.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfWardId.Value != string.Empty)
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
                    AddWard();
                else if (hbtntext.Value == "Update")
                    UpdateWard();
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
                txtWard.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}