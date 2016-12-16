////-----------------------------------------------------------------------
// <copyright file="DL_Bed.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_Bed : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Bed
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateBed</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateBed(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Bed_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Bed
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBed</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetBed(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Bed_Select", dbParameter);
        }
    }
}
