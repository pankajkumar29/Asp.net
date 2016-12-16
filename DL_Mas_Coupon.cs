////-----------------------------------------------------------------------
// <copyright file="DL_Mas_Coupon.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\Seshukumar</author>
// <email>Seshu.kumar@dhii.in</email>
// <date>10/12/2012</date>
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
    public class DL_Mas_Coupon : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Get All Batch names
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetAllBatch()
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Coupon_Batch_Select");
        }
        /// <summary>
        /// This Method is used to Insert or Update Coupon
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateCoupon</param>
        /// <returns>This Method Returns int</returns>
        public DataSet InsertUpdateCoupon(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Coupon_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get BatchId
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBatchId</param>
        /// <returns>This Method Returns int</returns>
        public DataSet GetBatchId()
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Coupon_BatchIdSelect");
        }
        /// <summary>
        /// This Method is used to Get all Coupons
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCoupon</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCoupon(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Coupon_Select", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Valid Coupons
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetValidCoupons</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetValidCoupons(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Coupon_Select_Valid", dbParameter);
        }
    }
}
