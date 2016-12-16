////-----------------------------------------------------------------------
// <copyright file="DL_MunicipalWard.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>13/03/2012</date>
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
    public class DL_MunicipalWard : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Municipal Ward
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateMunicipalWard</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateMunicipalWard(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_MunicipalWard_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Municipal Ward
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetMunicipalWard</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetMunicipalWard(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_MunicipalWard_Select", dbParameter);
        }
    }
}

