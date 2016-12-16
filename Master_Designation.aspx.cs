////-----------------------------------------------------------------------
// <copyright file="Master_Designation.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public partial class Master_Designation : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtDesignation.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Designation
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindDesignation</param>
        private void BindDesignation(bool openOnEmpty)
        {
            DataSet dsDesignation = null;

            if (ddlStatus.SelectedIndex > 0)
                dsDesignation = new BL_Designation().GetDesignation(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsDesignation = new BL_Designation().GetDesignation(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["Designation"] = dsDesignation;
            if (dsDesignation != null && dsDesignation.Tables.Count > 0 && dsDesignation.Tables[0].Rows.Count > 0)
            {
                gvDesignation.DataSource = dsDesignation;
                gvDesignation.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsDesignation.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvDesignation.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvDesignation.DataSource = null;
                gvDesignation.DataBind();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Designation
        /// </summary>
        private void AddDesignation()
        {
            modlpopup.Show();
            DataSet dsDesignation = new BL_Designation().GetDesignation(null, null, null);
            DataView dvDesignation = dsDesignation.Tables[0].DefaultView;
            DataRow[] rows = dsDesignation.Tables[0].Select("Designation ='" + txtDesignation.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Designation already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtDesignation.Text = string.Empty;
                txtDesignation.Focus();
                return;
            }
            BE_Designation beDesignation = new BE_Designation();
            beDesignation.DesignationId = 0;
            beDesignation.Designation = txtDesignation.Text.Trim().ToUpper();
            beDesignation.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beDesignation.Status = true;
                beDesignation.Reason = string.Empty;
            }
            else
            {
                beDesignation.Status = false;
                beDesignation.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Designation().InsertUpdateDesignation(beDesignation);
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
            BindDesignation(false);
            txtDesignation.Focus();
        }

        /// <summary>
        /// This Method is used to Update Designation
        /// </summary>
        private void UpdateDesignation()
        {
            modlpopup.Show();
            DataSet dsDesignation = new BL_Designation().GetDesignation(null, null, null);
            DataView dvDesignation = dsDesignation.Tables[0].DefaultView;
            DataRow[] rows = dsDesignation.Tables[0].Select("DesignationId <> " + Convert.ToInt32(hfDesignationId.Value) + "AND Designation ='" + txtDesignation.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Designation already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtDesignation.Text = string.Empty;
                txtDesignation.Focus();
                return;
            }
            BE_Designation beDesignation = new BE_Designation();
            beDesignation.DesignationId = Convert.ToInt32(hfDesignationId.Value.Trim().ToUpper());
            beDesignation.Designation = txtDesignation.Text.Trim().ToUpper();
            beDesignation.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beDesignation.Status = true;
                beDesignation.Reason = string.Empty;
            }
            else
            {
                beDesignation.Status = false;
                beDesignation.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Designation().InsertUpdateDesignation(beDesignation);
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
            BindDesignation(false);
            txtDesignation.Focus();
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
                    txtDesignation.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindDesignation(true);
                        btnAddDesignation.Focus();
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
                BindDesignation(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvDesignation);
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

        protected void gvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDesignation.PageIndex = e.NewPageIndex;
                BindDesignation(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsDesignation = new BL_Designation().GetDesignation(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsDesignation != null && dsDesignation.Tables.Count > 0 && dsDesignation.Tables[0].Rows.Count > 0)
                    {
                        hfDesignationId.Value = e.CommandArgument.ToString().ToUpper();
                        txtDesignation.Text = dsDesignation.Tables[0].Rows[0]["Designation"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsDesignation.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsDesignation.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtDesignation.Focus();

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
                                gvDesignation.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvDesignation_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Designation"] as DataSet;
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
                    gvDesignation.DataSource = dv;
                    gvDesignation.DataBind();
                    gvDesignation.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddDesignation_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                txtDesignation.Focus();
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
                txtDesignation.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfDesignationId.Value != string.Empty)
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
                    AddDesignation();
                else if (hbtntext.Value == "Update")
                    UpdateDesignation();
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
                txtDesignation.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}