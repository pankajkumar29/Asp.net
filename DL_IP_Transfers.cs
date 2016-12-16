////-----------------------------------------------------------------------
// <copyright file="DL_IP_Transfers.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>14/03/2012</date>
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
    public class DL_IP_Transfers : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to  Insert Transfers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to TransfersInsert</param>
        /// <returns>This Method Returns Dataset</returns>
        public int TransfersInsert(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_Transfers_Insert", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Transfers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectTransfers</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectTransfers(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_Transfers_Select", dbParams);
        }
        /// <summary>
        /// This Method is used to Update Transfers
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetBed</param>
        /// <returns>This Method Returns Dataset</returns>
        public int TransfersUpdate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_Transfers_Update", dbParams);
        }
    }
}
