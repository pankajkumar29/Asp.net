<%@ Page Title="Employee" Language="C#" MasterPageFile="~/Application/Common/Common.Master"
    AutoEventWireup="true" CodeFile="Master_Employee.aspx.cs" Inherits="DhiiLifeSpring.Application.Masters.Master_Employee"
    StylesheetTheme="Controls" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
    <script src="../../Javascripts/focusncheckchar.js" type="text/javascript"></script>
    <link href="../../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../Javascripts/date.js" type="text/javascript"></script>
    <script type="text/javascript">


        function ValidatePercentage(obj) {
            var Temp = obj.value;
            if (Temp != "") {
                var num = Temp;
                num = num.split('.');
                var s = num[0];
                var s1 = num[1];
                if (num[0].length > 2) {
                    num[0] = num[0].substring(0, 2);
                    obj.value = num[0];
                }
                else {
                    if (num[1] != undefined) {
                        if (num[1].length > 2) {
                            num[1] = num[1].substring(0, 2);
                        }
                        obj.value = num[0] + '.' + num[1];
                    }
                    else {
                        obj.value = num[0];
                    }
                }
            }
        }
        function AddClcik() {

            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            if (document.getElementById('<%=txtFirstName.ClientID %>').value.trim().length == 0) {
                msg.innerHTML = "Required employee name";
                msg.style.color = "red";
                $get("<%=txtFirstName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtFirstName.ClientID %>').value.trim().length < 3) {
                msg.innerHTML = "First name should require minimum 3 characters";
                msg.style.color = "red";
                $get("<%=txtFirstName.ClientID%>").focus();
                return false;
            }

            if (document.getElementById('<%=ddlGender.ClientID%>').selectedIndex == 0) {
                msg.innerHTML = "Select gender";
                msg.style.color = "red";
                $get("<%=ddlGender.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtMobileNo.ClientID%>').value.trim().length == 0) {
                msg.innerHTML = "Required mobile";
                msg.style.color = "red";
                $get("<%=txtMobileNo.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtMobileNo.ClientID%>').value.trim().length != 0) {
                if (document.getElementById('<%=txtMobileNo.ClientID%>').value.trim().length < 10) {
                    msg.innerHTML = "Invalid mobile";
                    msg.style.color = "red";

                    $get("<%=txtMobileNo.ClientID%>").focus();
                    return false;
                }
            }
            if (document.getElementById('<%=ddlDesignation.ClientID%>').selectedIndex == 0) {
                msg.innerHTML = "Select designation";
                msg.style.color = "red";
                $get("<%=ddlDesignation.ClientID%>").focus();
                return false;
            }

            if (document.getElementById('<%=chkLogin.ClientID%>').disabled == false) {
                if (document.getElementById('<%=chkLogin.ClientID%>').checked) {
                    if (document.getElementById('<%=txtUsername.ClientID %>').value.trim().length == 0) {
                        msg.innerHTML = "Required user name";
                        msg.style.color = "red";
                        $get("<%=txtUsername.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById('<%=txtUsername.ClientID %>').value.trim().length < 5) {
                        msg.innerHTML = "Minimum user name length should be 5";
                        msg.style.color = "red";
                        $get("<%=txtUsername.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById('<%=txtPassword.ClientID %>').value.trim().length == 0) {
                        msg.innerHTML = "Required password";
                        msg.style.color = "red";
                        $get("<%=txtPassword.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById('<%=txtPassword.ClientID %>').value.trim().length < 6) {
                        msg.innerHTML = "Minimum password length should be 6";
                        msg.style.color = "red";
                        $get("<%=txtPassword.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById('<%=txtReTypePassword.ClientID %>').value.trim().length == 0) {
                        msg.innerHTML = "Required confirm password";
                        msg.style.color = "red";
                        $get("<%=txtReTypePassword.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById('<%=txtReTypePassword.ClientID %>').value.trim().length < 6) {
                        msg.innerHTML = "Minimum confirm password length should be 6";
                        msg.style.color = "red";
                        $get("<%=txtReTypePassword.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById('<%=txtReTypePassword.ClientID %>').value.trim() != document.getElementById('<%=txtPassword.ClientID %>').value.trim()) {
                        msg.innerHTML = "Confirm password should be same as password";
                        msg.style.color = "red";
                        $get("<%=txtReTypePassword.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById('<%=ddlRole.ClientID%>').selectedIndex == 0) {
                        msg.innerHTML = "Select Role";
                        msg.style.color = "red";
                        $get("<%=ddlRole.ClientID%>").focus();
                        return false;
                    }

                }
            }

            var selectedvalue = $("#" + document.getElementById('<%=rbtnStatus.ClientID%>').id + " input:radio:checked").val();
            if (selectedvalue == "Inactive") {
                if (document.getElementById('<%=txtReason.ClientID%>').value.trim() == "") {
                    msg.innerHTML = "Required reason";
                    msg.style.color = "red";
                    document.getElementById('<%=txtReason.ClientID%>').focus();
                    return false;
                }
            }
            msg.innerHTML = "";
            return true;
        }

    </script>
    <script type="text/javascript">
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                if ($find('mdlPopup') != null) {
                    $find('mdlPopup').hide();
                }
                if ($find('mdlResetPassword') != null) {
                    $find('mdlResetPassword').hide();
                }
            }
        }

      
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <fieldset style="border: 1px dashed #ccc; margin-left: 10px; margin-top: 0px; text-align: right;
                padding-bottom: 5px;">
                <legend class="legend_right" style="margin-right: 10px;" align="right">
                    <asp:Label ID="lblDoc" runat="server" Text="Employee"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td align="left" colspan="4" style="font-weight: bold; height: 25px">
                            <asp:Label ID="lblPersonalDetails" runat="server" Text="Personal Details:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-top: 10px;">
                            <span class="star">&nbsp;&nbsp;</span>
                            <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:RadioButtonList ID="rbtnStatus" onfocus="Change(this, event)" onblur="Change(this, event)"
                                 CausesValidation="True" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rbtnStatus_SelectedIndexChanged">
                                <asp:ListItem Value="Active" Selected="True">Active</asp:ListItem>
                                <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            &nbsp;
                        </td>
                        <td align="left" style="padding-top: 5px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-top: 5px; width: 200px;">
                            <span class="star">*</span><asp:Label ID="lblBranch" runat="server" Text="Branch:"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 5px" colspan="3">
                            <asp:CheckBoxList ID="ChkBranch" runat="server" RepeatColumns="2">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-top: 5px; width: 200px;">
                            <span class="star">*</span><asp:Label ID="lblTextFirstName" runat="server" Text="Employee Name:"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 5px; width:200px;">
                            <asp:DropDownList ID="ddlSalutation" runat="server" onblur="Change(this, event)"
                                onfocus="Change(this, event)" SkinID="Dpd_Skin" Style="width: 45px">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="60" onblur="Change(this, event)"
                                onfocus="Change(this, event)" onkeyup="CheckFirstChar(this)" SkinID="Txt_Skin"
                                Style="width: 86px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteExternalLab" runat="server" Enabled="True"
                                FilterMode="InvalidChars" InvalidChars=";|~!@#$%^&amp;'*&lt;&gt;/\{}:';/*-+_?~`()[]=,1234567890&quot;"
                                TargetControlID="txtFirstName">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <span class="star">*</span><asp:Label ID="lbltextGender" runat="server" Text="Gender:"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <asp:DropDownList ID="ddlGender" runat="server" onblur="Change(this, event)" onfocus="Change(this, event)"
                                SkinID="Dpd_Skin">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-top: 5px">
                            <span class="star">*</span><asp:Label ID="lbltextMobile" runat="server" Text="Mobile:"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" onblur="Change(this, event)"
                                onfocus="Change(this, event)" onkeyup="CheckFirstChar(this)" SkinID="Txt_Skin"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                Enabled="True" TargetControlID="txtMobileNo" ValidChars="1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <span class="star">*</span><asp:Label ID="lbltextDesignation" runat="server" Text="Designation:"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <asp:DropDownList ID="ddlDesignation" runat="server" onblur="Change(this, event)"
                                onfocus="Change(this, event)" SkinID="Dpd_Skin" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4" style="font-weight: bold; height: 25px; padding-top: 10px;">
                            <asp:Label ID="lblLoginDetails" runat="server" Text="  Login Details:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-top: 5px; padding-bottom: 5px" colspan="3">
                            <span class="star">&nbsp;&nbsp;</span><asp:CheckBox ID="chkLogin" runat="server" AutoPostBack="True" OnCheckedChanged="chkLogin_CheckedChanged"
                                Text="Has Login Permission" />
                        </td>
                        <td align="left" style="padding-top: 5px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-top: 5px">
                            <span class="star">&nbsp;&nbsp;</span><asp:Label ID="lbltextUserName" runat="server" Text="User Name:"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <asp:TextBox ID="txtUsername" runat="server" Enabled="False" MaxLength="20" onblur="Change(this, event)"
                                onfocus="Change(this, event)" onkeyup="CheckFirstChar(this)" SkinID="Txt_Skin"></asp:TextBox>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <span class="star">&nbsp;&nbsp;</span><asp:Label ID="lbltextPassword" runat="server"
                                Text="Password:"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <asp:TextBox ID="txtPassword" runat="server" Enabled="False" MaxLength="20" onblur="Change(this, event)"
                                onfocus="Change(this, event)" onkeyup="CheckFirstChar(this)" SkinID="Txt_Skin"
                                TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-top: 5px">
                            <span class="star">&nbsp;&nbsp;</span><asp:Label ID="lbltextConfirmPassword" runat="server"
                                Text="Confirm Password:"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <asp:TextBox ID="txtReTypePassword" runat="server" SkinID="Txt_Skin" TextMode="Password"
                                MaxLength="20" Enabled="False" onfocus="Change(this, event)" onblur="Change(this, event)"
                                onkeyup="CheckFirstChar(this)"></asp:TextBox>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <span class="star">&nbsp;&nbsp;</span><asp:Label ID="lbltextRole" runat="server"
                                Text="Role:"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 5px">
                            <asp:DropDownList ID="ddlRole" runat="server" onblur="Change(this, event)" onfocus="Change(this, event)"
                                SkinID="Dpd_Skin" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4" style="font-weight: bold; height: 25px; padding-top: 10px;">
                            <asp:Label ID="lbltextInactiveReason" runat="server" Text="Inactive Reason:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-top: 5px">
                            <span class="star">&nbsp;&nbsp;</span><asp:Label ID="lbltextReason" runat="server" Text="Reason:"></asp:Label>
                        </td>
                        <td align="left" colspan="3" style="padding-top: 5px">
                            <asp:TextBox ID="txtReason" runat="server" SkinID="Txt_Skin" Style="width: 445px"
                                onkeyDown="checkTextAreaMaxLength(this,event,'70');" Enabled="False" onfocus="Change(this, event)"
                                onblur="Change(this, event)" onkeyup="CheckFirstChar(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" style="padding-top: 10px">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button_Add" OnClick="btnAdd_Click"
                                ToolTip="Add" OnClientClick="return AddClcik();" />
                            <asp:Button ID="btnResetPswd" runat="server" Text="Reset Password" ToolTip="Reset Password"
                                CausesValidation="False" CssClass="button_big_Add" OnClick="btnResetPswd_Click"
                                meta:resourcekey="btnResetPswdResource1" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" ToolTip="Text Clear" CausesValidation="False"
                                CssClass="button_Add" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" style="padding-top: 10px; padding-bottom: 10px;">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hfEmp_ID" runat="server" />
                            <asp:HiddenField ID="hbtntext" runat="server" />
                            <asp:HiddenField ID="txtCurDate" runat="server" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px" colspan="4">
                            <fieldset style="border: 1px dashed #CCC;">
                                <legend align="right" class="legend_right" style="margin-right: 10px;">
                                    <asp:Label ID="Label1" runat="server" Text="Employee Details"></asp:Label>
                                </legend>
                                <table width="98%" align="center" cellpadding="0" cellspacing="0" >
                                    <tr>
                                        <td align="left">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbltextserach" runat="server" Text="Search:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSearchFacility" runat="server" SkinID="Dpd_Skin" onfocus="Change(this, event)"
                                                            onblur="Change(this, event)" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchFacility_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSearch" runat="server" onfocus="Change(this, event)" onblur="Change(this, event)"
                                                            onkeyup="return CheckFirstChar(this);" SkinID="Txt_Skin" autocomplete="off" MaxLength="50"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender ID="ae1" BehaviorID="AutoCompleteEx" runat="server"
                                                            TargetControlID="txtSearch" ServicePath="~/DhiiCommonService.asmx" ServiceMethod="GetAllEmployees"
                                                            MinimumPrefixLength="1" CompletionInterval="500" CompletionSetCount="20" 
                                                            CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_ListItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                            DelimiterCharacters="" Enabled="True">
                                                        </ajaxToolkit:AutoCompleteExtender>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteSearch" runat="server" FilterMode="InvalidChars"
                                                            TargetControlID="txtSearch" InvalidChars=";|~!@#$%^&'*<>/\{}:';/*-+_?~`()[]=,1234567890&quot;"
                                                            Enabled="True">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtwSearch" runat="server" TargetControlID="txtSearch"
                                                            WatermarkText="Employee Name" WatermarkCssClass="watermarked" Enabled="True">
                                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" SkinID="Dpd_small_Skin">
                                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                                            <asp:ListItem Value="true">Active</asp:ListItem>
                                                            <asp:ListItem Value="false">Inactive</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="button_Search"
                                                            OnClick="btnSearch_Click"  ToolTip="Search" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top">
                                            <asp:GridView ID="grEmployee" AutoGenerateColumns="False" runat="server" SkinID="GridMasters"
                                                Width="100%" EmptyDataText="No Records Found" AllowSorting="True" AllowPaging="True"
                                                PageSize="5" OnRowCommand="grEmployee_RowCommand" OnPageIndexChanging="grEmployee_PageIndexChanging"
                                                OnRowDataBound="grEmployee_RowDataBound" OnSorting="grEmployee_Sorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmployee" runat="server" Text='<%# Bind("EmployeeName1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="headerBorderLine" />
                                                        <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Role">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRole" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# (bool)Eval("Status")?"Active":"Inactive" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkedit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("EmpID") %>'
                                                                CommandName="NEdit"  Height="20px" ImageUrl="~/Images/edit-notes.png"
                                                                ToolTip="Edit" Width="20px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-top: 10px; padding-bottom: 10px; font-weight: bold">
                                            <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:LinkButton ID="btnResetPasswordShow" runat="server" CausesValidation="False" />
            <ajaxToolkit:ModalPopupExtender ID="modlResetPassword" BehaviorID="mdlResetPassword"
                runat="server" TargetControlID="btnResetPasswordShow" PopupControlID="pnlResetPassword"
                BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True" />
            <asp:Panel ID="pnlResetPassword" runat="server" Style="text-align: center; width: 400px;
                display: none;">
                <div id="facebox">
                    <div class="popup">
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td class="tl">
                                    </td>
                                    <td class="b">
                                    </td>
                                    <td class="tr">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="b">
                                    </td>
                                    <td class="MidContent" align="center">
                                        <table width="100%" style="text-align: center">
                                            <tr>
                                                <td valign="middle" style="font-weight: bold">
                                                    <asp:Label ID="lblResetPassword" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4" style="padding-top: 30px;">
                                                    <asp:Button ID="btnOk" runat="server" CssClass="button_Add" Text="OK" 
                                                        OnClientClick="$find('mdlResetPassword').hide();return false;" />
                                                    <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Close" CausesValidation="False"
                                                        CssClass="close" OnClientClick="$find('mdlResetPassword').hide();return false;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="b">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="bl">
                                    </td>
                                    <td class="b">
                                    </td>
                                    <td class="br">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div style="color: White; font-weight: normal; text-align: center; vertical-align: middle;
                    padding-bottom: 10px; font-family: Arial; font-size: 10px;">
                    <asp:Label ID="lblpexces" runat="server" Text="Press 'Esc' key to close this window"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
