////-----------------------------------------------------------------------
// <copyright file="DL_Logs.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>05/03/2012</date>
// <summary>no summary</summary>
// <project>LifeSpring<project>
////********************************History********************************
//     Date    Add/Modified by     Method   Summary
//
////-----------------------------------------------------------------------

using System;
using System.Data.Common;
using DhiiLifeSpring.AppInfrastructure;

namespace DhiiLifeSpring.DataLayer
{
    public class DL_Logs : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Entry Log
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateEntryLog</param>
        /// <returns>This Method Returns Dataset</returns>
        public Int64 InsertUpdateEntryLog(DbParameter[] param)
        {
            return Convert.ToInt64(ExecuteStoredProcReturnObject("USP_Logs_Entry_InsertUpdate", param));
        }
        /// <summary>
        /// This Method is used to Insert Activity Log
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertActivityLog</param>
        /// <returns>This Method Returns Dataset</returns>
        public Int64 InsertActivityLog(DbParameter[] param)
        {
            return Convert.ToInt64(ExecuteStoredProcReturnObject("USP_Logs_Activity_Insert", param));
        }
    }
}