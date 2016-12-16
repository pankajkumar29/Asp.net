////-----------------------------------------------------------------------
// <copyright file="DL_DashBoard.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_DashBoard : BaseDataAccess
    {
        #region Nurse
        /// <summary>
        /// This Method is used to get Nurse Customer Occupancy
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetNurseCustomerOccupancy</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNurseCustomerOccupancy(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_Nurse_CustomerOccupancy", dbParams);
        }
        /// <summary>
        /// This Method is used to get Nurse Billing And Collection
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetNurseBillingCollection</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNurseBillingCollection(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_Nurse_BillingAndCollection", dbParams);
        }
        /// <summary>
        /// This Method is used to get Nurse Hospital Business
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetNurseHospitalBusiness</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNurseHospitalBusiness(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_Nurse_HospitalBusiness", dbParams);
        }
        /// <summary>
        /// This Method is used to get Nurse Deliveries
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetNurseDeliveries</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNurseDeliveries(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_Nurse_Deliveries", dbParams);
        }
        /// <summary>
        /// This Method is used to get Nurse Open Requests
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetNurseOpenRequests</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNurseOpenRequests(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_Nurse_OpenRequests", dbParams);
        }
        #endregion

        #region Business Head
        /// <summary>
        /// This Method is used to get Business Head Customer Occupancy
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBizHeadCustomerOccupancy</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBizHeadCustomerOccupancy(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_BizHead_CustomerOccupancy", dbParams);
        }
        /// <summary>
        /// This Method is used to get Business Head Billing And Collection
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBizHeadBillingCollection</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBizHeadBillingCollection(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_BizHead_BillingAndCollection", dbParams);
        }
        /// <summary>
        /// This Method is used to get Business Head Deliveries
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBizHeadDeliveries</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBizHeadDeliveries(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_BizHead_Deliveries", dbParams);
        }
        /// <summary>
        /// This Method is used to get Business Head Hospital Business
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBizHeadHospitalBusiness</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBizHeadHospitalBusiness(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_BizHead_HospitalBusiness", dbParams);
        }
        /// <summary>
        /// This Method is used to get Business Head New ANC
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBizHeadNewANC</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBizHeadNewANC(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_BizHead_NewANC", dbParams);
        }
        /// <summary>
        /// This Method is used to get Business Head Open Requests
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBizHeadOpenRequests</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBizHeadOpenRequests(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_BizHead_OpenRequests", dbParams);
        }
        #endregion

        #region CXO
        /// <summary>
        /// This Method is used to get CXO Customer Occupancy
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCXOCustomerOccupancy()
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_CXO_CustomerOccupancy");
        }
        /// <summary>
        /// This Method is used to get CXO Billing, Collection and Revenue
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCXOBillingCollectionRevenue()
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_CXO_BillingCollectionRevenue");
        }
        /// <summary>
        /// This Method is used to get CXO New ANC and Deliveries
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCXONewANCDeliveries()
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_CXO_NewANCDeliveries");
        }
        /// <summary>
        /// This Method is used to get CXO Open Requests
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCXOOpenRequests()
        {
            return ExecuteStoredProcReturnDataSet("USP_DashBoard_CXO_OpenRequests");
        }
        #endregion
    }
}
