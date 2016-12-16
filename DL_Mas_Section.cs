////-----------------------------------------------------------------------
// <copyright file="DL_Mas_Section.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\Seshukumar</author>
// <email>seshu.kumar@dhii.in</email>
// <date>05/02/2012</date>
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
    public class DL_Mas_Section : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Section
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateSection</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateSection(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Section_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Section
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetSection</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetSection(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Section_Select", dbParameter);
        }
    }
}
