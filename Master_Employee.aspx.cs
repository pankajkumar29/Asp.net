////-----------------------------------------------------------------------
// <copyright file="Master_Employee.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\kirankumarj</author>
// <email>kiran.kumarj@dhii.in</email>
// <date>16/02/2012</date>
// <summary>no summary</summary>
// <project>CHiMS<project>
////********************************History********************************
//     Date    Add/Modified by     Method   Summary
//
////-----------------------------------------------------------------------

namespace DhiiLifeSpring.Application.Masters
{
    #region Namespaces
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DhiiLifeSpring.AppInfrastructure;
    using DhiiLifeSpring.BusinessEntities;
    using DhiiLifeSpring.BusinessLayer;
    using DhiiLifeSpring.Security;
    using System.IO;
    using System.Configuration;
    #endregion

    public partial class Master_Employee : BasePage
    {
        #region Variable Declaration


        Int32 Clinic_ID = 0;
        #endregion

        #region User Defined Methods

        /// <summary>
        /// This Method is used to Bind Employees
        /// </summary>
        private void BindEmployees()
        {
            DataSet dsEmp = new DataSet();
            if (ddlSearchFacility.SelectedIndex > 0)
            {
                dsEmp = new BL_Employee().GetEmployeesClinicWise(int.Parse(ddlSearchFacility.SelectedValue));
            }
            else
            {
                dsEmp = new BL_Employee().GetEmployeesClinicWise(null);
            }

            if (dsEmp != null && dsEmp.Tables.Count > 0 && dsEmp.Tables[0].Rows.Count > 0)
            {
                DataView dvEmp = dsEmp.Tables[0].DefaultView;
                //dvEmp.RowFilter = "EmployeeName like '" + txtSearch.Text.Trim().ToUpper() + "%'";
                string str = string.Empty;
                if (txtSearch.Text.Trim().ToUpper() != "")
                {
                    str += "EmployeeName like '" + txtSearch.Text.Trim().ToUpper() + "%'";
                }
                if (ddlStatus.SelectedIndex > 0)
                {
                    if (str != "")
                        str += " And ";
                    str += "Status = '" + ddlStatus.SelectedValue + "'";
                }
                if (str != string.Empty)
                {
                    dvEmp.RowFilter = str;
                }

                if (dvEmp.Count > 0)
                {
                    grEmployee.DataSource = dvEmp;
                    grEmployee.DataBind();
                    lblTotalRecords.Visible = true;
                    lblTotalRecords.Text = "Total Records :" + dvEmp.Count.ToString().ToUpper();
                }
                else
                {
                    grEmployee.DataSource = null;
                    grEmployee.DataBind();
                    lblTotalRecords.Visible = true;
                    lblTotalRecords.Text = "Total Records :" + dvEmp.Count.ToString().ToUpper();
                }
            }
            else
            {
                grEmployee.DataSource = null;
                grEmployee.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records :" + dsEmp.Tables[0].Rows.Count.ToString().ToUpper();
            }
        }

        /// <summary>
        /// This Method is used to Bind Designation
        /// </summary>
        /// <param name="DesignationId">BindDesignation</param>
        private void BindDesignation(Int32? DesignationId)
        {
            ddlDesignation.Items.Clear();
            DataSet dsdesignations = new BL_Designation().GetDesignation(null, null, null);
            DataView dvdesignations = new DataView();
            if (dsdesignations.Tables.Count > 0 && dsdesignations != null && dsdesignations.Tables[0].Rows.Count > 0)
            {
                dvdesignations = dsdesignations.Tables[0].DefaultView;
                if ((hfEmp_ID.Value != string.Empty) && (DesignationId != null && DesignationId != 0))
                    dvdesignations.RowFilter = "Status= true or DesignationID =" + Convert.ToInt32(DesignationId);
                else
                    dvdesignations.RowFilter = "Status= true ";
                ddlDesignation.DataSource = dvdesignations;
                ddlDesignation.DataTextField = "Designation";
                ddlDesignation.DataValueField = "DesignationID";
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, "Select");
            }
            else
            {
                ddlDesignation.Items.Insert(0, "Select");
            }
        }

