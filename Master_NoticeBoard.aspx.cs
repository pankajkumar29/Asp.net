////-----------------------------------------------------------------------
// <copyright file="Master_NoticeBoard.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\kirankumarj</author>
// <email>kiran.kumarj@dhii.in</email>
// <date>21/03/2012</date>
// <summary>no summary</summary>
// <project>LifeSpring<project>
////********************************History********************************
//     Date    Add/Modified by     Method   Summary
//
////-----------------------------------------------------------------------

#region Namespaces
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhiiLifeSpring.AppInfrastructure;
using DhiiLifeSpring.BusinessEntities;
using DhiiLifeSpring.BusinessLayer;
#endregion

namespace DhiiLifeSpring.Application.Masters
{
    public partial class Master_NoticeBoard : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            chkBranch.SelectedIndex = -1;
            txtNoticeHeading.Text = string.Empty;
            txtNoticeDisc.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }

        /// <summary>
        /// This Method is used to Bind Notice Board
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to Bind Notice Board</param>
        private void BindNoticeBoard(bool openOnEmpty)
        {
            DataSet dsNoticeBoard = null;

            if (ddlStatus.SelectedIndex > 0)
                dsNoticeBoard = new BL_NoticeBoard().GetNoticeBoard(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsNoticeBoard = new BL_NoticeBoard().GetNoticeBoard(null, txtSearch.Text.Trim().ToUpper(), null);

            Cache["NoticeBoard"] = dsNoticeBoard;
            if (dsNoticeBoard != null && dsNoticeBoard.Tables.Count > 0 && dsNoticeBoard.Tables[0].Rows.Count > 0)
            {
                gvNoticeBoard.DataSource = dsNoticeBoard;
                gvNoticeBoard.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records" + ": " + dsNoticeBoard.Tables[0].Rows.Count.ToString().ToUpper();
                if (ViewState["SortExpression"] != null)
                {
                    if (ViewState["SortDirection"] != null)
                    {
                        if (ViewState["SortDirection"].ToString() == "ASC")
                        {
                            ViewState["SortDirection"] = "DESC";

                        }
                        else
                            ViewState["SortDirection"] = "ASC";
                        gvNoticeBoard.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }

                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvNoticeBoard.DataSource = null;
                gvNoticeBoard.DataBind();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Add Notice Board
        /// </summary>
        private void AddNoticeBoard()
        {
            modlpopup.Show();
            DataSet dsNoticeBoard = new BL_NoticeBoard().GetNoticeBoard(null, null, null);
            DataView dvNoticeBoard = dsNoticeBoard.Tables[0].DefaultView;
            DataRow[] rows = dsNoticeBoard.Tables[0].Select("NoticeHeading ='" + txtNoticeHeading.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Data already exists ";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtNoticeHeading.Text = string.Empty;
                txtNoticeHeading.Focus();
                return;
            }
            BE_NoticeBoard beNoticeBoard = new BE_NoticeBoard();
            beNoticeBoard.NoticeId = 0;
            beNoticeBoard.NoticeHeading = txtNoticeHeading.Text.Trim().ToUpper();
            beNoticeBoard.NoticeDisc = txtNoticeDisc.Text.Trim().ToUpper();
            beNoticeBoard.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beNoticeBoard.Status = true;
                beNoticeBoard.Reason = string.Empty;
            }
            else
            {
                beNoticeBoard.Status = false;
                beNoticeBoard.Reason = txtReason.Text.Trim().ToUpper();
            }

            int result = 0;
            dsNoticeBoard = new BL_NoticeBoard().InsertUpdateNoticeBoard(beNoticeBoard);
            if (dsNoticeBoard.Tables[0].Rows[0]["NoticeId"].ToString() != string.Empty)
            {
                beNoticeBoard.NoticeId = int.Parse(dsNoticeBoard.Tables[0].Rows[0]["NoticeId"].ToString());
                foreach (ListItem li in chkBranch.Items)
                {
                    if (li.Selected)
                    {
                        beNoticeBoard.FacilityId = Convert.ToInt32(li.Value);
                        result = new BL_NoticeBoard().InsertUpdateNoticeBoardFacility(beNoticeBoard);
                    }
                }
            }

            if (result > 0)
            {
                lblMsg.Text = "Added successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Failed to add details ";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            rbtnStatus.SelectedIndex = 0;
            chkBranch.SelectedIndex = -1;
            txtNoticeHeading.Text = string.Empty;
            txtNoticeDisc.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
            btnAdd.Text = "Add";
            btnAdd.ToolTip = "Add";
            hbtntext.Value = "Add";
            BindNoticeBoard(false);
            chkBranch.Focus();
        }

        /// <summary>
        /// This Method is used to Update Notice Board
        /// </summary>
        private void UpdateNoticeBoard()
        {
            modlpopup.Show();
            DataSet dsNoticeBoard = new BL_NoticeBoard().GetNoticeBoard(null, null, null);
            DataView dvNoticeBoard = dsNoticeBoard.Tables[0].DefaultView;
            DataRow[] rows = dsNoticeBoard.Tables[0].Select("NoticeId <> " + Convert.ToInt32(hfNoticeId.Value) + "AND NoticeHeading ='" + txtNoticeHeading.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Data already exists ";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtNoticeHeading.Text = string.Empty;
                txtNoticeHeading.Focus();
                return;
            }
            BE_NoticeBoard beNoticeBoard = new BE_NoticeBoard();
            beNoticeBoard.NoticeId = Convert.ToInt32(hfNoticeId.Value.Trim().ToUpper());
            beNoticeBoard.NoticeHeading = txtNoticeHeading.Text.Trim().ToUpper();
            beNoticeBoard.NoticeDisc = txtNoticeDisc.Text.Trim().ToUpper();
            //beNoticeBoard.FacilityId = Convert.ToInt32(ddlBranch.SelectedValue);
            beNoticeBoard.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beNoticeBoard.Status = true;
                beNoticeBoard.Reason = string.Empty;
            }
            else
            {
                beNoticeBoard.Status = false;
                beNoticeBoard.Reason = txtReason.Text.Trim().ToUpper();
            }

            int result = 0;
            dsNoticeBoard = new BL_NoticeBoard().InsertUpdateNoticeBoard(beNoticeBoard);
            if (dsNoticeBoard.Tables[0].Rows[0]["NoticeId"].ToString() == "SUCCESS")
            {
                result = new BL_NoticeBoard().DeleteNoticeBoardFacility(beNoticeBoard);
                foreach (ListItem li in chkBranch.Items)
                {
                    if (li.Selected)
                    {
                        beNoticeBoard.FacilityId = Convert.ToInt32(li.Value);
                        result = new BL_NoticeBoard().InsertUpdateNoticeBoardFacility(beNoticeBoard);
                    }
                }
            }
            //int result = new BL_NoticeBoard().InsertUpdateNoticeBoard(beNoticeBoard);
            if (result > 0)
            {
                lblMsg.Text = "Updated successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Failed to update details ";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            BindNoticeBoard(false);
            chkBranch.Focus();
        }

        /// <summary>
        /// This Method is used to Bind Facility
        /// </summary>
        void BindFacility()
        {
            chkBranch.Items.Clear();
            chkAll.Visible = false;
            DataSet dsfacility = new BL_Facility().GetFacilities();
            DataView dvFacility = new DataView();
            if (dsfacility.Tables.Count > 0 && dsfacility != null && dsfacility.Tables[0].Rows.Count > 0)
            {
                dvFacility = dsfacility.Tables[0].DefaultView;
                dvFacility.RowFilter = "Status= true and Corporate is null ";
                if (dvFacility.Count > 0)
                {
                    chkBranch.DataSource = dvFacility;
                    chkBranch.DataTextField = "FacilityName";
                    chkBranch.DataValueField = "FacilityId";
                    chkBranch.DataBind();
                }
            }
            if (chkBranch.Items.Count > 0)
                chkAll.Visible = true;
        }

        //private void BindFacility(Int32? FacilityId)
        //{
        //    //ddlBranch.Items.Clear();
        //    DataSet dsfacility = new BL_Facility().GetFacilities();
        //    DataView dvFacility = new DataView();
        //    if (dsfacility.Tables.Count > 0 && dsfacility != null && dsfacility.Tables[0].Rows.Count > 0)
        //    {
        //        dvFacility = dsfacility.Tables[0].DefaultView;
        //        if (dvFacility.Count > 0)
        //        {
        //            dvFacility.RowFilter = "Status= true";
        //            ddlBranch.DataSource = dvFacility;
        //            ddlBranch.DataTextField = "FacilityName";
        //            ddlBranch.DataValueField = "FacilityId";
        //            ddlBranch.DataBind();
        //            ddlBranch.Items.Insert(0, "Select");
        //        }
        //        else
        //            ddlBranch.Items.Insert(0, "Select");
        //    }
        //    else
        //    {
        //        ddlBranch.Items.Insert(0, "Select");
        //    }
        //}

        #endregion

        #region Page Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserID"] == null || (Session["UserID"] != null && Convert.ToString(Session["UserID"]) == string.Empty))
                    Response.Redirect(ConfigurationManager.AppSettings["LoginUrl"]);
                else
                {
                    lblMsg.Text = string.Empty;
                    txtNoticeDisc.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        BindNoticeBoard(true);
                        btnAddNotice.Focus();
                        BindFacility();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(ex is System.Threading.ThreadAbortException))
                    WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindNoticeBoard(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvNoticeBoard_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvNoticeBoard);
                    }
                    else
                    {
                        Image sortImage = new Image();
                        sortImage.ImageUrl = "~/Images/UpArrow.png";
                        sortImage.Width = 10;
                        sortImage.Height = 10;
                        sortImage.AlternateText = "Ascending Order";
                        e.Row.Cells[1].Controls.Add(sortImage);
                    }
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDescription = (Label)e.Row.FindControl("lblDescription");
                    string myString = lblDescription.Text.Trim().ToUpper();
                    string result = string.Empty;
                    for (int i = 0; i < myString.Length; i++)
                    {
                        result += (i % 50 == 0 && i != 0) ? (myString[i].ToString() + "<br/>") : myString[i].ToString().ToUpper();
                    }
                    lblDescription.Text = result.ToString().ToUpper();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvNoticeBoard_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvNoticeBoard.PageIndex = e.NewPageIndex;
                BindNoticeBoard(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvNoticeBoard_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsNoticeBoard = new BL_NoticeBoard().GetNoticeBoard(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    DataSet dsFacilityId = new BL_NoticeBoard().GetNoticesFacility(Convert.ToInt32(e.CommandArgument.ToString()), null);
                    int count = 0;
                    if (dsNoticeBoard != null && dsNoticeBoard.Tables.Count > 0 && dsNoticeBoard.Tables[0].Rows.Count > 0)
                    {
                        hfNoticeId.Value = e.CommandArgument.ToString().ToUpper();
                        BindFacility();
                        if (dsFacilityId.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsFacilityId.Tables[0].Rows)
                            {
                                foreach (ListItem li in chkBranch.Items)
                                {
                                    if (li.Value.ToString() == dr["FacilityId"].ToString())
                                    {
                                        li.Selected = true;
                                        count += 1;
                                    }
                                }
                            }
                            if (count == chkBranch.Items.Count)
                                chkAll.Checked = true;
                            else
                                chkAll.Checked = false;
                        }
                        //ddlBranch.SelectedValue = dsNoticeBoard.Tables[0].Rows[0]["FacilityId"].ToString().ToUpper();
                        txtNoticeHeading.Text = dsNoticeBoard.Tables[0].Rows[0]["NoticeHeading"].ToString().ToUpper();
                        txtNoticeDisc.Text = dsNoticeBoard.Tables[0].Rows[0]["NoticeDisc"].ToString().ToUpper();
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        if (Convert.ToBoolean(dsNoticeBoard.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsNoticeBoard.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        chkBranch.Focus();

                        if (ViewState["SortExpression"] != null)
                        {
                            if (ViewState["SortDirection"] != null)
                            {
                                if (ViewState["SortDirection"].ToString() == "ASC")
                                {
                                    ViewState["SortDirection"] = "DESC";
                                }
                                else
                                    ViewState["SortDirection"] = "ASC";
                                gvNoticeBoard.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvNoticeBoard_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["NoticeBoard"] as DataSet;
                DataView dv = new DataView();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    dv = new DataView(ds.Tables[0]);

                String SortExpresssion = e.SortExpression;
                String SortDirection;
                if (ViewState["SortDirection"].ToString() == "ASC")
                    SortDirection = "DESC";
                else
                    SortDirection = "ASC";
                ViewState["SortDirection"] = SortDirection;
                ViewState["SortExpression"] = SortExpresssion;

                if (null != SortExpresssion && string.Empty != SortExpresssion)
                {
                    String strSort = String.Format("{0} {1}", SortExpresssion, SortDirection);
                    dv = new DataView(dv.ToTable(), string.Empty, strSort, DataViewRowState.CurrentRows);
                    gvNoticeBoard.DataSource = dv;
                    gvNoticeBoard.DataBind();
                    gvNoticeBoard.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddNotice_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                BindFacility();
                chkBranch.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void rbtnStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                chkBranch.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfNoticeId.Value != string.Empty)
                    {
                        txtReason.Focus();
                    }
                }
                else
                {
                    txtReason.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var s = chkBranch.Items.Cast<ListItem>()
               .Where(item => item.Selected)
               .Aggregate("", (current, item) => current + (item.Value + ", "));
                if (s == null || s == string.Empty)
                {
                    lblMsg.Text = "select atleast one branch";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    chkBranch.Focus();
                    modlpopup.Show();
                    return;
                }
                if (hbtntext.Value == "Add")
                    AddNoticeBoard();
                else if (hbtntext.Value == "Update")
                    UpdateNoticeBoard();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                modlpopup.Show();
                chkBranch.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll.Checked)
                {
                    foreach (ListItem li in chkBranch.Items)
                    {
                        li.Selected = true;
                    }
                }
                else
                {
                    foreach (ListItem li in chkBranch.Items)
                    {
                        li.Selected = false;
                    }
                }
                modlpopup.Show();
                chkAll.Focus();
            }
            catch (Exception ex)
            {
                if (!(ex is System.Threading.ThreadAbortException))
                    WriteLogItem("Page_Load --- " + ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void chkBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                foreach (ListItem li in chkBranch.Items)
                {
                    if (li.Selected)
                        count += 1;
                }
                if (count == chkBranch.Items.Count)
                {
                    chkAll.Checked = true;
                }
                else
                {
                    chkAll.Checked = false;
                }
                modlpopup.Show();
                chkBranch.Focus();
            }
            catch (Exception ex)
            {
                if (!(ex is System.Threading.ThreadAbortException))
                    WriteLogItem("Page_Load --- " + ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}