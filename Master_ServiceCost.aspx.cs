////-----------------------------------------------------------------------
// <copyright file="Master_ServiceCost.aspx.cs" company="Dhii Health Tech Pvt. Ltd.">
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

namespace DhiiLifeSpring.Application.Masters
{
    #region Namespaces
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DhiiLifeSpring.AppInfrastructure;
    using DhiiLifeSpring.BusinessEntities;
    using DhiiLifeSpring.BusinessLayer;
    using System.Data;
    using System.Collections;
    using System.Configuration;


    #endregion

    public partial class Master_ServiceCost : BasePage
    {
        #region Variable Declaration
        ArrayList ParameterArray = new ArrayList();
        DataTable dtServices = new DataTable();
        string userID;
        BL_Tariff blTariff = null;
        BE_ServiceCost beServiceCost = new BE_ServiceCost();

        #endregion

        #region Event handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserID"] == null || (Session["UserID"] != null && Convert.ToString(Session["UserID"]) == string.Empty))
                    Response.Redirect(ConfigurationManager.AppSettings["LoginUrl"]);
                else
                {
                    userID = Session["UserId"].ToString().ToUpper();
                    if (!Page.IsPostBack)
                    {
                        BindServiceGroup();
                        BindTariffs();
                        BindFacility(null);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(ex is System.Threading.ThreadAbortException))
                    WriteLogItem("Page_Load --- " + ex.Message, ex.StackTrace, Session["UserId"].ToString());
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            int ServiceGroupId = 0;
            int TariffId = 0;
            int BranchId = 0;
            if (Convert.ToString(Request.Params[ddlTariff.UniqueID]) == null || Convert.ToString(Request.Params[ddlTariff.UniqueID]) == "Select Tariff")
            {
                gvServiceCost.DataSource = null;
                gvServiceCost.DataBind();
                ddlServiceGroup.SelectedIndex = -1;
                ddlServices.Items.Clear();

                return;
            }
            if (Convert.ToString(Request.Params[ddlServiceGroup.UniqueID]) == null || Convert.ToString(Request.Params[ddlServiceGroup.UniqueID]) == "Select Service")
            {
                ddlServices.Items.Clear();
                return;
            }
            if (Request.Params[ddlTariff.UniqueID] != "Select")
            {
                TariffId = Convert.ToInt32(Request.Params[ddlTariff.UniqueID]);
            }

            if (Request.Params[ddlServiceGroup.UniqueID] != "Select Service")
            {
                ServiceGroupId = Convert.ToInt32(Request.Params[ddlServiceGroup.UniqueID]);
            }
            if (Request.Params[ddlBranch.UniqueID] != "Select")
            {
                BranchId = Convert.ToInt32(Request.Params[ddlBranch.UniqueID]);
            }
            if (TariffId == null || TariffId == 0 || ServiceGroupId == null || ServiceGroupId == 0 || BranchId == null || BranchId == 0)
                return;
            beServiceCost.TariffId = TariffId;
            beServiceCost.ServiceGroupId = ServiceGroupId;
            beServiceCost.ClinicId = BranchId;
            DataSet ds = new DataSet();
            ds = new BL_ServiceCost().GetTariffByServiceNameInGrid(beServiceCost);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                //Iterate through the columns of the datatable to set the data bound field dynamically.
                foreach (DataColumn col in dt.Columns)
                {
                    if (col == dt.Columns[1])
                        continue;
                    if (col == dt.Columns[0])
                        continue;
                    if (col == dt.Columns[2])
                        continue;
                    //if (col == dt.Columns[3])
                    //    continue;
                    //Declare the bound field and allocate memory for the bound field.
                    TemplateField bfield = new TemplateField();

                    //Initalize the DataField value.
                    bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName, dt.Columns[1].ColumnName);

                    //Initialize the HeaderText field value.
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, dt.Columns[1].ColumnName);

                    TemplateField bfield1 = new TemplateField();

                    //Initalize the DataField value.
                    bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName, dt.Columns[0].ColumnName);

