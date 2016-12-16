////-----------------------------------------------------------------------
// <copyright file="DL_ServiceCost.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\kirankumarj</author>
// <email>kiran.kumarj@dhii.in</email>
// <date>13/03/2012</date>
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
    public class DL_ServiceCost : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Get Tariff By Service Name In Grid
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetTariffByServiceNameInGrid</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetTariffByServiceNameInGrid(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_ServiceCost_Tariffs_Select", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Get Tariff By Package In Grid
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetTariffByPackageInGrid</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetTariffByPackageInGrid(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_PackageCost_Tariffs_Select", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Get All Services By SubGroupId
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetAllServicesBySubGroupId</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetAllServicesBySubGroupId(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_ServiceCost_Services_SelectbyGroupId", Dbparameter);

        }
        /// <summary>
        /// This Method is used to Get Service Cost
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetServiceCost</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetServiceCost(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_ServiceCost_Select", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Delete
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to Delete</param>
        /// <returns>This Method Returns Integer</returns>
        public int Delete(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_ServiceCost_Delete", Dbparameter);
        }
        /// <summary>
        /// This Method is used to Insert/Update ServiceCost
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateServiceCost</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateServiceCost(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_ServiceCost_InsertOrUpdate", Dbparameter);

        }
        /// <summary>
        /// This Method is used to Insert/Update Package Service Cost
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdatePackageServiceCost</param>
        /// <returns>This Method Returns Dataset</returns>
        public int InsertUpdatePackageServiceCost(DbParameter[] Dbparameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Package_Cost_InsertOrUpdate", Dbparameter);

        }
        /// <summary>
        /// This Method is used to GetPackagService
        /// </summary>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetPackagService()
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_PackageServices_select");
        }
    }
}