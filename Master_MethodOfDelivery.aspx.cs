////-----------------------------------------------------------------------
// <copyright file="Master_MethodOfDelivery.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public partial class Master_MethodOfDelivery : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to ClearControls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtMethodOfDelivery.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Method Of Delivery
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindMethodOfDelivery</param>
        private void BindMethodOfDelivery(bool openOnEmpty)
        {
            DataSet dsMethodOfDelivery = null;

            if (ddlStatus.SelectedIndex > 0)
                dsMethodOfDelivery = new BL_MethodOfDelivery().GetMethodOfDelivery(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsMethodOfDelivery = new BL_MethodOfDelivery().GetMethodOfDelivery(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["MethodOfDelivery"] = dsMethodOfDelivery;
            if (dsMethodOfDelivery != null && dsMethodOfDelivery.Tables.Count > 0 && dsMethodOfDelivery.Tables[0].Rows.Count > 0)
            {
                gvMethodOfDelivery.DataSource = dsMethodOfDelivery;
                gvMethodOfDelivery.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsMethodOfDelivery.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvMethodOfDelivery.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvMethodOfDelivery.DataSource = null;
                gvMethodOfDelivery.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Method Of Delivery
        /// </summary>
        private void AddMethodOfDelivery()
        {
            modlpopup.Show();
            DataSet dsMethodOfDelivery = new BL_MethodOfDelivery().GetMethodOfDelivery(null, null, null);
            DataView dvMethodOfDelivery = dsMethodOfDelivery.Tables[0].DefaultView;
            DataRow[] rows = dsMethodOfDelivery.Tables[0].Select("MethodOfDelivery ='" + txtMethodOfDelivery.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Method of delivery already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtMethodOfDelivery.Text = string.Empty;
                txtMethodOfDelivery.Focus();
                return;
            }
            BE_MethodOfDelivery beMethodOfDelivery = new BE_MethodOfDelivery();
            beMethodOfDelivery.MethodOfDeliveryId = 0;
            beMethodOfDelivery.MethodOfDelivery = txtMethodOfDelivery.Text.Trim().ToUpper();
            beMethodOfDelivery.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beMethodOfDelivery.Status = true;
                beMethodOfDelivery.Reason = string.Empty;
            }
            else
            {
                beMethodOfDelivery.Status = false;
                beMethodOfDelivery.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_MethodOfDelivery().InsertUpdateMethodOfDelivery(beMethodOfDelivery);
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
            BindMethodOfDelivery(false);
            txtMethodOfDelivery.Focus();
        }

        /// <summary>
        /// This Method is used to Update Method Of Delivery
        /// </summary>
        private void UpdateMethodOfDelivery()
        {
            modlpopup.Show();
            DataSet dsMethodOfDelivery = new BL_MethodOfDelivery().GetMethodOfDelivery(null, null, null);
            DataView dvMethodOfDelivery = dsMethodOfDelivery.Tables[0].DefaultView;
            DataRow[] rows = dsMethodOfDelivery.Tables[0].Select("MethodOfDeliveryId <> " + Convert.ToInt32(hfMethodOfDeliveryId.Value) + "AND MethodOfDelivery ='" + txtMethodOfDelivery.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Method of delivery already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtMethodOfDelivery.Text = string.Empty;
                txtMethodOfDelivery.Focus();
                return;
            }
            BE_MethodOfDelivery beMethodOfDelivery = new BE_MethodOfDelivery();
            beMethodOfDelivery.MethodOfDeliveryId = Convert.ToInt32(hfMethodOfDeliveryId.Value.Trim().ToUpper());
            beMethodOfDelivery.MethodOfDelivery = txtMethodOfDelivery.Text.Trim().ToUpper();
            beMethodOfDelivery.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beMethodOfDelivery.Status = true;
                beMethodOfDelivery.Reason = string.Empty;
            }
            else
            {
                beMethodOfDelivery.Status = false;
                beMethodOfDelivery.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_MethodOfDelivery().InsertUpdateMethodOfDelivery(beMethodOfDelivery);
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
            BindMethodOfDelivery(false);
            txtMethodOfDelivery.Focus();
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
                    txtMethodOfDelivery.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindMethodOfDelivery(true);
                        btnAddMethodOfDelivery.Focus();
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
                BindMethodOfDelivery(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvMethodOfDelivery_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvMethodOfDelivery);
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

        protected void gvMethodOfDelivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMethodOfDelivery.PageIndex = e.NewPageIndex;
                BindMethodOfDelivery(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvMethodOfDelivery_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsMethodOfDelivery = new BL_MethodOfDelivery().GetMethodOfDelivery(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsMethodOfDelivery != null && dsMethodOfDelivery.Tables.Count > 0 && dsMethodOfDelivery.Tables[0].Rows.Count > 0)
                    {
                        hfMethodOfDeliveryId.Value = e.CommandArgument.ToString().ToUpper();
                        txtMethodOfDelivery.Text = dsMethodOfDelivery.Tables[0].Rows[0]["MethodOfDelivery"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsMethodOfDelivery.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsMethodOfDelivery.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        txtMethodOfDelivery.Focus();

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
                                gvMethodOfDelivery.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvMethodOfDelivery_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["MethodOfDelivery"] as DataSet;
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
                    gvMethodOfDelivery.DataSource = dv;
                    gvMethodOfDelivery.DataBind();
                    gvMethodOfDelivery.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddMethodOfDelivery_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                txtMethodOfDelivery.Focus();
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
                txtMethodOfDelivery.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfMethodOfDeliveryId.Value != string.Empty)
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
                    AddMethodOfDelivery();
                else if (hbtntext.Value == "Update")
                    UpdateMethodOfDelivery();
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
                txtMethodOfDelivery.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}