                    //Initialize the HeaderText field value.
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, dt.Columns[0].ColumnName);

                    TemplateField bfield2 = new TemplateField();

                    //Initalize the DataField value.
                    bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName, dt.Columns[2].ColumnName);

                    //Initialize the HeaderText field value.
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, dt.Columns[2].ColumnName);
                    TemplateField bfield3 = new TemplateField();

                    //Initalize the DataField value.
                    //   bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName, dt.Columns[3].ColumnName);

                    //Initialize the HeaderText field value.
                    // bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, dt.Columns[3].ColumnName);



                    //Add the newly created bound field to the GridView.
                    gvServiceCost.Columns.Add(bfield);
                    gvServiceCost.Columns.Add(bfield1);
                    gvServiceCost.Columns.Add(bfield2);
                    // gvServiceCost.Columns.Add(bfield3);
                }
            }
        }

        protected void ddlServiceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTariff.SelectedIndex <= 0)
                {
                    ddlServices.Items.Clear();
                    ddlServices.Items.Insert(0, "Select Service");

                    lblError.Text = "Please Select Tariff";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (ddlServiceGroup.SelectedIndex > 0)
                {
                    lblError.Text = "";
                    ddlServices.Items.Clear();
                    DataSet ds = new DataSet();
                    ds = new BL_ServiceCost().GetAllServicesBySubGroupId(Convert.ToInt32(ddlServiceGroup.SelectedValue));
                    lblError.Text = "";
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlServices.DataSource = ds.Tables[0];
                        ddlServices.DataTextField = "TestName";
                        ddlServices.DataValueField = "SERVICEID";
                        ddlServices.DataBind();
                        ddlServices.Items.Insert(0, "Select Service");
                    }
                    else
                    {
                        ddlServices.Items.Clear();
                        ddlServices.Items.Insert(0, "Select Service");
                    }
                }
                else
                {
                    ddlServices.Items.Clear();
                    ddlServices.Items.Insert(0, "Select Service");
                    lblError.Text = "";
                }
            }
            catch (Exception ex)
            {
                WriteLogItem("ddlServiceGroup_SelectedIndexChanged --- " + ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void ddlTariff_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (ddlTariff.SelectedIndex > 0)
            {
                //BindService();
            }
            else
            {
                ddlServiceGroup.SelectedIndex = 0;
                ddlServices.Items.Clear();
                ddlServices.Items.Insert(0, "Select Service");

                gvServiceCost.DataSource = null;
                gvServiceCost.DataBind();
            }


        }

        protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                if (ddlTariff.SelectedIndex > 0 && ddlServiceGroup.SelectedIndex > 0)
                {
                    // BindService();
                }
                else if (ddlTariff.SelectedIndex <= 0)
                {
                    lblError.Text = "Please select tariff";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                else if (ddlServiceGroup.SelectedIndex <= 0)
                {
                    lblError.Text = "Please select service group";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["UserId"]));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                lblTotalRecords.Visible = false;
                if (ddlTariff.SelectedIndex > 0 && ddlServiceGroup.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0)
                {
                    System.Threading.Thread.Sleep(2000);
                    lblBranchText.Visible = true;
                    ChkBranch.Items.Clear();
                    DataSet dsfacility = new BL_Facility().GetFacilities();
                    DataView dvFacility = new DataView();
                    if (dsfacility.Tables.Count > 0 && dsfacility != null && dsfacility.Tables[0].Rows.Count > 0)
                    {
                        dvFacility = dsfacility.Tables[0].DefaultView;
                        dvFacility.RowFilter = "Status= true and Corporate is null and FacilityId<>" + Convert.ToInt32(ddlBranch.SelectedValue) + " ";
                        if (dvFacility.Count > 0)
                        {
                            ChkBranch.DataSource = dvFacility;
                            ChkBranch.DataTextField = "FacilityName";
                            ChkBranch.DataValueField = "FacilityId";
                            ChkBranch.DataBind();

                        }
                    }
                    BindService();
                }
                else if (ddlTariff.SelectedIndex <= 0)
                {
                    ddlTariff.Focus();
                    lblError.Text = "Please select tariff";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblTotalRecords.Visible = false;
                }
                else if (ddlServiceGroup.SelectedIndex <= 0)
                {
                    ddlServiceGroup.Focus();
                    lblError.Text = "Please select service group";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblTotalRecords.Visible = false;
                }

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
                if (gvServiceCost.Rows.Count > 0)
                {
                    DataTable dtDelete = new DataTable("Delete");
                    dtDelete.Columns.Add("Id");
                    DataRow drDelete;
                    DataTable dt = new DataTable("Tariff");
                    DataSet dsDelete = new DataSet();
                    dt.Columns.Add("ServiceName");
                    dt.Columns.Add("ServiceId");
                    dt.Columns.Add("Bedcategory");
                    dt.Columns.Add("Cost");
                    dt.Columns.Add("Checked");
                    DataRow dr;
                    #region Selected Clinic
                    foreach (GridViewRow row in gvServiceCost.Rows)
                    {
                        Label lblServiceCode = (Label)row.FindControl("lblServiceCode");
                        Label lblServiceName = (Label)row.FindControl("lblServiceName");
                        CheckBox ChkChecked = (CheckBox)row.FindControl("chkDelete");
                        foreach (TableCell tc in row.Cells)
                        {
                            foreach (Control c in tc.Controls)
                            {
                                if (c is TextBox)
                                {
                                    TextBox txt = (TextBox)c;
                                    if (!string.IsNullOrEmpty(txt.Text))
                                    {
                                        dr = dt.NewRow();
                                        dr["ServiceName"] = lblServiceName.Text.Trim().ToUpper();
                                        dr["ServiceId"] = lblServiceCode.Text.Trim().ToUpper();
                                        dr["Bedcategory"] = txt.ID.ToString().ToUpper();
                                        dr["Cost"] = txt.Text.Trim().ToUpper();
                                        if (ChkChecked.Checked == true)
                                        {
                                            dr["Checked"] = "1";
                                        }
                                        else
                                            dr["Checked"] = "0";

                                        if (txt.ID.ToString() == "DOCSHARE")
                                        {
                                            if (txt.Text.Trim().ToUpper() != string.Empty)
                                            {
                                                if (Convert.ToInt32(txt.Text.Trim().ToUpper()) > 100)
                                                {
                                                    txt.Focus();
                                                    lblError.Text = "DOCSHARE should not be greaterthan 100";
                                                    lblError.ForeColor = System.Drawing.Color.Red;
                                                    return;
                                                }
                                            }
                                        }

                                        dt.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        beServiceCost.ClinicId = Convert.ToInt32(ddlBranch.SelectedValue);
                                        beServiceCost.TariffId = Convert.ToInt32(ddlTariff.SelectedValue);
                                        beServiceCost.ServiceId = Convert.ToInt32(lblServiceCode.Text);
                                        beServiceCost.ServiceName = lblServiceName.Text.Trim().ToUpper().ToString().ToUpper();
                                        beServiceCost.Ward = txt.ID.ToString().ToUpper();
                                        dsDelete = new BL_ServiceCost().GetServiceCost(beServiceCost);
                                        if (dsDelete != null && dsDelete.Tables.Count > 0 && dsDelete.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow drRow in dsDelete.Tables[0].Rows)
                                            {
                                                drDelete = dtDelete.NewRow();
                                                drDelete["Id"] = drRow["Id"].ToString().ToUpper();
                                                dtDelete.Rows.Add(drDelete);
                                            }
                                        }
                                        foreach (ListItem li in ChkBranch.Items)
                                        {
                                            if (li.Selected)
                                            {
                                                if (ChkChecked.Checked == true)
                                                {
                                                    beServiceCost.ClinicId = Convert.ToInt32(li.Value);
                                                    beServiceCost.TariffId = Convert.ToInt32(ddlTariff.SelectedValue);
                                                    beServiceCost.ServiceId = Convert.ToInt32(lblServiceCode.Text);
                                                    beServiceCost.ServiceName = lblServiceName.Text.Trim().ToUpper().ToString().ToUpper();
                                                    beServiceCost.Ward = txt.ID.ToString().ToUpper();
                                                    dsDelete = new BL_ServiceCost().GetServiceCost(beServiceCost);
                                                    if (dsDelete != null && dsDelete.Tables.Count > 0 && dsDelete.Tables[0].Rows.Count > 0)
                                                    {
                                                        foreach (DataRow drRow in dsDelete.Tables[0].Rows)
                                                        {
                                                            drDelete = dtDelete.NewRow();
                                                            drDelete["Id"] = drRow["Id"].ToString().ToUpper();
                                                            dtDelete.Rows.Add(drDelete);
                                                        }
                                                    }
                                                }

                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    if (dtDelete.Rows.Count > 0)
                    {

                        foreach (DataRow drdelete in dtDelete.Rows)
                        {
                            new BL_ServiceCost().Delete(Convert.ToInt32(drdelete["Id"]));
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        //using (StringWriter swStringWriter = new StringWriter())
                        //{
                        //    dt.TableName = "Tariff";
                        //    dt.WriteXml(swStringWriter);
                        //    beServiceCost.Tariffdetails = swStringWriter.ToString().ToUpper();
                        //}
                        int result = 0;
                        foreach (DataRow Drcost in dt.Rows)
                        {
                            // beServiceCost.Createdby = userID;
                            // beServiceCost.TariffId = Convert.ToInt32(ddlTariff.SelectedValue);
                            Int32 TariffId = Convert.ToInt32(ddlTariff.SelectedValue);
                            Int32 ServiceID = Convert.ToInt32(Drcost["ServiceID"]);
                            string Bedcategory = Drcost["Bedcategory"].ToString().ToUpper();
                            string createOrUpdateBy = userID;
                            decimal cost = Convert.ToDecimal(Drcost["Cost"].ToString());
                            int ClinicId = Convert.ToInt32(ddlBranch.SelectedValue);
                            result = new BL_ServiceCost().InsertUpdateServiceCost(TariffId, ServiceID, Bedcategory, createOrUpdateBy, cost, ClinicId);
                            if (Drcost["Checked"].ToString() == "1")
                            {
                                foreach (ListItem li in ChkBranch.Items)
                                {
                                    if (li.Selected)
                                    {
                                        result = new BL_ServiceCost().InsertUpdateServiceCost(TariffId, ServiceID, Bedcategory, createOrUpdateBy, cost, Convert.ToInt32(li.Value));
                                    }
                                }
                            }
                        }
                        if (result >= 0)
                        {
                            lblError.Text = "Tariff configured successfully";
                            lblError.ForeColor = System.Drawing.Color.Green;
                            ddlServices.Items.Clear();
                            ddlBranch.SelectedIndex = 0;
                            ddlServices.Items.Insert(0, "Select");
                            ddlServiceGroup.SelectedIndex = 0;
                            ddlTariff.SelectedIndex = 0;
                            gvServiceCost.DataSource = null;
                            gvServiceCost.DataBind();
                            lblTotalRecords.Text = string.Empty;
                            ChkBranch.Items.Clear();
                            lblBranchText.Visible = false;
                        }
                    }
                    else
                    {
                        lblError.Text = "Configure atleast one tariff";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblError.Text = "No rows selected...";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                WriteLogItem("btnAdd --- " + ex.Message, ex.StackTrace, Session["UserId"].ToString());
            }

        }

        #endregion

        #region User Define Methods

        /// <summary>
        /// This Method is used to Bind Tariffs
        /// </summary>
        void BindTariffs()
        {
            ddlTariff.Items.Clear();
            DataSet dsTariff = new DataSet();
            dsTariff = new BL_Tariff().GetTariff(null, null, true);

            if (dsTariff != null && dsTariff.Tables.Count > 0 && dsTariff.Tables[0].Rows.Count > 0)
            {
                ddlTariff.Items.Clear();
                ddlTariff.DataSource = dsTariff.Tables[0];
                ddlTariff.DataTextField = "Tariff";
                ddlTariff.DataValueField = "TariffId";
                ddlTariff.DataBind();
                ddlTariff.Items.Insert(0, "Select");

            }
            else
                ddlTariff.Items.Insert(0, "Select");
        }

        /// <summary>
        /// This Method is used to Bind Service Group
        /// </summary>
        void BindServiceGroup()
        {
            ddlServiceGroup.Items.Clear();

            DataSet dsSericeGroup = new DataSet();
            dsSericeGroup = new BL_ServiceGroup().GetServiceGroup(null, null, true);
            if (dsSericeGroup != null && dsSericeGroup.Tables.Count > 0 && dsSericeGroup.Tables[0].Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = dsSericeGroup.Tables[0].DefaultView;
                dv.RowFilter = "ServiceGroup<>'OBSTETRICS AND GYNAECOLOGY' and ServiceGroup<>'GENERAL'";
                if (dv.Count > 0)
                {
                    ddlServiceGroup.Items.Clear();
                    ddlServiceGroup.DataSource = dsSericeGroup.Tables[0];
                    ddlServiceGroup.DataTextField = "ServiceGroup";
                    ddlServiceGroup.DataValueField = "ServiceGroupId";
                    ddlServiceGroup.DataBind();
                    ddlServiceGroup.Items.Insert(0, "Select Service");
                }
                else
                {
                    ddlServiceGroup.Items.Insert(0, "Select Service");

                }

            }
            else
                ddlServiceGroup.Items.Insert(0, "Select Service");
        }

        /// <summary>
        /// This Method is used to Bind Service
        /// </summary>
        protected void BindService()
        {
            gvServiceCost.DataSource = null;
            gvServiceCost.DataBind();

            if (ddlTariff.SelectedIndex > 0)
            {
                beServiceCost.TariffId = Convert.ToInt32(ddlTariff.SelectedValue);
                beServiceCost.ServiceGroupId = Convert.ToInt32(ddlServiceGroup.SelectedValue);
                beServiceCost.ClinicId = Convert.ToInt32(ddlBranch.SelectedValue);
                DataSet ds = new DataSet();
                ds = new BL_ServiceCost().GetTariffByServiceNameInGrid(beServiceCost);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataView dv = new DataView();
                    dv = ds.Tables[0].DefaultView;
                    if (ddlServiceGroup.SelectedIndex > 0 && ddlServices.SelectedIndex > 0)
                        dv.RowFilter = "ServiceGroupId='" + ddlServiceGroup.SelectedValue.ToString() + "' and ServiceId='" + ddlServices.SelectedValue.ToString() + "'";
                    else if (ddlServiceGroup.SelectedIndex > 0)
                        dv.RowFilter = "ServiceGroupId='" + ddlServiceGroup.SelectedValue.ToString() + "'";
                    else if (ddlServices.SelectedIndex > 0)
                        dv.RowFilter = "ServiceId='" + ddlServices.SelectedValue.ToString() + "' ";
                    if (dv.Count > 0)
                    {
                        gvServiceCost.DataSource = dv;
                        gvServiceCost.DataBind();
                        DataTable dtnew = dv.ToTable();
                        lblTotalRecords.Visible = true;
                        lblTotalRecords.Text = "Total Records: " + dtnew.Rows.Count;
                    }
                }
            }
        }

        /// <summary>
        /// This Method is used to Bind Facility
        /// </summary>
        /// <param name="FacilityId">This Parameter Belongs to BindFacility</param>
        private void BindFacility(Int32? FacilityId)
        {
            ddlBranch.Items.Clear();
            DataSet dsfacility = new BL_Facility().GetFacilities();
            DataView dvFacility = new DataView();
            if (dsfacility.Tables.Count > 0 && dsfacility != null && dsfacility.Tables[0].Rows.Count > 0)
            {
                dvFacility = dsfacility.Tables[0].DefaultView;
                if (dvFacility.Count > 0)
                {
                    dvFacility.RowFilter = "Status= true and Corporate is null";
                    ddlBranch.DataSource = dvFacility;
                    ddlBranch.DataTextField = "FacilityName";
                    ddlBranch.DataValueField = "FacilityId";
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, "Select");
                }
                else
                    ddlBranch.Items.Insert(0, "Select");
            }
            else
            {
                ddlBranch.Items.Insert(0, "Select");
            }
        }

        #endregion

        public class GridViewTemplate : ITemplate
        {
            //A variable to hold the type of ListItemType.
            ListItemType _templateType;

            //A variable to hold the column name.
            string _columnName;

            string _tariff;

            //Constructor where we define the template type and column name.
            public GridViewTemplate(ListItemType type, string colname, string tariff)
            {
                //Stores the template type.
                _templateType = type;

                //Stores the column name.
                _columnName = colname;

                _tariff = tariff;
            }

            void ITemplate.InstantiateIn(System.Web.UI.Control container)
            {
                switch (_templateType)
                {
                    case ListItemType.Header:
                        //Creates a new label control and add it to the container.
                        Label lbl = new Label();            //Allocates the new label object.
                        lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                        container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                        break;

                    case ListItemType.Item:
                        //Creates a new text box control and add it to the container.
                        TextBox tb1 = new TextBox();
                        //Allocates the new text box object.
                        tb1.ID = _columnName;
                        tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                        //tb1.Columns = 4;
                        tb1.MaxLength = 5;
                        tb1.Width = 60;
                        tb1.CssClass = "txt_SmallSkin";

                        AjaxControlToolkit.FilteredTextBoxExtender obj = new AjaxControlToolkit.FilteredTextBoxExtender();
                        obj.TargetControlID = _columnName;
                        if (_columnName.Contains("OP"))
                        {
                            tb1.AutoPostBack = true;
                            // tb1.TextChanged += new EventHandler(tb1_TextChanged); 
                            // tb1.onblur += new EventHandler(tb1TextChanged_DataBinding); 

                        }
                        if (_columnName.Contains("PRINTCODE"))
                        {
                            obj.FilterMode = AjaxControlToolkit.FilterModes.InvalidChars;
                            obj.InvalidChars = @"&quot;;|~!@#$%^&'*<>/\{}:';/*_?~`()[]=,";

                        }
                        else
                        {
                            obj.FilterMode = AjaxControlToolkit.FilterModes.ValidChars;
                            obj.ValidChars = "0123456789";
                        }
                        obj.ID = "FltText" + _columnName;
                        //obj.BehaviorID = "Bid" + new Guid();

                        //Creates a column with size 4.
                        container.Controls.Add(tb1);
                        container.Controls.Add(obj);//Adds the newly created textbox to the container.
                        break;

                    case ListItemType.EditItem:
                        //As, I am not using any EditItem, I didnot added any code here.
                        break;

                    case ListItemType.Footer:
                        CheckBox chkColumn = new CheckBox();
                        chkColumn.ID = "Chk" + _columnName;
                        container.Controls.Add(chkColumn);
                        break;
                }
            }

            /// <summary>
            /// This is the event, which will be raised when the binding happens.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// 
            void tb1_DataBinding(object sender, EventArgs e)
            {
                TextBox txtdata = (TextBox)sender;
                GridViewRow container = (GridViewRow)txtdata.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _columnName);
                if (dataValue != DBNull.Value)
                {
                    txtdata.Text = dataValue.ToString().ToUpper();
                }
            }

            //protected void tb1_TextChanged(object sender, EventArgs e)
            //{
            //    TextBox txtdata = (TextBox)sender;
            //    GridViewRow container = (GridViewRow)txtdata.NamingContainer;
            //    if (txtdata.Text != string.Empty)
            //    {
            //        BL_BedCategory _blBedCategory = new BL_BedCategory(new CMSDataContext());
            //        IEnumerable<Mas_BedCategory> _bedCategoryCollection = _blBedCategory.GetAllBedCategorysGrid();
            //        _bedCategoryCollection = _bedCategoryCollection.Where(i => i.Status == true);
            //        foreach (var bed in _bedCategoryCollection)
            //        {
            //            int strprice=0;
            //            int strPriceLastDigit = 0;
            //            string ShortName = bed.ShortName.ToUpper();
            //            Double IncrementPercent = Convert.ToDouble(bed.IncrementPersentage);
            //            TextBox txtShortName = (TextBox)container.FindControl(ShortName);
            //           // Convert.ToInt32(Convert.ToDouble(txtdata.Text) + ((Convert.ToDouble(txtdata.Text) * IncrementPercent) / 100)).ToString().ToUpper();
            //            strprice = Convert.ToInt32(Convert.ToDouble(txtdata.Text) + ((Convert.ToDouble(txtdata.Text) * IncrementPercent) / 100));
            //            strPriceLastDigit = strprice % 10;
            //            if (strPriceLastDigit == 0 || strPriceLastDigit == 5)
            //            {
            //                txtShortName.Text = Convert.ToInt32(strprice).ToString().ToUpper();
            //            }
            //            else if (strPriceLastDigit == 1 || strPriceLastDigit == 6)
            //            {
            //                txtShortName.Text = Convert.ToInt32(strprice - 1).ToString().ToUpper();
            //            }
            //            else if (strPriceLastDigit == 2 || strPriceLastDigit == 7)
            //            {
            //                txtShortName.Text = Convert.ToInt32(strprice - 2).ToString().ToUpper();
            //            }
            //            else if (strPriceLastDigit == 3 || strPriceLastDigit == 8)
            //            {
            //                txtShortName.Text = Convert.ToInt32(strprice +2).ToString().ToUpper();
            //            }
            //            else if (strPriceLastDigit == 4 || strPriceLastDigit == 9)
            //            {
            //                txtShortName.Text = Convert.ToInt32(strprice + 1).ToString().ToUpper();
            //            }                       
            //        }

            //    }

            //}
        }

    }
}