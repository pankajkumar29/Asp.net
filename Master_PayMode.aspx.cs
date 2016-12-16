////-----------------------------------------------------------------------
// <copyright file="Master_PayMode.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public partial class Master_PayMode : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtPayMode.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Pay Mode
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindPayMode</param>
        private void BindPayMode(bool openOnEmpty)
        {
            DataSet dsPayMode = null;

            if (ddlStatus.SelectedIndex > 0)
                dsPayMode = new BL_PayMode().GetPayMode(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsPayMode = new BL_PayMode().GetPayMode(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["PayMode"] = dsPayMode;
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                gvPayMode.DataSource = dsPayMode;
                gvPayMode.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsPayMode.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvPayMode.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }

                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvPayMode.DataSource = null;
                gvPayMode.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Pay Mode
        /// </summary>
        private void AddPayMode()
        {
            modlpopup.Show();
            DataSet dsPayMode = new BL_PayMode().GetPayMode(null, null, null);
            DataView dvPayMode = dsPayMode.Tables[0].DefaultView;
            DataRow[] rows = dsPayMode.Tables[0].Select("PayMode ='" + txtPayMode.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Pay Mode already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtPayMode.Text = string.Empty;
                txtPayMode.Focus();
                return;
            }
            BE_PayMode bePayMode = new BE_PayMode();
            bePayMode.PayModeId = 0;
            bePayMode.PayMode = txtPayMode.Text.Trim().ToUpper();
            bePayMode.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                bePayMode.Status = true;
                bePayMode.Reason = string.Empty;
            }
            else
            {
                bePayMode.Status = false;
                bePayMode.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_PayMode().InsertUpdatePayMode(bePayMode);
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
            BindPayMode(false);
            txtPayMode.Focus();
        }

        /// <summary>
        /// This Method is used to Update Pay Mode
        /// </summary>
        private void UpdatePayMode()
        {
            modlpopup.Show();
            DataSet dsPayMode = new BL_PayMode().GetPayMode(null, null, null);
            DataView dvPayMode = dsPayMode.Tables[0].DefaultView;
            DataRow[] rows = dsPayMode.Tables[0].Select("PayModeId <> " + Convert.ToInt32(hfPayModeId.Value) + "AND PayMode ='" + txtPayMode.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Pay Mode already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtPayMode.Text = string.Empty;
                txtPayMode.Focus();
                return;
            }
            BE_PayMode bePayMode = new BE_PayMode();
            bePayMode.PayModeId = Convert.ToInt32(hfPayModeId.Value.Trim().ToUpper());
            bePayMode.PayMode = txtPayMode.Text.Trim().ToUpper();
            bePayMode.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                bePayMode.Status = true;
                bePayMode.Reason = string.Empty;
            }
            else
            {
                bePayMode.Status = false;
                bePayMode.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_PayMode().InsertUpdatePayMode(bePayMode);
            if (result > 0)
            {
                lblMsg.Text = "Added successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Failed to update details";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            BindPayMode(false);
            txtPayMode.Focus();
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
                    txtPayMode.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindPayMode(true);
                        btnAddPayMode.Focus();
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
                BindPayMode(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvPayMode_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvPayMode);
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

        protected void gvPayMode_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPayMode.PageIndex = e.NewPageIndex;
                BindPayMode(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvPayMode_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsPayMode = new BL_PayMode().GetPayMode(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
                    {
                        hfPayModeId.Value = e.CommandArgument.ToString().ToUpper();
                        txtPayMode.Text = dsPayMode.Tables[0].Rows[0]["PayMode"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsPayMode.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsPayMode.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtPayMode.Focus();

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
                                gvPayMode.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvPayMode_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["PayMode"] as DataSet;
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
                    gvPayMode.DataSource = dv;
                    gvPayMode.DataBind();
                    gvPayMode.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddPayMode_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                modlpopup.Show();
                txtPayMode.Focus();
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
                txtPayMode.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfPayModeId.Value != string.Empty)
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
                    AddPayMode();
                else if (hbtntext.Value == "Update")
                    UpdatePayMode();
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
                txtPayMode.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}