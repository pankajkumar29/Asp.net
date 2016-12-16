////-----------------------------------------------------------------------
// <copyright file="DL_CashManagement.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\kirankumarj</author>
// <email>kiran.kumarj@dhii.in</email>
// <date>12/03/2012</date>
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
    public class DL_CashManagement : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Get Counter Details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCounter</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCounter(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Get_Counter_Details", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Counter Transaction Insert Or Updated
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to CounterTransactionInsertOrUpdate</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet CounterTransactionInsertOrUpdate(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Counter_Transaction_InsertOrUpdate", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Total Collected Amount By EmpId
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetTotalCollectedAmountByEmpId</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetTotalCollectedAmountByEmpId(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Get_TotalCollectedAmount_ByEmpId", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Cash in Hand
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCashInHand</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCashInHand(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Counter_CashInHand", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Duplicate Bill Details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDuplicateBillDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDuplicateBillDetails(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Get_Bill_Details", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Duplicate Cash Handover
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDuplicateCashHandover</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDuplicateCashHandover(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Counter_HandOver_Select_ForPrint", Dbparameter);
        }
    }
}
