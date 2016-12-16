////-----------------------------------------------------------------------
// <copyright file="Master_Question.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
//     Copyright © Dhii Health Tech Pvt. Ltd.. All rights reserved.
// </copyright>
// <author>DHII\SeshukumarM</author>
// <email>Seshu.kumar@dhii.in</email>
// <date>10/12/2012</date>
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
    public partial class Master_Question : BasePage
    {
        #region User Defined Methods
        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnAnswerType.SelectedIndex = -1;
            rbtnWhen.SelectedIndex = -1;
            txtQuestion.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtSearch.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            trAnswerType.Visible = false;
            trWhen.Visible = false;
            txtQuestionWhen.Text = string.Empty;
            trDependentQue.Visible = false;
            chkAnswerType.Checked = false;
            rbtnStatus.SelectedIndex = 0;
            txtNoOfOptions.Text = string.Empty;
            trNoOfOptions.Visible = false;
            trgvCheckBoxList.Visible = false;
            gvCheckBoxList.DataSource = null;
            ddlSection.SelectedIndex = -1;
            txtDisplayOrder.Text = string.Empty;
            BindSection(null);
        }
        /// <summary>
        /// This Method is used to Bind Question
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to Bind Question</param>
        private void BindQuestion(bool openOnEmpty)
        {
            DataSet dsQuestion = null;
            dsQuestion = new BL_Mas_Question().GetQuestion(null, null, "Direct", null, null, null, null);
            String str = String.Empty;
            if (ddlStatus.SelectedIndex > 0)
            {
                if (str != "")
                    str += "And ";
                str += "Status=" + ddlStatus.SelectedValue.ToString() + " ";
            }
            if (txtSearch.Text != String.Empty)
            {
                if (str != "")
                    str += "And ";
                str += "Question='" + txtSearch.Text.Trim() + "'";
            }
            DataView dv = dsQuestion.Tables[0].DefaultView;
            dv.RowFilter = str.ToString();
            Cache["Question"] = dv;
            if (dsQuestion != null && dsQuestion.Tables.Count > 0 && dsQuestion.Tables[0].Rows.Count > 0)
            {
                gvQuestion.DataSource = dv;
                gvQuestion.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dv.Count.ToString();
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
                        gvQuestion.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvQuestion.DataSource = null;
                gvQuestion.DataBind();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hbtntext.Value = "Add";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }
        /// <summary>
        /// This Method is used to Add Question
        /// </summary>
        private void AddQuestion()
        {
            modlpopup.Show();
            int result = 0, count = 0;
            if (txtNoOfOptions.Text != string.Empty)
            {
                foreach (GridViewRow gvr in gvCheckBoxList.Rows)
                {
                    TextBox txtOptions = (TextBox)gvr.FindControl("txtOptions");
                    if (txtOptions.Text != string.Empty)
                        count++;
                }
                if (count != Convert.ToInt32(txtNoOfOptions.Text))
                {
                    lblMsg.Text = "Required Options";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    txtQuestion.Text = string.Empty;
                    gvCheckBoxList.Focus();
                    return;
                }
            }
            DataSet dsQuestion = new BL_Mas_Question().GetQuestion(null, null, null, null, null, null, null);
            DataRow[] rows = dsQuestion.Tables[0].Select("Question = '" + txtQuestion.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Question already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtQuestion.Text = string.Empty;
                txtQuestion.Focus();
                return;
            }
            BE_Mas_Question beQuestion = new BE_Mas_Question();
            beQuestion.QuestionId = 0;
            if (ddlSection.SelectedIndex > 0)
                beQuestion.SectionId = Convert.ToInt32(ddlSection.SelectedValue.ToString());
            if (txtDisplayOrder.Text != string.Empty)
                beQuestion.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);
            if (rbtnAnswerType.SelectedValue == "Radio Button")
            {
                beQuestion.DependentQuestion = txtQuestionWhen.Text.Trim().ToUpper();
            }
            beQuestion.DependentQuestionId = null;
            if (rbtnAnswerType.SelectedValue.ToString() == "CheckBoxList")
            {
                beQuestion.NoOfOptions = Convert.ToInt32(txtNoOfOptions.Text.Trim());
            }
            if (rbtnWhen.SelectedIndex > -1)
            {
                if (rbtnWhen.SelectedValue.ToString() == "Yes")
                    beQuestion.WhenAnswerIs = true;
                else
                    beQuestion.WhenAnswerIs = false;
            }
            else
                beQuestion.WhenAnswerIs = null;
            beQuestion.Question = txtQuestion.Text.Trim().ToUpper();
            beQuestion.AnswerType = rbtnAnswerType.SelectedValue.ToString();
            beQuestion.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beQuestion.Status = true;
                beQuestion.Reason = string.Empty;
            }
            else
            {
                beQuestion.Status = false;
                beQuestion.Reason = txtReason.Text.Trim().ToUpper();
            }
            if (trgvCheckBoxList.Visible == true)
            {
                foreach (GridViewRow gvr in gvCheckBoxList.Rows)
                {
                    //Label lblOptionNo = (Label)gvr.FindControl("lblOptionNo");
                    TextBox txtOptions = (TextBox)gvr.FindControl("txtOptions");
                    //beQuestion.OptionNo = lblOptionNo.Text.Trim();
                    beQuestion.Options = txtOptions.Text.Trim();
                    result = new BL_Mas_Question().InsertUpdateQuestion(beQuestion);
                }
            }
            else
            {
                result = new BL_Mas_Question().InsertUpdateQuestion(beQuestion);
            }
            if (result > 0)
            {
                lblMsg.Text = "Added successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Failed to add details";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClearControls();
            BindQuestion(false);
            btnAdd.Text = "Add";
            btnAdd.ToolTip = "Add";
            hbtntext.Value = "Add";
            ddlSection.Focus();
        }
        /// <summary>
        /// This Method is used to Update Question
        /// </summary>
        private void UpdateQuestion()
        {
            int result = 0;
            modlpopup.Show();
            DataSet dsQuestion = new BL_Mas_Question().GetQuestion(null, null, null, null, null, null, null);
            DataRow[] rows = dsQuestion.Tables[0].Select("QuestionId <>" + hfQuestionId.Value + " " + "AND Question = '" + txtQuestion.Text.Trim().ToUpper() + "'" +
                "And QuestionType='Direct'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Question already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtQuestion.Text = string.Empty;
                txtQuestion.Focus();
                return;
            }
            BE_Mas_Question beQuestion = new BE_Mas_Question();
            beQuestion.QuestionId = Convert.ToInt32(hfQuestionId.Value);
            beQuestion.QuestionType = "Direct";
            if (ddlSection.SelectedIndex > 0)
                beQuestion.SectionId = Convert.ToInt32(ddlSection.SelectedValue.ToString());
            if (txtDisplayOrder.Text != string.Empty)
                beQuestion.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);

            if (rbtnAnswerType.SelectedValue == "Text Box")
                beQuestion.DependentQuestionId = null;
            else if (rbtnAnswerType.SelectedValue == "CheckBoxList")
            {
                beQuestion.NoOfOptions = Convert.ToInt32(txtNoOfOptions.Text.Trim());
            }
            else
            {
                beQuestion.QuestionType = "Dependent";
                if (hfQuestionId.Value != null)
                    beQuestion.DependentQuestionId = Convert.ToInt32(hfQuestionId.Value);
                beQuestion.DependentQuestion = txtQuestionWhen.Text.Trim().ToUpper();
            }
            if (rbtnWhen.SelectedIndex > -1)
            {
                if (rbtnWhen.SelectedValue.ToString() == "Yes")
                    beQuestion.WhenAnswerIs = true;
                else
                    beQuestion.WhenAnswerIs = false;
            }
            else
                beQuestion.WhenAnswerIs = null;
            beQuestion.Question = txtQuestion.Text.Trim().ToUpper();
            beQuestion.AnswerType = rbtnAnswerType.SelectedValue.ToString();
            beQuestion.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beQuestion.Status = true;
                beQuestion.Reason = string.Empty;
            }
            else
            {
                beQuestion.Status = false;
                beQuestion.Reason = txtReason.Text.Trim().ToUpper();
            }
            if (trgvCheckBoxList.Visible == true)
            {
                int FlagId = 0;
                foreach (GridViewRow gvr in gvCheckBoxList.Rows)
                {

                    TextBox txtoptions = (TextBox)gvr.FindControl("txtOptions");
                    beQuestion.FlagId = FlagId;
                    beQuestion.Options = txtoptions.Text.Trim();
                    result = new BL_Mas_Question().InsertUpdateQuestion(beQuestion);
                    FlagId += 1;
                }
            }
            else
            {
                result = new BL_Mas_Question().InsertUpdateQuestion(beQuestion);
            }
            if (result > 0)
            {
                lblMsg.Text = "Updated successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Failed to update details";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClearControls();
            BindQuestion(false);
            ddlSection.Focus();
        }
        /// <summary>
        /// This Method is used to bind section
        /// </summary>
        private void BindSection(int? SectionId)
        {
            ddlSection.Items.Clear();
            DataSet dsSection = new BL_Mas_Section().GetSection(null, null, null);
            if (dsSection != null && dsSection.Tables.Count > 0 && dsSection.Tables[0].Rows.Count > 0)
            {
                DataView dvSection = dsSection.Tables[0].DefaultView;
                if (hfQuestionId.Value == "")
                    dvSection.RowFilter = "Status = true";
                if ((hfQuestionId.Value != string.Empty) && (SectionId != null))
                    dvSection.RowFilter = "Status = true or SectionId = " + SectionId;
                ddlSection.DataSource = dvSection;
                ddlSection.DataTextField = "Section";
                ddlSection.DataValueField = "SectionId";
                ddlSection.DataBind();
            }
            ddlSection.Items.Insert(0, "Select");
        }
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
                    txtQuestion.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ClearControls();
                        ViewState["SortDirection"] = "ASC";
                        BindQuestion(true);
                        btnAddQuestion.Focus();
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
                BindQuestion(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvQuestion);
                    }
                    else
                    {
                        Image sortImage = new Image();
                        sortImage.ImageUrl = "~/Images/UpArrow.png";
                        sortImage.Width = 10;
                        sortImage.Height = 10;
                        sortImage.AlternateText = "Ascending Order";
                        e.Row.Cells[2].Controls.Add(sortImage);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvQuestion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsQuestion = new BL_Mas_Question().GetQuestion(Convert.ToInt32(e.CommandArgument.ToString()), null, null, null, null, null, null);
                    if (dsQuestion != null && dsQuestion.Tables.Count > 0 && dsQuestion.Tables[0].Rows.Count > 0)
                    {
                        btnAdd.Text = "Update";
                        hbtntext.Value = "Update";
                        btnAdd.ToolTip = "Update";
                        trAnswerType.Visible = false;
                        trDependentQue.Visible = false;
                        trNoOfOptions.Visible = false;
                        trWhen.Visible = false;
                        trgvCheckBoxList.Visible = false;
                        hfQuestionId.Value = e.CommandArgument.ToString().ToUpper();
                        if (dsQuestion.Tables[0].Rows[0]["SectionId"].ToString() != string.Empty)
                        {
                            BindSection(Convert.ToInt32(dsQuestion.Tables[0].Rows[0]["SectionId"].ToString()));
                            ddlSection.SelectedValue = dsQuestion.Tables[0].Rows[0]["SectionId"].ToString();
                        }
                        if (dsQuestion.Tables[0].Rows[0]["DisplayOrder"].ToString() != string.Empty)
                            txtDisplayOrder.Text = dsQuestion.Tables[0].Rows[0]["Displayorder"].ToString();
                        if (dsQuestion.Tables[0].Rows[0]["Question"].ToString() != null)
                            txtQuestion.Text = dsQuestion.Tables[0].Rows[0]["Question"].ToString().ToUpper();
                        if (dsQuestion.Tables[0].Rows[0]["AnswerType"].ToString() == "Text Box")
                            rbtnAnswerType.SelectedIndex = 0;
                        else if (dsQuestion.Tables[0].Rows[0]["AnswerType"].ToString() == "Radio Button")
                            rbtnAnswerType.SelectedIndex = 1;
                        else if (dsQuestion.Tables[0].Rows[0]["AnswerType"].ToString() == "CheckBoxList")
                        {
                            rbtnAnswerType.SelectedIndex = 2;
                            trNoOfOptions.Visible = true;
                            trgvCheckBoxList.Visible = true;
                            txtNoOfOptions.Text = dsQuestion.Tables[0].Rows[0]["NoOfOptions"].ToString();
                            DataSet dsOptions = new BL_Mas_Question().GetQuestionOptions(Convert.ToInt32(e.CommandArgument.ToString()), true);
                            {
                                if (dsOptions != null && dsOptions.Tables.Count > 0 && dsOptions.Tables[0].Rows.Count > 0)
                                {
                                    gvCheckBoxList.DataSource = dsOptions;
                                    gvCheckBoxList.DataBind();
                                }
                            }
                        }
                        DataSet dsQuestionType = new BL_Mas_Question().GetQuestion(null, null, "Dependent", Convert.ToInt32(e.CommandArgument.ToString()), null, null, true);
                        if (dsQuestionType != null && dsQuestionType.Tables.Count > 0 && dsQuestionType.Tables[0].Rows.Count > 0)
                        {
                            if (dsQuestionType.Tables[0].Rows[0]["WhenAnswerIs"] != null)
                            {
                                trAnswerType.Visible = true;
                                chkAnswerType.Checked = true;
                                trWhen.Visible = true;
                                trDependentQue.Visible = true;
                                if (dsQuestionType.Tables[0].Rows[0]["WhenAnswerIs"].ToString() == "False")
                                    rbtnWhen.SelectedValue = "No";
                                else
                                    rbtnWhen.SelectedValue = "Yes";
                                txtQuestionWhen.Text = dsQuestionType.Tables[0].Rows[0]["Question"].ToString();
                            }
                        }
                        if (Convert.ToBoolean(dsQuestion.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsQuestion.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        modlpopup.Show();
                        ddlSection.Focus();

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
                                gvQuestion.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
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

        protected void gvQuestion_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataView dv = Cache["Question"] as DataView;
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
                    gvQuestion.DataSource = dv;
                    gvQuestion.DataBind();
                    gvQuestion.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvQuestion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvQuestion.PageIndex = e.NewPageIndex;
                BindQuestion(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void chkAnswerType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                if (chkAnswerType.Checked == true)
                {
                    trWhen.Visible = true;
                    trDependentQue.Visible = true;
                }
                else
                {
                    trWhen.Visible = false;
                    trDependentQue.Visible = false;
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Add";
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                modlpopup.Show();
                ddlSection.Focus();
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
                ddlSection.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfQuestionId.Value != string.Empty)
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

        protected void rbtnAnswerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                rbtnWhen.SelectedIndex = -1;
                chkAnswerType.Checked = false;
                txtQuestionWhen.Text = string.Empty;
                trWhen.Visible = false;
                trAnswerType.Visible = false;
                trDependentQue.Visible = false;
                trNoOfOptions.Visible = false;
                trgvCheckBoxList.Visible = false;
                if (rbtnAnswerType.SelectedValue.ToString() == "Radio Button")
                    trAnswerType.Visible = true;
                if (rbtnAnswerType.SelectedValue.ToString() == "CheckBoxList")
                    trNoOfOptions.Visible = true;
                if (rbtnAnswerType.SelectedValue.ToString() == "Text Box")
                    trAnswerType.Visible = false;
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void txtNoOfOptions_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trgvCheckBoxList.Visible = false;
                modlpopup.Show();
                if (txtNoOfOptions.Text != string.Empty)
                {
                    int OptionNo = Convert.ToInt32(txtNoOfOptions.Text.Trim());
                    DataTable dt = new DataTable();
                    dt.Columns.Add("OptionId");
                    dt.Columns.Add("Options");
                    for (int i = 1; i <= OptionNo; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["OptionId"] = i;
                        dt.Rows.Add(dr);
                    }
                    trgvCheckBoxList.Visible = true;
                    gvCheckBoxList.DataSource = dt;
                    gvCheckBoxList.DataBind();
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
                if (hbtntext.Value == "Add")
                    AddQuestion();
                else if (hbtntext.Value == "Update")
                    UpdateQuestion();
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
                modlpopup.Show();
                ClearControls();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        #endregion
    }
}