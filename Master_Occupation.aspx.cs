﻿////-----------------------------------------------------------------------
// <copyright file="Master_Occupation.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public partial class Master_Occupation : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtOccupation.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Occupation
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindOccupation</param>
        private void BindOccupation(bool openOnEmpty)
        {
            DataSet dsOccupation = null;

            if (ddlStatus.SelectedIndex > 0)
                dsOccupation = new BL_Occupation().GetOccupation(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsOccupation = new BL_Occupation().GetOccupation(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["Occupation"] = dsOccupation;
            if (dsOccupation != null && dsOccupation.Tables.Count > 0 && dsOccupation.Tables[0].Rows.Count > 0)
            {
                gvOccupation.DataSource = dsOccupation;
                gvOccupation.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsOccupation.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvOccupation.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvOccupation.DataSource = null;
                gvOccupation.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Occupation
        /// </summary>
        private void AddOccupation()
        {
            modlpopup.Show();
            DataSet dsOccupation = new BL_Occupation().GetOccupation(null, null, null);
            DataView dvOccupation = dsOccupation.Tables[0].DefaultView;
            DataRow[] rows = dsOccupation.Tables[0].Select("Occupation ='" + txtOccupation.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Occupation already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtOccupation.Text = string.Empty;
                txtOccupation.Focus();
                return;
            }
            BE_Occupation beOccupation = new BE_Occupation();
            beOccupation.OccupationId = 0;
            beOccupation.Occupation = txtOccupation.Text.Trim().ToUpper();
            beOccupation.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beOccupation.Status = true;
                beOccupation.Reason = string.Empty;
            }
            else
            {
                beOccupation.Status = false;
                beOccupation.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Occupation().InsertUpdateOccupation(beOccupation);
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
            BindOccupation(false);
            txtOccupation.Focus();
        }

        /// <summary>
        /// This Method is used to Update Occupation
        /// </summary>
        private void UpdateOccupation()
        {
            modlpopup.Show();
            DataSet dsOccupation = new BL_Occupation().GetOccupation(null, null, null);
            DataView dvOccupation = dsOccupation.Tables[0].DefaultView;
            DataRow[] rows = dsOccupation.Tables[0].Select("OccupationId <> " + Convert.ToInt32(hfOccupationId.Value) + "AND Occupation ='" + txtOccupation.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Occupation already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtOccupation.Text = string.Empty;
                txtOccupation.Focus();
                return;
            }
            BE_Occupation beOccupation = new BE_Occupation();
            beOccupation.OccupationId = Convert.ToInt32(hfOccupationId.Value.Trim().ToUpper());
            beOccupation.Occupation = txtOccupation.Text.Trim().ToUpper();
            beOccupation.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beOccupation.Status = true;
                beOccupation.Reason = string.Empty;
            }
            else
            {
                beOccupation.Status = false;
                beOccupation.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Occupation().InsertUpdateOccupation(beOccupation);
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
            BindOccupation(false);
            txtOccupation.Focus();
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
                    String jsname = "OnSubmitScript";
                    Type jstype = this.GetType();

                    // Check to see if the OnSubmit statement is already registered.
                    if (!Page.ClientScript.IsOnSubmitStatementRegistered(jstype, jsname))
                    {
                        String jstext = "if (typeof(ValidatorOnSubmit) == 'function' && ValidatorOnSubmit() == false)return false;\n";
                        jstext += "else\n";
                        jstext += "{\n";
                        jstext += "\t var button = document.getElementById('" + this.btnAddOccupation.ClientID + "');\n";
                        jstext += "\t button.value = 'Add New';\n";
                        jstext += "\t button.disabled = true;\n";
                        jstext += "}\n";
                        jstext += "return true;\n";
                        Page.ClientScript.RegisterOnSubmitStatement(jstype, jsname, jstext);
                    }

                    // Check to see if the OnSubmit statement is already registered.
                    if (!Page.ClientScript.IsOnSubmitStatementRegistered(jstype, jsname))
                    {
                        String jstext = "if (typeof(ValidatorOnSubmit) == 'function' && ValidatorOnSubmit() == false)return false;\n";
                        jstext += "else\n";
                        jstext += "{\n";
                        jstext += "\t var button = document.getElementById('" + this.btnAdd.ClientID + "');\n";
                        jstext += "\t button.value = 'Add';\n";
                        jstext += "\t button.disabled = true;\n";
                        jstext += "}\n";
                        jstext += "return true;\n";
                        Page.ClientScript.RegisterOnSubmitStatement(jstype, jsname, jstext);
                    }
                    //Page.Form.Enctype = "multipart/form-data";

                    lblMsg.Text = string.Empty;
                    txtOccupation.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindOccupation(true);
                        btnAddOccupation.Focus();
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
                BindOccupation(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvOccupation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvOccupation);
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

        protected void gvOccupation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvOccupation.PageIndex = e.NewPageIndex;
                BindOccupation(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvOccupation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsOccupation = new BL_Occupation().GetOccupation(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsOccupation != null && dsOccupation.Tables.Count > 0 && dsOccupation.Tables[0].Rows.Count > 0)
                    {
                        hfOccupationId.Value = e.CommandArgument.ToString().ToUpper();
                        txtOccupation.Text = dsOccupation.Tables[0].Rows[0]["Occupation"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsOccupation.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsOccupation.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtOccupation.Focus();

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
                                gvOccupation.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvOccupation_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Occupation"] as DataSet;
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
                    gvOccupation.DataSource = dv;
                    gvOccupation.DataBind();
                    gvOccupation.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddOccupation_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                txtOccupation.Focus();
                btnAddOccupation.Enabled = true;
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
                txtOccupation.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfOccupationId.Value != string.Empty)
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
                    AddOccupation();
                else if (hbtntext.Value == "Update")
                    UpdateOccupation();
                btnAdd.Enabled = true;
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
                txtOccupation.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}