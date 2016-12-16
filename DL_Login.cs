////-----------------------------------------------------------------------
// <copyright file="DL_Login.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_Login : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Validate User
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to ValidateUser</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet ValidateUser(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Login_Validate_User", dbParams);
        }
        /// <summary>
        /// This Method is used to Change Password
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to ChangePassword</param>
        /// <returns>This Method Returns Dataset</returns>
        public int ChangePassword(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Change_Password", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Menu
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetMenu</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetMenu(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Login_Select_Menu", dbParams);
        }
        /// <summary>
        /// This Method is used to Get Sub Menu
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetSubMenu</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetSubMenu(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Login_Select_Menu_SubMenu", dbParams);
        }
        /// <summary>
        /// This Method is used to Get Sub Sub Menu
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetSubSubMenu</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetSubSubMenu(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Login_Select_Menu_SubSubMenu", dbParams);
        }
    }
}