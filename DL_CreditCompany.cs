////-----------------------------------------------------------------------
// <copyright file="DL_CreditCompany.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>02/04/2012</date>
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
    public class DL_CreditCompany : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Credit Company
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateCreditCompany</param>
        /// <returns>This Method Returns Dataset</returns>
        public int InsertUpdateCreditCompany(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_CreditCompany_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Credit Company
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCreditCompany</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCreditCompany(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_CreditCompany_Select", dbParameter);
        }
    }
}



