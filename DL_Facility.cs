////-----------------------------------------------------------------------
// <copyright file="DL_Facility.cs" company="Dhii Health Tech Pvt. Ltd.">
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

using System.Data;
using System.Data.Common;
using DhiiLifeSpring.AppInfrastructure;

namespace DhiiLifeSpring.DataLayer
{
    public class DL_Facility : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Facility
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateFacility</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet InsertUpdateFacility(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Facility_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Facilities
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetFacilities()
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Facility_Select");
        }
        /// <summary>
        /// This Method is used to Get Facility Wards and Beds
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetFacilityWardBeds</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetFacilityWardBeds(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Facility_Select_WardBeds", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Facility Messages
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetFacilityMessages</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetFacilityMessages(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Facility_Select_Messages", dbParameter);
        }
    }
}
