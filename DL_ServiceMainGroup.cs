////-----------------------------------------------------------------------
// <copyright file="DL_ServiceMainGroup.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>24/04/2012</date>
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
    public class DL_ServiceMainGroup : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Service Main Group
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateServiceMainGroup</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateServiceMainGroup(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_ServiceMainGroup_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Service Main Group
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetServiceMainGroup</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetServiceMainGroup(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_ServiceMainGroup_Select", dbParameter);
        }
    }
}
