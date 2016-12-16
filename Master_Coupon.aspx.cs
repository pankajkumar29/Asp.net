////-----------------------------------------------------------------------
// <copyright file="Master_Coupon.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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
using System.Text;
using DhiiLifeSpring.AppInfrastructure;
using DhiiLifeSpring.BusinessEntities;
using DhiiLifeSpring.BusinessLayer;
#endregion
namespace DhiiLifeSpring.Application.Masters
{
    public partial class Master_Coupon : BasePage
    {
        #region User Defined Methods

        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            txtBatchName.Text = String.Empty;
            txtCoupons.Text = string.Empty;
            txtCouponAmount.Text = string.Empty;
            txtValidFrom.Text = string.Empty;
            txtValidTo.Text = string.Empty;
            BindValidIsFacility();
            BindServiceGroup();
            chkAll.Checked = false;
            chkServiceGroupAll.Checked = false;
        }

        /// <summary>
        /// This Method is used to Bind Coupon
        /// </summary>
        /// <param name="openOnEmpty">This Parameter Belongs to Bind Coupon</param>
        private void BindCoupon(bool openOnEmpty)
        {
            DataSet dsCoupon = new BL_Mas_Coupon().GetCoupon(null, null, null);
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
                str += "CouponNo='" + txtSearch.Text.Trim() + "'";
            }
            DataView dv = dsCoupon.Tables[0].DefaultView;
            dv.RowFilter = str.ToString();
            Cache["Coupon"] = dv;
            if (dsCoupon != null && dsCoupon.Tables.Count > 0 && dsCoupon.Tables[0].Rows.Count > 0)
            {
                gvCoupon.DataSource = dv;
                gvCoupon.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dv.Count.ToString().ToUpper();
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
                        gvCoupon.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                gvCoupon.DataSource = null;
                gvCoupon.DataBind();
                ClearControls();
                btnSave.Text = "Save";
                btnSave.ToolTip = "Save";
                hbtntext.Value = "Save";
                if (openOnEmpty)
                    modlpopup.Show();
            }
        }

        /// <summary>
        /// This Method is used to Bind Valid is facility 
        /// </summary>
        private void BindValidIsFacility()
        {
            chkValidIsFacility.Items.Clear();
            DataSet dsValidIsFacility = null;
            if (Session["RoleName"].ToString() == "BUSINESS HEAD")
                dsValidIsFacility = new BL_Employee().GetEmployeeFacility(null, Convert.ToInt32(Session["EmpId"]));
            else
                dsValidIsFacility = new BL_Facility().GetFacilities();
            DataView dvValidIsFacility = new DataView();
            if (dsValidIsFacility.Tables.Count > 0 && dsValidIsFacility != null && dsValidIsFacility.Tables[0].Rows.Count > 0)
            {
                dvValidIsFacility = dsValidIsFacility.Tables[0].DefaultView;
                //if (Session["RoleName"].ToString() == "BUSINESS HEAD")
                dvValidIsFacility.RowFilter = "FacilityId <> 1";
                if (dvValidIsFacility.Count > 0)
                {
                    chkValidIsFacility.DataSource = dvValidIsFacility;
                    chkValidIsFacility.DataTextField = "FacilityName";
                    chkValidIsFacility.DataValueField = "FacilityId";
                    chkValidIsFacility.DataBind();
                }
            }
        }

        /// <summary>
        /// This Method is used to Bind Service Group names 
        /// </summary>
        private void BindServiceGroup()
        {
            chkServiceGroup.Items.Clear();
            DataSet dsServiceGroupy = new BL_ServiceGroup().GetServiceGroup(null, null, true);
            DataView dvValidIsFacility = new DataView();
            if (dsServiceGroupy.Tables.Count > 0 && dsServiceGroupy != null && dsServiceGroupy.Tables[0].Rows.Count > 0)
            {
                dvValidIsFacility = dsServiceGroupy.Tables[0].DefaultView;
                if (dvValidIsFacility.Count > 0)
                {
                    chkServiceGroup.DataSource = dvValidIsFacility;
                    chkServiceGroup.DataTextField = "ServiceGroup";
                    chkServiceGroup.DataValueField = "ServiceGroupId";
                    chkServiceGroup.DataBind();
                }
            }
        }

        /// <summary>
        /// This Method is used to Generate Coupon
        /// </summary>
        /// <returns>This method returns string</returns>
        public static string GenerateCoupon()
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(4);
            for (int i = 0; i < 4; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            string chars = "0123456789";
            StringBuilder result1 = new StringBuilder(2);
            for (int i = 0; i < 2; i++)
            {
                result1.Append(chars[random.Next(chars.Length)]);
            }
            string CouponNobr = string.Concat(result, result1).ToUpper();
            return CouponNobr;
        }

        /// <summary>
        /// This Method is used to Add Coupon
        /// </summary>
        private void AddCoupon()
        {
            modlpopup.Show();
            int Count = 0;
            string FacilityId = null;
            foreach (ListItem li in chkValidIsFacility.Items)
            {
                if (li.Selected == true)
                {
                    FacilityId += Convert.ToString(li.Value + ",");
                    Count = 1;
                }
            }
            if (Count == 0)
            {
                lblMsg.Text = "Select at least one Branch";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                chkValidIsFacility.Focus();
                return;
            }
            int ServiceGroupCount = 0;
            string ServiceGroupId = null;
            foreach (ListItem li in chkServiceGroup.Items)
            {
                if (li.Selected == true)
                {
                    ServiceGroupId += Convert.ToString(li.Value + ",");
                    ServiceGroupCount = 1;
                }
            }
            if (ServiceGroupCount == 0)
            {
                lblMsg.Text = "Select at least one Service Group";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                chkServiceGroup.Focus();
                return;
            }
            int CouponNo = Convert.ToInt32(txtCoupons.Text);
            BE_Mas_Coupon becoupon = new BE_Mas_Coupon();
            becoupon.CouponAmount = Convert.ToInt32(txtCouponAmount.Text.Trim());
            becoupon.ValidFrom = Convert.ToDateTime(txtValidFrom.Text);
            becoupon.ValidTo = Convert.ToDateTime(txtValidTo.Text);
            if (FacilityId != null)
                FacilityId = FacilityId.Substring(0, FacilityId.Length - 1);
            becoupon.FacilityId = FacilityId;
            if (ServiceGroupId != null)
                ServiceGroupId = ServiceGroupId.Substring(0, ServiceGroupId.Length - 1);
            becoupon.ServiceGroupId = ServiceGroupId;
            becoupon.CreatedBy = Session["UserId"].ToString();
            becoupon.BatchName = txtBatchName.Text.Trim();
            //DataSet dsBatchId = new BL_Mas_Coupon().GetBatchId();
            //int? BatchId = 0;
            for (int i = 1; i <= CouponNo; i++)
            {
                string codeNo = GenerateCoupon();
                while (codeNo.ToUpper() == becoupon.CouponNo)
                    codeNo = GenerateCoupon();
                // becoupon.BatchId = BatchId;
                becoupon.CouponNo = codeNo.ToUpper();
                DataSet dsBatch = new BL_Mas_Coupon().InsertUpdateCoupon(becoupon);
                //BatchId = Convert.ToInt32(dsBatch.Tables[0].Rows[0][0]);
            }
            lblMsg.Text = "Saved successfully";
            lblMsg.ForeColor = System.Drawing.Color.Green;
            ClearControls();
            BindCoupon(false);
            hbtntext.Value = "Save";
            btnSave.Text = "Save";
            btnSave.ToolTip = "Save";
            txtCoupons.Focus();
        }

        #endregion

        #region Page Event Handler

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (Session["UserID"] == null || (Session["UserID"] != null && Convert.ToString(Session["UserID"]) == string.Empty))
                    Response.Redirect(ConfigurationManager.AppSettings["LoginUrl"]);
                else
                {
                    txtCoupons.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnSave.ClientID + "', event)");
                    if (!Page.IsPostBack)
                    {
                        ViewState["SortDirection"] = "ASC";
                        ClearControls();
                        BindCoupon(true);
                        btnAddCoupon.Focus();
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
                BindCoupon(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvCoupon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), gvCoupon);
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
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvCoupon_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataView dv = Cache["Coupon"] as DataView;
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
                    gvCoupon.DataSource = dv;
                    gvCoupon.DataBind();
                    gvCoupon.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void gvCoupon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCoupon.PageIndex = e.NewPageIndex;
                BindCoupon(false);
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void chkValidIsFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                int count = 0;
                chkAll.Checked = false;
                foreach (ListItem li in chkValidIsFacility.Items)
                    if (li.Selected)
                        count += 1;
                if (count == chkValidIsFacility.Items.Count)
                    chkAll.Checked = true;
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void chkServiceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                int count = 0;
                chkServiceGroupAll.Checked = false;
                foreach (ListItem li in chkServiceGroup.Items)
                    if (li.Selected)
                        count += 1;
                if (count == chkServiceGroup.Items.Count)
                    chkServiceGroupAll.Checked = true;
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnAddCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                hbtntext.Value = "Save";
                btnSave.Text = "Save";
                btnSave.ToolTip = "Save";
                modlpopup.Show();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                if (hbtntext.Value == "Save")
                    AddCoupon();
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