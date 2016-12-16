<%@ Page Title="Manage Roles" Language="C#" MasterPageFile="~/Application/Common/Common.Master"
    AutoEventWireup="true" CodeFile="ManageRoles.aspx.cs" Inherits="DhiiLifeSpring.Application.Masters.ManageRoles"
    StylesheetTheme="Controls" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="Server">
    <script src="../../Javascripts/jquery.js" type="text/javascript"></script>
    <script src="../../Javascripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Javascripts/jquery.alerts.js" type="text/javascript"></script>
    <link href="../../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../Javascripts/focusncheckchar.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function pageLoad(sender, args) {
            if (!args.get_isPartialLoad()) {
                //  add our handler to the document's
                //  keydown event
                $addHandler(document, "keydown", onKeyDown);
            }
            $find("mdlPopup").add_shown(onModalPopupShown);
        }
        function onModalPopupShown() {
            $get("<%=txtRole.ClientID%>").focus();
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                $find('mdlPopup').hide();
                document.getElementById('<%=btnAddRole.ClientID %>').focus();
            }
        }
        function btnAddClick() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            if (document.getElementById('<%=txtRole.ClientID%>').value.trim() == "") {
                msg.innerHTML = "Required role";
                msg.style.color = "red";
                document.getElementById('<%=txtRole.ClientID%>').focus();
                return false;
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
            document.getElementById('<%=lblMsg.ClientID %>').innerHTML = "";
            return true;
        }
        function SelectAllChildNodes() {
            var obj = window.event.srcElement;
            if (obj.tagName == "INPUT" && obj.type == "checkbox") {
                ToggleChildCheckBoxes(obj);
                ToggleParentCheckBox(obj);
            }
        }
        //Ended Treeview Script
        function AddClick() {
            var msg = document.getElementById('<%=lblAssignScreensMsg.ClientID %>');
            if (document.getElementById('<%=ddlRole.ClientID%>').selectedIndex == 0) {
                msg.innerHTML = "Select role";
                msg.style.color = "red";
                $get("<%=ddlRole.ClientID%>").focus();
                return false;
            }
            document.getElementById('<%=lblAssignScreensMsg.ClientID %>').innerHTML = "";
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        //Modalpopup Drag Event
        function setBodyHeightToContentHeight() {
            document.body.style.height = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight) + "px";
        }
        setBodyHeightToContentHeight();
        $addHandler(window, "resize", setBodyHeightToContentHeight);    
    </script>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="btnShow" runat="server" CausesValidation="False" />
            <ajaxToolkit:ModalPopupExtender ID="modlpopup" BehaviorID="mdlPopup" runat="server"
                TargetControlID="btnShow" PopupControlID="pnlPopup" BackgroundCssClass="modalBackground"
                DynamicServicePath="" Enabled="True" />
            <asp:Panel ID="pnlPopup" runat="server" Style="text-align: center; display: none;
                width: 450px;">
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
                                                <td valign="middle" class="mdpopup-header">
                                                    <asp:Label ID="lblmdlheadertext" runat="server" Text="Add/Update Roles"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="padding-left: 12px;">
                                                    <table width="80%" align="center">
                                                        <tr>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <span class="star">&nbsp;&nbsp;</span><asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <asp:RadioButtonList ID="rbtnStatus" runat="server" RepeatDirection="Horizontal"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="rbtnStatus_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Active" Selected="True">Active</asp:ListItem>
                                                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtRole" runat="server" SkinID="Txt_Skin" MaxLength="30" onblur="Change(this, event)"
                                                                    onfocus="Change(this, event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteRole" runat="server" TargetControlID="txtRole"
                                                                    FilterMode="InvalidChars" InvalidChars=";|~!@#$%&amp;()^*&lt;&gt;/\{}:';/*-+_?~,.`[]=1234567890&quot;"
                                                                    Enabled="True">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="left" style="padding-top: 5px;">
                                                                <span class="star">&nbsp;&nbsp;</span><asp:Label ID="lblInactiveReason" runat="server"
                                                                    Text="Inactive Reason:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtReason" runat="server" SkinID="Txt_MultiSkin" TextMode="MultiLine"
                                                                    Enabled="False" onfocus="Change(this, event)" onblur="Change(this, event)" onkeyup="return CheckFirstChar(this);"
                                                                    onkeyDown="checkTextAreaMaxLength(this,event,'70');"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteReason" runat="server" TargetControlID="txtReason"
                                                                    FilterMode="InvalidChars" InvalidChars=";|~!@#$%^*<>/\{}:';/*+_?~`[]=&quot;">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2" style="padding-top: 10px;">
                                                                <asp:Button ID="btnAdd" runat="server" CssClass="button_Add" Text="Add" ToolTip="Click to add"
                                                                    OnClientClick="return btnAddClick();" OnClick="btnAdd_Click" />
                                                                <asp:Button ID="btnClear" runat="server" CssClass="button_Add" Text="Clear" ToolTip="Click to clear"
                                                                    CausesValidation="False" OnClick="btnClear_Click" />
                                                                <asp:LinkButton ID="lnkClose" runat="server" ToolTip="Close" CausesValidation="False"
                                                                    CssClass="close" OnClientClick="$find('mdlPopup').hide();return false;" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="height: 15px;">
                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True"></asp:Label>
                                                    <asp:HiddenField ID="hfRoleId" runat="server" />
                                                    <asp:HiddenField ID="hfbtntext" runat="server" />
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
                    <asp:Label ID="lblEsc" runat="server" Text="Press 'Esc' key to Close This Window"></asp:Label>
                </div>
            </asp:Panel>
            <table width="100%">
                <tr>
                    <td>
                        <fieldset style="padding-bottom: 10px; border: 1px dashed #CCC; min-height: 210px;
                            margin-left: 10px;">
                            <legend align="right" class="legend_right">
                                <asp:Label ID="lblAddRoles" runat="server" Text="Add Roles"></asp:Label>
                            </legend>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <table width="90%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="padding-bottom: 10px; text-align: left">
                                                    <table align="left" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblSearch" runat="server" Text="Search:"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSearch" runat="server" SkinID="Txt_Skin" MaxLength="30" autocomplete="off"
                                                                    onblur="Change(this, event)" onfocus="Change(this, event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:AutoCompleteExtender ID="aceSearch" runat="server" BehaviorID="bidSearch"
                                                                    CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_ListItem"
                                                                    CompletionSetCount="20" MinimumPrefixLength="1" ServiceMethod="GetAllRoles" ServicePath="~/DhiiCommonService.asmx"
                                                                    TargetControlID="txtSearch" DelimiterCharacters="" Enabled="True">
                                                                </ajaxToolkit:AutoCompleteExtender>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteSearch" runat="server" TargetControlID="txtSearch"
                                                                    FilterMode="InvalidChars" InvalidChars=";|~!@#$%&amp;()^*&lt;&gt;/\{}:';/*-+_?~,.`[]=1234567890&quot;"
                                                                    Enabled="True">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                                <ajaxToolkit:TextBoxWatermarkExtender ID="wmeSearch" runat="server" TargetControlID="txtSearch"
                                                                    WatermarkCssClass="watermarked" WatermarkText="Role" Enabled="True">
                                                                </ajaxToolkit:TextBoxWatermarkExtender>
                                                            </td>
                                                            <td align="left" style="padding-left: 5px;">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" SkinID="Dpd_small_Skin">
                                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                    <asp:ListItem Value="true">Active</asp:ListItem>
                                                                    <asp:ListItem Value="false">Inactive</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSearch" runat="server" CssClass="button_Search" ToolTip="Click to search"
                                                                    CausesValidation="False" OnClick="btnSearch_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right" style="padding-bottom: 10px;">
                                                    <asp:Button ID="btnAddRole" runat="server" CssClass="button_Add" Text="Add Role"
                                                        ToolTip="click to add role" CausesValidation="False" OnClick="btnAddRole_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:GridView ID="grdRoles" runat="server" SkinID="GridMasters" AutoGenerateColumns="False"
                                                        AllowPaging="True" AllowSorting="True" PageSize="5" EmptyDataText="No Records Found"
                                                        Width="98%" OnRowCommand="grdRoles_RowCommand" OnPageIndexChanging="grdRoles_PageIndexChanging"
                                                        OnSorting="grdRoles_Sorting" OnRowDataBound="grdRoles_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S. No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60px" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Role" SortExpression="RoleName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRoleName" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="headerBorderLine" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# (bool)Eval("Status")?"Active":"Inactive" %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60px" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton CausesValidation="False" ID="lnkedit" runat="server" Width="20px"
                                                                        Height="20px" ImageUrl="~/Images/edit-notes.png" ToolTip="Edit" CommandName="NEdit"
                                                                        CommandArgument='<%# Eval("RoleId") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="right" style="padding-top: 10px; font-weight: bold">
                                                    <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <fieldset style="border: 1px dashed #CCC; min-height: 300px; margin-left: 10px;">
                            <legend align="right" class="legend_right">
                                <asp:Label ID="lblAssignScreens" runat="server" Text="Assign Screens"></asp:Label>
                            </legend>
                            <table cellpadding="5" cellspacing="5" style="padding-top: 10px;">
                                <tr>
                                    <td valign="top" align="right" style="padding-top: 10px; padding-right: 5px; width: 20%">
                                        <asp:Label ID="lblSelectRole" runat="server" Text="Select Role: " Width="150px"></asp:Label>
                                    </td>
                                    <td valign="top" align="left" style="padding-top: 10px; width: 15%">
                                        <asp:DropDownList ID="ddlRole" runat="server" SkinID="Dpd_Skin" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top" style="padding-top: 10px;" align="left">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="left">
                                        <asp:GridView ID="gvMenu" runat="server" AutoGenerateColumns="False" EmptyDataText="No Records Found"
                                            SkinID="GridMasters" Width="100%" OnDataBinding="gvMenu_DataBinding" OnRowDataBound="gvMenu_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Menu Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMenuName" runat="server" Text='<%# Eval("MenuName") %>'></asp:Label>
                                                        <asp:Label ID="lblParentId" Visible="false" runat="server" Text='<%# Eval("ParentId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="headerBorderLine" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Menu">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubMenu" runat="server" Text='<%# Eval("SubMenu") %>'></asp:Label>
                                                        <asp:Label ID="lblMenuId" Visible="false" runat="server" Text='<%# Eval("MenuId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbtnEdit" GroupName="G" runat="server" />
                                                        <asp:Label ID="lblEdit" Visible="false" runat="server" Text='<%# Eval("Edit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Read Only">
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbtnReadOnly" GroupName="G" runat="server" />
                                                        <asp:Label ID="lblReadOnly" Visible="false" runat="server" Text='<%# Eval("ReadOnly") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reset">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnReset" runat="server" ImageUrl="~/Images/reset.png" OnClick="imgbtnReset_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <asp:Button ID="btnSave" runat="server" CssClass="button_Add" Text="Save" ToolTip="Click to save"
                                            Visible="False" OnClick="btnSave_Click" OnClientClick="return AddClick();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Label ID="lblAssignScreensMsg" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
