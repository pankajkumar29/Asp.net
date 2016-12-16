////-----------------------------------------------------------------------
// <copyright file="DL_IP_Billing.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>15/03/2012</date>
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
    public class DL_IP_Billing : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update IP Billing  
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to IPBillingInsertUpdate</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet IPBillingInsertUpdate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Billing_InsertUpdate", dbParams);
        }
        /// <summary>
        /// This Method is used to Select IP Billing
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectIPBilling</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectIPBilling(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Billing_Select", dbParams);
        }
        /// <summary>
        /// This Method is used to Select IP Billing Summary
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectIPBillingSummary</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectIPBillingSummary(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Billing_Select_Summary", dbParams);
        }
        /// <summary>
        /// This Method is used to Select IP Bill Services
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectIPBillServices</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectIPBillServices(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Bill_Services_Select", dbParams);
        }
        /// <summary>
        /// This Method is used to Update IP Billing
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to Update IP Billing</param>
        /// <returns>This Method Returns Dataset</returns>
        public int UpdateIPBilling(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_Billing_Update", dbParams);
        }
        /// <summary>
        /// This Method is used to Update IP Bill Services
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to UpdateIPBillServices</param>
        /// <returns>This Method Returns Dataset</returns>
        public int UpdateIPBillServices(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_Bill_Services_Update", dbParams);
        }
        /// <summary>
        /// This Method is used to Select IP Billing For Cancellation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectIPBillingForCancellation</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectIPBillingForCancellation(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Billing_Select_ForCancellation", dbParams);
        }
        /// <summary>
        /// This Method is used to Select IP Bill Services For Cancellation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectIPBillServicesForCancellation</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectIPBillServicesForCancellation(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Bill_Services_Select_ForCancellation", dbParams);
        }
        /// <summary>
        /// This Method is used to Select OP Bill Consultation For Cancellation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectOPBillConsultationForCancellation</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectOPBillConsultationForCancellation(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_OP_Bill_Consultation_Select_ForCancellation", dbParams);
        }
        /// <summary>
        /// This Method is used to IP Discount Request Insert
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to IPDiscountRequestInsert</param>
        /// <returns>This Method Returns Dataset</returns>
        public int IPDiscountRequestInsert(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_DiscountRequest_Insert", dbParams);
        }
        /// <summary>
        /// This Method is used to IP Admin Discount Request Insert
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to IPAdminDiscountRequestInsert</param>
        /// <returns>This Method Returns Dataset</returns>
        public int IPAdminDiscountRequestInsert(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_Admin_DiscountRequest_Insert", dbParams);
        }
        /// <summary>
        /// This Method is used to IP Discount Request Update
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to IPDiscountRequestUpdate</param>
        /// <returns>This Method Returns Dataset</returns>
        public int IPDiscountRequestUpdate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_DiscountRequest_Update", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Discount Requests
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectDiscountRequests</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectDiscountRequests(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_DiscountRequest_Select", dbParams);
        }
        /// <summary>
        /// This Method is used to Insert IP Final Bill 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to IPFinalBillInsert</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet IPFinalBillInsert(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_FinalBill_Insert", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Receipt For Print
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectReceiptForPrint</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectReceiptForPrint(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_BillReceipt_Select_ForPrint", dbParams);
        }
        /// <summary>
        /// This Method is used to Select POS Receipt For Print
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectPOSReceiptForPrint</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectPOSReceiptForPrint(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Bill_Select_ForPrint", dbParams);
        }
        /// <summary>
        /// This Method is used to Select A4 Receipt For Print
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectA4ReceiptForPrint</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectA4ReceiptForPrint(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Bill_Select_ForPrint_A4", dbParams);
        }
        /// <summary>
        /// This Method is used to IP Final Bill Update
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to IPFinalBillUpdate</param>
        /// <returns>This Method Returns Dataset</returns>
        public int IPFinalBillUpdate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_FinalBill_Update", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Final Bill For Cheque Realization
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectFinalBillForChequeRealization()
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_FinalBill_Select_ForChequeRealization");
        }
        /// <summary>
        /// This method is used to update mobile no of IP patient in CustomerIndex table
        /// </summary>
        /// <param name="dbParams">This Parameter belongs to UpdateMobileNo</param>
        /// <returns>This Method Returns Integer</returns>
        public int UpdateMobileNo(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_CustomerIndex_Mobile_Update", dbParams);
        }
        /// <summary>
        /// This method is used to update Admission doctor of IP patient in CustomerIndex table
        /// </summary>
        /// <param name="dbParams">This Parameter belongs to UpdateMobileNo</param>
        /// <returns>This Method Returns Integer</returns>
        public int UpdateAdmissionDoctor(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_Admission_Consultant_Update", dbParams);
        }
    }
}
