////-----------------------------------------------------------------------
// <copyright file="DL_Tariff.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_Tariff : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Tariff
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateTariff</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateTariff(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Tariff_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Tariff
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetTariff</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetTariff(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Tariff_Select", dbParameter);
        }
    }
}
