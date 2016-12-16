////-----------------------------------------------------------------------
// <copyright file="Master_Religion.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>21/03/2012</date>
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
    public partial class Master_Religion : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtReligion.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Religion
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindReligion</param>
        private void BindReligion(bool openOnEmpty)
        {
            DataSet dsReligion = null;

            if (ddlStatus.SelectedIndex > 0)
                dsReligion = new BL_Religion().GetReligion(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsReligion = new BL_Religion().GetReligion(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["Religion"] = dsReligion;
            if (dsReligion != null && dsReligion.Tables.Count > 0 && dsReligion.Tables[0].Rows.Count > 0)
            {
                gvReligion.DataSource = dsReligion;
                gvReligion.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsReligion.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvReligion.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvReligion.DataSource = null;
                gvReligion.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Religion
        /// </summary>
        private void AddReligion()
        {
            modlpopup.Show();
            DataSet dsReligion = new BL_Religion().GetReligion(null, null, null);
            DataView dvReligion = dsReligion.Tables[0].DefaultView;
            DataRow[] rows = dsReligion.Tables[0].Select("Religion ='" + txtReligion.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Religion already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtReligion.Text = string.Empty;
                txtReligion.Focus();
                return;
            }
            BE_Religion beReligion = new BE_Religion();
            beReligion.ReligionId = 0;
            beReligion.Religion = txtReligion.Text.Trim().ToUpper();
            beReligion.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beReligion.Status = true;
                beReligion.Reason = string.Empty;
            }
            else
            {
                beReligion.Status = false;
                beReligion.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Religion().InsertUpdateReligion(beReligion);
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
            BindReligion(false);
            txtReligion.Focus();
        }

        /// <summary>
        /// This Method is used to Update Religion
        /// </summary>
        private void UpdateReligion()
        {
            modlpopup.Show();
            DataSet dsReligion = new BL_Religion().GetReligion(null, null, null);
            DataView dvReligion = dsReligion.Tables[0].DefaultView;
            DataRow[] rows = dsReligion.Tables[0].Select("ReligionId <> " + Convert.ToInt32(hfReligionId.Value) + "AND Religion ='" + txtReligion.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Religion already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtReligion.Text = string.Empty;
                txtReligion.Focus();
                return;
            }
            BE_Religion beReligion = new BE_Religion();
            beReligion.ReligionId = Convert.ToInt32(hfReligionId.Value.Trim().ToUpper());
            beReligion.Religion = txtReligion.Text.Trim().ToUpper();
            beReligion.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beReligion.Status = true;
                beReligion.Reason = string.Empty;
            }
            else
            {
                beReligion.Status = false;
                beReligion.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Religion().InsertUpdateReligion(beReligion);
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
            BindReligion(false);
            txtReligion.Focus();
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
                    txtReligion.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindReligion(true);
                        btnAddReligion.Focus();
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
                BindReligion(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvReligion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvReligion);
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

        protected void gvReligion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvReligion.PageIndex = e.NewPageIndex;
                BindReligion(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvReligion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsReligion = new BL_Religion().GetReligion(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsReligion != null && dsReligion.Tables.Count > 0 && dsReligion.Tables[0].Rows.Count > 0)
                    {
                        hfReligionId.Value = e.CommandArgument.ToString().ToUpper();
                        txtReligion.Text = dsReligion.Tables[0].Rows[0]["Religion"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsReligion.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsReligion.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtReligion.Focus();

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
                                gvReligion.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvReligion_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Religion"] as DataSet;
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
                    gvReligion.DataSource = dv;
                    gvReligion.DataBind();
                    gvReligion.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddReligion_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                txtReligion.Focus();
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
                txtReligion.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfReligionId.Value != string.Empty)
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
                    AddReligion();
                else if (hbtntext.Value == "Update")
                    UpdateReligion();
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
                txtReligion.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}