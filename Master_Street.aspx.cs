////-----------------------------------------------------------------------
// <copyright file="Master_Street.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public partial class Master_Street : BasePage
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
            ddlColony.SelectedIndex = 0;
            txtStreet.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Cities
        /// </summary>
        /// <param name="CityId">This Parameter Belongs to BindCities</param>
        public void BindCities(Int32? CityId)
        {
            ddlCity.Items.Clear();
            DataSet dsCity = new BL_City().GetCity(null, null, null);
            DataView dvcountries = new DataView();
            if (dsCity.Tables.Count > 0 && dsCity != null && dsCity.Tables[0].Rows.Count > 0)
            {
                DataView dvCity = dsCity.Tables[0].DefaultView;
                if (hfStreetId.Value == "")
                {
                    dvCity.RowFilter = "Status = true";
                }
                if ((hfStreetId.Value != string.Empty) && (CityId != null))
                {
                    dvCity.RowFilter = "Status = true or CityId = " + CityId;
                }
                ddlCity.DataSource = dvCity;
                ddlCity.DataTextField = "City";
                ddlCity.DataValueField = "CityId";
                ddlCity.DataBind();
            }
            ddlCity.Items.Insert(0, "Select");
            ddlMunicipalWard.Items.Clear();
            ddlMunicipalWard.Items.Insert(0, "Select");
            ddlColony.Items.Clear();
            ddlColony.Items.Insert(0, "Select");
        }

        /// <summary>
        /// This Method is used to Bind Municipal Wards
        /// </summary>
        /// <param name="MunicipalWardId">This Parameter Belongs to BindMunicipalWards</param>
        public void BindMunicipalWards(Int32? MunicipalWardId)
        {
            ddlMunicipalWard.Items.Clear();
            DataSet dsMunicipalWards = new BL_MunicipalWard().GetMunicipalWard(null, null, Convert.ToInt32(ddlCity.SelectedValue.Trim().ToUpper()), null);
            if (dsMunicipalWards.Tables.Count > 0 && dsMunicipalWards != null && dsMunicipalWards.Tables[0].Rows.Count > 0)
            {
                DataView dvMunicipalWards = dsMunicipalWards.Tables[0].DefaultView;
                if (hfStreetId.Value == "")
                {
                    dvMunicipalWards.RowFilter = "Status = true";
                }
                if ((hfStreetId.Value != string.Empty) && (MunicipalWardId != null))
                {
                    dvMunicipalWards.RowFilter = "Status = true or MunicipalWardId = " + MunicipalWardId;
                }
                ddlMunicipalWard.DataSource = dvMunicipalWards;
                ddlMunicipalWard.DataTextField = "MunicipalWard";
                ddlMunicipalWard.DataValueField = "MunicipalWardId";
                ddlMunicipalWard.DataBind();
            }
            ddlMunicipalWard.Items.Insert(0, "Select");
            ddlColony.Items.Clear();
            ddlColony.Items.Insert(0, "Select");
        }

        /// <summary>
        /// This Method is used to Bind Colonies
        /// </summary>
        /// <param name="ColonyId">This Parameter Belongs to BindColonies</param>
        public void BindColonies(Int32? ColonyId)
        {
            ddlColony.Items.Clear();
            DataSet dsColonies = new BL_Colony().GetColony(null, null, Convert.ToInt32(ddlMunicipalWard.SelectedValue), null);
            if (dsColonies.Tables.Count > 0 && dsColonies != null && dsColonies.Tables[0].Rows.Count > 0)
            {
                DataView dvColonies = new DataView();
                dvColonies = dsColonies.Tables[0].DefaultView;
                if (hfStreetId.Value == "")
                {
                    dvColonies.RowFilter = "Status = true";
                }
                if ((hfStreetId.Value != string.Empty) && (ColonyId != null))
                {
                    dvColonies.RowFilter = "Status = true or ColonyId = " + ColonyId;
                }
                ddlColony.DataSource = dvColonies;
                ddlColony.DataTextField = "Colony";
                ddlColony.DataValueField = "ColonyId";
                ddlColony.DataBind();
            }
            ddlColony.Items.Insert(0, "Select");
        }

        /// <summary>
        /// This Method is used to Bind Street
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to BindStreet</param>
        private void BindStreet(bool openOnEmpty)
        {
            DataSet dsStreet = null;

            if (ddlStatus.SelectedIndex > 0)
                dsStreet = new BL_Street().GetStreet(null, txtSearch.Text.Trim().ToUpper(), null, Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsStreet = new BL_Street().GetStreet(null, txtSearch.Text.Trim().ToUpper(), null, null);

            Cache["Street"] = dsStreet;
            if (dsStreet != null && dsStreet.Tables.Count > 0 && dsStreet.Tables[0].Rows.Count > 0)
            {
                gvStreet.DataSource = dsStreet;
                gvStreet.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsStreet.Tables[0].Rows.Count.ToString().ToUpper();
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
                        gvStreet.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvStreet.DataSource = null;
                gvStreet.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Street
        /// </summary>
        private void AddStreet()
        {
            modlpopup.Show();
            DataSet dsStreet = new BL_Street().GetStreet(null, null, null, null);
            DataView dvStreet = dsStreet.Tables[0].DefaultView;
            DataRow[] rows = dsStreet.Tables[0].Select("Street = '" + txtStreet.Text.Trim().ToUpper() + "' and ColonyId = " + ddlColony.SelectedValue);
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Street already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtStreet.Text = string.Empty;
                txtStreet.Focus();
                return;
            }
            BE_Street beStreet = new BE_Street();
            beStreet.StreetId = 0;
            beStreet.ColonyId = Convert.ToInt32(ddlColony.SelectedValue);
            beStreet.Street = txtStreet.Text.Trim().ToUpper();
            beStreet.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beStreet.Status = true;
                beStreet.Reason = string.Empty;
            }
            else
            {
                beStreet.Status = false;
                beStreet.Reason = txtReason.Text.Trim().ToUpper();
            }
            DataSet dsResult = new BL_Street().InsertUpdateStreet(beStreet);
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
            BindStreet(false);
            ddlCity.Focus();
        }

        /// <summary>
        /// This Method is used to Update Street
        /// </summary>
        private void UpdateStreet()
        {
            modlpopup.Show();
            DataSet dsStreet = new BL_Street().GetStreet(null, null, null, null);
            DataView dvStreet = dsStreet.Tables[0].DefaultView;
            DataRow[] rows = dsStreet.Tables[0].Select("StreetId <> " + Convert.ToInt32(hfStreetId.Value) + "AND Street ='" + txtStreet.Text.Trim().ToUpper() + "' and ColonyId=" + ddlColony.SelectedValue);
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Street already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtStreet.Text = string.Empty;
                txtStreet.Focus();
                return;
            }
            BE_Street beStreet = new BE_Street();
            beStreet.StreetId = Convert.ToInt32(hfStreetId.Value.Trim().ToUpper());
            beStreet.ColonyId = Convert.ToInt32(ddlColony.SelectedValue);
            beStreet.Street = txtStreet.Text.Trim().ToUpper();
            beStreet.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beStreet.Status = true;
                beStreet.Reason = string.Empty;
            }
            else
            {
                beStreet.Status = false;
                beStreet.Reason = txtReason.Text.Trim().ToUpper();
            }
            DataSet dsResult = new BL_Street().InsertUpdateStreet(beStreet);
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
            BindStreet(false);
            ddlCity.Focus();
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
                    txtStreet.Attributes.Add("onkeyup", "CheckFirstChar(this);  return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", " CheckFirstChar(this);  return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindStreet(true);
                        BindCities(null);
                        btnAddStreet.Focus();
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
                BindStreet(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvStreet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvStreet);
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

        protected void gvStreet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvStreet.PageIndex = e.NewPageIndex;
                BindStreet(false);
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
                    ddlMunicipalWard.Focus();
                }
                else
                {
                    ddlMunicipalWard.Items.Clear();
                    ddlMunicipalWard.Items.Insert(0, "Select");
                    ddlColony.Items.Clear();
                    ddlColony.Items.Insert(0, "Select");
                    ddlCity.Focus();
                }

            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void ddlMunicipalWard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                if (ddlMunicipalWard.SelectedIndex > 0)
                {
                    BindColonies(null);
                    ddlColony.Focus();
                }
                else
                {
                    ddlColony.Items.Clear();
                    ddlColony.Items.Insert(0, "Select");
                    ddlMunicipalWard.Focus();
                }

            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvStreet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsStreet = new BL_Street().GetStreet(Convert.ToInt32(e.CommandArgument.ToString()), null, null, null);
                    if (dsStreet != null && dsStreet.Tables.Count > 0 && dsStreet.Tables[0].Rows.Count > 0)
                    {
                        hfStreetId.Value = e.CommandArgument.ToString().ToUpper();
                        txtStreet.Text = dsStreet.Tables[0].Rows[0]["Street"].ToString().ToUpper();
                        int CityId = Convert.ToInt32(dsStreet.Tables[0].Rows[0]["CityId"].ToString());
                        BindCities(CityId);
                        ddlCity.SelectedValue = dsStreet.Tables[0].Rows[0]["CityId"].ToString().ToUpper();
                        int MunicipalWardId = Convert.ToInt32(dsStreet.Tables[0].Rows[0]["MunicipalWardId"].ToString());
                        BindMunicipalWards(MunicipalWardId);
                        ddlMunicipalWard.SelectedValue = dsStreet.Tables[0].Rows[0]["MunicipalWardId"].ToString().ToUpper();
                        int ColonyId = Convert.ToInt32(dsStreet.Tables[0].Rows[0]["ColonyId"].ToString());
                        BindColonies(ColonyId);
                        ddlColony.SelectedValue = dsStreet.Tables[0].Rows[0]["ColonyId"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsStreet.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsStreet.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
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
                                gvStreet.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvStreet_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Street"] as DataSet;
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
                    gvStreet.DataSource = dv;
                    gvStreet.DataBind();
                    gvStreet.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddStreet_Click(object sender, EventArgs e)
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
                txtStreet.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfStreetId.Value != string.Empty)
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
                    AddStreet();
                else if (hbtntext.Value == "Update")
                    UpdateStreet();
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