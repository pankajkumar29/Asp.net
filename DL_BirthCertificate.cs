////-----------------------------------------------------------------------
// <copyright file="DL_BirthCertificate.cs" company="Dhii Health Tech Pvt. Ltd.">
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
    public class DL_BirthCertificate : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert BirthCertificate AND Birth details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertBirthCertificate</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet InsertBirthCertificate(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_BirthCertificate_Insert", dbParameter);
        }
        /// <summary>
        /// This Method is used to  Update BirthCertificate
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to UpdateBirthCertificate</param>
        /// <returns>This Method Returns Dataset</returns>
        public int UpdateBirthCertificate(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_BirthCertificate_Update", dbParameter);
        }
        /// <summary>
        /// This Method is used to Update BirthCertificate Details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to UpdateBirthCertificateDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public int UpdateBirthCertificateDetails(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_IP_BirthCertificateDetails_Update", dbParameter);
        }
        /// <summary>
        /// This Method is used to Select BirthCertificate
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectBirthCertificate</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectBirthCertificate(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_BirthCertificate_Select", dbParameter);
        }
        /// <summary>
        /// This Method is used to Select BirthCertificate Details
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to SelectBirthCertificateDetails</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet SelectBirthCertificateDetails(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_IP_BirthCertificate_SelectDetails", dbParameter);
        }
    }
}
