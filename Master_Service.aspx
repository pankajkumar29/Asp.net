<%@ Page Title="Service" Language="C#" MasterPageFile="~/Application/Common/Common.Master"
    AutoEventWireup="true" CodeFile="~/Application/Masters/Master_Service.aspx.cs"
    Inherits="DhiiLifeSpring.Application.Masters.Master_Service" StylesheetTheme="Controls"
    EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
    <script src="../../Javascripts/jquery.js" type="text/javascript"></script>
    <script src="../../Javascripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Javascripts/jquery.alerts.js" type="text/javascript"></script>
    <link href="../../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../Javascripts/focusncheckchar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
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
            $get("<%=ddlServiceGroup.ClientID%>").focus();
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                // if the key pressed is the escape key, dismiss the dialog
                $find('mdlPopup').hide();
                document.getElementById('<%=btnAddService.ClientID %>').focus();
            }
        }
        function AddClcik() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            if (document.getElementById('<%=ddlServiceGroup.ClientID %>').selectedIndex == 0) {
                msg.innerHTML = "Select service group";
                msg.style.color = "red";
                document.getElementById('<%=ddlServiceGroup.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtService.ClientID%>').value.trim() == "") {
                msg.innerHTML = "Required service name";
                msg.style.color = "red";
                document.getElementById('<%=txtService.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=ChkPackage.ClientID%>').checked) {
                if (document.getElementById('<%=txtPackageDays.ClientID%>').value.trim() == "") {
                    msg.innerHTML = "Required package days";
                    msg.style.color = "red";
                    document.getElementById('<%=txtPackageDays.ClientID %>').focus();
                    return false;
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
        //Modalpopup Drag Event
        function setBodyHeightToContentHeight() {
            document.body.style.height = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight) + "px";
        }
        setBodyHeightToContentHeight();
        $addHandler(window, "resize", setBodyHeightToContentHeight);    
    </script>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="btnShow" runat="server" CausesValidation="False" />
            <ajaxToolkit:ModalPopupExtender ID="modlpopup" BehaviorID="mdlPopup" runat="server"
                TargetControlID="btnShow" PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" />
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
                                                    <asp:Label ID="lblmdlheadertext" runat="server" Text="Add/Update Service"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <table width="80%">
                                                        <tr>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <span class="star">&nbsp;&nbsp;</span>Status:
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <asp:RadioButtonList ID="rbtnStatus" onfocus="Change(this, event)" onblur="Change(this, event)"
                                                                     CausesValidation="true" runat="server" RepeatDirection="Horizontal"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="rbtnStatus_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Active" Selected="True">Active</asp:ListItem>
                                                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 3px;">
                                                                <span class="star">*</span><asp:Label ID="lblServiceGroup" runat="server" Text="Service Group:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 3px;">
                                                                <asp:DropDownList ID="ddlServiceGroup" runat="server" SkinID="Dpd_Skin" Style="width: 160px;"
                                                                    onfocus="Change(this, event)" onblur="Change(this, event)">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 3px;">
                                                                <span class="star">*</span><asp:Label ID="lblService" runat="server" Text="Service:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 3px;">
                                                                <asp:TextBox ID="txtService" runat="server" MaxLength="150" SkinID="Txt_Skin" Style="width: 155px;"
                                                                    onfocus="Change(this, event)" onblur="Change(this, event)" onkeyup="CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteService" runat="server" TargetControlID="txtService"
                                                                    FilterMode="InValidChars" InvalidChars=";|~!@#$%^*/\{}:';/*+_?~`[]=&quot;">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px; padding-left: 8px;">
                                                                <asp:CheckBox ID="ChkPackage" runat="server" Text="Is Package" AutoPostBack="true"
                                                                    OnCheckedChanged="ChkPackage_CheckedChanged" />
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:Label ID="lblPackageDays" runat="server" Text="Package Days:"></asp:Label>
                                                                <asp:TextBox ID="txtPackageDays" runat="server" MaxLength="2" SkinID="Txt_Skin" Style="width: 30px;"
                                                                    Enabled="false" onfocus="Change(this, event)" onblur="Change(this, event)" onkeyup="CheckFirstCharLogin(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftePackageDays" runat="server" TargetControlID="txtPackageDays"
                                                                    FilterMode="ValidChars" ValidChars="1234567890">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="left" style="padding-top: 3px;">
                                                                <span class="star">&nbsp;&nbsp;</span>Inactive Reason:
                                                            </td>
                                                            <td align="left" style="padding-top: 3px;">
                                                                <asp:TextBox ID="txtReason" runat="server" SkinID="Txt_MultiSkin" Style="width: 155px;"
                                                                    Enabled="false" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'70');"
                                                                    onfocus="Change(this, event)" onblur="Change(this, event)"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2" style="padding-top: 10px;">
                                                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button_Add" ToolTip="Add"
                                                                    OnClick="btnAdd_Click" OnClientClick="return AddClcik();" /> <%----%>
                                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button_Add" ToolTip="Clear"
                                                                    CausesValidation="false" OnClick="btnClear_Click"  />
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="close" ToolTip="Close"
                                                                    CausesValidation="false" OnClientClick="$find('mdlPopup').hide();return false;"
                                                                     />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="height: 15px;">
                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label>
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
                    Press 'Esc' key to Close This Window
                </div>
            </asp:Panel>
            <fieldset style="border: 1px dashed #ccc; margin-left: 10px; margin-top: 0px; text-align: right;
                padding-bottom: 10px;">
                <legend class="legend_right" style="margin-right: 10px;" align="right">
                    <asp:Label ID="lblDoc" runat="server" Text="Service"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table width="98%" style="text-align: center;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-bottom: 10px; text-align: left">
                                        <table>
                                            <tr>
                                                <td>
                                                    Search:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSearchServiceGroup" runat="server" SkinID="Dpd_Skin" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlSearchServiceGroup_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtSearch" runat="server" autocomplete="off" MaxLength="150" onblur="Change(this, event)"
                                                        onfocus="Change(this, event)" onkeydown="return CheckFirstChar(this);" onkeyup="return CheckFirstChar(this);"
                                                        SkinID="Txt_Skin"></asp:TextBox>
                                                    <ajaxToolkit:AutoCompleteExtender ID="aceSearch" runat="server" BehaviorID="bidSearch"
                                                        CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_ListItem"
                                                        CompletionSetCount="20" MinimumPrefixLength="1" ServicePath="~/DhiiCommonService.asmx"
                                                        ServiceMethod="GetAllServices" TargetControlID="txtSearch" DelimiterCharacters="">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="fltSearch" runat="server" FilterMode="InvalidChars"
                                                        InvalidChars=";|~!@#$%^*/\{}:';/*+_?~`[]=&quot;" TargetControlID="txtSearch">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtwSearch" runat="server" TargetControlID="txtSearch"
                                                        WatermarkCssClass="watermarked" WatermarkText="Service">
                                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" SkinID="Dpd_small_Skin">
                                                        <asp:ListItem Value="Select">Status</asp:ListItem>
                                                        <asp:ListItem Value="true">Active</asp:ListItem>
                                                        <asp:ListItem Value="false">Inactive</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="button_Search"
                                                        OnClick="btnSearch_Click"  Text="" ToolTip="Search" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right" style="padding-bottom: 10px">
                                        <asp:Button ID="btnAddService" runat="server" CausesValidation="false" CssClass="button_Add"
                                            OnClick="btnAddService_Click"  Text="Add New"
                                            ToolTip="Add New" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:GridView ID="gvService" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" DataKeyNames="ServiceId" EmptyDataText="No Records Found"
                                            OnPageIndexChanging="gvService_PageIndexChanging" OnRowCommand="gvService_RowCommand"
                                            OnRowDataBound="gvService_RowDataBound" OnSorting="gvService_Sorting" PageSize="15"
                                            SkinID="GridMasters" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No." HeaderStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Group" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left"
                                                    SortExpression="ServiceGroup">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblServiceGroup" runat="server" Text='<%# Eval("ServiceGroup") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service" ItemStyle-HorizontalAlign="Left" SortExpression="Service">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblServiceName" runat="server" Text='<%# Eval("Service") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="headerBorderLine" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Status" ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# (bool)Eval("Status")?"Active":"Inactive" %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Edit" ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkedit" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ServiceId") %>'
                                                            CommandName="NEdit" Height="20px" ImageUrl="~/Images/edit-notes.png" 
                                                            ToolTip="Edit" Width="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hfServiceId" runat="server" />
                                        <asp:HiddenField ID="hbtntext" runat="server" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
