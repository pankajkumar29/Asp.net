////-----------------------------------------------------------------------
// <copyright file="DL_NoticeBoard.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\kirankumarj</author>
// <email>kiran.kumarj@dhii.in</email>
// <date>26/03/2012</date>
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
    public class DL_NoticeBoard : BaseDataAccess
    {
        /// <summary>
        /// This Method is used to Insert/Update Notice Board
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateNoticeBoard</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet InsertUpdateNoticeBoard(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_NoticeBoard_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Insert/Update Notice Board Facility
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateNoticeBoardFacility</param>
        /// <returns>This Method Returns Integer</returns>
        public int InsertUpdateNoticeBoardFacility(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_NoticeBoard_Facility_InsertUpdate", dbParameter);
        }
        /// <summary>
        /// This Method is used to Delete Notice Board Facility
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to InsertUpdateNoticeBoardFacilityDelete</param>
        /// <returns>This Method Returns Integer</returns>
        public int DeleteNoticeBoardFacility(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnInteger("USP_Mas_NoticeBoard_Facility_Delete", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Notice Board
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetNoticeBoard</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNoticeBoard(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_NoticeBoard_Select", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Latest Notices
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetLatestNotices</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetLatestNotices(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_NoticeBoard_Select_Top3", dbParameter);
        }
        /// <summary>
        /// This Method is used to Get Notices Facility
        /// </summary>
        /// <param name="dbParameter">This Parameter belongs to GetNoticesFacility</param>
        /// <returns>This Method Returns Dataset</returns>
        public DataSet GetNoticesFacility(DbParameter[] dbParameter)
        {
            return ExecuteStoredProcReturnDataSet("USP_Mas_NoticeBoard_Facility_Select", dbParameter);
        }
    }
}
