<%@ Page Title="Colony" Language="C#" MasterPageFile="~/Application/Common/Common.Master"
    AutoEventWireup="true" CodeFile="Master_Colony.aspx.cs" Inherits="DhiiLifeSpring.Application.Masters.Master_Colony"
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
            $get("<%=ddlMunicipalWard.ClientID%>").focus();
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                $find('mdlPopup').hide();
                document.getElementById('<%=btnAddColony.ClientID %>').focus();
            }
        }
        function AddClcik() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            if (document.getElementById('<%=ddlCity.ClientID %>').selectedIndex == 0) {
                msg.innerHTML = "Select city";
                msg.style.color = "red";
                document.getElementById('<%=ddlCity.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlMunicipalWard.ClientID %>').selectedIndex == 0) {
                msg.innerHTML = "Select municipal ward";
                msg.style.color = "red";
                document.getElementById('<%=ddlMunicipalWard.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtColony.ClientID %>').value.trim() == "") {
                msg.innerHTML = "Required colony";
                msg.style.color = "red";
                document.getElementById('<%=txtColony.ClientID %>').focus();
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
            <asp:LinkButton ID="btnShow" runat="server" CausesValidation="False"  />
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
                                                    <asp:Label ID="lblmdlheadertext" runat="server" Text="Add/Update Colony"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <table width="80%">
                                                        <tr>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <span class="star">&nbsp;&nbsp;</span><asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <asp:RadioButtonList ID="rbtnStatus" onfocus="Change(this, event)" onblur="Change(this, event)"
                                                                     CausesValidation="True" runat="server" RepeatDirection="Horizontal"
                                                                    OnSelectedIndexChanged="rbtnStatus_SelectedIndexChanged" AutoPostBack="True">
                                                                    <asp:ListItem Value="Active" Selected="True">Active</asp:ListItem>
                                                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:DropDownList ID="ddlCity" runat="server" SkinID="Dpd_Skin" onfocus="Change(this, event)"
                                                                    onblur="Change(this, event)" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblMunicipalWard" runat="server" Text="Municipal Ward:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:DropDownList ID="ddlMunicipalWard" runat="server" SkinID="Dpd_Skin" onfocus="Change(this, event)"
                                                                    onblur="Change(this, event)">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblColony" runat="server" Text="Colony:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtColony" runat="server" MaxLength="50" onfocus="Change(this, event)"
                                                                    onkeyup="return CheckFirstChar(this);" onkeydown="return CheckFirstChar(this);"
                                                                    onblur="Change(this, event)" SkinID="Txt_Skin"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteColony" runat="server" FilterMode="InvalidChars"
                                                                    TargetControlID="txtColony" InvalidChars=";|~!@#$%^&*<>/\{}:;/*-+_?~`[]=1234567890&quot;"
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
                                                                <asp:TextBox ID="txtReason" runat="server" SkinID="Txt_MultiSkin" Enabled="False"
                                                                    TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'70');" onfocus="Change(this, event)"
                                                                    onblur="Change(this, event)"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2" style="padding-top: 10px;">
                                                                <div class="base">
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button_Add" OnClick="btnAdd_Click"
                                                                        OnClientClick="return AddClcik();" ToolTip="Add" /> <%----%>
                                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button_Add"
                                                                        OnClick="btnClear_Click" ToolTip="Clear"  />
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Close" CausesValidation="False"
                                                                        CssClass="close" OnClientClick="$find('mdlPopup').hide();return false;"  />
                                                                </div>
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
                    <asp:Label ID="lblDoc" runat="server" Text=" Colony"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table width="98%" style="text-align: center;" cellpadding="0" cellspacing="0">
                                <caption>
                                    <tr>
                                        <td style="padding-bottom: 10px; text-align: left">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSearch" runat="server" Text="Search:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSearch" runat="server" autocomplete="off" MaxLength="50" onblur="Change(this, event)"
                                                            onfocus="Change(this, event)" onkeydown="return CheckFirstChar(this);" onkeyup="return CheckFirstChar(this);"
                                                            SkinID="Txt_Skin"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender ID="ae1" runat="server" BehaviorID="AutoCompleteEx"
                                                            CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_ListItem"
                                                            CompletionSetCount="20" MinimumPrefixLength="1" ServiceMethod="GetAllColonies"
                                                            ServicePath="~/DhiiCommonService.asmx" TargetControlID="txtSearch" DelimiterCharacters=""
                                                            Enabled="True">
                                                        </ajaxToolkit:AutoCompleteExtender>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="fltSearch" runat="server" FilterMode="InvalidChars"
                                                            InvalidChars="0123456789;|~!@#$%^&amp;'*&lt;&gt;/\{}:';/*-+_?~`()[]=,.1234567890&quot;"
                                                            TargetControlID="txtSearch" Enabled="True">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtwSearch" runat="server" TargetControlID="txtSearch"
                                                            WatermarkCssClass="watermarked" WatermarkText="Colony" Enabled="True">
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
                                                        <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="button_Search"
                                                            OnClick="btnSearch_Click"  ToolTip="Search" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" style="padding-bottom: 10px">
                                            <asp:Button ID="btnAddColony" runat="server" CausesValidation="False" CssClass="button_Add"
                                                OnClick="btnAddColony_Click"  Text="Add New"
                                                ToolTip="Add New" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:GridView ID="gvColony" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPageIndexChanging="gvColony_PageIndexChanging"
                                                OnRowCommand="gvColony_RowCommand" OnRowDataBound="gvColony_RowDataBound" OnSorting="gvColony_Sorting"
                                                PageSize="15" SkinID="GridMasters" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="60px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="City ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="120px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Municipal Ward">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMunicipalWard" runat="server" Text='<%# Eval("MunicipalWard") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="130px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Colony" SortExpression="Colony">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblColony" runat="server" Text='<%# Eval("Colony") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="headerBorderLine" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# (bool)Eval("Status")?"Active":"Inactive" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="60px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkedit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ColonyId") %>'
                                                                CommandName="NEdit" Height="20px" ImageUrl="~/Images/edit-notes.png" 
                                                                ToolTip="Edit" Width="20px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hfColonyId" runat="server" />
                                            <asp:HiddenField ID="hbtntext" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right" style="padding-top: 10px; font-weight: bold">
                                            <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </caption>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
