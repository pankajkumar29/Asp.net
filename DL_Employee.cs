////-----------------------------------------------------------------------
// <copyright file="DL_Employee.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\NagaJyothiG</author>
// <email>naga.jyothig@dhii.in</email>
// <date>03/13/2012</date>
// <summary>no summary</summary>
// <project>CHiMS<project>
////********************************History********************************
//     Date    Add/Modified by     Method   Summary
//  
////-----------------------------------------------------------------------

using System.Data;
using System.Data.Common;
using DhiiLifeSpring.AppInfrastructure;

namespace DhiiLifeSpring.DataLayer
{
    public class DL_Employee : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Employee
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateEmployee</param>
        /// <returns>This Method Returns Integer</returns>
        public DataSet InsertUpdateEmployee(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Employee_InsertUpdate", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Employees Clinic Wise
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetEmployeesClinicWise</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetEmployeesClinicWise(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Employee_Select", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Employees Clinic Wise Cash Handing
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetEmployeesClinicWiseCashHanding</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetEmployeesClinicWiseCashHanding(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Employee_Select_Facility", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Doctors
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDoctors</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDoctors(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Employee_Select_Doctors", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Doctors Reports
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetDoctorsReports</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetDoctorsReports(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Employee_Select_Doctors_Report", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Change Password
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to ChangePassword</param>
        /// <returns>This Method Returns Integer</returns>
        public int ChangePassword(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Change_Password", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get User By EmpId
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetUserByEmpId</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetUserByEmpId(DbParameter[] parameters)
        {
            return ExecuteStoredProcReturnDataSet("USP_Admin_Users_Select_ByEmpId", parameters);
        }
        /// <summary>
        /// This Method is used to Insert Or Update Employee Facility 
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to EmployeeFaxcilityInsertOrUpdate</param>
        /// <returns>This Method Returns Integer</returns>
        public int EmployeeFacilityInsertOrUpdate(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_MAS_Employee_Facility_InsertOrUPdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Employee Facility
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to Get Employee Facility</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetEmployeeFacility(DbParameter[] parameters)
        {
            return ExecuteStoredProcReturnDataSet("USP_MAS_Employee_Facility_Select", parameters);
        }
        /// <summary>
        /// This Method is used to Employee Faxcility Delete
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to EmployeeFaxcilityDelete</param>
        /// <returns>This Method Returns Integer</returns>
        public int EmployeeFacilityDelete(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_MAS_Employee_Facility_Delete", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Consultant Cost
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetConsultantCost</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetConsultantCost(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_GET_ConsultantCost_By_EmpId", dbParameter);
        }
        /// <summary>
        /// This Method is used to Insert or Update Consultant Cost
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateConsultantCost</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateConsultantCost(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_ConsultationCost_InsertOrUpdate", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Update Employee
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to UpdateEmployee</param>
        /// <returns>This Method Returns Integer</returns>
        public int UpdateEmployee(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Employee_Update", dbParams);
        }
    }
}