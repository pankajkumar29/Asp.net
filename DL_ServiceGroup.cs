////-----------------------------------------------------------------------
// <copyright file="DL_ServiceGroup.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_ServiceGroup : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Service Group
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateServiceGroup</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateServiceGroup(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_ServiceGroup_InsertUpdate", dbParameter);
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
