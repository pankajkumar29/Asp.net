////-----------------------------------------------------------------------
// <copyright file="DL_Ward.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
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
    public class DL_Ward : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Ward Common
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateWardCommon</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateWardCommon(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_WardCommon_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Ward Common
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetWardCommon</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetWardCommon(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_WardCommon_Select", dbParameter);
        }
        /// <summary>
        /// This Method is used to Insert or Update Ward
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateWard</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet InsertUpdateWard(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Ward_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Ward
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetWard</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetWard(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Ward_Select", dbParameter);
        }
    }
}
