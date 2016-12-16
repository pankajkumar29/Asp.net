////-----------------------------------------------------------------------
// <copyright file="DL_Reports.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\Pavan Kumar</author>
// <email>pavan.kumar@dhii.in</email>
// <date>27/03/2012</date>
// <summary>no summary</summary>
// <project>LifeSpring<project>
////********************************History********************************
//     Date    Add/Modified by     Method   Summary
//
////-----------------------------------------------------------------------
using System.Data;
using System.Data.Common;
using DhiiLifeSpring.AppInfrastructure;

namespace DhiiLifeSpring.DataLayer
{
    public class DL_Reports : BaseDataAccess
    {
        #region Master Reports
        public DataSet GetRoles()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Role");
        }
        /// <summary>
        /// This Method is used to Get Role Permissions 
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetRolePermissions()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_RolePermissions");
        }
        /// <summary>
        /// This Method is used to Get Branches
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBranches()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Branch");
        }
        /// <summary>
        /// This Method is used to Get Cities
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCities()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_City");
        }
        /// <summary>
        /// This Method is used to Get Colonies
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetColonies()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Colony");
        }
        /// <summary>
        /// This Method is used to Get Consultant Cost 
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetConsultantCost()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_ConsultantCost");
        }
        /// <summary>
        /// This Method is used to Get Coupons
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCoupons()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Coupon");
        }
        /// <summary>
        /// This Method is used to Get Coupons
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBranchWiseCoupons(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Coupon_Select", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Credit Companies
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCreditCompanies()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_CreditCompany");
        }
        /// <summary>
        /// This Method is used to Get Designations
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDesignations()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Designation");
        }
        /// <summary>
        /// This Method is used to Get Employees
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetEmployees()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Employee");
        }
        /// <summary>
        /// This Method is used to Get Literacy 
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetLiteracy()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Literacy");
        }
        /// <summary>
        /// This Method is used to Get Method Of Delivery 
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetMethodOfDelivery()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_MethodOfDelivery");
        }
        /// <summary>
        /// This Method is used to Get Municipal Wards
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetMunicipalWards()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_MunicipalWard");
        }
        /// <summary>
        /// This Method is used to Get Nationalities
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNationalities()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Nationality");
        }
        /// <summary>
        /// This Method is used to Get Notice Board 
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNoticeBoard()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_NoticeBoard");
        }
        /// <summary>
        /// This Method is used to Get Occupations
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetOccupations()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Occupation");
        }
        /// <summary>
        /// This Method is used to Get Package Cost
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetPackageCost(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_PackageCost", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Pay Modes
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetPayModes()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_PayMode");
        }
        /// <summary>
        /// This Method is used to Get Questions
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetQuestions()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Question");
        }
        /// <summary>
        /// This Method is used to Get Religions
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetReligions()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Religion");
        }
        /// <summary>
        /// This Method is used to Get Services
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetServices(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Service", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Service Cost
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetServiceCost(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_ServiceCost", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Service Groups
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetServiceGroups()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_ServiceGroup");
        }
        /// <summary>
        /// This Method is used to Get Service Main Groups
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetServiceMainGroups()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_ServiceMainGroup");
        }
        /// <summary>
        /// This Method is used to Get Streets
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetStreets()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Street");
        }
        /// <summary>
        /// This Method is used to Get Tariffs
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetTariffs()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Tariff");
        }
        /// <summary>
        /// This Method is used to Get Wards
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetWards()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Ward");
        }
        /// <summary>
        /// This Method is used to Get Beds
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBeds()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Bed");
        }
        /// <summary>
        /// This Method is used to Get Sections
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetSections()
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Mas_Section");
        }
        #endregion

        #region Billing Reports
        /// <summary>
        /// This Method is used to Get Daily Cash Scroll
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDailyCashScroll</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDailyCashScroll(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_DailyCashScroll", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Daily Cash Scroll Service Wise Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDailyCashScrollServiceWiseSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDailyCashScrollServiceWiseSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_DailyCashScrollServiceWiseSummary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Daily Cash Scroll Cash Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDailyCashScrollCashSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDailyCashScrollCashSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_DailyCashScrollCashSummary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Daily Cash Scroll Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDailyCashScrollSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDailyCashScrollSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_DailyCashScroll_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Bills List
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBillsList</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBillsList(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_BillsList", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Bills List Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBillsListSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBillsListSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_BillsList_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Counter Transactions
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCounterTransactions</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCounterTransactions(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_CounterTransactions", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Counter Transactions By TranId
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCounterTransactionsByTranId</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCounterTransactionsByTranId(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_CounterTransactions_By_TranId", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Bill Details By TranId
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBillDetailsByTranId</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBillDetailsByTranId(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_BillDetails_By_TranId", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Daily MIS
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDailyBusinessMIS</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDailyBusinessMIS(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_DailyBusinessMIS", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Daily Mis Discounts
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDailyBusinessMISDiscounts</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDailyBusinessMISDiscounts(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_DailyBusinessMISDiscounts", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Doctor Revenue
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDoctorRevenue</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDoctorRevenue(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_DoctorRevenue", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Doctor Revenue
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDoctorRevenue</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDoctorRevenueSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_DoctorRevenue_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Bed Occupancy
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBedOccupancy</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBedOccupancy(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_HospitalBedOccupancy", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Bed Occupancy Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBedOccupancySummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBedOccupancySummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_HospitalBedOccupancy_Summary", dbParamas);
        }
        /// <summary>
        /// This method is used to List of Doctors for Doctor Payments Report
        /// </summary>
        /// <param name="dbParams">pass this parameter for GetDoctorsForDoctorPayments</param>
        /// <returns>This method returns Integer</returns>
        public DataSet GetDoctorsForDoctorPayments(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_DoctorPayments_Select_Dotors", dbParams);
        }
        /// <summary>
        /// This method is used to Insert the Parameters of Doctor Payment in Reports_DoctorPayments
        /// </summary>
        /// <param name="dbParams">pass this parameter for InsertDoctorPayments</param>
        /// <returns>This method returns Integer</returns>
        public int InsertDoctorPayments(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_Report_Billing_DoctorPayments_Insert", dbParams);
        }
        /// <summary>
        /// This method is used to Get Doctor Payments Report
        /// </summary>
        /// <param name="dbParams">pass this parameter for GetDoctorPayments</param>
        /// <returns>This method returns Dataset</returns>
        public DataSet GetDoctorPayments(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Billing_DoctorPayments", dbParams);
        }
        #endregion

        #region Control Reports
        /// <summary>
        /// This Method is used to Get Admission Delay
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetAdmissionDelay</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetAdmissionDelay(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_AdmissionDelay", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Admission Delay Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetAdmissionDelaySummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetAdmissionDelaySummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_AdmissionDelay_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get IP Billing Delay
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetIPBillingDelay</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetIPBillingDelay(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_BillingDelay", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get IP Billing Delay Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetIPBillingDelaySummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetIPBillingDelaySummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_BillingDelay_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Advance Collection Delay 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetAdvanceCollectionDelay</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetAdvanceCollectionDelay(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_AdvanceCollectionDelay", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Advance Collection Delay Summary 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetAdvanceCollectionDelaySummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetAdvanceCollectionDelaySummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_AdvanceCollectionDelay_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Credit Bill Aging 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCreditBillAging</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCreditBillAging(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_CreditBillAging", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Credit Bill Aging Summary 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCreditBillAgingSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCreditBillAgingSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_CreditBillAging_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Ward Upgrade Downgrade
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetWardUpGradeDownGrade</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetWardUpGradeDownGrade(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_WardUpGradeDownGrade", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Ward Upgrade Downgrade Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetWardUpGradeDownGradeSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetWardUpGradeDownGradeSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_WardUpGradeDownGrade_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Bill Record Mapping
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBillRecordMapping</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBillRecordMapping(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_BillRecordMapping", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Collection Deposits
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCollectionDeposits</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCollectionDeposits(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_CollectionDeposits", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Collection Deposits Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCollectionDepositsSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCollectionDepositsSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_CollectionDeposits_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Cash Handover Discrepancy
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCashHandoverDiscrepancy</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCashHandoverDiscrepancy(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Control_CashHandoverDiscrepancy", dbParamas);
        }
        #endregion

        #region Clinical Reports
        /// <summary>
        /// This Method is used to Get Birth Documentation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBirthDocumentation</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBirthDocumentation(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Clinical_BirthDocumentation", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Birth Documentation Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBirthDocumentationSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBirthDocumentationSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Clinical_BirthDocumentationSummary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Clinical Quality
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetClinicalQuality</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetClinicalQuality(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Clinical_ClinicalQuality", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get NICU Admissions
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetNICUAdmissions</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNICUAdmissions(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Clinical_NICUAdmissions", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get details of C Section
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCSection</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCSection(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Clinical_CSection", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Investigations
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetInvestigations</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetInvestigations(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Clinical_Investigations", dbParamas);
        }
        #endregion

        #region Marketing Reports
        /// <summary>
        /// This Method is used to Get ANC Conversion details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetANCConversionDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetANCConversionDetails(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_ANCConversion", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get ANC Conversion details Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetANCConversionDetailsSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetANCConversionDetailsSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_ANCConversionSummary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get ANC Register details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetANCRegisterDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetANCRegisterDetails(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_ANCRegister", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get ANC Register details Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetANCRegisterDetailsSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetANCRegisterDetailsSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_ANCRegisterSummary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get ANC Register details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetANCRegisterDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetANCAppointmentTrackerDetails(DbParameter[] dbParamas)
        {
            //return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_ANCAppointmentTracker", dbParamas);
            return ExecuteStoredProcReturnDataSet("USP_SAMPLE_Report_ANC_AppointmentTracker", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get ANC Register details Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetANCRegisterDetailsSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetANCAppointmentTrackerDetailsSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_ANCAppointmentTrackerSummary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Birth Cohort Tracker Details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBirthCohortTrackerDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBirthCohortTrackerDetails(DbParameter[] dbParamas)
        {
            //return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_BirthCohortTracker", dbParamas); 
            return ExecuteStoredProcReturnDataSet("USP_SAMPLE_Report_BirthCohortTracker", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Birth Cohort Tracker details Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBirthCohortTrackerSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBirthCohortTrackerSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_BirthCohortTrackerSummary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get ANC To Delivery Conversion details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetANCToDeliveryConversion</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetANCToDeliveryConversionDetails(DbParameter[] dbParamas)
        {
            //return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_ANCToDeliveryConversion", dbParamas);
            return ExecuteStoredProcReturnDataSet("USP_SAMPLE_Report_ANC_To_DeliveryConversion", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get ANC To Delivery Conversion details Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetANCToDeliveryConversionDetailsSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetANCToDeliveryConversionDetailsSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_ANCToDeliveryConversionSummary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Referral Performance Details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetReferralPerformanceDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetReferralPerformanceDetails(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_ReferralPerformance", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Market Coverage Details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetMarketCoverageDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetMarketCoverageDetails(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_MarketCoverage", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Market Coverage Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to Get Market Coverage Summary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetMarketCoverageSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_MarketCoverage_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Get Customer Exit Feedback details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCustomerExitFeedbackDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCustomerExitFeedbackDetails(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_CustomerExitFeedBack", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Customer Exit Feedback Details Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCustomerExitFeedbackDetailsSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCustomerExitFeedbackDetailsSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_CustomerExitFeedBack_Summary", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Dr. Performance Details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDrPerformanceDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDrPerformanceDetails(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DrPerformance", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Delivered Cutomer Profile TotalBirths
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileTotalBirths</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileTotalBirths(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_TotalBirths", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileLiveStillBirths
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileLiveStillBirths</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileLiveStillBirths(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_LiveStillBirths", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileTotalCustomers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileTotalCustomers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileTotalCustomers(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_TotalCustomers", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfilePreNatalVisits
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfilePreNatalVisits</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfilePreNatalVisits(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_PreNatalVisits", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileTrimesterofJoining
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileTrimesterofJoining</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileTrimesterofJoining(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_TrimesterofJoining", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileBirthWeight
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileBirthWeight</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileBirthWeight(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_BirthWeight", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileDeliveryFinancing
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileDeliveryFinancing</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileDeliveryFinancing(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_DeliveryFinancing", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileSexofInfant
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileSexofInfant</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileSexofInfant(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_SexofInfant", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileReligion
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileReligion</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileReligion(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_Religion", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileBirthOrder
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileBirthOrder</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileBirthOrder(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_BirthOrder", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileCustomersWhoHad
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileCustomersWhoHad</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileCustomersWhoHad(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_CustomersWhoHad", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileSelectedWards
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileSelectedWards</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileSelectedWards(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_SelectedWards", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileMothersAge
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileMothersAge</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileMothersAge(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_MothersAge", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileReferralMode
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileReferralMode</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileReferralMode(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_ReferralMode", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileSatisfaction
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileSatisfaction</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileSatisfaction(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_Satisfaction", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileEducationForHusband
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileEducationForHusband</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileEducationForHusband(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_EducationForHusband", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileTubectomyWithDelivery
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileTubectomyWithDelivery</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileTubectomyWithDelivery(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_TubectomyWithDelivery", dbParamas);
        }
        /// <summary>
        /// This Method is used to GetDeliveredCutomerProfileOccupation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDeliveredCutomerProfileOccupation</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveredCutomerProfileOccupation(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Marketing_DeliveredCustomerProfile_Occupation", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Max Edd Date
        /// </summary>
        /// <returns></returns>
        public DataSet GetMaxEddDate()
        {
            return ExecuteStoredProcReturnDataSet("USP_Select_MaxEdd");
        }
        #endregion

        #region Operational Reports
        /// <summary>
        /// This Method is used to Get Admission Register details
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetAdmissionRegister</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetAdmissionRegister(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Operational_AdmissionRegister", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Discharge Register details
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetDischargeRegister</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDischargeRegister(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Operational_DischargeRegister", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Delivery Register details
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetDeliveryRegister</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDeliveryRegister(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Operational_DeliveryRegister", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Birth Certificate Register details
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetBirthCertificateRegister</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBirthCertificateRegister(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Operational_BirthCertificateRegister", dbParamas);
        }
        /// <summary>
        /// This method is used to Get Inactive Customers
        /// </summary>
        /// <returns>This method returns dataset</returns>
        public DataSet GetInactiveCustomers(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Operational_InactiveCustomers", dbParamas);
        }
        /// <summary>
        /// This method is used to Get CRM
        /// </summary>
        /// <returns>This method returns dataset</returns>
        public DataSet GetCRM(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Operational_CRM", dbParamas);
        }
        /// <summary>
        /// This method is used to Get CRM
        /// </summary>
        /// <returns>This method returns dataset</returns>
        public DataSet GetCRMSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Operational_CRM_SUMMARY", dbParamas);
        }
        #endregion

        #region Trend Reports
        /// <summary>
        /// This Method is used to Get Occupancy Trend
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetOccupancy</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetOccupancy(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Trend_Occupancy", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Consultation Billing Length Trend
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetConsultationBillingLength</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetConsultationBillingLength(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Trend_ConsultationBillingLength", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Revenue Daywise Trend
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetRevenueDaywise</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetRevenueDaywise(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Trend_RevenueByServiceGroup_Daywise", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Revenue Monthwise Trend
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetRevenueMonthwise</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetRevenueMonthwise(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Trend_RevenueByServiceGroup_Monthwise", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Revenue Daywise Summary Trend
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetRevenueDaywiseSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetRevenueDaywiseSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Trend_RevenueByServiceGroup_Daywise", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Revenue Monthwise Summary Trend
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetRevenueMonthwiseSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetRevenueMonthwiseSummary(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Trend_RevenueByServiceGroup_Monthwise", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get New ANC Daywise Trend
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetNewANCDaywise</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNewANCDaywise(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Trend_NewANC_Daywise", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get New ANC Monthwise Trend
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetNewANCMonthwise</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNewANCMonthwise(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Trend_NewANC_Monthwise", dbParamas);
        }
        /// <summary>
        /// This Method is used to Get Regular Cutomers Percentage Trend
        /// </summary>
        /// <param name="dbParamas">This Parameter belongs to GetRegularCutomersPer</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetRegularCutomersPer(DbParameter[] dbParamas)
        {
            return ExecuteStoredProcReturnDataSet("USP_Report_Trend_RegularCutomersPer", dbParamas);
        }
        #endregion
    }
}
