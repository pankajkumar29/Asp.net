////-----------------------------------------------------------------------
// <copyright file="Mas_ConsultantCost.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\kirankumarj</author>
// <email>kiran.kumarj@dhii.in</email>
// <date>19/03/2012</date>
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
using System.Web.UI.WebControls;
using DhiiLifeSpring.AppInfrastructure;
using DhiiLifeSpring.BusinessEntities;
using DhiiLifeSpring.BusinessLayer;
#endregion

namespace DhiiLifeSpring.Application.Masters
{
    public partial class Mas_ConsultantCost : BasePage
    {
        #region User Defined Methods
        /// <summary>
        /// This Method is used to Bind Branches
        /// </summary>
        private void BindBranch()
        {
            ddlBranch.Items.Clear();
            DataSet dsfacility = new BL_Facility().GetFacilities();
            if (dsfacility.Tables.Count > 0 && dsfacility != null && dsfacility.Tables[0].Rows.Count > 0)
            {
                DataView dvFacility = dsfacility.Tables[0].DefaultView;
                dvFacility.RowFilter = "Status = true  and Corporate is null";
                ddlBranch.DataSource = dvFacility;
                ddlBranch.DataTextField = "FacilityName";
                ddlBranch.DataValueField = "FacilityId";
                ddlBranch.DataBind();
            }
            ddlBranch.Items.Insert(0, "Select");
        }
        /// <summary>
        /// This Method is used to Bind Doctors
        /// </summary>
        private void BindDoctors()
        {
            ddlConsultant.Items.Clear();
            DataSet dsConsultants = new BL_Employee().GetDoctors(Convert.ToInt32(ddlBranch.SelectedValue));
            if (dsConsultants.Tables.Count > 0 && dsConsultants != null && dsConsultants.Tables[0].Rows.Count > 0)
            {
                ddlConsultant.DataSource = dsConsultants;
                ddlConsultant.DataTextField = "DoctorName";
                ddlConsultant.DataValueField = "EmpId";
                ddlConsultant.DataBind();
            }
            ddlConsultant.Items.Insert(0, "Select");
        }
        /// <summary>
        /// This Method is used to Bind OP Cost
        /// </summary>
        private void BindOPCost()
        {
            if (ddlConsultant.SelectedIndex > 0)
            {
                DataSet dsOPCost = new BL_Employee().GetConsultantCost(Convert.ToInt32(ddlConsultant.SelectedValue));
                if (dsOPCost != null && dsOPCost.Tables.Count > 0 && dsOPCost.Tables[0].Rows.Count > 0)
                {
                    gvOpConsultantcost.DataSource = dsOPCost.Tables[0];
                    gvOpConsultantcost.DataBind();
                }
                else
                {
                    gvOpConsultantcost.DataSource = null;
                    gvOpConsultantcost.DataBind();
                }
            }
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
                    Page.Form.Enctype = "multipart/form-data";
                    lblError.Text = string.Empty;
                    if (!Page.IsPostBack)
                    {
                        BindBranch();
                        PanOp.Visible = false;
                        trbutton.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(ex is System.Threading.ThreadAbortException))
                    WriteLogItem(ex.Message, ex.StackTrace, Session["UserID"].ToString());
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    BindDoctors();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Session["UserID"].ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindOPCost();
                PanOp.Visible = true;
                trbutton.Visible = true;
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Session["UserID"].ToString());
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BE_Employee _beEmployee = new BE_Employee();
                DataTable dtDelete = new DataTable("Delete");
                dtDelete.Columns.Add("Id");
                DataTable dt = new DataTable("Tariff");
                DataSet dsDelete = new DataSet();
                dt.Columns.Add("SpecialisationId");
                dt.Columns.Add("ConsultationAmount");
                dt.Columns.Add("DocShare");
                dt.Columns.Add("Validdays");
                DataRow dr;
                int ResultOp = 0;
                if (gvOpConsultantcost.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow in gvOpConsultantcost.Rows)
                    {
                        Label lblSPECIALIZATIONID = (Label)gvrow.FindControl("lblSPECIALIZATIONID");
                        TextBox txtConsultantCost = (TextBox)gvrow.FindControl("txtConsultantCost");
                        TextBox txtConsultantAmount = (TextBox)gvrow.FindControl("txtConsultantAmount");
                        TextBox txtValidDays = (TextBox)gvrow.FindControl("txtValidDays");
                        if (lblSPECIALIZATIONID.Text != string.Empty && txtConsultantCost.Text != string.Empty && txtValidDays.Text != string.Empty)
                        {
                            dr = dt.NewRow();
                            dr["SpecialisationId"] = lblSPECIALIZATIONID.Text;
                            dr["ConsultationAmount"] = txtConsultantCost.Text;
                            dr["DocShare"] = txtConsultantAmount.Text;
                            dr["Validdays"] = txtValidDays.Text;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow Drcost in dt.Rows)
                    {
                        _beEmployee.SpecialisationId = Convert.ToInt32(Drcost["SpecialisationId"]);
                        _beEmployee.EmpId = Convert.ToInt32(ddlConsultant.SelectedValue);
                        _beEmployee.ConsultationAmount = Convert.ToDecimal(Drcost["ConsultationAmount"]);
                        _beEmployee.Validdays = Convert.ToInt32(Drcost["Validdays"]);
                        if (Drcost["DocShare"].ToString() != string.Empty)
                            _beEmployee.DocShare = Convert.ToDecimal(Drcost["DocShare"]);
                        _beEmployee.CreatedBy = Session["UserID"].ToString().ToUpper();
                        ResultOp = new BL_Employee().InsertUpdateConsultantCost(_beEmployee);
                    }
                    if (ResultOp > 0)
                    {
                        lblError.Text = "Consultation fee added successfully";
                        lblError.ForeColor = System.Drawing.Color.Green;
                        gvOpConsultantcost.DataSource = null;
                        gvOpConsultantcost.DataBind();
                        BindOPCost();
                    }
                }
                else
                {
                    lblError.Text = "Enter at least one record";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Session["UserID"].ToString());
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                BindBranch();
                ddlConsultant.Items.Clear();
                gvOpConsultantcost.DataSource = null;
                gvOpConsultantcost.DataBind();
                PanOp.Visible = false;
                trbutton.Visible = false;
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Session["UserID"].ToString());
            }
        }

        #endregion
    }
}