////-----------------------------------------------------------------------
// <copyright file="Master_Literacy.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public partial class Master_Literacy : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtLiteracy.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Literacy
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindLiteracy</param>
        private void BindLiteracy(bool openOnEmpty)
        {
            DataSet dsLiteracy = null;

            if (ddlStatus.SelectedIndex > 0)
                dsLiteracy = new BL_Literacy().GetLiteracy(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsLiteracy = new BL_Literacy().GetLiteracy(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["Literacy"] = dsLiteracy;
            if (dsLiteracy != null && dsLiteracy.Tables.Count > 0 && dsLiteracy.Tables[0].Rows.Count > 0)
            {
                gvLiteracy.DataSource = dsLiteracy;
                gvLiteracy.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsLiteracy.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvLiteracy.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvLiteracy.DataSource = null;
                gvLiteracy.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Admd Literacy
        /// </summary>
        private void AddLiteracy()
        {
            modlpopup.Show();
            DataSet dsLiteracy = new BL_Literacy().GetLiteracy(null, null, null);
            DataView dvLiteracy = dsLiteracy.Tables[0].DefaultView;
            DataRow[] rows = dsLiteracy.Tables[0].Select("Literacy ='" + txtLiteracy.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Literacy already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtLiteracy.Text = string.Empty;
                txtLiteracy.Focus();
                return;
            }
            BE_Literacy beLiteracy = new BE_Literacy();
            beLiteracy.LiteracyId = 0;
            beLiteracy.Literacy = txtLiteracy.Text.Trim().ToUpper();
            beLiteracy.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beLiteracy.Status = true;
                beLiteracy.Reason = string.Empty;
            }
            else
            {
                beLiteracy.Status = false;
                beLiteracy.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Literacy().InsertUpdateLiteracy(beLiteracy);
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
            BindLiteracy(false);
            txtLiteracy.Focus();
        }

        /// <summary>
        /// This Method is used to Update Literacy
        /// </summary>
        private void UpdateLiteracy()
        {
            modlpopup.Show();
            DataSet dsLiteracy = new BL_Literacy().GetLiteracy(null, null, null);
            DataView dvLiteracy = dsLiteracy.Tables[0].DefaultView;
            DataRow[] rows = dsLiteracy.Tables[0].Select("LiteracyId <> " + Convert.ToInt32(hfLiteracyId.Value) + "AND Literacy ='" + txtLiteracy.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Literacy already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtLiteracy.Text = string.Empty;
                txtLiteracy.Focus();
                return;
            }
            BE_Literacy beLiteracy = new BE_Literacy();
            beLiteracy.LiteracyId = Convert.ToInt32(hfLiteracyId.Value.Trim().ToUpper());
            beLiteracy.Literacy = txtLiteracy.Text.Trim().ToUpper();
            beLiteracy.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beLiteracy.Status = true;
                beLiteracy.Reason = string.Empty;
            }
            else
            {
                beLiteracy.Status = false;
                beLiteracy.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Literacy().InsertUpdateLiteracy(beLiteracy);
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
            BindLiteracy(false);
            txtLiteracy.Focus();
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
                    txtLiteracy.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindLiteracy(true);
                        btnAddLiteracy.Focus();
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
                BindLiteracy(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvLiteracy_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvLiteracy);
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

        protected void gvLiteracy_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLiteracy.PageIndex = e.NewPageIndex;
                BindLiteracy(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvLiteracy_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsLiteracy = new BL_Literacy().GetLiteracy(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsLiteracy != null && dsLiteracy.Tables.Count > 0 && dsLiteracy.Tables[0].Rows.Count > 0)
                    {
                        hfLiteracyId.Value = e.CommandArgument.ToString().ToUpper();
                        txtLiteracy.Text = dsLiteracy.Tables[0].Rows[0]["Literacy"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsLiteracy.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsLiteracy.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtLiteracy.Focus();

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
                                gvLiteracy.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvLiteracy_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Literacy"] as DataSet;
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
                    gvLiteracy.DataSource = dv;
                    gvLiteracy.DataBind();
                    gvLiteracy.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddLiteracy_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                txtLiteracy.Focus();
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
                txtLiteracy.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfLiteracyId.Value != string.Empty)
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
                    AddLiteracy();
                else if (hbtntext.Value == "Update")
                    UpdateLiteracy();
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
                txtLiteracy.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}