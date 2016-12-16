////-----------------------------------------------------------------------
// <copyright file="DL_Street.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_Street : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Street
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateStreet</param>
        /// <returns>This Method Returns Integer</returns>
        public DataSet InsertUpdateStreet(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Street_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Street
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetStreet</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetStreet(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Street_Select", dbParameter);
        }
    }
}

