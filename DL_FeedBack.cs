////-----------------------------------------------------------------------
// <copyright file="DL_FeedBack.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\seshukumar</author>
// <email>seshu.kumar@dhii.in</email>
// <date>17/12/2012</date>
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
    public class DL_FeedBack : BaseDataAccess
    {
        #region FeedBack
        /// <summary>
        /// This Method is used to Get Customers 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCustomers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCustomers(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_Customers", dbparameter);
        }
        /// <summary>
        /// This Method is used to Get OP Customers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetOPCustomers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetOPCustomers(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_OPCustomers", dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Admitted Customers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetAdmittedCustomers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetAdmittedCustomers(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_AdmittedCustomers", dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Discharged Customers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDischargedCustomers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDischargedCustomers(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_DischargedCustomers", dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Birth Cohort Customers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBirthCohortCustomers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBirthCohortCustomers(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_BirthCohortCustomers", dbparameter);
        }
        /// <summary>
        /// This Method is used to Get New ANC Customers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetNewANCCustomers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNewANCCustomers(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_NewANCCustomers", dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Complaint Customers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetComplaintCustomers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetComplaintCustomers(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_ComplaintCustomers", dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Customers Without EDD
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCustomersWithoutEDD</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCustomersWithoutEDD(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_CustomersWithoutEDD", dbparameter);
        }
        /// <summary>
        /// This Method is used to Get IP Billing Information
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to Get Ip Billing Information</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetIPBillingInformation(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_IPBillingInformation", dbparameter);
        }
        /// <summary>
        /// This Method is used to Get OP Billing Information
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to Get OP Billing Information</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetOPBillingInformation(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_OPBillingInformation", dbparameter);
        }
        #endregion

        #region FeedBack Details
        /// <summary>
        /// This Method is Used to Get FeedBack CuStomerDetails
        /// </summary>
        /// <param name="dbparameter">This parameter belongs to Get FeedBack CuStomer Details</param>
        /// <returns>This Method returns Dataset</returns>
        public DataSet GetFeedBackCuStomerDetails(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_CustomerDetails_Select", dbparameter);
        }
        /// <summary>
        /// This Method is used to UpdateFeedBackCustomerDetails
        /// </summary>
        /// <param name="dbparameter">This Parameter belongs to UpdateFeedBackCustomerDetails</param>
        /// <returns>This Method returns int</returns>
        public int UpdateFeedBackCustomerDetails(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnInteger("USP_FeedBack_CustomerDetails_Update", dbparameter);
        }
        /// <summary>
        /// This Method is used to GetFeedBack Customer EDDDetails
        /// </summary>
        /// <param name="dbparameter">This Parameter belongs to GetFeedBack Customer EDDDetails </param>
        /// <returns>This Method returns Dataset</returns>
        public DataSet GetFeedBackCustomerEDDDetails(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_CustomerEDD_Select", dbparameter);
        }
        /// <summary>
        /// This Method is used to GetCustomerFirstVisitDate
        /// </summary>
        /// <param name="dbparameter">This Parameter belong to GetCustomerFirstVisitDate</param>
        /// <returns>This Method returns dataset</returns>
        public DataSet GetCustomerFirstVisitDate(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_CustomerFirstVisitDate", dbparameter);
        }
        /// <summary>
        /// This Method is used to InsertUpdate FeedBack Customer EDD
        /// </summary>
        /// <param name="dbparameter">This parameter belongs to  Insert Update FeedBack Customer EDD</param>
        /// <returns>This Method returns int</returns>
        public int InsertUpdateFeedBackCustomerEDD(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnInteger("USP_FeedBack_CustomerEDD_InsertUpdate", dbparameter);
        }
        /// <summary>
        /// This Method is used to Insert UPdate FeedBack Details
        /// </summary>
        /// <param name="dbparameter">This parameter belongs to  Insert UPdate FeedBack Details</param>
        /// <returns>This Method returns int</returns>
        public DataSet InsertUPdateFeedBackDetails(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_FeedBackDetails_InsertUpdate", dbparameter);
        }
        /// <summary>
        /// This Method is used to Get FeedBack Details 
        /// </summary>
        /// <param name="dbparameter">This parameter belongs to  Get FeedBack Details</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetFeedBackDetails(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_FeedBackDetails_Select", dbparameter);
        }
        /// <summary>
        /// This Method is used to InsertUpdate feedback Customer Profile
        /// </summary>
        /// <param name="dbparameter">This Parameter belongs to insert Update feedback Customer Profile</param>
        /// <returns>This method returns int</returns>
        public int InsertUpdateFeedBackCustomerProfile(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_FeedBack_BirthCertificate_InsertUpdate", dbparameter);
        }
        /// <summary>
        /// This Method is used to GetFeedBackCustomerProfile
        /// </summary>
        /// <param name="dbparameter">This parameter belongs to GetFeedBackCustomerProfile</param>
        /// <returns>This Method returns Dataset</returns>
        public DataSet GetFeedBackCustomerProfile(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_FeedBack_CustomerProfile_Select", dbparameter);
        }
        /// <summary>
        /// This Method is used to Insert Billing Checklist
        /// </summary>
        /// <param name="dbparameter">This Parameter belongs to Insert Billing CheckList</param>
        /// <returns>This method return integer</returns>
        public int InsertBillingChecklist(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnInteger("USP_FeedBack_BillingCheckList_InsertUpdate", dbparameter);
        }
        /// <summary>
        /// This Method is used to GetBilling CheckList
        /// </summary>
        /// <param name="dbparameter">This Parameter belongs to GetBilling CheckList</param>
        /// <returns>This Method returns Dataset</returns>
        public DataSet GetBillingCheckList(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_BillingChecklist_Select", dbparameter);
        }
        /// <summary>
        /// This Method is used to GetFeedBack RedFlagList
        /// </summary>
        /// <param name="dbparameter">This Parameter belongs to GetFeedBack RedFlagList</param>
        /// <returns>This Method returns Dataset</returns>
        public DataSet GetFeedBackRedFlagList(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_FeedBack_RedFlagList_Select", dbparameter);
        }
        /// <summary>
        /// This Method is used to InsertUpdate FeedBack RedFlagList
        /// </summary>
        /// <param name="dbparameter">This Parameter belongs to Insert Update FeedBackRedFlagList</param>
        /// <returns>This method return integer</returns>
        public int InsertUpdateFeedBackRedFlagList(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnInteger("USP_FeedBack_RedFlagList_InsertUpdate", dbparameter);
        }
        /// <summary>
        /// This Method is used to GetFeedBack Irregular
        /// </summary>
        /// <param name="dbparameter">This Parameter belongs to GetFeedBack Irregular</param>
        /// <returns>This Method returns Dataset</returns>
        public DataSet GetFeedBackIrregular(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Feedback_Irregular", dbparameter);
        }
        #endregion
    }
}
