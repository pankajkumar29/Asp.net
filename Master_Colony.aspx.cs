////-----------------------------------------------------------------------
// <copyright file="Master_Colony.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public partial class Master_Colony : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlMunicipalWard.SelectedIndex = 0;
            txtColony.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Colony
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindColony</param>
        private void BindColony(bool openOnEmpty)
        {
            DataSet dsColony = null;

            if (ddlStatus.SelectedIndex > 0)
                dsColony = new BL_Colony().GetColony(null, txtSearch.Text.Trim().ToUpper().ToUpper(), null, Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper().ToUpper()));
            else
                dsColony = new BL_Colony().GetColony(null, txtSearch.Text.Trim().ToUpper().ToUpper(), null, null);

            Cache["Colony"] = dsColony;
            if (dsColony != null && dsColony.Tables.Count > 0 && dsColony.Tables[0].Rows.Count > 0)
            {
                gvColony.DataSource = dsColony;
                gvColony.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsColony.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvColony.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }

                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvColony.DataSource = null;
                gvColony.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Colony
        /// </summary>
        private void AddColony()
        {
            modlpopup.Show();
            DataSet dsColony = new BL_Colony().GetColony(null, null, null, null);
            DataView dvColony = dsColony.Tables[0].DefaultView;
            DataRow[] rows = dsColony.Tables[0].Select("Colony ='" + txtColony.Text.Trim().ToUpper().ToUpper() + "' and MunicipalWardId =" + ddlMunicipalWard.SelectedValue);
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Colony already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtColony.Text = string.Empty;
                txtColony.Focus();
                return;
            }
            BE_Colony beColony = new BE_Colony();
            beColony.ColonyId = 0;
            beColony.MunicipalWardId = Convert.ToInt32(ddlMunicipalWard.SelectedValue);
            beColony.Colony = txtColony.Text.Trim().ToUpper().ToUpper();
            beColony.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beColony.Status = true;
                beColony.Reason = string.Empty;
            }
            else
            {
                beColony.Status = false;
                beColony.Reason = txtReason.Text.Trim().ToUpper().ToUpper();
            }
            DataSet dsResult = new BL_Colony().InsertUpdateColony(beColony);
            if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
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
            BindColony(false);
            ddlMunicipalWard.Focus();
        }

        /// <summary>
        /// This Method is used to Update Colony
        /// </summary>
        private void UpdateColony()
        {
            modlpopup.Show();
            DataSet dsColony = new BL_Colony().GetColony(null, null, null, null);
            DataView dvColony = dsColony.Tables[0].DefaultView;
            DataRow[] rows = dsColony.Tables[0].Select("ColonyId <> " + Convert.ToInt32(hfColonyId.Value) + "AND Colony ='" + txtColony.Text.Trim().ToUpper().ToUpper() + "' AND MunicipalWardId =" + ddlMunicipalWard.SelectedValue);
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Colony already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtColony.Text = string.Empty;
                txtColony.Focus();
                return;
            }
            BE_Colony beColony = new BE_Colony();
            beColony.ColonyId = Convert.ToInt32(hfColonyId.Value.Trim().ToUpper().ToUpper());
            beColony.MunicipalWardId = Convert.ToInt32(ddlMunicipalWard.SelectedValue);
            beColony.Colony = txtColony.Text.Trim().ToUpper().ToUpper();
            beColony.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beColony.Status = true;
                beColony.Reason = string.Empty;
            }
            else
            {
                beColony.Status = false;
                beColony.Reason = txtReason.Text.Trim().ToUpper().ToUpper();
            }
            DataSet dsResult = new BL_Colony().InsertUpdateColony(beColony);
            if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "Updated successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Failed to update details";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            BindColony(false);
            ddlMunicipalWard.Focus();
        }

        /// <summary>
        /// This Method is used to Bind Cities
        /// </summary>
        /// <param name="CityId">This Parameter Belongs to BindCities</param>
        public void BindCities(Int32? CityId)
        {
            ddlCity.Items.Clear();
            DataSet dsCities = new BL_City().GetCity(null, null, null);
            if (dsCities.Tables.Count > 0 && dsCities != null && dsCities.Tables[0].Rows.Count > 0)
            {
                DataView dvCities = dsCities.Tables[0].DefaultView;
                if (hfColonyId.Value == "")
                {
                    dvCities.RowFilter = "Status = true";
                }
                if ((hfColonyId.Value != string.Empty) && (CityId != null))
                {
                    dvCities.RowFilter = "Status = true or CityId = " + CityId;
                }
                ddlCity.DataSource = dvCities;
                ddlCity.DataTextField = "City";
                ddlCity.DataValueField = "CityId";
                ddlCity.DataBind();
            }
            ddlCity.Items.Insert(0, "Select");
            ddlMunicipalWard.Items.Clear();
            ddlMunicipalWard.Items.Insert(0, "Select");
        }

        /// <summary>
        /// This Method is used to Bind Municipal Wards
        /// </summary>
        /// <param name="MunicipalWardId">This Parameter Belongs to BindMunicipalWards</param>
        public void BindMunicipalWards(Int32? MunicipalWardId)
        {
            ddlMunicipalWard.Items.Clear();
            DataSet dsMunicipalWards = new BL_MunicipalWard().GetMunicipalWard(null, null, Convert.ToInt32(ddlCity.SelectedValue.Trim().ToUpper().ToUpper()), null);
            if (dsMunicipalWards.Tables.Count > 0 && dsMunicipalWards != null && dsMunicipalWards.Tables[0].Rows.Count > 0)
            {
                DataView dvMunicipalWards = dsMunicipalWards.Tables[0].DefaultView;
                if (hfColonyId.Value == "")
                {
                    dvMunicipalWards.RowFilter = "Status = true";
                }
                if ((hfColonyId.Value != string.Empty) && (MunicipalWardId != null))
                {
                    dvMunicipalWards.RowFilter = "Status = true or MunicipalWardId = " + MunicipalWardId;
                }
                ddlMunicipalWard.DataSource = dvMunicipalWards;
                ddlMunicipalWard.DataTextField = "MunicipalWard";
                ddlMunicipalWard.DataValueField = "MunicipalWardId";
                ddlMunicipalWard.DataBind();
            }
            ddlMunicipalWard.Items.Insert(0, "Select");
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
                    txtColony.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindColony(true);
                        BindCities(null);
                        btnAddColony.Focus();
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
                BindColony(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvColony_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvColony);
                    }
                    else
                    {
                        Image sortImage = new Image();
                        sortImage.ImageUrl = "~/Images/UpArrow.png";
                        sortImage.Width = 10;
                        sortImage.Height = 10;
                        sortImage.AlternateText = "Ascending Order";
                        e.Row.Cells[3].Controls.Add(sortImage);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvColony_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvColony.PageIndex = e.NewPageIndex;
                BindColony(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvColony_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsColony = new BL_Colony().GetColony(Convert.ToInt32(e.CommandArgument.ToString()), null, null, null);
                    if (dsColony != null && dsColony.Tables.Count > 0 && dsColony.Tables[0].Rows.Count > 0)
                    {
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        hfColonyId.Value = e.CommandArgument.ToString().ToUpper();
                        int CityId = Convert.ToInt32(dsColony.Tables[0].Rows[0]["CityId"].ToString());
                        BindCities(CityId);
                        ddlCity.SelectedValue = dsColony.Tables[0].Rows[0]["CityId"].ToString().ToUpper();
                        int MunicipalWardId = Convert.ToInt32(dsColony.Tables[0].Rows[0]["MunicipalWardId"].ToString());
                        BindMunicipalWards(MunicipalWardId);
                        ddlMunicipalWard.SelectedValue = dsColony.Tables[0].Rows[0]["MunicipalWardId"].ToString().ToUpper();
                        txtColony.Text = dsColony.Tables[0].Rows[0]["Colony"].ToString().ToUpper();
                        if (Convert.ToBoolean(dsColony.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsColony.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        ddlMunicipalWard.Focus();

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
                                gvColony.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvColony_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Colony"] as DataSet;
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
                    gvColony.DataSource = dv;
                    gvColony.DataBind();
                    gvColony.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddColony_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                ddlMunicipalWard.Focus();
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
                txtColony.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfColonyId.Value != string.Empty)
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

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                if (ddlCity.SelectedIndex > 0)
                {
                    BindMunicipalWards(null);
                }
                else
                {
                    ddlMunicipalWard.Items.Clear();
                    ddlMunicipalWard.Items.Insert(0, "Select");
                }
                ddlCity.Focus();
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
                    AddColony();
                else if (hbtntext.Value == "Update")
                    UpdateColony();
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
                ddlMunicipalWard.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}