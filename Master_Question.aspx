<%@ Page Title="Question" Language="C#" AutoEventWireup="true" CodeFile="Master_Question.aspx.cs"
    MasterPageFile="~/Application/Common/Common.Master" Inherits="DhiiLifeSpring.Application.Masters.Master_Question"
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
            $get("<%=txtQuestion.ClientID%>").focus();
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                $find('mdlPopup').hide();
                document.getElementById('<%=btnAddQuestion.ClientID %>').focus();
            }
        }
        function AddClick() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            if (document.getElementById('<%=ddlSection.ClientID %>').selectedIndex == 0) {
                msg.innerHTML = "Select Section";
                msg.style.color = "red";
                document.getElementById('<%=ddlSection.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtQuestion.ClientID%>').value.trim() == "") {
                msg.innerHTML = "Required question";
                msg.style.color = "red";
                document.getElementById('<%=txtQuestion.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtDisplayOrder.ClientID %>').value.trim() == "") {
                msg.innerHTML = "Required Display order";
                msg.style.color = "red";
                document.getElementById('<%=txtDisplayOrder.ClientID %>').focus();
                return false;
            }
            if ($("#" + document.getElementById('<%=rbtnAnswerType.ClientID%>').id + " input:radio:checked").length == 0) {
                msg.innerHTML = " Select answer type";
                msg.style.color = "red";
                document.getElementById('<%=rbtnAnswerType.ClientID%>').focus();
                return false;
            }
            var rbtnAnswerType = $("#" + document.getElementById('<%=rbtnAnswerType.ClientID%>').id + " input:radio:checked").val();
            if (rbtnAnswerType == "Radio Button") {
                var chkAnswerType = document.getElementById('<%=chkAnswerType.ClientID%>').checked;
                if (chkAnswerType == true) {
                    if ($("#" + document.getElementById('<%=rbtnWhen.ClientID%>').id + " input:radio:checked").length == 0) {
                        msg.innerHTML = " Select  yes or no";
                        msg.style.color = "red";
                        document.getElementById('<%=rbtnWhen.ClientID%>').focus();
                        return false;
                    }
                    if (document.getElementById('<%=txtQuestionWhen.ClientID%>').value.trim() == "") {
                        msg.innerHTML = "Required question";
                        msg.style.color = "red";
                        document.getElementById('<%=txtQuestionWhen.ClientID%>').focus();
                        return false;
                    }
                }
            }
            if (rbtnAnswerType == "CheckBoxList") {
                if (document.getElementById('<%=txtNoOfOptions.ClientID%>').value.trim() == "") {
                    msg.innerHTML = "Required no.of options";
                    msg.style.color = "red";
                    document.getElementById('<%=txtNoOfOptions.ClientID%>').focus();
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
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="btnShow" runat="server" CausesValidation="False" />
            <ajaxToolkit:ModalPopupExtender ID="modlpopup" BehaviorID="mdlPopup" runat="server"
                TargetControlID="btnShow" PopupControlID="pnlPopup" BackgroundCssClass="modalBackground"
                DynamicServicePath="" Enabled="True" />
            <asp:Panel ID="pnlPopup" runat="server" Style="text-align: center; display: block;
                width: 565px;">
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
                                                    <asp:Label ID="lblmdlheadertext" runat="server" Text="Add/Update Question"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="padding-left: 12px;">
                                                    <table width="100%" align="center">
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
                                                            <td align="left" style="padding-top: 10px;">
                                                                <span class="star">*</span><asp:Label ID="lblSection" runat="server" Text="Section:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <asp:DropDownList ID="ddlSection" runat="server" SkinID="Dpd_Skin" onfocus="Change(this,event)"
                                                                    onblur="Change(this,event)">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblQuestion" runat="server" Text="Question:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtQuestion" runat="server" SkinID="Txt_Skin" Width="300px" onfocus="Change(this,event)"
                                                                    onblur="Change(this,event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteQuestion" runat="server" TargetControlID="txtQuestion"
                                                                    BehaviorID="txtQuestion" FilterMode="InvalidChars" InvalidChars=";|~!@#$%^*<>/\{}:';/*-+_~`[]=&quot;">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <span class="star">*</span><asp:Label ID="lblDisplay" runat="server" Text="Display Order:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtDisplayOrder" runat="server" MaxLength="2" SkinID="Txt_SkinSmall"
                                                                    onKeyup="CheckFirstchar(this)" onfocus="Change(this,event)" onblur="Change(this,event)"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteDisplayOrder" runat="server" TargetControlID="txtDisplayOrder"
                                                                    FilterType="Numbers" Enabled="true">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px; width: 180px;">
                                                                <span class="star">*</span><asp:Label ID="lblAnswerType" runat="server" Text="Answer Type:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:RadioButtonList ID="rbtnAnswerType" runat="server" SkinID="RbList_Skin" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="rbtnAnswerType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Text Box">Text Box</asp:ListItem>
                                                                    <asp:ListItem Value="Radio Button">Radio Button with Yes & No Options</asp:ListItem>
                                                                    <asp:ListItem Value="CheckBoxList">CheckBoxList</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr id="trNoOfOptions" runat="server" visible="false">
                                                            <td align="left" style="padding-top: 10px;">
                                                                <span class="star">*</span><asp:Label ID="lblNoOfOptions" runat="server" Text="No. Of Options"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtNoOfOptions" runat="server" SkinID="Txt_Skin" MaxLength="2" onKeyup="CheckFirstchar(this)"
                                                                    onfocus="Change(this,event)" onblur="Change(this,event)" AutoPostBack="True"
                                                                    OnTextChanged="txtNoOfOptions_TextChanged"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteNoOfOptions" runat="server" TargetControlID="txtNoOfOptions"
                                                                    FilterType="Numbers" Enabled="true">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr id="trgvCheckBoxList" runat="server" visible="false">
                                                            <td colspan="2" align="right" style="padding-top: 10px;">
                                                                <asp:GridView ID="gvCheckBoxList" runat="server" AutoGenerateColumns="false" SkinID="GridTransactions"
                                                                    Width="65%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Option No." HeaderStyle-Width="70px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOptionNo" runat="server" Text='<%#Eval("OptionId") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="70px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtOptions" runat="server" Text='<%#Eval("Options") %>' SkinID="Txt_Skin"
                                                                                    Width="200px" onfocus="Change(this,event)" onblur="Change(this,event)"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr id="trAnswerType" runat="server" visible="false">
                                                            <td colspan="2" align="left" style="padding-top: 5px;">
                                                                <span class="star">&nbsp;&nbsp;</span><asp:CheckBox ID="chkAnswerType" runat="server"
                                                                    OnCheckedChanged="chkAnswerType_CheckedChanged" AutoPostBack="True" />
                                                                <asp:Label ID="lblDependent" runat="server" Text="Do you want to have dependent question"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="trWhen" runat="server" visible="false">
                                                            <td colspan="2" align="left" style="padding-top: 5px;">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <span class="star">*</span><asp:Label ID="lblWhen" runat="server" Text="When" Width="50px"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rbtnWhen" runat="server" SkinID="RbList_Skin" Width="100px"
                                                                                RepeatDirection="Horizontal">
                                                                                <asp:ListItem>Yes</asp:ListItem>
                                                                                <asp:ListItem>No</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDependentQue" runat="server" visible="false">
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblQuestionWhen" runat="server" Text="Question:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtQuestionWhen" runat="server" SkinID="Txt_Skin" Width="300px"
                                                                    onfocus="Change(this,event)" onblur="Change(this,event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteQuestionWhen" runat="server" TargetControlID="txtQuestionWhen"
                                                                    BehaviorID="txtQuestionWhen" FilterMode="InvalidChars" InvalidChars=";|~!@#$%^*<>/\{}:';/*-+_~`[]=&quot;">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="left" style="padding-top: 5px; width: 160px;">
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
                                                                    OnClientClick="return AddClick();" OnClick="btnAdd_Click" />
                                                                <asp:Button ID="btnClear" runat="server" CssClass="button_Add" Text="Clear" ToolTip="Clear"
                                                                    CausesValidation="False" OnClick="btnClear_Click" />
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
                    <asp:Label ID="lblDoc" runat="server" Text="Question"></asp:Label>
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
                                                    <asp:TextBox ID="txtSearch" runat="server" SkinID="Txt_Skin" Style="width: 200px;"
                                                        MaxLength="100" autocomplete="off" onblur="Change(this, event)" onfocus="Change(this, event)"
                                                        onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                    <ajaxToolkit:AutoCompleteExtender ID="aceSearch" runat="server" BehaviorID="bidSearch"
                                                        CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_ListItem"
                                                        CompletionSetCount="20" MinimumPrefixLength="1" ServiceMethod="GetAllQuestions"
                                                        ServicePath="~/DhiiCommonService.asmx" TargetControlID="txtSearch" DelimiterCharacters=""
                                                        Enabled="True">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteSearch" runat="server" TargetControlID="txtSearch"
                                                        FilterMode="InvalidChars" InvalidChars=";|~!@#$%^*<>/\{}:';/*-+_~`[]=&quot;">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="wmeSearch" runat="server" TargetControlID="txtSearch"
                                                        WatermarkCssClass="watermarked" WatermarkText="Question">
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
                                                        CausesValidation="False" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right" style="padding-bottom: 10px">
                                        <asp:Button ID="btnAddQuestion" runat="server" CssClass="button_Add" Text="Add New"
                                            ToolTip="Add New" CausesValidation="False" OnClick="btnAddQuestion_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:GridView ID="gvQuestion" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                            AllowSorting="True" EmptyDataText="No Records Found" OnPageIndexChanging="gvQuestion_PageIndexChanging"
                                            OnRowCommand="gvQuestion_RowCommand" OnRowDataBound="gvQuestion_RowDataBound"
                                            OnSorting="gvQuestion_Sorting" PageSize="15" SkinID="GridMasters" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="120px" />
                                                    <ItemStyle Width="120px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="110px" />
                                                    <ItemStyle Width="110px" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Question Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuestionType" runat="server" Text='<%# Eval("QuestionType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="120px" />
                                                    <ItemStyle Width="120px" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="120px" />
                                                    <ItemStyle Width="120px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Answer Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAnswerType" runat="server" Text='<%# Eval("AnswerType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="120px" />
                                                    <ItemStyle Width="120px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Display Order">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDisplayOrder" runat="server" Text='<%# Eval("DisplayOrder") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="120px" />
                                                    <ItemStyle Width="120px" />
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
                                                        <asp:ImageButton ID="lnkedit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("QuestionId") %>'
                                                            CommandName="NEdit" Width="20px" Height="20px" ImageUrl="~/Images/edit-notes.png"
                                                            ToolTip="Edit" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hfQuestionId" runat="server" />
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
