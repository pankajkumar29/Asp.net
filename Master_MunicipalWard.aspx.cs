////-----------------------------------------------------------------------
// <copyright file="Master_MunicipalWard.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>13/03/2012</date>
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
    public partial class Master_MunicipalWard : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            txtMunicipalWard.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Municipal Ward
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindMunicipalWard</param>
        private void BindMunicipalWard(bool openOnEmpty)
        {
            DataSet dsMunicipalWard = null;

            if (ddlStatus.SelectedIndex > 0)
                dsMunicipalWard = new BL_MunicipalWard().GetMunicipalWard(null, txtSearch.Text.Trim().ToUpper(), null, Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsMunicipalWard = new BL_MunicipalWard().GetMunicipalWard(null, txtSearch.Text.Trim().ToUpper(), null, null);

            Cache["MunicipalWard"] = dsMunicipalWard;
            if (dsMunicipalWard != null && dsMunicipalWard.Tables.Count > 0 && dsMunicipalWard.Tables[0].Rows.Count > 0)
            {
                gvMunicipalWard.DataSource = dsMunicipalWard;
                gvMunicipalWard.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsMunicipalWard.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvMunicipalWard.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }

                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvMunicipalWard.DataSource = null;
                gvMunicipalWard.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Municipal Ward
        /// </summary>
        private void AddMunicipalWard()
        {
            modlpopup.Show();
            DataSet dsMunicipalWard = new BL_MunicipalWard().GetMunicipalWard(null, null, null, null);
            DataView dvMunicipalWard = dsMunicipalWard.Tables[0].DefaultView;
            DataRow[] rows = dsMunicipalWard.Tables[0].Select("MunicipalWard ='" + txtMunicipalWard.Text.Trim().ToUpper() + "' and CityId=" + ddlCity.SelectedValue);
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Municipal Ward already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtMunicipalWard.Text = string.Empty;
                txtMunicipalWard.Focus();
                return;
            }
            BE_MunicipalWard beMunicipalWard = new BE_MunicipalWard();
            beMunicipalWard.MunicipalWardId = 0;
            beMunicipalWard.CityId = Convert.ToInt32(ddlCity.SelectedValue);
            beMunicipalWard.MunicipalWard = txtMunicipalWard.Text.Trim().ToUpper();
            beMunicipalWard.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beMunicipalWard.Status = true;
                beMunicipalWard.Reason = string.Empty;
            }
            else
            {
                beMunicipalWard.Status = false;
                beMunicipalWard.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_MunicipalWard().InsertUpdateMunicipalWard(beMunicipalWard);
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
            BindMunicipalWard(false);
            ddlCity.Focus();
        }

        /// <summary>
        /// This Method is used to Update Municipal Ward
        /// </summary>
        private void UpdateMunicipalWard()
        {
            modlpopup.Show();
            DataSet dsMunicipalWard = new BL_MunicipalWard().GetMunicipalWard(null, null, null, null);
            DataView dvMunicipalWard = dsMunicipalWard.Tables[0].DefaultView;
            DataRow[] rows = dsMunicipalWard.Tables[0].Select("MunicipalWardId <> " + Convert.ToInt32(hfMunicipalWardId.Value) + "AND MunicipalWard ='" + txtMunicipalWard.Text.Trim().ToUpper() + "' AND CityId=" + ddlCity.SelectedValue);
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Municipal Ward already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtMunicipalWard.Text = string.Empty;
                txtMunicipalWard.Focus();
                return;
            }
            BE_MunicipalWard beMunicipalWard = new BE_MunicipalWard();
            beMunicipalWard.MunicipalWardId = Convert.ToInt32(hfMunicipalWardId.Value.Trim().ToUpper());
            beMunicipalWard.CityId = Convert.ToInt32(ddlCity.SelectedValue);
            beMunicipalWard.MunicipalWard = txtMunicipalWard.Text.Trim().ToUpper();
            beMunicipalWard.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beMunicipalWard.Status = true;
                beMunicipalWard.Reason = string.Empty;
            }
            else
            {
                beMunicipalWard.Status = false;
                beMunicipalWard.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_MunicipalWard().InsertUpdateMunicipalWard(beMunicipalWard);
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
            BindMunicipalWard(false);
            ddlCity.Focus();
        }

        /// <summary>
        /// This Method is used to Bind Cities
        /// </summary>
        /// <param name="CityId">This Parameter Belongs to BindCities</param>
        public void BindCities(Int32? CityId)
        {
            ddlCity.Items.Clear();
            DataSet dsCity = new BL_City().GetCity(null, null, null);
            if (dsCity.Tables.Count > 0 && dsCity != null && dsCity.Tables[0].Rows.Count > 0)
            {
                DataView dvCity = dsCity.Tables[0].DefaultView;
                if (hfMunicipalWardId.Value == "")
                {
                    dvCity.RowFilter = "Status = true";
                }
                if ((hfMunicipalWardId.Value != string.Empty) && (CityId != null))
                {
                    dvCity.RowFilter = "Status = true or CityId = " + CityId;
                }
                ddlCity.DataSource = dvCity;
                ddlCity.DataTextField = "City";
                ddlCity.DataValueField = "CityId";
                ddlCity.DataBind();
            }
            ddlCity.Items.Insert(0, "Select");
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
                    txtMunicipalWard.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindMunicipalWard(true);
                        BindCities(null);
                        btnAddMunicipalWard.Focus();
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
                BindMunicipalWard(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvMunicipalWard_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvMunicipalWard);
                    }
                    else
                    {
                        Image sortImage = new Image();
                        sortImage.ImageUrl = "~/Images/UpArrow.png";
                        sortImage.Width = 10;
                        sortImage.Height = 10;
                        sortImage.AlternateText = "Ascending Order";
                        e.Row.Cells[2].Controls.Add(sortImage);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvMunicipalWard_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMunicipalWard.PageIndex = e.NewPageIndex;
                BindMunicipalWard(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvMunicipalWard_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsMunicipalWard = new BL_MunicipalWard().GetMunicipalWard(Convert.ToInt32(e.CommandArgument.ToString()), null, null, null);
                    if (dsMunicipalWard != null && dsMunicipalWard.Tables.Count > 0 && dsMunicipalWard.Tables[0].Rows.Count > 0)
                    {
                        hfMunicipalWardId.Value = e.CommandArgument.ToString().ToUpper();
                        txtMunicipalWard.Text = dsMunicipalWard.Tables[0].Rows[0]["MunicipalWard"].ToString().ToUpper();
                        ddlCity.SelectedValue = dsMunicipalWard.Tables[0].Rows[0]["CityId"].ToString().ToUpper();
                        int CityId = Convert.ToInt32(dsMunicipalWard.Tables[0].Rows[0]["CityId"].ToString());
                        BindCities(CityId);
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsMunicipalWard.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsMunicipalWard.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        ddlCity.Focus();

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
                                gvMunicipalWard.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvMunicipalWard_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["MunicipalWard"] as DataSet;
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
                    gvMunicipalWard.DataSource = dv;
                    gvMunicipalWard.DataBind();
                    gvMunicipalWard.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddMunicipalWard_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                ddlCity.Focus();
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
                txtMunicipalWard.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfMunicipalWardId.Value != string.Empty)
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
                    AddMunicipalWard();
                else if (hbtntext.Value == "Update")
                    UpdateMunicipalWard();
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
                ddlCity.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}