        /// <summary>
        /// This Method is used to Bind Facility
        /// </summary>
        private void BindFacility()
        {
            ChkBranch.Items.Clear();
            DataSet dsfacility = new BL_Facility().GetFacilities();
            DataView dvFacility = new DataView();
            if (dsfacility.Tables.Count > 0 && dsfacility != null && dsfacility.Tables[0].Rows.Count > 0)
            {
                dvFacility = dsfacility.Tables[0].DefaultView;
                dvFacility.RowFilter = "Status= true";
                if (dvFacility.Count > 0)
                {
                    ChkBranch.DataSource = dvFacility;
                    ChkBranch.DataTextField = "FacilityName";
                    ChkBranch.DataValueField = "FacilityId";
                    ChkBranch.DataBind();
                    // ChkBranch.SelectedIndex = 0;

                }

            }
        }

        /// <summary>
        /// This Method is used to Bind Search Facility
        /// </summary>
        private void BindSearchFacility()
        {
            ddlSearchFacility.Items.Clear();
            DataSet dsfacility = new BL_Facility().GetFacilities();
            DataView dvFacility = new DataView();
            if (dsfacility.Tables.Count > 0 && dsfacility != null && dsfacility.Tables[0].Rows.Count > 0)
            {
                dvFacility = dsfacility.Tables[0].DefaultView;

                dvFacility.RowFilter = "Status= true ";
                ddlSearchFacility.DataSource = dvFacility;
                ddlSearchFacility.DataTextField = "FacilityName";
                ddlSearchFacility.DataValueField = "FacilityId";
                ddlSearchFacility.DataBind();
                ddlSearchFacility.Items.Insert(0, "Select");
            }
            else
            {
                ddlSearchFacility.Items.Insert(0, "Select");
            }
        }

        /// <summary>
        /// This Method is used to Bind Salutation
        /// </summary>
        /// <param name="Salutation">This Parameter Belongs to BindSalutation</param>
        private void BindSalutation(string Salutation)
        {

            ddlSalutation.Items.Clear();

            ddlSalutation.Items.Add(new ListItem("Mr", "Mr"));
            ddlSalutation.Items.Add(new ListItem("Miss", "Miss"));
            ddlSalutation.Items.Add(new ListItem("Mrs", "Mrs"));
            ddlSalutation.Items.Add(new ListItem("Dr", "Dr"));
            ddlSalutation.Items.Add(new ListItem("Master", "Master"));
        }

        /// <summary>
        /// This Method is used to Bind Gender
        /// </summary>
        /// <param name="Gender">This Parameter Belongs to BindGender</param>
        private void BindGender(string Gender)
        {
            ddlGender.Items.Clear();
            ddlGender.Items.Add(new ListItem("SELECT", "SELECT"));
            ddlGender.Items.Add(new ListItem("MALE", "MALE"));
            ddlGender.Items.Add(new ListItem("FEMALE", "FEMALE"));
            ddlGender.Items.Add(new ListItem("OTHER", "OTHER"));
        }

