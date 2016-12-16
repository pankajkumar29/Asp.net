<%@ Page Title="Service Cost" Language="C#" AutoEventWireup="true" CodeFile="Master_ServiceCost.aspx.cs"
    MasterPageFile="~/Application/Common/Common.Master" StylesheetTheme="Controls"
    Inherits="DhiiLifeSpring.Application.Masters.Master_ServiceCost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
    <script src="../../Javascripts/jquery.js" type="text/javascript"></script>
    <script src="../../Javascripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Javascripts/jquery.alerts.js" type="text/javascript"></script>
    <link href="../../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .updateProgress
        {
            border-width: 1px;
            border-style: solid;
            background-color: #FFFFFF;
            position: absolute;
            width: 180px;
            height: 65px;
        }
        #progressBackgroundFilter
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: #000;
            filter: alpha(opacity=40);
            opacity: 0.7;
            z-index: 1000;
        }
        #processMessage
        {
            position: fixed;
            top: 45%;
            left: 40%;
            padding: 10px;
            width: 16%;
            z-index: 1001;
            background-color: #fff;
            border: solid 1px #000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <script type="text/JavaScript" language="JavaScript">

        function Change(obj, evt) {
            if (evt.type == "focus")
                obj.style.borderColor = "#fa7e0f";
            else if (evt.type == "blur")
                obj.style.borderColor = "LightGrey";
        }
        function SaveServiceCost() {

            if (document.getElementById('<%=ddlBranch.ClientID%>').selectedIndex == 0) {
                var msg = document.getElementById('<%=lblError.ClientID %>');
                msg.innerHTML = "Select branch";
                msg.style.color = "red";
                $get("<%=ddlBranch.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=ddlTariff.ClientID %>').selectedIndex == 0) {
                var msg = document.getElementById('<%=lblError.ClientID %>');
                msg.innerHTML = "Select tariff";
                msg.style.color = "red";
                $get("<%=ddlTariff.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=ddlServiceGroup.ClientID %>').selectedIndex == 0) {
                var msg = document.getElementById('<%=lblError.ClientID %>');
                msg.innerHTML = "Select service group";
                msg.style.color = "red";
                $get("<%=ddlServiceGroup.ClientID%>").focus();
                return false;
            }
        }
        function Click(obj, keyCode) {
            var objId = obj.id.ToString().ToUpper();
            if (keyCode == 13) {
                // if the key pressed is the enter key, dismiss the dialog
                document.getElementById(objId).click()
            }
            //if (keyCode == 32) {
            // if the key pressed is the space key, dismiss the dialog
            //   document.getElementById(objId).click()
            //}
        }
        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvServiceCost.ClientID %>');
            var TargetChildControl = "chkDelete";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;
            //Reset Counter
            //Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function Click(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvServiceCost.ClientID %>');
            var TargetChildControl = "chkDelete";
            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            // check to see if all other checkboxes are checked
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                    // Whoops, there is an unchecked checkbox, make sure
                    // that the header checkbox is unchecked
                    if (!Inputs[n].checked) {
                        Inputs[0].checked = false;
                        return;
                    }
                }
            // If we reach here, ALL GridView checkboxes are checked
            Inputs[0].checked = true;
        }
    </script>
    <script type="text/javascript">
        //Modalpopup Drag Event
        function setBodyHeightToContentHeight() {
            document.body.style.height = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight) + "px";

        }
        setBodyHeightToContentHeight();
        $addHandler(window, "resize", setBodyHeightToContentHeight);    
    </script>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <br />
            <fieldset style="border: 1px dashed #ccc; margin-left: 10px; margin-top: 0px; text-align: right;
                padding-bottom: 10px;">
                <legend class="legend_right" style="margin-right: 10px;" align="right">
                    <asp:Label ID="lblDoc" runat="server" Text=" Service Cost"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td align="left" style="width: 150px;">
                            <span class="star">*</span><asp:Label ID="lblBranch" runat="server" Text="Branch:"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlBranch" runat="server" SkinID="Dpd_Skin" onfocus="Change(this, event)"
                                onblur="Change(this, event)" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <span class="star">*</span><asp:Label ID="Tariff" SkinID="Lbl_Verdana" runat="server"
                                Text="Tariff: "></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlTariff" runat="server" onblur="Change(this, event)" onfocus="Change(this, event)"
                                Width="150px" SkinID="Dpd_Skin" AutoPostBack="true" OnSelectedIndexChanged="ddlTariff_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <span class="star">*</span><asp:Label ID="lblServiceType" SkinID="Lbl_Verdana" runat="server"
                                Text="Service Group:" Width="120px"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlServiceGroup" runat="server" AutoPostBack="true" onblur="Change(this, event)"
                                onfocus="Change(this, event)" OnSelectedIndexChanged="ddlServiceGroup_SelectedIndexChanged"
                                SkinID="Dpd_Skin">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            &nbsp;
                            <asp:Label ID="lblService0" runat="server" SkinID="Lbl_Verdana" Text="Service:"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlServices" runat="server" AutoPostBack="true" onblur="Change(this, event)"
                                onfocus="Change(this, event)" OnSelectedIndexChanged="ddlServices_SelectedIndexChanged"
                                SkinID="Dpd_Skin" Width="200px" Height="20px" />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnsearch" runat="server" CausesValidation="false" CssClass="button_Add"
                                OnClick="btnSearch_Click"  Text="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
                                <ProgressTemplate>
                                    <div id="progressBackgroundFilter">
                                    </div>
                                    <div id="processMessage">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/i_process.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <br />
                            <div style="width: 650px; height: auto; overflow: scroll;">
                                <asp:GridView ID="gvServiceCost" runat="server" AutoGenerateColumns="False" SkinID="GridTransactions"
                                    Width="600px">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkBxHeader" onclick="javascript:HeaderClick(this);" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkDelete" runat="server" onclick="javascript:Click(this);" Visible="true" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Height="20px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="50px" HeaderText="S. No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Service Group" ItemStyle-HorizontalAlign="Left" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblServicegropCode" runat="server" Text='<%# BIND("ServiceGroupId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Service Name" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblServiceCode" runat="server" Visible="false" Text='<%# BIND("serviceId") %>'></asp:Label>
                                                <asp:Label ID="lblServiceName" runat="server" Text='<%# BIND("serviceName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            &nbsp;
                        </td>
                        <td colspan="2" align="right" style="padding-top: 10px; font-weight: bold">
                            <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblBranchText" runat="server" Text="Select branch to copy data:" Visible="false"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:CheckBoxList ID="ChkBranch" runat="server" RepeatColumns="2">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnSave" runat="server" CssClass="button_Add" onblur="Change(this, event)"
                                OnClick="btnSave_Click" OnClientClick="return SaveServiceCost()" onfocus="Change(this, event)"
                                Text="Save" /> <%----%>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Label ID="lblError" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right" style="padding-top: 10px; font-weight: bold">
                            <asp:HiddenField ID="hfScheduleCode" runat="server" />
                            <asp:HiddenField ID="hbtntext" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
