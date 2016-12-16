////-----------------------------------------------------------------------
// <copyright file="DL_IP_Admission.cs" company="Dhii Health Tech Pvt. Ltd.">
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

using System.Data;
using System.Data.Common;
using DhiiLifeSpring.AppInfrastructure;

namespace DhiiLifeSpring.DataLayer
{
    public class DL_IP_Admission : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to IP Admission Insert Update
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to IPAdmissionInsertUpdate</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet IPAdmissionInsertUpdate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Admission_InsertUpdate", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Customer Index
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to Select Customer Index</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectCustomerIndex(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_CustomerIndex_Select", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Customers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectCustomers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectCustomers(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Admission_Select", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Customers For Log
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectCustomersForLog()
        {
            return ExecuteStoredProcReturnDataSet("USP_CustomerIndex_Select_ForLog");
        }
        /// <summary>
        /// This Method is used to Get Customer Log
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCustomerLog</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCustomerLog(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Get_Customer_Log", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Customers For Approval
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectCustomersForApproval()
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Admission_Select_ForApproval");
        }
        /// <summary>
        /// This Method is used to Update Customers Approval Details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to UpdateCustomersApprovalDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public int UpdateCustomersApprovalDetails(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_Admission_Approval_Update", dbParams);
        }
        /// <summary>
        /// This Method is used to Get Discharge Patient list
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDischargePatientlist</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDischarePatientlist(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Get_Ip_Discharge_Pateint_List", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Customer Index By MRNO
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectCustomerIndexByMRNO</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectCustomerIndexByMRNO(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_CustomerIndex_Select_ByMRNO", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Customer Index By Customer Name
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectCustomerIndexByCustomerName</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectCustomerIndexByCustomerName(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_CustomerIndex_Select_ByCustomerName", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Customer Index By Mobile
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectCustomerIndexByMobile</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectCustomerIndexByMobile(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_CustomerIndex_Select_ByMobile", dbParams);
        }
    }
}
