////-----------------------------------------------------------------------
// <copyright file="DL_Religion.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_Religion : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Religion
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateReligion</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateReligion(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Religion_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Religion
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetReligion</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetReligion(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Religion_Select", dbParameter);
        }
    }
}
