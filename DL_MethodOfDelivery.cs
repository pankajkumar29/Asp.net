////-----------------------------------------------------------------------
// <copyright file="DL_MethodOfDelivery.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_MethodOfDelivery : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Method Of Delivery
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateMethodOfDelivery</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateMethodOfDelivery(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_MethodOfDelivery_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Method Of Delivery
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetMethodOfDelivery</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetMethodOfDelivery(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_MethodOfDelivery_Select", dbParameter);
        }
    }
}
