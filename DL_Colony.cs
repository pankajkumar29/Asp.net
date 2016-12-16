////-----------------------------------------------------------------------
// <copyright file="DL_Colony.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_Colony : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Get Insert or Update Colony
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateColony</param>
        /// <returns>This Method Returns Integer</returns>
        public DataSet InsertUpdateColony(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Colony_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Colony
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetColony</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetColony(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Colony_Select", dbParameter);
        }
    }
}

