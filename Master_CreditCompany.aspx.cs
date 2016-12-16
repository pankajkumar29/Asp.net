////-----------------------------------------------------------------------
// <copyright file="Master_CreditCompany.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>02/04/2012</date>
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
    public partial class Master_CreditCompany : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtCreditCompany.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Credit Company
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindCreditCompany</param>
        private void BindCreditCompany(bool openOnEmpty)
        {
            DataSet dsCreditCompany = null;

            if (ddlStatus.SelectedIndex > 0)
                dsCreditCompany = new BL_CreditCompany().GetCreditCompany(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsCreditCompany = new BL_CreditCompany().GetCreditCompany(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["CreditCompany"] = dsCreditCompany;
            if (dsCreditCompany != null && dsCreditCompany.Tables.Count > 0 && dsCreditCompany.Tables[0].Rows.Count > 0)
            {
                gvCreditCompany.DataSource = dsCreditCompany;
                gvCreditCompany.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsCreditCompany.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvCreditCompany.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }

                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvCreditCompany.DataSource = null;
                gvCreditCompany.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Credit Company
        /// </summary>
        private void AddCreditCompany()
        {
            modlpopup.Show();
            DataSet dsCreditCompany = new BL_CreditCompany().GetCreditCompany(null, null, null);
            DataView dvCreditCompany = dsCreditCompany.Tables[0].DefaultView;
            DataRow[] rows = dsCreditCompany.Tables[0].Select("CreditCompany ='" + txtCreditCompany.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Credit Company already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtCreditCompany.Text = string.Empty;
                txtCreditCompany.Focus();
                return;
            }
            BE_CreditCompany beCreditCompany = new BE_CreditCompany();
            beCreditCompany.CreditCompanyId = 0;
            beCreditCompany.CreditCompany = txtCreditCompany.Text.Trim().ToUpper();
            beCreditCompany.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beCreditCompany.Status = true;
                beCreditCompany.Reason = string.Empty;
            }
            else
            {
                beCreditCompany.Status = false;
                beCreditCompany.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_CreditCompany().InsertUpdateCreditCompany(beCreditCompany);
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
            BindCreditCompany(false);
            txtCreditCompany.Focus();
        }

        /// <summary>
        /// This Method is used to Update Credit Company
        /// </summary>
        private void UpdateCreditCompany()
        {
            modlpopup.Show();
            DataSet dsCreditCompany = new BL_CreditCompany().GetCreditCompany(null, null, null);
            DataView dvCreditCompany = dsCreditCompany.Tables[0].DefaultView;
            DataRow[] rows = dsCreditCompany.Tables[0].Select("CreditCompanyId <> " + Convert.ToInt32(hfCreditCompanyId.Value) + "AND CreditCompany ='" + txtCreditCompany.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Credit Company already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtCreditCompany.Text = string.Empty;
                txtCreditCompany.Focus();
                return;
            }
            BE_CreditCompany beCreditCompany = new BE_CreditCompany();
            beCreditCompany.CreditCompanyId = Convert.ToInt32(hfCreditCompanyId.Value.Trim().ToUpper());
            beCreditCompany.CreditCompany = txtCreditCompany.Text.Trim().ToUpper();
            beCreditCompany.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beCreditCompany.Status = true;
                beCreditCompany.Reason = string.Empty;
            }
            else
            {
                beCreditCompany.Status = false;
                beCreditCompany.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_CreditCompany().InsertUpdateCreditCompany(beCreditCompany);
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
            BindCreditCompany(false);
            txtCreditCompany.Focus();
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
                    txtCreditCompany.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindCreditCompany(true);
                        btnAddCreditCompany.Focus();
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
                BindCreditCompany(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvCreditCompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvCreditCompany);
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

        protected void gvCreditCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCreditCompany.PageIndex = e.NewPageIndex;
                BindCreditCompany(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvCreditCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsCreditCompany = new BL_CreditCompany().GetCreditCompany(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsCreditCompany != null && dsCreditCompany.Tables.Count > 0 && dsCreditCompany.Tables[0].Rows.Count > 0)
                    {
                        hfCreditCompanyId.Value = e.CommandArgument.ToString().ToUpper();
                        txtCreditCompany.Text = dsCreditCompany.Tables[0].Rows[0]["CreditCompany"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsCreditCompany.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsCreditCompany.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtCreditCompany.Focus();

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
                                gvCreditCompany.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvCreditCompany_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["CreditCompany"] as DataSet;
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
                    gvCreditCompany.DataSource = dv;
                    gvCreditCompany.DataBind();
                    gvCreditCompany.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddCreditCompany_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                modlpopup.Show();
                txtCreditCompany.Focus();
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
                txtCreditCompany.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfCreditCompanyId.Value != string.Empty)
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
                    AddCreditCompany();
                else if (hbtntext.Value == "Update")
                    UpdateCreditCompany();
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
                txtCreditCompany.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}