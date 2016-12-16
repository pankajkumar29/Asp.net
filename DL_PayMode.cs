////-----------------------------------------------------------------------
// <copyright file="DL_PayMode.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_PayMode : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Pay Mode
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdatePayMode</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdatePayMode(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_PayMode_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Pay Mode
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetPayMode</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetPayMode(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_PayMode_Select", dbParameter);
        }
    }
}


