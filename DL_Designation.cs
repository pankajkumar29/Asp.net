////-----------------------------------------------------------------------
// <copyright file="DL_Designation.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_Designation : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Designation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateDesignation</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateDesignation(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Designation_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Designation
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDesignation</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDesignation(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Designation_Select", dbParameter);
        }
    }
}
