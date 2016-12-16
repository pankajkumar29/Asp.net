////-----------------------------------------------------------------------
// <copyright file="DL_OP_Registration.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\kirankumarj</author>
// <email>kiran.kumarj@dhii.in</email>
// <date>14/03/2012</date>
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
    public class DL_OP_Registration : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update OP Patient 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to OPPatientInsertUpdate</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet OPPatientInsertUpdate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_OP_Registration_InsertUpdate", dbParams);
        }
        /// <summary>
        /// This Method is used to Insert/Update OP Patient Service 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to OPPatientServiceInsertUpdate</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet OPPatientServiceInsertUpdate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_OP_Service_Registration_InsertUpdate", dbParams);
        }
        /// <summary>
        /// This Method is used to Select OP CustomerIndex
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectOPCustomerIndex</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectOPCustomerIndex(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_OP_CustomerIndex_Select", dbParams);
        }
        /// <summary>
        /// This Method is used to Insert/Update OP Billing 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to OPBillingInsertUpdate</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet OPBillingInsertUpdate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_OP_Billing_InsertUpdate", dbParams);
        }
        /// <summary>
        /// This Method is used to Insert/Update OP Service Posting 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to OPServicePostingInsertUpdate</param>
        /// <returns>This Method Returns Integer</returns>
        public int OPServicePostingInsertUpdate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_OP_ServiceDetails_InsertOrUpdate", dbParams);
        }
        /// <summary>
        /// This Method is used to Get Last Transaction On Counter
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetLastTransactionOnCounter</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetLastTransactionOnCounter(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_GET_Last_Transaction_OnCounter", dbParams);
        }
        /// <summary>
        /// This Method is used to Get Last Transaction By Customer
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetLastTransactionByCustomer</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetLastTransactionByCustomer(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_GET_Last_Transaction_ByCusyomer", dbParams);
        }
        /// <summary>
        /// This Method is used to Get all bill Nmbers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetallbillNmbers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetallbillNmbers(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Get_Op_BillNO", dbParams);
        }
        /// <summary>
        /// This Method is used to Get bill Details by BillNo
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetbillDetailsbyBillNo</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetbillDetailsbyBillNo(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Get_Op_Patient_Service", dbParams);
        }
        /// <summary>
        /// This Method is used to OP Service Cancellation Approve Or reject
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to OPServiceCancellationApproveOrreject</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet OPServiceCancellationApproveOrreject(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Service_Cancellation_ApproveOrReject", dbParams);
        }
        /// <summary>
        /// This Method is used to OP Consultation Cancellation Approve Or reject
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to OPConsultationCancellationApproveOrreject</param>
        /// <returns>This Method Returns Integer</returns>
        public int OPConsultationCancellationApproveOrreject(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_Consultation_Cancellation_ApproveOrReject", dbParams);
        }
        /// <summary>
        /// This Method is used to OP Consultation Canellation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to OPConsultationCanellation</param>
        /// <returns>This Method Returns Integer</returns>
        public int OPConsultationCanellation(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_Consultation_Cancellation", dbParams);
        }
        /// <summary>
        /// This Method is used to OP Service Cancellation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to OPServiceCancellation</param>
        /// <returns>This Method Returns Integer</returns>
        public int OPServiceCancellation(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_Service_Cancellation", dbParams);
        }
        /// <summary>
        /// This Method is used to Insert Or Update Procepect Data
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertOrUpdateProcepectData</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet InsertOrUpdateProcepectData(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_ProcepectData_InsertOrUpdate", dbParams);
        }
        /// <summary>
        /// This Method is used to Get Edd Patient
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetEddPatient</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetEddPatient(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Select_Edd_Patient", dbParams);
        }
        /// <summary>
        /// This Method is used to Insert Or Update Edd Date
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertOrUpdateEddDate</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet InsertOrUpdateEddDate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_EDD_InsertORUpdate", dbParams);
        }
        /// <summary>
        /// This Method is used to Insert Or Update Next visit Date
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to UpdateNextVisitDate</param>
        /// <returns>This Method Returns Dataset</returns>
        public int UpdateNextVisitDate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_NextVisit_Update", dbParams);
        }
        /// <summary>
        /// This Method is used to Get OPd Bill Print
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to Get OPd BillPrint</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetOPdBillPrint(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_GET_OP_BillDetails_For_Print", dbParams);
        }
        /// <summary>
        /// This Method is used to Get OPd Bill Service Print
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetOPdBillServicePrint</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetOPdBillServicePrint(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_GET_OP_BillDetails_Service_For_Print", dbParams);
        }
        /// <summary>
        /// This Method is used to Get Edd Date By MRno
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetEddDateByMRno</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetEddDateByMRno(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Select_Edd_Patient_ByMRNO", dbParams);
        }
        /// <summary>
        /// This Method is used to Get Bill Cancellation Status
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBillCancellationStatus</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBillCancellationStatus(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_OP_Bill_Cancelltion_Status", dbParams);
        }
        /// <summary>
        /// This Method is used to Select OP Customer Index By MRNO
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectOPCustomerIndexByMRNO</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectOPCustomerIndexByMRNO(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_OP_CustomerIndex_Select_ByMRNO", dbParams);
        }
        /// <summary>
        /// This Method is used to Select OP Customer Index By Customer Name
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectOPCustomerIndexByCustomerName</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectOPCustomerIndexByCustomerName(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_OP_CustomerIndex_Select_ByCustomerName", dbParams);
        }
        /// <summary>
        /// This Method is used to Select OP Customer Index By Mobile
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectOPCustomerIndexByMobile</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectOPCustomerIndexByMobile(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_OP_CustomerIndex_Select_ByMobile", dbParams);
        }
    }
}