        /// <summary>
        /// This Method is used to Bind Roles
        /// </summary>
        /// <param name="RoleId">This Parameter Belongs to BindRoles</param>
        private void BindRoles(Int32? RoleId)
        {
            ddlRole.Items.Clear();
            DataSet dsRole = new DataSet();
            dsRole = new BL_Role().GetRoles(null, null, null);
            DataView dvdsRole = new DataView();
            if (dsRole.Tables.Count > 0 && dsRole != null && dsRole.Tables[0].Rows.Count > 0)
            {
                dvdsRole = dsRole.Tables[0].DefaultView;

                if ((hfEmp_ID.Value != string.Empty) && (RoleId != null && RoleId != 0))
                    dvdsRole.RowFilter = "Status = true and RoleId <> 1 or RoleId =" + Convert.ToInt32(RoleId);
                else
                    dvdsRole.RowFilter = "Status = true";
                ddlRole.DataSource = dvdsRole;
                ddlRole.DataTextField = "RoleName";
                ddlRole.DataValueField = "RoleId";
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, "Select");
            }
            else
            {
                ddlRole.Items.Insert(0, "Select");
            }
        }

        /// <summary>
        /// This Method is used to Clear Form
        /// </summary>
        private void ClearForm()
        {
            hfEmp_ID.Value = string.Empty;
            rbtnStatus.SelectedIndex = 0;
            chkLogin.Enabled = true;
            chkLogin.Checked = false;
            txtFirstName.Text = string.Empty;
            BindFacility();
            BindGender(null);
            BindDesignation(null);
            BindRoles(null);
            txtUsername.Text = string.Empty;
            txtUsername.Enabled = false;
            txtPassword.Text = string.Empty;
            txtPassword.Enabled = false;
            txtReTypePassword.Text = string.Empty;
            txtReTypePassword.Enabled = false;
            ddlRole.SelectedIndex = 0;
            ddlRole.Enabled = false;
            txtReason.Text = "";
            btnAdd.Text = "Add";
            hbtntext.Value = "Add";
            btnResetPswd.Visible = false;

            ddlSalutation.SelectedIndex = 0;


            txtMobileNo.Text = string.Empty;

        }

        /// <summary>
        /// This Method is used to Add Employee
        /// </summary>
        private void AddEmployee()
        {
            int i = 0;
            foreach (ListItem li in ChkBranch.Items)
            {

                if (li.Selected)
                {
                    i = 1;
                }


            }
            if (i == 0)
            {
                lblMsg.Text = "Select at least one branch";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            BE_Employee _beEmployee = new BE_Employee();
            // _beEmployee.ClinicId = Convert.ToInt32(ddlBranch.SelectedValue);
            _beEmployee.EmpId = 0;
            _beEmployee.Salutation = Convert.ToString(ddlSalutation.SelectedValue);
            _beEmployee.FirstName = txtFirstName.Text.Trim().ToUpper();

            if (ddlGender.SelectedIndex > 0)
                _beEmployee.Gender = Convert.ToString(ddlGender.SelectedValue);
            if (ddlDesignation.SelectedIndex > 0)
                _beEmployee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
            if (ddlRole.SelectedIndex > 0)
                _beEmployee.RoleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            if (txtUsername.Enabled == true)
            {
                _beEmployee.UserName = txtUsername.Text.Trim();
                _beEmployee.Password = Cryptography.Encrypt(txtPassword.Text.Trim());
            }
            else
            {
                _beEmployee.UserName = "";
                _beEmployee.Password = "";
            }
            if (rbtnStatus.SelectedValue == "Active")
                _beEmployee.Status = true;
            else
                _beEmployee.Status = false;
            _beEmployee.Reason = txtReason.Text.Trim().ToUpper();
            _beEmployee.CreatedBy = Convert.ToString(Session["UserID"]);
            _beEmployee.CreatedBy = Convert.ToString(Session["UserId"]);
            _beEmployee.Mobile = txtMobileNo.Text;


            DataSet dsResult = new BL_Employee().InsertUpdateEmployee(_beEmployee);
            if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "SUCCESS")
                {
                    new BL_Employee().EmployeeFacilityDelete(Convert.ToInt32(dsResult.Tables[0].Rows[0]["EmpId"].ToString()));
                    foreach (ListItem li in ChkBranch.Items)
                    {
                        if (li.Selected)
                        {
                            new BL_Employee().EmployeeFacilityInsertOrUpdate(0, Convert.ToInt32(li.Value), Convert.ToInt32(dsResult.Tables[0].Rows[0]["EmpId"].ToString()), Convert.ToString(Session["UserId"]));
                        }
                    }
                    lblMsg.Text = "Employee details added successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindEmployees();
                    ClearForm();
                    ClearControls();
                }
                else
                {
                    lblMsg.Text = dsResult.Tables[0].Rows[0]["Msg"].ToString().ToUpper();
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    if (lblMsg.Text == "Employee name already exists")
                    {
                        txtFirstName.Focus();
                    }
                    else
                    {
                        txtUsername.Focus();
                    }
                }
            }
            else
            {
                lblMsg.Text = "Failed to add employee details";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// This Method is used to Update Employee
        /// </summary>
        private void UpdateEmployee()
        {
            int i = 0;
            foreach (ListItem li in ChkBranch.Items)
            {

                if (li.Selected)
                {
                    i = 1;
                }

            }
            if (i == 0)
            {
                lblMsg.Text = "Select at least one branch";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            BE_Employee _beEmployee = new BE_Employee();
            // _beEmployee.ClinicId = Convert.ToInt32(ddlBranch.SelectedValue);
            _beEmployee.EmpId = Convert.ToInt32(hfEmp_ID.Value.ToString());
            _beEmployee.Salutation = Convert.ToString(ddlSalutation.SelectedValue);
            _beEmployee.FirstName = txtFirstName.Text.Trim().ToUpper();

            if (ddlGender.SelectedIndex > 0)
                _beEmployee.Gender = Convert.ToString(ddlGender.SelectedValue);

            _beEmployee.Mobile = txtMobileNo.Text;
            if (ddlDesignation.SelectedIndex > 0)
                _beEmployee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
            if (ddlRole.SelectedIndex > 0)
                _beEmployee.RoleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            if (rbtnStatus.SelectedValue == "Active")
                _beEmployee.Status = true;
            else
                _beEmployee.Status = false;
            if (txtUsername.Enabled == true)
            {
                _beEmployee.UserName = txtUsername.Text.Trim();
                _beEmployee.Password = Cryptography.Encrypt(txtPassword.Text.Trim());
            }
            else
            {
                _beEmployee.UserName = "";
                _beEmployee.Password = "";
            }
            _beEmployee.Reason = txtReason.Text.Trim().ToUpper();
            _beEmployee.CreatedBy = Convert.ToString(Session["UserID"]);


            DataSet dsResult = new BL_Employee().InsertUpdateEmployee(_beEmployee);
            if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "SUCCESS")
                {
                    new BL_Employee().EmployeeFacilityDelete(Convert.ToInt32(dsResult.Tables[0].Rows[0]["EmpId"].ToString()));
                    foreach (ListItem li in ChkBranch.Items)
                    {
                        if (li.Selected)
                        {
                            new BL_Employee().EmployeeFacilityInsertOrUpdate(0, Convert.ToInt32(li.Value), Convert.ToInt32(dsResult.Tables[0].Rows[0]["EmpId"].ToString()), Convert.ToString(Session["UserId"]));
                        }
                    }
                    lblMsg.Text = "Employee details updated successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    ClearControls();
                    ClearForm();
                    // ddlBranch.Focus();
                }
                else
                {
                    lblMsg.Text = dsResult.Tables[0].Rows[0]["Msg"].ToString().ToUpper();
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    if (lblMsg.Text == "Employee name already exists")
                    {
                        txtFirstName.Focus();
                    }
                    else
                    {
                        txtUsername.Focus();
                    }
                }
            }
            else
            {
                lblMsg.Text = "Failed to update employee details";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                //ddlBranch.Focus();
            }
            BindEmployees();
            //btnAdd.Text = "Add";
            //btnAdd.ToolTip = "Add";
        }

        /// <summary>
        /// This Method is used to  Clear Controls
        /// </summary>
        private void ClearControls()
        {
            BindFacility();

            btnAdd.Text = "Add";
            btnAdd.ToolTip = "Add";
        }

        #endregion

        #region Page Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || (Session["UserID"] != null && Convert.ToString(Session["UserID"]) == string.Empty))
                Response.Redirect(ConfigurationManager.AppSettings["LoginUrl"]);


            try
            {
                Clinic_ID = Convert.ToInt32(Session["FacilityId"]);
                lblMsg.Text = string.Empty;

                if (!IsPostBack)
                {
                    // Session["FacilityId"] = "0";
                    txtReason.Attributes.Add("onkeyup", "return controlEnter('" + btnAdd.ClientID + "', event)");
                    ddlRole.Attributes.Add("onkeyup", "return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReTypePassword.Attributes.Add("onkeyup", "return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtCurDate.Value = DateTime.Now.ToString("dd-MM-yyyy");
                    ViewState["SortDirection"] = "ASC";
                    ClearForm();
                    BindFacility();
                    BindSearchFacility();
                    BindEmployees();
                    BindSalutation(null);


                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void rbtnStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedValue == "Active")
                {
                    txtReason.Enabled = false;
                }
                else
                {
                    txtReason.Enabled = true;
                    if (hfEmp_ID.Value != string.Empty)
                    {
                        txtReason.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void chkLogin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkLogin.Focus();
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtReTypePassword.Text = string.Empty;
                ddlRole.SelectedIndex = 0;
                if (chkLogin.Checked == true)
                {
                    txtUsername.Enabled = true;
                    txtPassword.Enabled = true;
                    txtReTypePassword.Enabled = true;
                    ddlRole.Enabled = true;
                }
                else
                {
                    txtUsername.Enabled = false;
                    txtPassword.Enabled = false;
                    txtReTypePassword.Enabled = false;
                    ddlRole.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (hbtntext.Value == "Add")
                {
                    AddEmployee();
                }
                else if (hbtntext.Value == "Update")
                {
                    UpdateEmployee();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnResetPswd_Click(object sender, EventArgs e)
        {
            try
            {
                string UserName = txtUsername.Text.Trim();
                string Password = Cryptography.Encrypt("life321");
                new BL_Employee().ChangePassword(UserName, Password);
                lblResetPassword.Text = "Password has been reseted to life321";
                modlResetPassword.Show();
                btnOk.Focus();
                StringBuilder strBuilder = new StringBuilder();
                //ScriptManager.RegisterStartupScript(btnResetPswd, btnResetPswd.GetType(), "err_msg", "alert('Password has been rested to dhii1234');", true);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                BindEmployees();
                ClearForm();
                ClearControls();

            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindEmployees();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));

            }
        }

        protected void grEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    BindFacility();
                    //ClearForm();
                    ClearControls();
                    int EmployeeId = Convert.ToInt32(e.CommandArgument);
                    DataSet dsEmp = new BL_Employee().GetEmployeesClinicWise(null); //Convert.ToInt32(Session["FacilityId"].ToString())
                    DataView dvEmpByEmpid = dsEmp.Tables[0].DefaultView;
                    dvEmpByEmpid.RowFilter = "EmpID = '" + EmployeeId + " ' ";
                    if (dvEmpByEmpid.Count > 0)
                    {
                        hfEmp_ID.Value = EmployeeId.ToString().ToUpper();
                        if (Convert.ToBoolean(dvEmpByEmpid[0]["Status"]))
                        {
                            rbtnStatus.SelectedValue = "Active";
                            txtReason.Enabled = false;
                            txtReason.Text = string.Empty;
                        }
                        else
                        {
                            rbtnStatus.SelectedValue = "Inactive";
                            txtReason.Enabled = true;
                            txtReason.Text = dvEmpByEmpid[0]["Reason"].ToString().ToUpper();
                        }
                        string Salutation = string.Empty;
                        int FacilityId, DesignationId, RoleId, EmpId;
                        if (Convert.ToString(dvEmpByEmpid[0]["Salutation"]) != string.Empty)
                            Salutation = Convert.ToString(dvEmpByEmpid[0]["Salutation"]);

                        String Gender = Convert.ToString(dvEmpByEmpid[0]["Gender"]);

                        if (Convert.ToString(dvEmpByEmpid[0]["DesignationID"]) == string.Empty)
                            DesignationId = 0;
                        else
                            DesignationId = Convert.ToInt32(dvEmpByEmpid[0]["DesignationID"]);
                        if (Convert.ToString(dvEmpByEmpid[0]["RoleId"]) == string.Empty)
                            RoleId = 0;
                        else
                            RoleId = Convert.ToInt32(dvEmpByEmpid[0]["RoleId"]);

                        if (Convert.ToString(dvEmpByEmpid[0]["EmpId"]) == string.Empty)
                            EmpId = 0;
                        else
                            EmpId = Convert.ToInt32(dvEmpByEmpid[0]["EmpId"]);


                        //if (Convert.ToString(dvEmpByEmpid[0]["FacilityId"]) == string.Empty || dvEmpByEmpid[0]["FacilityId"].ToString() == "0")
                        //    FacilityId = 0;
                        //else
                        //    FacilityId = Convert.ToInt32(dvEmpByEmpid[0]["FacilityId"]);

                        BindSalutation(Salutation);
                        BindGender(Gender);
                        BindDesignation(DesignationId);
                        BindFacility();
                        BindRoles(RoleId);



                        if (Convert.ToString(dvEmpByEmpid[0]["Salutation"]) == string.Empty || Convert.ToString(dvEmpByEmpid[0]["Salutation"]) == "0")
                            ddlSalutation.SelectedIndex = 0;
                        else
                            ddlSalutation.SelectedValue = Convert.ToString(dvEmpByEmpid[0]["Salutation"]);
                        txtFirstName.Text = Convert.ToString(dvEmpByEmpid[0]["EmpName"]);
                        if (dvEmpByEmpid[0]["gender"].ToString() != string.Empty)
                            ddlGender.SelectedValue = dvEmpByEmpid[0]["gender"].ToString().ToUpper();
                        txtMobileNo.Text = dvEmpByEmpid[0]["Mobile"].ToString().ToUpper();
                        DataSet DeEMpFacility = new BL_Employee().GetEmployeeFacility(null, EmployeeId);
                        if (DeEMpFacility.Tables.Count > 0 && DeEMpFacility != null && DeEMpFacility.Tables[0].Rows.Count > 0)
                        {

                            foreach (ListItem li in ChkBranch.Items)
                            {
                                foreach (DataRow dr in DeEMpFacility.Tables[0].Rows)
                                {
                                    if (li.Value == dr["FacilityId"].ToString())
                                    {
                                        li.Selected = true;
                                    }


                                }
                            }
                        }
                        //if (Convert.ToString(dvEmpByEmpid[0]["FacilityId"]) == string.Empty || dvEmpByEmpid[0]["FacilityId"].ToString() == "0")
                        //    ddlBranch.SelectedIndex = 0;
                        //else
                        //    ddlBranch.SelectedValue = dvEmpByEmpid[0]["FacilityId"].ToString().ToUpper();
                        if (Convert.ToString(dvEmpByEmpid[0]["DesignationID"]) == string.Empty || Convert.ToString(dvEmpByEmpid[0]["DesignationID"]) == "0")
                            ddlDesignation.SelectedIndex = 0;
                        else
                            ddlDesignation.SelectedValue = Convert.ToString(dvEmpByEmpid[0]["DesignationID"]);
                        if (Convert.ToString(dvEmpByEmpid[0]["RoleId"]) == string.Empty || Convert.ToString(dvEmpByEmpid[0]["RoleId"]) == "0")
                            ddlRole.SelectedIndex = 0;
                        else
                        {
                            ddlRole.SelectedValue = dvEmpByEmpid[0]["RoleId"].ToString().ToUpper();
                            ddlRole.Enabled = true;
                        }

                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        lblMsg.Text = string.Empty;
                        txtUsername.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        txtReTypePassword.Text = string.Empty;
                        txtUsername.Enabled = false;
                        txtPassword.Enabled = false;
                        txtReTypePassword.Enabled = false;

                        //ddlRole.SelectedIndex = 0;
                        DataSet dsUsers = new BL_Employee().GetUserByEmpId(EmployeeId);
                        if (dsUsers.Tables[0].Rows.Count > 0)
                        {
                            txtUsername.Text = dsUsers.Tables[0].Rows[0]["UserName"].ToString();
                            ddlRole.SelectedValue = dsUsers.Tables[0].Rows[0]["RoleId"].ToString().ToUpper();
                            chkLogin.Enabled = false;
                            btnResetPswd.Visible = true;

                        }
                        else
                        {
                            txtUsername.Text = string.Empty;
                            ddlRole.SelectedIndex = 0;
                            chkLogin.Enabled = true;
                            btnResetPswd.Visible = false;

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));

            }

        }

        protected void grEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grEmployee.PageIndex = e.NewPageIndex;
                BindEmployees();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));

            }
        }

        protected void grEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), grEmployee);
                    }
                    else
                    {
                        Image sortImage = new Image();
                        sortImage.ImageUrl = "~/Images/UpArrow.png";
                        sortImage.Width = 10;
                        sortImage.Height = 10;
                        sortImage.AlternateText = "Ascending Order";
                        e.Row.Cells[0].Controls.Add(sortImage);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void grEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet dsEmp = new BL_Employee().GetEmployeesClinicWise(Convert.ToInt32(Session["FacilityId"].ToString()));
                DataView dvEmp = dsEmp.Tables[0].DefaultView;
                dvEmp.RowFilter = "EmployeeName like '" + txtSearch.Text.Trim().ToUpper() + "%'";

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
                    dvEmp = new DataView(dvEmp.ToTable(), string.Empty, strSort, DataViewRowState.CurrentRows);
                    grEmployee.DataSource = dvEmp;
                    grEmployee.DataBind();
                    grEmployee.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }

        }

        protected void ddlSearchFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (ddlSearchFacility.SelectedIndex > 0)
                //{
                //    Session["SearchFacilityId"] = ddlSearchFacility.SelectedValue;
                //}
                //else
                //{
                //    Session["SearchFacilityId"] = 0;
                //}
                BindEmployees();

            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion




    }
}