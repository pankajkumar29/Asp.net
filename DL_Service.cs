////-----------------------------------------------------------------------
// <copyright file="DL_Service.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\KiranKumarj</author>
// <email>Kiran.KumarJ@dhii.in</email>
// <date>15/02/2012</date>
// <summary>no summary</summary>
// <project>CHiMS<project>
////********************************History********************************
//     Date    Add/Modified by     Method   Summary
//
////-----------------------------------------------------------------------

using System.Data;
using System.Data.Common;
using DhiiLifeSpring.AppInfrastructure;

namespace DhiiLifeSpring.DataLayer
{
    public class DL_Service : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Service
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateService</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateService(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Service_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Service
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetService</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetService(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Service_Select", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Service Group
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetServiceGroup</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetServiceGroup(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_ServiceGroup_Select", dbParameter);
        }
    }
}

