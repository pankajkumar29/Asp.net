////-----------------------------------------------------------------------
// <copyright file="DL_Occupation.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>21/03/2012</date>
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
    public class DL_Occupation : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Occupation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateOccupation</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateOccupation(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Occupation_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Occupation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetOccupation</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetOccupation(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Occupation_Select", dbParameter);
        }
    }
}
