////-----------------------------------------------------------------------
// <copyright file="DL_Nationality.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_Nationality : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Nationality
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateNationality</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateNationality(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Nationality_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Nationality
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetNationality</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNationality(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Nationality_Select", dbParameter);
        }
    }
}
