////-----------------------------------------------------------------------
// <copyright file="ManageRoles.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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

#region Namespaces
using System;
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
    public partial class ManageRoles : BasePage
    {
        #region Variable Declaration
        Boolean Block;
        #endregion

        #region User Defined Methods
        /// <summary>
        /// This Method is used to Clear Controls
        /// </summary>
        private void ClearControls()
        {
            rbtnStatus.SelectedIndex = 0;
            txtRole.Text = string.Empty;
            txtReason.Text = string.Empty;
            txtReason.Enabled = false;
        }
        /// <summary>
        /// This Method is used to Bind Roles GridView
        /// </summary>
        public void BindGridRoles()
        {
            DataSet dsRoles = null;
            if (ddlStatus.SelectedIndex > 0)
                dsRoles = new BL_Role().GetRoles(null, txtSearch.Text.Trim().ToUpper(), Convert.ToBoolean(ddlStatus.SelectedValue.Trim().ToUpper()));
            else
                dsRoles = new BL_Role().GetRoles(null, txtSearch.Text.Trim().ToUpper(), null);
            Cache["Roles"] = dsRoles;
            if (dsRoles != null && dsRoles.Tables.Count > 0 && dsRoles.Tables[0].Rows.Count > 0)
            {
                grdRoles.DataSource = dsRoles;
                grdRoles.DataBind();
                lblTotalRecords.Visible = true;
                lblTotalRecords.Text = "Total Records: " + dsRoles.Tables[0].Rows.Count.ToString().ToUpper();
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
                        grdRoles.Sort(ViewState["SortExpression"].ToString(), SortDirection.Ascending);
                    }
                }
            }
            else
            {
                lblTotalRecords.Visible = false;
                grdRoles.DataSource = null;
                grdRoles.DataBind();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hfbtntext.Value = "Add";
            }
        }
        /// <summary>
        /// This Method is used to Bind Roles
        /// </summary>
        private void BindRoles()
        {
            ddlRole.Items.Clear();
            DataSet dsRoles = new BL_Role().GetRoles(null, null, true);
            if (dsRoles != null && dsRoles.Tables.Count > 0 && dsRoles.Tables[0].Rows.Count > 0)
            {
                ddlRole.DataSource = dsRoles;
                ddlRole.DataTextField = "RoleName";
                ddlRole.DataValueField = "RoleId";
                ddlRole.DataBind();
            }
            ddlRole.Items.Insert(0, "Select Role");
        }
        /// <summary>
        /// This Method is used to Add Role
        /// </summary>
        private void AddRole()
        {
            modlpopup.Show();
            DataSet dsRole = new BL_Role().GetRoles(null, null, null);
            DataRow[] rows = dsRole.Tables[0].Select("RoleName ='" + txtRole.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Role already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtRole.Text = string.Empty;
                txtRole.Focus();
                return;
            }
            BE_Role beRole = new BE_Role();
            beRole.RoleId = 0;
            beRole.RoleName = txtRole.Text.Trim().ToUpper();
            beRole.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beRole.Status = true;
                beRole.Reason = string.Empty;
            }
            else
            {
                beRole.Status = false;
                beRole.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Role().InsertUpdateRole(beRole);
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
            btnAdd.Text = "Add";
            btnAdd.ToolTip = "Add";
            hfbtntext.Value = "Add";
            BindGridRoles();
            BindRoles();
            txtRole.Focus();
        }
        /// <summary>
        /// This Method is used to Update Role
        /// </summary>
        private void UpdateRole()
        {
            modlpopup.Show();
            DataSet dsRole = new BL_Role().GetRoles(null, null, null);
            DataRow[] rows = dsRole.Tables[0].Select("RoleId <> " + Convert.ToInt32(hfRoleId.Value) + "AND RoleName ='" + txtRole.Text.Trim().ToUpper() + "'");
            if (rows.Count() > 0)
            {
                lblMsg.Text = "Role already exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtRole.Text = string.Empty;
                txtRole.Focus();
                return;
            }
            BE_Role beRole = new BE_Role();
            beRole.RoleId = Convert.ToInt32(hfRoleId.Value.Trim().ToUpper());
            beRole.RoleName = txtRole.Text.Trim().ToUpper();
            beRole.CreatedBy = Convert.ToString(Session["UserId"]);
            if (rbtnStatus.SelectedIndex == 0)
            {
                beRole.Status = true;
                beRole.Reason = string.Empty;
            }
            else
            {
                beRole.Status = false;
                beRole.Reason = txtReason.Text.Trim().ToUpper();
            }
            int result = new BL_Role().InsertUpdateRole(beRole);
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
            BindGridRoles();
            BindRoles();
            txtRole.Focus();
        }
        /// <summary>
        /// This Method is used to Menu Tree
        /// </summary>
        private void MenuTree()
        {
            DataSet dsMenu = new BL_Role().GetMenu();
            if (dsMenu != null && dsMenu.Tables.Count > 0 && dsMenu.Tables[0].Rows.Count > 0)
            {
                dsMenu.DataSetName = "Menus";
                dsMenu.Tables[0].TableName = "Menu";
                DataRelation dsRelation = new DataRelation("ParentChild", dsMenu.Tables["Menu"].Columns["MenuId"], dsMenu.Tables["Menu"].Columns["ParentId"], false);
                dsRelation.Nested = true;
                dsMenu.Relations.Add(dsRelation);
            }
        }
        /// <summary>
        /// This Method is used to Expand Parent Nodes
        /// </summary>
        /// <param name="treeNode">This Parameter Belongs to ExpandParentNodes</param>
        private void ExpandParentNodes(TreeNode treeNode)
        {
            if (treeNode != null)
            {
                ExpandParentNodes(treeNode.Parent);
                treeNode.Expand();
            }
        }
        /// <summary>
        /// This Method is used to Get Selected Menu Ids
        /// </summary>
        /// <param name="node">This Parameter Belongs to GetSelectedMenuIds</param>
        /// <param name="MenuIds">This Parameter Belongs to GetSelectedMenuIds</param>
        private void GetSelectedMenuIds(TreeNode node, ref String MenuIds)
        {
            if (node.Checked == true)
                MenuIds += node.Value + ",";
            foreach (TreeNode childnode in node.ChildNodes)
            {
                GetSelectedMenuIds(childnode, ref MenuIds);
            }
        }
        /// <summary>
        /// This Method is used to create Empty Table
        /// </summary>
        private DataTable GeneralEmptyTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("MenuId"));
            dt.Columns.Add(new DataColumn("Edit"));
            dt.Columns.Add(new DataColumn("ReadOnly"));
            return dt;
        }
        #endregion

        #region Page Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                lblAssignScreensMsg.Text = string.Empty;
                txtRole.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                txtReason.Attributes.Add("onkeyup", "CheckFirstChar(this); return controlEnter('" + btnAdd.ClientID + "', event)");
                if (!Page.IsPostBack)
                {
                    ViewState["SortDirection"] = "ASC";
                    BindGridRoles();
                    BindRoles();
                    grdRoles.Focus();
                }
            }
            catch (Exception ex)
            {
                if (!(ex is System.Threading.ThreadAbortException))
                    WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridRoles();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hfbtntext.Value = "Add";
                txtRole.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void grdRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        CommonUIFunctions.AddSortImage(e.Row, ViewState["SortExpression"].ToString(), ViewState["SortDirection"].ToString(), grdRoles);
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
                    Label lblRoleName = (Label)e.Row.FindControl("lblRoleName");
                    ImageButton lnkedit = (ImageButton)e.Row.FindControl("lnkedit");
                    if (lblRoleName.Text.Trim().ToUpper() == "ADMINISTRATOR")
                        lnkedit.Visible = false;
                    else
                        lnkedit.Visible = true;
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void grdRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "NEdit")
                {
                    DataSet dsRoles = new BL_Role().GetRoles(Convert.ToInt32(e.CommandArgument.ToString()), null, null);
                    if (dsRoles != null && dsRoles.Tables.Count > 0 && dsRoles.Tables[0].Rows.Count > 0)
                    {
                        modlpopup.Show();
                        btnAdd.Text = "Update";
                        btnAdd.ToolTip = "Update";
                        hfbtntext.Value = "Update";
                        hfRoleId.Value = e.CommandArgument.ToString().ToUpper();
                        txtRole.Text = dsRoles.Tables[0].Rows[0]["RoleName"].ToString().ToUpper();
                        if (Convert.ToBoolean(dsRoles.Tables[0].Rows[0]["Status"].ToString()))
                        {
                            rbtnStatus.SelectedIndex = 0;
                            txtReason.Text = string.Empty;
                            txtReason.Enabled = false;
                        }
                        else
                        {
                            rbtnStatus.SelectedIndex = 1;
                            txtReason.Enabled = true;
                            txtReason.Text = dsRoles.Tables[0].Rows[0]["Reason"].ToString().ToUpper();
                        }
                        txtRole.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void grdRoles_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataSet ds = Cache["Roles"] as DataSet;
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
                    grdRoles.DataSource = dv;
                    grdRoles.DataBind();
                    grdRoles.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void grdRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdRoles.PageIndex = e.NewPageIndex;
                BindGridRoles();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void rbtnStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                txtRole.Focus();
                txtReason.Text = string.Empty;
                if (rbtnStatus.SelectedIndex == 1)
                {
                    txtReason.Enabled = true;
                    if (hfRoleId.Value != string.Empty)
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
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfbtntext.Value == "Add")
                    AddRole();
                else if (hfbtntext.Value == "Update")
                    UpdateRole();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                modlpopup.Show();
                ClearControls();
                btnAdd.Text = "Add";
                btnAdd.ToolTip = "Add";
                hfbtntext.Value = "Add";
                txtRole.Focus();
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlRole.SelectedIndex == 0)
                {
                    btnSave.Visible = false;
                    return;
                }
                MenuTree();
                DataSet dsEmpApp = new BL_Role().GetEmpAppByRoleId(Convert.ToInt32(ddlRole.SelectedValue));
                if (dsEmpApp != null && dsEmpApp.Tables.Count > 0 && dsEmpApp.Tables[0].Rows.Count > 0)
                {
                    gvMenu.Visible = true;
                    gvMenu.DataSource = dsEmpApp.Tables[0];
                    gvMenu.DataBind();
                }
                btnSave.Visible = true;
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void TreeViewMenu_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            try
            {
                if (Block == true)
                    return;
                Block = true;

                ExpandParentNodes(e.Node.Parent);
                e.Node.Expand();
                Block = false;
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRole.SelectedIndex > 0)
                {
                    DataTable dt = GeneralEmptyTable();
                    string MenuIds = string.Empty;
                    foreach (GridViewRow gvrow in gvMenu.Rows)
                    {
                        RadioButton rbtnEdit = (RadioButton)gvrow.FindControl("rbtnEdit");
                        RadioButton rbtnReadOnly = (RadioButton)gvrow.FindControl("rbtnReadOnly");
                        Label lblParentId = (Label)gvrow.FindControl("lblParentId");
                        Label lblMenuId = (Label)gvrow.FindControl("lblMenuId");

                        DataRow dr = null;
                        DataRow[] rowsMenu = dt.Select("MenuId='" + lblMenuId.Text + "'");
                        if (rowsMenu.Count() <= 0)
                        {
                            MenuIds = lblMenuId.Text;
                            dr = dt.NewRow();
                            if (rbtnEdit.Checked == true)
                            {
                                dr["MenuId"] = lblMenuId.Text;
                                dr["Edit"] = true;
                                dr["ReadOnly"] = false;
                                dt.Rows.Add(dr);
                                if (lblParentId.Text != string.Empty)
                                {
                                    DataRow[] rows = dt.Select("MenuId='" + lblParentId.Text + "'");
                                    if (rows.Count() == 0)
                                    {
                                        dr = dt.NewRow();
                                        dr["MenuId"] = lblParentId.Text;
                                        dr["Edit"] = true;
                                        dr["ReadOnly"] = false;
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                            else if (rbtnReadOnly.Checked == true)
                            {
                                dr["MenuId"] = lblMenuId.Text;
                                dr["Edit"] = false;
                                dr["ReadOnly"] = true;
                                dt.Rows.Add(dr);
                                if (lblParentId.Text != string.Empty)
                                {
                                    DataRow[] rows = dt.Select("MenuId='" + lblParentId.Text + "'");
                                    if (rows.Count() == 0)
                                    {
                                        dr = dt.NewRow();
                                        dr["MenuId"] = lblParentId.Text;
                                        dr["Edit"] = true;
                                        dr["ReadOnly"] = false;
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                            else
                            {
                                dr["MenuId"] = lblMenuId.Text;
                                dr["Edit"] = null;
                                dr["ReadOnly"] = null;
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                    if (MenuIds == "")
                    {
                        lblAssignScreensMsg.Text = "Select at least one screen to assign";
                        lblAssignScreensMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            String Message = string.Empty;
                            foreach (DataRow drs in dt.Rows)
                            {
                                if (drs["MenuId"].ToString() != string.Empty)
                                {
                                    BE_Role beRole = new BE_Role();
                                    beRole.RoleId = Convert.ToInt32(ddlRole.SelectedValue);
                                    beRole.MenuIds = Convert.ToInt32(drs["MenuId"]).ToString();
                                    if (drs["Edit"].ToString() != string.Empty)
                                        beRole.Edit = Convert.ToBoolean(drs["Edit"].ToString());
                                    if (drs["ReadOnly"].ToString() != string.Empty)
                                        beRole.ReadOnly = Convert.ToBoolean(drs["ReadOnly"].ToString());
                                    beRole.CreatedBy = Convert.ToString(Session["UserID"]);
                                    Message = new BL_Role().InsertUpdateEmpApp(beRole);
                                }
                            }
                            if (Message.ToUpper() == "SUCCESS")
                            {
                                lblAssignScreensMsg.Text = "Screens added/ modified successfully to the selected role";
                                lblAssignScreensMsg.ForeColor = System.Drawing.Color.Green;
                                ddlRole.SelectedIndex = 0;
                                gvMenu.Visible = false;
                                btnSave.Visible = false;
                            }
                            else
                            {
                                lblAssignScreensMsg.Text = "Failed to add/ modify screens to the selected role";
                                lblAssignScreensMsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
                else
                {
                    lblAssignScreensMsg.Text = "Select role";
                    lblAssignScreensMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void gvMenu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblEdit = (Label)e.Row.Cells[0].FindControl("lblEdit");
                    Label lblReadOnly = (Label)e.Row.Cells[0].FindControl("lblReadOnly");
                    RadioButton rbtnEdit = (RadioButton)e.Row.Cells[0].FindControl("rbtnEdit");
                    RadioButton rbtnReadOnly = (RadioButton)e.Row.Cells[0].FindControl("rbtnReadOnly");
                    if (lblEdit.Text != string.Empty)
                    {
                        if (Convert.ToBoolean(lblEdit.Text) == true)
                        {
                            rbtnEdit.Checked = true;
                            rbtnReadOnly.Checked = false;
                        }
                    }
                    if (lblReadOnly.Text != string.Empty)
                    {
                        if (Convert.ToBoolean(lblReadOnly.Text) == true)
                        {
                            rbtnEdit.Checked = false;
                            rbtnReadOnly.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserID"]));
            }
        }

        protected void gvMenu_DataBinding(object sender, EventArgs e)
        {
            try
            {
                GridViewGroup first = new GridViewGroup(gvMenu, null, "MenuName");
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void imgbtnReset_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imgbtnReset = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)imgbtnReset.NamingContainer;
                RadioButton rbtnEdit = (RadioButton)gvRow.FindControl("rbtnEdit");
                RadioButton rbtnReadOnly = (RadioButton)gvRow.FindControl("rbtnReadOnly");
                rbtnEdit.Checked = false;
                rbtnReadOnly.Checked = false;
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Session[""].ToString());
            }
        }

        #endregion
    }
}