<%@ Page Title="Ward" Language="C#" MasterPageFile="~/Application/Common/Common.Master"
    AutoEventWireup="true" CodeFile="Master_Ward.aspx.cs" Inherits="DhiiLifeSpring.Application.Masters.Master_Ward"
    StylesheetTheme="Controls" EnableEventValidation="false" %>

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
            $get("<%=txtWard.ClientID%>").focus();
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                $find('mdlPopup').hide();
                document.getElementById('<%=btnAddWard.ClientID %>').focus();
            }
        }
        function AddClick() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            if (document.getElementById('<%=txtWard.ClientID%>').value.trim() == "") {
                msg.innerHTML = "Required Ward";
                msg.style.color = "red";
                document.getElementById('<%=txtWard.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtShortCode.ClientID%>').value.trim() == "") {
                msg.innerHTML = "Required Short Code";
                msg.style.color = "red";
                document.getElementById('<%=txtShortCode.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlOrder.ClientID %>').selectedIndex == 0) {
                msg.innerHTML = "Select order";
                msg.style.color = "red";
                document.getElementById('<%=ddlOrder.ClientID %>').focus();
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
                TargetControlID="btnShow" PopupControlID="pnlPopup" BackgroundCssClass="modalBackground"
                DynamicServicePath="" Enabled="True" />
            <asp:Panel ID="pnlPopup" runat="server" Style="text-align: center; display: block;
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
                                                    <asp:Label ID="lblmdlheadertext" runat="server" Text="Add/Update Ward"></asp:Label>
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
                                                                <span class="star">*</span><asp:Label ID="lblWard" runat="server" Text="Ward:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtWard" runat="server" SkinID="Txt_Skin" MaxLength="50" onfocus="Change(this, event)"
                                                                    onblur="Change(this, event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteWard" runat="server" TargetControlID="txtWard"
                                                                    FilterMode="InvalidChars" InvalidChars=";|~!@#$%^*<>/\{}:';/*-+_?~`[]=1234567890&quot;">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblShortCode" runat="server" Text="Short Code:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtShortCode" runat="server" SkinID="Txt_Skin" MaxLength="5" onfocus="Change(this, event)"
                                                                    onblur="Change(this, event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteShortCode" runat="server" TargetControlID="txtShortCode"
                                                                    FilterMode="InvalidChars" InvalidChars=";|~!@#$%^*<>/\{}:';/*-+_?~`[]=1234567890&quot;">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblOrder" runat="server" Text="Order:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:DropDownList ID="ddlOrder" runat="server" SkinID="Dpd_Skin" onfocus="Change(this, event)"
                                                                    onblur="Change(this, event)">
                                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                                </asp:DropDownList>
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
                                                                <asp:Button ID="btnAdd" runat="server" CssClass="button_Add" Text="Add" ToolTip="Add"
                                                                    OnClientClick="return AddClick();" OnClick="btnAdd_Click" /> <%----%>
                                                                <asp:Button ID="btnClear" runat="server" CssClass="button_Add" Text="Clear" ToolTip="Clear"
                                                                    CausesValidation="False" OnClick="btnClear_Click"  />
                                                                <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" ToolTip="Close" CausesValidation="False"
                                                                    OnClientClick="$find('mdlPopup').hide();return false;" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="padding-top: 10px; height: 15px;">
                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True"></asp:Label>
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
            <fieldset style="border: 1px dashed #ccc; margin-left: 10px; margin-top: 0px; text-align: right;
                padding-bottom: 10px;">
                <legend class="legend_right" style="margin-right: 10px;" align="right">
                    <asp:Label ID="lblDoc" runat="server" Text="Ward"></asp:Label>
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
                                                    <asp:Label ID="lblSearch" runat="server" Text="Search:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSearch" runat="server" SkinID="Txt_Skin" MaxLength="50" autocomplete="off"
                                                        onblur="Change(this, event)" onfocus="Change(this, event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                    <ajaxToolkit:AutoCompleteExtender ID="aceSearch" runat="server" BehaviorID="bidSearch"
                                                        CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_ListItem"
                                                        CompletionSetCount="20" MinimumPrefixLength="1" ServiceMethod="GetAllWards" ServicePath="~/DhiiCommonService.asmx"
                                                        TargetControlID="txtSearch" DelimiterCharacters="" Enabled="True">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteSearch" runat="server" TargetControlID="txtSearch"
                                                        FilterMode="InvalidChars" InvalidChars="0123456789;|~!@#$%^&amp;'*&lt;&gt;/\{}:';/*-+_?~`()[]=,.1234567890&quot;">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="wmeSearch" runat="server" TargetControlID="txtSearch"
                                                        WatermarkCssClass="watermarked" WatermarkText="Ward">
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
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="button_Search" ToolTip="Search"
                                                        CausesValidation="False" OnClick="btnSearch_Click"  />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right" style="padding-bottom: 10px">
                                        <asp:Button ID="btnAddWard" runat="server" CssClass="button_Add" Text="Add New" ToolTip="Add New"
                                            CausesValidation="False" OnClick="btnAddWard_Click"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:GridView ID="gvWard" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                            AllowSorting="True" EmptyDataText="No Records Found" OnRowDataBound="gvWard_RowDataBound"
                                            OnRowCommand="gvWard_RowCommand" OnSorting="gvWard_Sorting" OnPageIndexChanging="gvWard_PageIndexChanging"
                                            PageSize="15" SkinID="GridMasters" Width="635px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No." HeaderStyle-Width="60px" >
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ward" SortExpression="Ward" HeaderStyle-CssClass="headerBorderLine" HeaderStyle-Width="250px" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWardName" runat="server" Text='<%# Eval("Ward") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Short Code" HeaderStyle-Width="120px" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShortCode" runat="server" Text='<%# Eval("ShortCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order" HeaderStyle-Width="60px" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrder" runat="server" Text='<%# Eval("Ordering") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="60px" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# (bool)Eval("Status")?"Active":"Inactive" %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="50px" >
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkedit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("WardId") %>'
                                                            CommandName="NEdit" Width="20px" Height="20px" ImageUrl="~/Images/edit-notes.png"
                                                             ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hfWardId" runat="server" />
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
