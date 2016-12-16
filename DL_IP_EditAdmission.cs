////-----------------------------------------------------------------------
// <copyright file="DL_IP_EditAdmission.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>07/06/2012</date>
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
    public class DL_IP_EditAdmission : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert IP Edit Admission 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to IPEditAdmissionInsert</param>
        /// <returns>This Method Returns Dataset</returns>
        public int IPEditAdmissionInsert(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_EditAdmission_Insert", dbParams);
        }
        /// <summary>
        /// This Method is used to Select Edit Admissions For Approval
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectEditAdmissionsForApproval</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectEditAdmissionsForApproval()
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_EditAdmission_Select_ForApproval");
        }
        /// <summary>
        /// This Method is used to IP Edit Admission Update
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to IPEditAdmissionUpdate</param>
        /// <returns>This Method Returns Dataset</returns>
        public int IPEditAdmissionUpdate(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_EditAdmission_Update", dbParams);
        }
    }
}
