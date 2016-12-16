////-----------------------------------------------------------------------
// <copyright file="ManageFacilities.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>05/03/2012</date>
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
using System.Web.UI.WebControls;
using DhiiLifeSpring.AppInfrastructure;
using DhiiLifeSpring.BusinessEntities;
using DhiiLifeSpring.BusinessLayer;

#endregion

namespace DhiiLifeSpring.Application.Masters
{
    public partial class ManageFacilities : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Bind Facilities
        /// </summary>
        private void BindFacilities()
        {
            DataSet dsFacilities = new BL_Facility().GetFacilities();
            Cache["Facilities"] = dsFacilities;
            if (dsFacilities != null && dsFacilities.Tables.Count > 0 && dsFacilities.Tables[0].Rows.Count > 0)
            {
                gvFacilities.DataSource = dsFacilities;
                gvFacilities.DataBind();
            }
            else
            {
                gvFacilities.DataSource = null;
                gvFacilities.DataBind();
            }
        }

        /// <summary>
        /// This Method is used to Bind Move Facility
        /// </summary>
        private void BindMoveFacility()
        {
            ddlbarnch.Items.Clear();
            DataSet dsfacility = new BL_Facility().GetFacilities();
            DataView dvFacility = new DataView();
            if (dsfacility.Tables.Count > 0 && dsfacility != null && dsfacility.Tables[0].Rows.Count > 0)
            {
                dvFacility = dsfacility.Tables[0].DefaultView;

                dvFacility.RowFilter = "Status= true and Corporate is null";
                ddlbarnch.DataSource = dvFacility;
                ddlbarnch.DataTextField = "FacilityName";
                ddlbarnch.DataValueField = "FacilityId";
                ddlbarnch.DataBind();
                ddlbarnch.Items.Insert(0, "Select");
            }
            else
            {
                ddlbarnch.Items.Insert(0, "Select");
            }
        }

