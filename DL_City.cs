﻿////-----------------------------------------------------------------------
// <copyright file="DL_City.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_City : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update City
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateCity</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateCity(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_City_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get City
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetCity</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetCity(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_City_Select", dbParameter);
        }
    }
}

