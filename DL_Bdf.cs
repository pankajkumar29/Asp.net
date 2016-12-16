////-----------------------------------------------------------------------
// <copyright file="DL_Bdf.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\kirankumarj</author>
// <email>kiran.kumarj@dhii.in</email>
// <date>05/04/2012</date>
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
    public class DL_Bdf : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Get Patients For Bdf
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetPatientsForBdf</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetPatientsForBdf()
        {
            return ExecuteStoredProcReturnDataSet("USP_Select_Patients_EMR");
        }
        /// <summary>
        /// This Method is used to Insert or Update Bdf
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateBdf</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateBdf(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Customer_Bdf_InsertORUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Insert or Update Bdf Pdf
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateBdfPdf</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateBdfPdf(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Customer_Bdf_FileUpload", dbParameter);
        }
        /// <summary>
        /// This Method is used to Insert or Update Bdf Pdf
        /// </summary>
        /// <param name="dbParams">This Parameter belongs to GetPatientBdf</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetPatientBdf(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Customer_Bdf_Select", dbParams);
        }
        /// <summary>
        /// This Method is used to GetErRPdf
        /// </summary>
        /// <param name="dbParams">This Parameter belongs to GetErRPdf</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetPatientBdfPdf(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Customer_EMR_Get_Pdf", dbParams);
        }
        /// <summary>
        /// This Method is used to Insert or Update Investigation Chart
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateInvestigationChart</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateInvestigationChart(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Customer_InvestigationChart_InsertOrUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Investigation Chart
        /// </summary>
        /// <param name="dbParams">This Parameter belongs to GetInvestigationChart</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetInvestigationChart(DbParameter[] dbParams)
        {
            return ExecuteStoredProcReturnDataSet("USP_Customer_InvestigationChart_Select", dbParams);
        }
        /// <summary>
        /// This Method is used to Get EMR List
        /// </summary>
        /// <param name="dbParams">This Parameter belongs to GetViewEMRList</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetViewEMRList()
        {
            return ExecuteStoredProcReturnDataSet("USP_Get_ViewEMR_List");
        }
    }
}
