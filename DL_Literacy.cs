////-----------------------------------------------------------------------
// <copyright file="DL_Literacy.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_Literacy : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Literacy
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateLiteracy</param>
        /// <returns>This Method Returns Dataset</returns>
        public int InsertUpdateLiteracy(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Literacy_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Literacy
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetLiteracy</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetLiteracy(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Literacy_Select", dbParameter);
        }
    }
}