        /// <summary>
        /// This Method is used to Add Facility
        /// </summary>
        private void AddFacility()
        {
            modlpopup.Show();
            DataSet dsFacility = new BL_Facility().GetFacilities();
            DataRow[] rows = dsFacility.Tables[0].Select("FacilityName = '" + txtFacility.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Branch already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtFacility.Text = string.Empty;
                txtFacility.Focus();
                return;
            }
            BE_Facility beFacility = new BE_Facility();
            beFacility.FacilityId = 0;
            beFacility.FacilityName = txtFacility.Text.Trim().ToUpper();
            beFacility.Address = txtAddress.Text.Trim().ToUpper();
            beFacility.Phone = txtPhoneNo.Text.Trim().ToUpper();
            beFacility.Mobile = txtMobileNo.Text.Trim().ToUpper();
            if (ddlCity.SelectedIndex > 0)
                beFacility.DefaultCityId = Convert.ToInt32(ddlCity.SelectedValue);
            if (ddlMunicipalWard.SelectedIndex > 0)
                beFacility.DefaultMunicipalWardId = Convert.ToInt32(ddlMunicipalWard.SelectedValue);
            if (ddlColony.SelectedIndex > 0)
                beFacility.DefaultColonyId = Convert.ToInt32(ddlColony.SelectedValue);
            if (ddlStreet.SelectedIndex > 0)
                beFacility.DefaultStreetId = Convert.ToInt32(ddlStreet.SelectedValue);
            TextBox txtOPBillMessage = (TextBox)gvMessages.Rows[0].FindControl("txtMessages");
            beFacility.OPBillMessage = txtOPBillMessage.Text.Trim().ToUpper();
            TextBox txtIPBillMessage = (TextBox)gvMessages.Rows[1].FindControl("txtMessages");
            beFacility.IPBillMessage = txtIPBillMessage.Text.Trim().ToUpper();
            TextBox txtServicesBillMessage = (TextBox)gvMessages.Rows[2].FindControl("txtMessages");
            beFacility.ServicesBillMessage = txtServicesBillMessage.Text.Trim().ToUpper();
            beFacility.CreatedBy = Convert.ToString(Session["UserId"]);
            beFacility.Status = true;
            DataSet dsresult = new BL_Facility().InsertUpdateFacility(beFacility);
            if (dsresult != null && dsresult.Tables.Count > 0 && dsresult.Tables[0].Rows.Count > 0)
            {
                int FacilityId = Convert.ToInt32(dsresult.Tables[0].Rows[0][0].ToString());
                if (gvWards.Enabled == true)
                {
                    foreach (GridViewRow gvr in gvWards.Rows)
                    {
                        CheckBox ChkSelect = (CheckBox)gvr.FindControl("ChkSelect");
                        Label lblWardName = (Label)gvr.FindControl("lblWardName");
                        Label lblShortCode = (Label)gvr.FindControl("lblShortCode");
                        Label lblOrder = (Label)gvr.FindControl("lblOrder");
                        TextBox txtBeds = (TextBox)gvr.FindControl("txtBeds");
                        if (ChkSelect.Checked == true)
                        {
                            if (txtBeds.Text != string.Empty)
                            {
                                int WardId = 0;
                                BE_Ward beWard = new BE_Ward();
                                beWard.FacilityId = FacilityId;
                                beWard.WardId = 0;
                                beWard.Ward = lblWardName.Text.Trim().ToUpper();
                                beWard.ShortCode = lblShortCode.Text.Trim().ToUpper();
                                beWard.Ordering = Convert.ToInt32(lblOrder.Text.Trim().ToUpper());
                                beWard.CreatedBy = Convert.ToString(Session["UserId"]);
                                beWard.Status = true;
                                beWard.Reason = string.Empty;
                                DataSet dsWard = new BL_Ward().InsertUpdateWard(beWard);
                                if (dsWard != null && dsWard.Tables.Count > 0 && dsWard.Tables[0].Rows.Count > 0)
                                {
                                    WardId = Convert.ToInt32(dsWard.Tables[0].Rows[0][0].ToString());
                                    for (int i = 1; i <= Convert.ToInt32(txtBeds.Text.Trim()); i++)
                                    {
                                        BE_Bed beBed = new BE_Bed();
                                        beBed.FacilityId = FacilityId;
                                        beBed.BedId = 0;
                                        beBed.WardId = WardId;
                                        beBed.WardShortCode = lblShortCode.Text.Trim().ToUpper();
                                        beBed.Bed = i.ToString().ToUpper();
                                        beBed.CreatedBy = Convert.ToString(Session["UserId"]);
                                        beBed.Status = true;
                                        beBed.Reason = string.Empty;
                                        new BL_Bed().InsertUpdateBed(beBed);
                                    }
                                }
                            }
                        }
                    }

                    BE_ServiceCost BeServicecost = new BE_ServiceCost();
                    if (ddlbarnch.SelectedIndex > 0)
                    {

                        BeServicecost.ClinicId = Convert.ToInt32(ddlbarnch.SelectedValue);
                        BeServicecost.Ward = null;
                        DataSet dsServicecost = new BL_ServiceCost().GetServiceCost(BeServicecost);
                        if (dsServicecost != null && dsServicecost.Tables.Count > 0 && dsServicecost.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsServicecost.Tables[0].Rows)
                            {
                                Int32 TariffId = Convert.ToInt32(dr["TariffId"].ToString());
                                Int32 ServiceID = Convert.ToInt32(dr["ServiceID"]);
                                string Bedcategory = dr["Ward"].ToString().ToUpper();
                                string createOrUpdateBy = Convert.ToString(Session["UserId"]);
                                decimal cost = Convert.ToDecimal(dr["ServiceCost"].ToString());
                                int ClinicId = Convert.ToInt32(FacilityId);
                                new BL_ServiceCost().InsertUpdateServiceCost(TariffId, ServiceID, Bedcategory, createOrUpdateBy, cost, ClinicId);
                            }

                        }
                    }
                }
                lblMsg.Text = "Added successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                ClearFields();
                BindFacilities();
            }
        }

        /// <summary>
        /// This Method is used to Update Facility
        /// </summary>
        private void UpdateFacility()
        {
            modlpopup.Show();
            DataSet dsFacility = new BL_Facility().GetFacilities();
            DataRow[] rows = dsFacility.Tables[0].Select("FacilityId <> " + Convert.ToInt32(hfFacilityId.Value) + "AND FacilityName = '" + txtFacility.Text.Trim() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Branch already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtFacility.Text = string.Empty;
                txtFacility.Focus();
                return;
            }
            BE_Facility beFacility = new BE_Facility();
            beFacility.FacilityId = Convert.ToInt32(hfFacilityId.Value.Trim());
            beFacility.FacilityName = txtFacility.Text.Trim().ToUpper();
            beFacility.Address = txtAddress.Text.Trim().ToUpper();
            beFacility.Phone = txtPhoneNo.Text.Trim().ToUpper();
            beFacility.Mobile = txtMobileNo.Text.Trim().ToUpper();
            if (ddlCity.SelectedIndex > 0)
                beFacility.DefaultCityId = Convert.ToInt32(ddlCity.SelectedValue);
            if (ddlMunicipalWard.SelectedIndex > 0)
                beFacility.DefaultMunicipalWardId = Convert.ToInt32(ddlMunicipalWard.SelectedValue);
            if (ddlColony.SelectedIndex > 0)
                beFacility.DefaultColonyId = Convert.ToInt32(ddlColony.SelectedValue);
            if (ddlStreet.SelectedIndex > 0)
                beFacility.DefaultStreetId = Convert.ToInt32(ddlStreet.SelectedValue);
            TextBox txtOPBillMessage = (TextBox)gvMessages.Rows[0].FindControl("txtMessages");
            beFacility.OPBillMessage = txtOPBillMessage.Text.Trim().ToUpper();
            TextBox txtIPBillMessage = (TextBox)gvMessages.Rows[1].FindControl("txtMessages");
            beFacility.IPBillMessage = txtIPBillMessage.Text.Trim().ToUpper();
            TextBox txtServicesBillMessage = (TextBox)gvMessages.Rows[2].FindControl("txtMessages");
            beFacility.ServicesBillMessage = txtServicesBillMessage.Text.Trim().ToUpper();
            beFacility.CreatedBy = Convert.ToString(Session["UserId"]);
            beFacility.Status = true;
            DataSet dsresult = new BL_Facility().InsertUpdateFacility(beFacility);
            if (dsresult != null && dsresult.Tables.Count > 0 && dsresult.Tables[0].Rows.Count > 0)
            {
                int FacilityId = Convert.ToInt32(dsresult.Tables[0].Rows[0][0].ToString());
                if (gvWards.Enabled == true)
                {
                    foreach (GridViewRow gvr in gvWards.Rows)
                    {
                        CheckBox ChkSelect = (CheckBox)gvr.FindControl("ChkSelect");
                        Label lblFacilityWardId = (Label)gvr.FindControl("lblFacilityWardId");
                        Label lblFacilityNoOfBeds = (Label)gvr.FindControl("lblFacilityNoOfBeds");
                        Label lblWardName = (Label)gvr.FindControl("lblWardName");
                        Label lblShortCode = (Label)gvr.FindControl("lblShortCode");
                        Label lblOrder = (Label)gvr.FindControl("lblOrder");
                        TextBox txtBeds = (TextBox)gvr.FindControl("txtBeds");
                        if (ChkSelect.Checked == true)
                        {
                            if (txtBeds.Text != string.Empty)
                            {
                                int WardId = 0;
                                BE_Ward beWard = new BE_Ward();
                                beWard.FacilityId = FacilityId;
                                beWard.WardId = Convert.ToInt32(lblFacilityWardId.Text.Trim().ToUpper());
                                beWard.Ward = lblWardName.Text.Trim().ToUpper();
                                beWard.ShortCode = lblShortCode.Text.Trim().ToUpper();
                                beWard.Ordering = Convert.ToInt32(lblOrder.Text.Trim().ToUpper());
                                beWard.CreatedBy = Convert.ToString(Session["UserId"]);
                                beWard.Status = true;
                                beWard.Reason = string.Empty;
                                DataSet dsWard = new BL_Ward().InsertUpdateWard(beWard);
                                if (dsWard != null && dsWard.Tables.Count > 0 && dsWard.Tables[0].Rows.Count > 0)
                                {
                                    WardId = Convert.ToInt32(dsWard.Tables[0].Rows[0][0].ToString());
                                    int j = Convert.ToInt32(lblFacilityNoOfBeds.Text.Trim()) + 1;
                                    for (int i = j; i <= Convert.ToInt32(txtBeds.Text.Trim()); i++)
                                    {
                                        BE_Bed beBed = new BE_Bed();
                                        beBed.FacilityId = FacilityId;
                                        beBed.BedId = 0;
                                        beBed.WardId = WardId;
                                        beBed.WardShortCode = lblShortCode.Text.Trim().ToUpper();
                                        beBed.Bed = i.ToString().ToUpper();
                                        beBed.CreatedBy = Convert.ToString(Session["UserId"]);
                                        beBed.Status = true;
                                        beBed.Reason = string.Empty;
                                        new BL_Bed().InsertUpdateBed(beBed);
                                    }
                                }
                            }
                        }
                    }

                    BE_ServiceCost BeServicecost = new BE_ServiceCost();
                    if (ddlbarnch.SelectedIndex > 0)
                    {

                        BeServicecost.ClinicId = Convert.ToInt32(ddlbarnch.SelectedValue);
                        BeServicecost.Ward = null;
                        DataSet dsServicecost = new BL_ServiceCost().GetServiceCost(BeServicecost);
                        if (dsServicecost != null && dsServicecost.Tables.Count > 0 && dsServicecost.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsServicecost.Tables[0].Rows)
                            {
                                decimal? cost = null;
                                Int32 TariffId = Convert.ToInt32(dr["TariffId"].ToString());
                                Int32 ServiceID = Convert.ToInt32(dr["ServiceID"]);
                                string Bedcategory = dr["Ward"].ToString().ToUpper();
                                string createOrUpdateBy = Convert.ToString(Session["UserId"]);
                                if (dr["ServiceCost"].ToString() != string.Empty)
                                {
                                    cost = Convert.ToDecimal(dr["ServiceCost"].ToString());
                                }
                                int ClinicId = Convert.ToInt32(FacilityId);
                                new BL_ServiceCost().InsertUpdateServiceCost(TariffId, ServiceID, Bedcategory, createOrUpdateBy, cost, ClinicId);
                            }

                        }
                    }
                }
            }
            lblMsg.Text = "Updated successfully";
            lblMsg.ForeColor = System.Drawing.Color.Green;
            DataSet dsFacilityWards = new BL_Facility().GetFacilityWardBeds(Convert.ToInt32(hfFacilityId.Value));
            if (dsFacilityWards != null && dsFacilityWards.Tables.Count > 0 && dsFacilityWards.Tables[0].Rows.Count > 0)
            {
                gvWards.DataSource = dsFacilityWards;
                gvWards.DataBind();
            }
            else
            {
                gvWards.DataSource = null;
                gvWards.DataBind();
            }

            foreach (GridViewRow gvr in gvWards.Rows)
            {
                Label lblFacilityWardId = (Label)gvr.FindControl("lblFacilityWardId");
                CheckBox ChkSelect = (CheckBox)gvr.FindControl("ChkSelect");
                Label lblFacilityNoOfBeds = (Label)gvr.FindControl("lblFacilityNoOfBeds");
                TextBox txtBeds = (TextBox)gvr.FindControl("txtBeds");
                if (lblFacilityWardId.Text != "0")
                    ChkSelect.Checked = true;
                else
                    ChkSelect.Checked = false;
                if (lblFacilityNoOfBeds.Text == "0")
                    txtBeds.Text = string.Empty;
                else
                    txtBeds.Text = lblFacilityNoOfBeds.Text.Trim();
            }
            BindFacilities();
        }

        /// <summary>
        /// This Method is used to Clear Fields
        /// </summary>
        private void ClearFields()
        {
            txtFacility.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            BindCities(null);
            BindWards();
            BindMessages();
            btnCreate.Text = "Add";
            btnCreate.ToolTip = "Add";
            hfbtntext.Value = "Add";
            gvFacilities.Enabled = true;
            BindMoveFacility();
            gvWards.Enabled = true;
            gvMessages.Enabled = true;
            ddlbarnch.Enabled = true;
        }

        /// <summary>
        /// This Method is used to Bind Cities
        /// </summary>
        /// <param name="CityId">This Parameter Belongs to BindCities</param>
        public void BindCities(Int32? CityId)
        {
            ddlCity.Items.Clear();
            DataSet dsCity = new BL_City().GetCity(null, null, null);
            if (dsCity != null && dsCity.Tables.Count > 0 && dsCity.Tables[0].Rows.Count > 0)
            {
                DataView dvCity = dsCity.Tables[0].DefaultView;
                if (CityId != null)
                    dvCity.RowFilter = "Status = true or CityId = " + CityId;
                else
                    dvCity.RowFilter = "Status = true";
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
            ddlStreet.Items.Clear();
            ddlStreet.Items.Insert(0, "Select");
        }

        /// <summary>
        /// This Method is used to Bind Municipal Wards
        /// </summary>
        /// <param name="MunicipalWardId">This Parameter Belongs to BindMunicipalWards</param>
        public void BindMunicipalWards(Int32? MunicipalWardId)
        {
            ddlMunicipalWard.Items.Clear();
            DataSet dsMunicipalWards = new BL_MunicipalWard().GetMunicipalWard(null, null, Convert.ToInt32(ddlCity.SelectedValue.Trim()), null);
            if (dsMunicipalWards != null && dsMunicipalWards.Tables.Count > 0 && dsMunicipalWards.Tables[0].Rows.Count > 0)
            {
                DataView dvMunicipalWards = dsMunicipalWards.Tables[0].DefaultView;
                if (MunicipalWardId != null)
                    dvMunicipalWards.RowFilter = "Status = true or MunicipalWardId = " + MunicipalWardId;
                else
                    dvMunicipalWards.RowFilter = "Status = true";
                ddlMunicipalWard.DataSource = dvMunicipalWards;
                ddlMunicipalWard.DataTextField = "MunicipalWard";
                ddlMunicipalWard.DataValueField = "MunicipalWardId";
                ddlMunicipalWard.DataBind();
            }
            ddlMunicipalWard.Items.Insert(0, "Select");
            ddlColony.Items.Clear();
            ddlColony.Items.Insert(0, "Select");
            ddlStreet.Items.Clear();
            ddlStreet.Items.Insert(0, "Select");
        }

        /// <summary>
        /// This Method is used to Bind Colonies
        /// </summary>
        /// <param name="ColonyId">This Parameter Belongs to BindColonies</param>
        public void BindColonies(Int32? ColonyId)
        {
            ddlColony.Items.Clear();
            DataSet dsColonies = new BL_Colony().GetColony(null, null, Convert.ToInt32(ddlMunicipalWard.SelectedValue), null);
            if (dsColonies != null && dsColonies.Tables.Count > 0 && dsColonies.Tables[0].Rows.Count > 0)
            {
                DataView dvColonies = new DataView();
                dvColonies = dsColonies.Tables[0].DefaultView;
                if (ColonyId != null)
                    dvColonies.RowFilter = "Status = true or ColonyId = " + ColonyId;
                else
                    dvColonies.RowFilter = "Status = true";
                ddlColony.DataSource = dvColonies;
                ddlColony.DataTextField = "Colony";
                ddlColony.DataValueField = "ColonyId";
                ddlColony.DataBind();
            }
            ddlColony.Items.Insert(0, "Select");
            ddlStreet.Items.Clear();
            ddlStreet.Items.Insert(0, "Select");
        }

        /// <summary>
        /// This Method is used to Bind Streets
        /// </summary>
        /// <param name="StreetId">This Parameter Belongs to BindStreets</param>
        public void BindStreets(Int32? StreetId)
        {
            ddlStreet.Items.Clear();
            DataSet dsStreets = new BL_Street().GetStreet(null, null, Convert.ToInt32(ddlColony.SelectedValue.Trim()), true);
            if (dsStreets != null && dsStreets.Tables.Count > 0 && dsStreets.Tables[0].Rows.Count > 0)
            {
                DataView dvStreets = new DataView();
                dvStreets = dsStreets.Tables[0].DefaultView;
                if (StreetId != null)
                    dvStreets.RowFilter = "Status = true or StreetId = " + StreetId;
                else
                    dvStreets.RowFilter = "Status = true";
                ddlStreet.DataSource = dvStreets;
                ddlStreet.DataTextField = "Street";
                ddlStreet.DataValueField = "StreetId";
                ddlStreet.DataBind();
            }
            ddlStreet.Items.Insert(0, "Select");
        }

        /// <summary>
        /// This Method is used to Bind Wards
        /// </summary>
        private void BindWards()
        {
            DataSet dsWards = new BL_Ward().GetWardCommon(null, null, true);
            if (dsWards != null && dsWards.Tables.Count > 0 && dsWards.Tables[0].Rows.Count > 0)
            {
                gvWards.DataSource = dsWards;
                gvWards.DataBind();
            }
            else
            {
                gvWards.DataSource = null;
                gvWards.DataBind();
            }
        }

        /// <summary>
        /// This Method is used to Bind Messages
        /// </summary>
        private void BindMessages()
        {
            DataTable dtMessages = new DataTable();
            dtMessages.Columns.Add("Type");
            dtMessages.Columns.Add("Message");

            DataRow row1 = dtMessages.NewRow();
            row1["Type"] = "OP";
            row1["Message"] = string.Empty;
            dtMessages.Rows.Add(row1);

            DataRow row2 = dtMessages.NewRow();
            row2["Type"] = "IP";
            row2["Message"] = string.Empty;
            dtMessages.Rows.Add(row2);

            DataRow row3 = dtMessages.NewRow();
            row3["Type"] = "Services";
            row3["Message"] = string.Empty;
            dtMessages.Rows.Add(row3);

            gvMessages.DataSource = dtMessages;
            gvMessages.DataBind();
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
                    if (!IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        ClearFields();
                        BindFacilities();
                        BindMoveFacility();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void gvFacilities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    hfFacilityId.Value = e.CommandArgument.ToString().ToUpper();
                    DataSet dsFacilities = new BL_Facility().GetFacilities();
                    if (dsFacilities != null && dsFacilities.Tables.Count > 0 && dsFacilities.Tables[0].Rows.Count > 0)
                    {
                        DataRow[] drFacilities = dsFacilities.Tables[0].Select("FacilityId=" + Convert.ToInt32(hfFacilityId.Value));
                        if (drFacilities.Count() > 0)
                        {
                            modlpopup.Show();
                            DataRow drFacility = drFacilities[0];
                            ClearFields();
                            txtFacility.Text = Convert.ToString(drFacility["FacilityName"]);
                            txtAddress.Text = Convert.ToString(drFacility["Address"]);
                            txtPhoneNo.Text = Convert.ToString(drFacility["Phone"]);
                            txtMobileNo.Text = Convert.ToString(drFacility["Mobile"]);
                            int CityId = Convert.ToInt32(drFacility["DefaultCityId"].ToString());
                            int MunicipalWardId = Convert.ToInt32(drFacility["DefaultMunicipalWardId"].ToString());
                            int ColonyId = Convert.ToInt32(drFacility["DefaultColonyId"].ToString());
                            int StreetId = Convert.ToInt32(drFacility["DefaultStreetId"].ToString());
                            BindCities(CityId);
                            ddlCity.SelectedValue = CityId.ToString().ToUpper();
                            BindMunicipalWards(MunicipalWardId);
                            ddlMunicipalWard.SelectedValue = MunicipalWardId.ToString().ToUpper();
                            BindColonies(ColonyId);
                            ddlColony.SelectedValue = ColonyId.ToString().ToUpper();
                            BindStreets(StreetId);
                            ddlStreet.SelectedValue = StreetId.ToString().ToUpper();

                            if (drFacility["Corporate"].ToString() == "C")
                            {
                                gvWards.Enabled = false;
                                gvMessages.Enabled = false;
                                ddlbarnch.Enabled = false;
                            }
                            else
                            {
                                gvWards.Enabled = true;
                                gvMessages.Enabled = true;
                                ddlbarnch.Enabled = true;

                                DataSet dsFacilityWards = new BL_Facility().GetFacilityWardBeds(Convert.ToInt32(hfFacilityId.Value));
                                if (dsFacilityWards != null && dsFacilityWards.Tables.Count > 0 && dsFacilityWards.Tables[0].Rows.Count > 0)
                                {
                                    gvWards.DataSource = dsFacilityWards;
                                    gvWards.DataBind();
                                }
                                else
                                {
                                    gvWards.DataSource = null;
                                    gvWards.DataBind();
                                }

                                DataSet dsFacilityMessages = new BL_Facility().GetFacilityMessages(Convert.ToInt32(hfFacilityId.Value));
                                if (dsFacilityMessages != null && dsFacilityMessages.Tables.Count > 0 && dsFacilityMessages.Tables[0].Rows.Count > 0)
                                {
                                    gvMessages.DataSource = dsFacilityMessages;
                                    gvMessages.DataBind();
                                }

                                foreach (GridViewRow gvr in gvWards.Rows)
                                {
                                    Label lblFacilityWardId = (Label)gvr.FindControl("lblFacilityWardId");
                                    CheckBox ChkSelect = (CheckBox)gvr.FindControl("ChkSelect");
                                    Label lblFacilityNoOfBeds = (Label)gvr.FindControl("lblFacilityNoOfBeds");
                                    TextBox txtBeds = (TextBox)gvr.FindControl("txtBeds");
                                    if (lblFacilityWardId.Text != "0")
                                        ChkSelect.Checked = true;
                                    else
                                        ChkSelect.Checked = false;
                                    if (lblFacilityNoOfBeds.Text == "0")
                                        txtBeds.Text = string.Empty;
                                    else
                                        txtBeds.Text = lblFacilityNoOfBeds.Text.Trim();
                                }
                            }

                            btnCreate.Text = "Update";
                            btnCreate.ToolTip = "Update";
                            hfbtntext.Value = "Update";
                            txtFacility.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void gvFacilities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvFacilities.PageIndex = e.NewPageIndex;
                BindFacilities();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void gvFacilities_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvFacilities);
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
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void gvFacilities_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Facilities"] as DataSet;
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
                    gvFacilities.DataSource = dv;
                    gvFacilities.DataBind();
                    gvFacilities.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddFacility_Click(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                ClearFields();
                txtFacility.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
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
                    ddlColony.Items.Clear();
                    ddlColony.Items.Insert(0, "Select");
                    ddlStreet.Items.Clear();
                    ddlStreet.Items.Insert(0, "Select");
                }
                ddlCity.Focus();
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
                }
                else
                {
                    ddlColony.Items.Clear();
                    ddlColony.Items.Insert(0, "Select");
                    ddlStreet.Items.Clear();
                    ddlStreet.Items.Insert(0, "Select");
                }
                ddlMunicipalWard.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void ddlColony_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                if (ddlColony.SelectedIndex > 0)
                {
                    BindStreets(null);
                }
                else
                {
                    ddlStreet.Items.Clear();
                    ddlStreet.Items.Insert(0, "Select");
                }
                ddlColony.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void txtBeds_TextChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                TextBox txtBeds = (TextBox)sender;
                GridViewRow gvRow = (GridViewRow)(sender as TextBox).NamingContainer;
                Label lblFacilityNoOfBeds = (Label)gvRow.FindControl("lblFacilityNoOfBeds");
                if (lblFacilityNoOfBeds.Text != "0")
                {
                    int PresentBeds = Convert.ToInt32(txtBeds.Text.Trim());
                    int PreviousBeds = Convert.ToInt32(lblFacilityNoOfBeds.Text.Trim());
                    if (PresentBeds < PreviousBeds)
                    {
                        lblMsg.Text = "No. of beds cannot be less than " + PreviousBeds;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        txtBeds.Text = PreviousBeds.ToString().ToUpper();
                    }
                }
                txtBeds.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                int i = 0, j = 0;
                if (gvWards.Enabled == true)
                {
                    foreach (GridViewRow gvr in gvWards.Rows)
                    {
                        CheckBox ChkSelect = (CheckBox)gvr.FindControl("ChkSelect");
                        TextBox txtBeds = (TextBox)gvr.FindControl("txtBeds");
                        if (ChkSelect.Checked == true)
                        {
                            i = 1;
                            if (txtBeds.Text == string.Empty)
                                j = 1;
                        }
                    }
                    if (i == 0)
                    {
                        lblMsg.Text = "Select atleast one ward for the Branch";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        gvWards.Focus();
                        return;
                    }
                    if (j == 1)
                    {
                        lblMsg.Text = "Required no. of beds for the selected wards";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        gvWards.Focus();
                        return;
                    }
                }
                if (hfbtntext.Value == "Add")
                    AddFacility();
                else if (hfbtntext.Value == "Update")
                    UpdateFacility();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnClear_CLick(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                ClearFields();
                BindFacilities();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        #endregion
    }
}