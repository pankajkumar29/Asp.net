////-----------------------------------------------------------------------
// <copyright file="DL_Mas_Question.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\Seshukumar</author>
// <email>Seshu.kumar@dhii.in</email>
// <date>10/12/2012</date>
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
    public class DL_Mas_Question:BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert or Update Question
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateQuestion</param>
        /// <returns>This Method Returns int</returns>
        public int InsertUpdateQuestion(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_Question_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Question
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to Get Question</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetQuestion(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Question_Select", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Question Options
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to Get Question Options</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetQuestionOptions(DbParameter[] dbparameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_Question_Options_Select", dbparameter);
        }
    }
}
