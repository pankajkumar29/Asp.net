////-----------------------------------------------------------------------
// <copyright file="DL_Role.cs" company="Dhii Health Tech Pvt. Ltd.">
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
using System.Data;
using System.Data.Common;
using DhiiLifeSpring.AppInfrastructure;

namespace DhiiLifeSpring.DataLayer
{
    public class DL_Role : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Role
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateRole</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateRole(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Admin_Role_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Roles
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetRoles</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetRoles(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Admin_Role_Select", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Menu
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetMenu()
        {
            return ExecuteStoredProcReturnDataSet("USP_Admin_Menu_Select");
        }
        /// <summary>
        /// This Method is used to Insert or Update EmpApp
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateEmpApp</param>
        /// <returns>This Method Returns String</returns>
        public String InsertUpdateEmpApp(DbParameter[] dbParam)
        {
            return ExecuteStoredProcReturnObject("USP_Admin_EmpApp_InsertUpdate", dbParam).ToString();
        }
        /// <summary>
        /// This Method is used to Get Emp App By Role Id
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetEmpAppByRoleId</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetEmpAppByRoleId(DbParameter[] dbParam)
        {
            return ExecuteStoredProcReturnDataSet("USP_Admin_EmpApp_SelectByRoleId", dbParam);
        }
        /// <summary>
        /// This Method is used to Get Emp App By RoleId and Screenid
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetEmpAppByRoleIdandScreenId</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetEmpAppByRoleIdandScreenId(DbParameter[] dbParam)
        {
            return ExecuteStoredProcReturnDataSet("USP_Admin_EmpApp_SelectByRoleId_ScreenId", dbParam);
        }
    }
}

