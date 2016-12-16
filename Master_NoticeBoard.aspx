<%@ Page Title="Notice Board" Language="C#" MasterPageFile="~/Application/Common/Common.Master"
    AutoEventWireup="true" CodeFile="~/Application/Masters/Master_NoticeBoard.aspx.cs"
    Inherits="DhiiLifeSpring.Application.Masters.Master_NoticeBoard" StylesheetTheme="Controls"
    EnableEventValidation="false" %>

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
            $get("<%=txtNoticeHeading.ClientID%>").focus();
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {

                $find('mdlPopup').hide();
                document.getElementById('<%=btnAddNotice.ClientID %>').focus();
            }
        }

        function AddClcik() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');

            if (document.getElementById('<%=txtNoticeHeading.ClientID%>').value.trim() == "") {
                var msg = document.getElementById('<%=lblMsg.ClientID %>');
                msg.innerHTML = "Required notice heading ";
                msg.style.color = "red";
                document.getElementById('<%=txtNoticeHeading.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtNoticeDisc.ClientID%>').value.trim() == "") {
                var msg = document.getElementById('<%=lblMsg.ClientID %>');
                msg.innerHTML = "Required notice description";
                msg.style.color = "red";
                document.getElementById('<%=txtNoticeDisc.ClientID%>').focus();
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

        function Change(obj, evt) {
            if (evt.type == "focus")
                obj.style.borderColor = "#fa7e0f";
            else if (evt.type == "blur")
                obj.style.borderColor = "LightGrey";
        }
        function Click(obj, keyCode) {
            var objId = obj.id.ToString().ToUpper();
            if (keyCode == 13) {
                // if the key pressed is the enter key, dismiss the dialog
                document.getElementById(objId).click()
            }

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
            <asp:Panel ID="pnlPopup" runat="server" Style="display: none; width: 600px;">
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
                                    <td class="MidContent">
                                        <center>
                                            <table width="100%">
                                                <tr>
                                                    <td valign="middle" class="mdpopup-header">
                                                        <asp:Label ID="lblmdlheadertext" runat="server" Text="Add/Update Notice Board"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="left" style="padding-left: 15px; padding-top: 5px;">
                                                                    <span class="star">&nbsp;&nbsp;</span><asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                                                                </td>
                                                                <td align="left" style="padding-top: 5px;">
                                                                    <asp:RadioButtonList ID="rbtnStatus" runat="server" RepeatDirection="Horizontal"
                                                                        onfocus="Change(this, event)" onblur="Change(this, event)" 
                                                                        AutoPostBack="True" OnSelectedIndexChanged="rbtnStatus_SelectedIndexChanged">
                                                                        <asp:ListItem Value="Active" Selected="True">Active</asp:ListItem>
                                                                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="padding-left: 15px; padding-top: 5px;">
                                                                    <span class="star">*</span><asp:Label ID="lblBranch" runat="server" Text="Branch:"></asp:Label>
                                                                </td>
                                                                <td align="left" style="padding-top: 5px; font-size: 11px; text-align:left">
                                                                    <asp:CheckBoxList ID="chkBranch" runat="server" RepeatColumns="3" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="chkBranch_SelectedIndexChanged" Width="400px">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                            <tr id="trAllBranch" runat="server">
                                                                <td align="left" style="font-size: 11px;">
                                                                </td>
                                                                <td align="left" style="padding-top: 5px; font-size: 11px;">
                                                                    <asp:CheckBox ID="chkAll" runat="server" Text="ALL" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="padding-left: 15px; padding-top: 5px;">
                                                                    <span class="star">*</span><asp:Label ID="lblHeading" runat="server" Text="Heading:"></asp:Label>
                                                                </td>
                                                                <td align="left" style="padding-top: 5px;">
                                                                    <asp:TextBox ID="txtNoticeHeading" runat="server" SkinID="Txt_Skin" Style="width: 300px"
                                                                        onfocus="Change(this, event)" onblur="Change(this, event)" onkeyup="return CheckFirstChar(this);"
                                                                        MaxLength="50"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteDept" runat="server" FilterMode="InvalidChars"
                                                                        TargetControlID="txtNoticeHeading" InvalidChars=";|~!@#$%^&'*<>/\{}:';/*-+_-?~`()[]=,.1234567890&quot;"
                                                                        Enabled="True">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="left" style="padding-top: 5px; padding-left: 15px;">
                                                                    <span class="star">*</span><asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
                                                                </td>
                                                                <td align="left" style="padding-top: 5px;">
                                                                    <asp:TextBox ID="txtNoticeDisc" runat="server" SkinID="Txt_Skin" Style="width: 300px;
                                                                        height: 100px;" TextMode="MultiLine" onfocus="Change(this, event)" onblur="Change(this, event)"
                                                                        onkeyup="return CheckFirstChar(this);" onkeyDown="checkTextAreaMaxLength(this,event,'250');"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="top" style="padding-left: 15px; padding-top: 5px;">
                                                                    <span class="star">&nbsp;&nbsp;</span>Inactive Reason:
                                                                </td>
                                                                <td align="left" style="padding-top: 5px;">
                                                                    <asp:TextBox ID="txtReason" runat="server" SkinID="Txt_Skin" Style="width: 300px;"
                                                                        Enabled="False" TextMode="MultiLine" onfocus="Change(this, event)" onblur="Change(this, event)"
                                                                        onkeyup="return CheckFirstChar(this);" onkeyDown="checkTextAreaMaxLength(this,event,'70');"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteReason" runat="server" FilterMode="InvalidChars"
                                                                        TargetControlID="txtReason" InvalidChars=";|~!@#$%^*<>/\{}:';/*+_?~`[]=&quot;"
                                                                        Enabled="True">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2" style="padding-top: 10px;">
                                                                    <div class="base">
                                                                        <asp:Button ID="btnAdd" runat="server" CssClass="button_Add" Text="Add" ToolTip="Add"
                                                                            OnClientClick="return AddClcik();" OnClick="btnAdd_Click"  /><%----%>
                                                                        <asp:Button ID="btnClear" runat="server" CssClass="button_Add" Text="Clear" ToolTip="Clear"
                                                                            CausesValidation="False" OnClick="btnClear_Click"  />
                                                                        <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" ToolTip="Close" CausesValidation="False"
                                                                            OnClientClick="$find('mdlPopup').hide();return false;" />
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
                                        </center>
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
            <fieldset style="border: 1px dashed #ccc; margin-left: 10px; margin-top: 3px; padding-bottom: 10px;">
                <legend class="legend_right" style="margin-right: 10px;" align="right">
                    <asp:Label ID="lblDoc" runat="server" Text="Notice Board"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table width="98%" style="text-align: center;" cellpadding="0" cellspacing="0">
                                <caption>
                                    <br />
                                    <tr>
                                        <td style="padding-bottom: 10px; text-align: left">
                                            <table>
                                                <tr>
                                                    <td>
                                                        Search:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="Txt_Skin" MaxLength="50" autocomplete="off"
                                                            onblur="Change(this, event)" onfocus="Change(this, event)"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender ID="aceSearch" runat="server" BehaviorID="bidSearch"
                                                            CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_ListItem"
                                                            CompletionSetCount="20" MinimumPrefixLength="1" ServiceMethod="GetAllNotices"
                                                            ServicePath="~/DhiiCommonService.asmx" TargetControlID="txtSearch" DelimiterCharacters=""
                                                            Enabled="True">
                                                        </ajaxToolkit:AutoCompleteExtender>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteSearch" runat="server" TargetControlID="txtSearch"
                                                            FilterMode="InvalidChars" InvalidChars=";|~!@#$%^&'*<>/\{}:';/*-+_-?~`()[]=,.1234567890&quot;"
                                                            Enabled="True">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="wmeSearch" runat="server" TargetControlID="txtSearch"
                                                            WatermarkCssClass="watermarked" WatermarkText="Heading" Enabled="True">
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
                                                            CausesValidation="False"  OnClick="btnSearch_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" style="padding-bottom: 10px">
                                            <asp:Button ID="btnAddNotice" runat="server" CssClass="button_Add" CausesValidation="False"
                                                Text="Add New" ToolTip="Add New"  OnClick="btnAddNotice_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:GridView ID="gvNoticeBoard" runat="server" SkinID="GridMasters" AutoGenerateColumns="False"
                                                AllowPaging="True" AllowSorting="True" EmptyDataText="No Records Found" OnPageIndexChanging="gvNoticeBoard_PageIndexChanging"
                                                OnRowCommand="gvNoticeBoard_RowCommand" OnRowDataBound="gvNoticeBoard_RowDataBound"
                                                OnSorting="gvNoticeBoard_Sorting" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle Width="50px" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Branch">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("FacilityName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Heading" SortExpression="NoticeHeading">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeptName" runat="server" Text='<%# Eval("NoticeHeading") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="headerBorderLine" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("NoticeDisc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="250px" />
                                                        <ItemStyle Width="250px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# (bool)Eval("Status")?"Active":"Inactive" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkedit" runat="server" ImageUrl="~/Images/edit-notes.png" CausesValidation="False"
                                                                CommandName="NEdit" CommandArgument='<%# Eval("NoticeId") %>' ToolTip="Edit"
                                                                Width="20px" Height="20px"  />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hfNoticeId" runat="server" />
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
