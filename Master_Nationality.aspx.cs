////-----------------------------------------------------------------------
// <copyright file="Master_Nationality.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public partial class Master_Nationality : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtNationality.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Nationality
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindNationality</param>
        private void BindNationality(bool openOnEmpty)
        {
            DataSet dsNationality = null;

            if (ddlStatus.SelectedIndex > 0)
                dsNationality = new BL_Nationality().GetNationality(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsNationality = new BL_Nationality().GetNationality(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["Nationality"] = dsNationality;
            if (dsNationality != null && dsNationality.Tables.Count > 0 && dsNationality.Tables[0].Rows.Count > 0)
            {
                gvNationality.DataSource = dsNationality;
                gvNationality.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsNationality.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvNationality.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvNationality.DataSource = null;
                gvNationality.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Nationality
        /// </summary>
        private void AddNationality()
        {
            modlpopup.Show();
            DataSet dsNationality = new BL_Nationality().GetNationality(null, null, null);
            DataView dvNationality = dsNationality.Tables[0].DefaultView;
            DataRow[] rows = dsNationality.Tables[0].Select("Nationality ='" + txtNationality.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Nationality already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtNationality.Text = string.Empty;
                txtNationality.Focus();
                return;
            }
            BE_Nationality beNationality = new BE_Nationality();
            beNationality.NationalityId = 0;
            beNationality.Nationality = txtNationality.Text.Trim().ToUpper();
            beNationality.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beNationality.Status = true;
                beNationality.Reason = string.Empty;
            }
            else
            {
                beNationality.Status = false;
                beNationality.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Nationality().InsertUpdateNationality(beNationality);
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
            BindNationality(false);
            txtNationality.Focus();
        }

        /// <summary>
        /// This Method is used to Update Nationality
        /// </summary>
        private void UpdateNationality()
        {
            modlpopup.Show();
            DataSet dsNationality = new BL_Nationality().GetNationality(null, null, null);
            DataView dvNationality = dsNationality.Tables[0].DefaultView;
            DataRow[] rows = dsNationality.Tables[0].Select("NationalityId <> " + Convert.ToInt32(hfNationalityId.Value) + "AND Nationality ='" + txtNationality.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Nationality already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtNationality.Text = string.Empty;
                txtNationality.Focus();
                return;
            }
            BE_Nationality beNationality = new BE_Nationality();
            beNationality.NationalityId = Convert.ToInt32(hfNationalityId.Value.Trim().ToUpper());
            beNationality.Nationality = txtNationality.Text.Trim().ToUpper();
            beNationality.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beNationality.Status = true;
                beNationality.Reason = string.Empty;
            }
            else
            {
                beNationality.Status = false;
                beNationality.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Nationality().InsertUpdateNationality(beNationality);
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
            BindNationality(false);
            txtNationality.Focus();
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
                    txtNationality.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindNationality(true);
                        btnAddNationality.Focus();
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
                BindNationality(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvNationality_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvNationality);
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

        protected void gvNationality_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvNationality.PageIndex = e.NewPageIndex;
                BindNationality(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvNationality_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsNationality = new BL_Nationality().GetNationality(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsNationality != null && dsNationality.Tables.Count > 0 && dsNationality.Tables[0].Rows.Count > 0)
                    {
                        hfNationalityId.Value = e.CommandArgument.ToString().ToUpper();
                        txtNationality.Text = dsNationality.Tables[0].Rows[0]["Nationality"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsNationality.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsNationality.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtNationality.Focus();

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
                                gvNationality.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvNationality_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Nationality"] as DataSet;
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
                    gvNationality.DataSource = dv;
                    gvNationality.DataBind();
                    gvNationality.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddNationality_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                txtNationality.Focus();
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
                txtNationality.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfNationalityId.Value != string.Empty)
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
                    AddNationality();
                else if (hbtntext.Value == "Update")
                    UpdateNationality();
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
                txtNationality.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}