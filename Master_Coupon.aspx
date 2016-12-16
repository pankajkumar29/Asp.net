<%@ Page Title="Coupon" Language="C#" AutoEventWireup="true" CodeFile="Master_Coupon.aspx.cs"
    MasterPageFile="~/Application/Common/Common.Master" Inherits="DhiiLifeSpring.Application.Masters.Master_Coupon"
    StylesheetTheme="Controls" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
    <script src="../../Javascripts/jquery.js" type="text/javascript"></script>
    <script src="../../Javascripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Javascripts/jquery.alerts.js" type="text/javascript"></script>
    <link href="../../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../Javascripts/focusncheckchar.js" type="text/javascript"></script>
    <script src="../../Javascripts/date.js" type="text/javascript"></script>
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
            $get("<%=txtCoupons.ClientID%>").focus();
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                $find('mdlPopup').hide();
            }
        }
        function CheckAll() {
            var checkAll = document.getElementById('<%=chkAll.ClientID %>').checked;
            var chkValidIsFacility = document.getElementById('<%=chkValidIsFacility.ClientID %>').getElementsByTagName('input');
            if (checkAll == true) {
                for (var i = 0; i < chkValidIsFacility.length; i++) {
                    chkValidIsFacility[i].checked = true;
                }
            }
            if (checkAll == false)
                for (var i = 0; i < chkValidIsFacility.length; i++) {
                    chkValidIsFacility[i].checked = false;
                }
        }
        function CheckServiceGroupAll() {
            var checkAll = document.getElementById('<%=chkServiceGroupAll.ClientID %>').checked;
            var chkValidIsFacility = document.getElementById('<%=chkServiceGroup.ClientID %>').getElementsByTagName('input');
            if (checkAll == true) {
                for (var i = 0; i < chkValidIsFacility.length; i++) {
                    chkValidIsFacility[i].checked = true;
                }
            }
            if (checkAll == false)
                for (var i = 0; i < chkValidIsFacility.length; i++) {
                    chkValidIsFacility[i].checked = false;
                }
        }
        function getJSDate(date) {
            var temp = new Array();
            temp = date.toString().split('-');
            return temp[1] + '/' + temp[0] + '/' + temp[2];
        }
        function ValidFrom() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            var txtValidFromDate = document.getElementById('<%=txtValidFrom.ClientID %>').value;
            if (!datecheck(txtValidFromDate)) {
                document.getElementById('<%=txtValidFrom.ClientID %>').focus();
                document.getElementById('<%=txtValidFrom.ClientID %>').value = '';
                return false;
            }
            var startDate = new Date(getJSDate(txtValidFromDate));
            var curDate = new Date();
            if (startDate < curDate) {
                msg.innerHTML = "Valid from should not be less than or equal to today's date";
                msg.style.color = "red";
                document.getElementById('<%=txtValidFrom.ClientID%>').value = "";
                document.getElementById('<%=txtValidFrom.ClientID%>').focus();
                return false;
            }
        }
        function ValidTo() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            var txtToDate = document.getElementById('<%=txtValidTo.ClientID %>').value;
            if (!datecheck(txtToDate)) {
                document.getElementById('<%=txtValidTo.ClientID %>').focus();
                document.getElementById('<%=txtValidTo.ClientID %>').value = '';
                return false;
            }
            var startDate = new Date(getJSDate(txtToDate));
            var curDate = new Date();
            if (startDate < curDate) {
                msg.innerHTML = "Valid to should not be less than or equal to  today's date";
                msg.style.color = "red";
                document.getElementById('<%=txtValidTo.ClientID%>').value = "";
                document.getElementById('<%=txtValidTo.ClientID%>').focus();
                return false;
            }
        }
        function AddClick() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            if (document.getElementById('<%=txtCoupons.ClientID %>').value.trim() == "") {
                msg.innerHTML = 'Required No. of coupons';
                msg.style.color = "red";
                document.getElementById('<%=txtCoupons.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtBatchName.ClientID %>').value.trim() == "") {
                msg.innerHTML = 'Required batch name';
                msg.style.color = "red";
                document.getElementById('<%=txtBatchName.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtCouponAmount.ClientID %>').value.trim() == "") {
                msg.innerHTML = 'Required Coupon amount ';
                msg.style.color = "red";
                document.getElementById('<%=txtCouponAmount.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtValidFrom.ClientID%>').value.trim() == "") {
                msg.innerHTML = 'Required Valid from ';
                msg.style.color = "red";
                document.getElementById('<%=txtValidFrom.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtValidTo.ClientID%>').value.trim() == "") {
                msg.innerHTML = 'Required Valid to ';
                msg.style.color = "red";
                document.getElementById('<%=txtValidTo.ClientID%>').focus();
                return false;
            }
            var txtValidFrom = document.getElementById('<%=txtValidFrom.ClientID %>').value;
            var txtValidTo = document.getElementById('<%=txtValidTo.ClientID %>').value;
            var startDate = new Date(getJSDate(txtValidFrom));
            var endDate = new Date(getJSDate(txtValidTo));
            if (startDate > endDate) {
                msg.innerHTML = "Valid from  should not be greater than valid to ";
                msg.style.color = "red";
                document.getElementById('<%=txtValidFrom.ClientID%>').value = "";
                document.getElementById('<%=txtValidFrom.ClientID%>').focus();
                return false;
            }
        }
    </script>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="btnShow" runat="server" CausesValidation="False" />
            <ajaxToolkit:ModalPopupExtender ID="modlpopup" BehaviorID="mdlPopup" runat="server"
                TargetControlID="btnShow" PopupControlID="pnlPopup" BackgroundCssClass="modalBackground"
                DynamicServicePath="" Enabled="True" />
            <asp:Panel ID="pnlPopup" runat="server" Style="text-align: center; display: block;
                width: 800px;">
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
                                                    <asp:Label ID="lblmdlheadertext" runat="server" Text="Add/Update Coupon"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="padding-left: 12px;">
                                                    <table width="100%" align="center">
                                                        <tr>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <span class="star">*</span><asp:Label ID="lblCoupons" runat="server" Text="No.of Coupons:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtCoupons" runat="server" MaxLength="4" SkinID="Txt_SkinSmall"
                                                                    onfocus="Change(this,event)" onblur="Change(this,event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteCoupons" runat="server" TargetControlID="txtCoupons"
                                                                    BehaviorID="bidCoupons" FilterType="Numbers">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px; width: 120px;">
                                                                <span class="star">*</span><asp:Label ID="lblBatchName" runat="server" Text="Batch Name:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtBatchName" runat="server" MaxLength="30" SkinID="Txt_Skin" onfocus="Change(this,event)"
                                                                    onblur="Change(this,event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    TargetControlID="txtBatchName" BehaviorID="bidBatchName" FilterMode="InvalidChars"
                                                                    InvalidChars=";|~!@#$%^*<>/\{}:';/*+_?~`[]=&quot;">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblCouponAmount" runat="server" Text="Coupon Amount:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;" colspan="3">
                                                                <asp:TextBox ID="txtCouponAmount" runat="server" SkinID="Txt_Skin" MaxLength="10"
                                                                    onfocus="Change(this,event)" onblur="Change(this,event)" onkeyup="return ValidateDecimal72(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteCouponAmount" runat="server" TargetControlID="txtCouponAmount"
                                                                    BehaviorID="bidCouponAmount" FilterMode="validChars" ValidChars="0123456789"
                                                                    Enabled="true">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblValidFrom" runat="server" Text="Valid From:"></asp:Label>
                                                            </td>
                                                            <td align="left" colspan="3" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtValidFrom" runat="server" SkinID="Txt_Skin" onfocus="Change(this,event)"
                                                                    onChange="ValidFrom()" onblur="Change(this,event)" onkeyup="return CheckFirstCharLogin(this);"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="calValidFrom" runat="server" BehaviorID="CalValidFrom"
                                                                    PopupButtonID="txtValidFrom" TargetControlID="txtValidFrom" CssClass="cal_Theme1"
                                                                    Format="dd-MM-yyyy" Enabled="true">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteValidFrom" runat="server" TargetControlID="txtValidFrom"
                                                                    BehaviorID="txtValidFrom" FilterMode="ValidChars" ValidChars="0123456789-">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblValidTo" runat="server" Text="Valid To:"></asp:Label>
                                                            </td>
                                                            <td align="left" colspan="3" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtValidTo" runat="server" SkinID="Txt_Skin" onfocus="Change(this,event)"
                                                                    onChange="ValidTo()" onblur="Change(this,event)" onkeyup="return CheckFirstCharLogin(this);"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalValidTo" runat="server" BehaviorID="CalValidTo"
                                                                    PopupButtonID="txtValidTo" TargetControlID="txtValidTo" CssClass="cal_Theme1"
                                                                    Format="dd-MM-yyyy" Enabled="true">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FteValidTo" runat="server" TargetControlID="txtValidTo"
                                                                    BehaviorID="txtValidTo" FilterMode="ValidChars" ValidChars="0123456789-">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblValidFacility" runat="server" Text="Valid in Branches:"></asp:Label>
                                                            </td>
                                                            <td align="left" colspan="3" style="padding-top: 5px; font-size: 11px;">
                                                                <asp:CheckBoxList ID="chkValidIsFacility" runat="server" RepeatColumns="3" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="chkValidIsFacility_SelectedIndexChanged">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                            </td>
                                                            <td align="left" colspan="3" style="padding-top: 5px; font-size: 11px;">
                                                                <asp:CheckBox ID="chkAll" runat="server" Text="ALL" onclick="CheckAll()" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblServiceGroup" runat="server" Text="Valid for Service Groups:"></asp:Label>
                                                            </td>
                                                            <td align="left" colspan="3" style="padding-top: 5px; font-size: 11px;">
                                                                <asp:CheckBoxList ID="chkServiceGroup" runat="server" RepeatColumns="3" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="chkServiceGroup_SelectedIndexChanged">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                            </td>
                                                            <td align="left" colspan="3" style="padding-top: 5px; font-size: 11px;">
                                                                <asp:CheckBox ID="chkServiceGroupAll" runat="server" Text="ALL" onclick="CheckServiceGroupAll()" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="4" style="padding-top: 10px;">
                                                                <asp:Button ID="btnSave" runat="server" CssClass="button_Add" Text="Save" ToolTip="Save"
                                                                    OnClientClick="return AddClick();" OnClick="btnSave_Click" />
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
                    <asp:Label ID="lblDoc" runat="server" Text="Coupon"></asp:Label>
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
                                                        MaxLength="10" autocomplete="off" onblur="Change(this, event)" onfocus="Change(this, event)"
                                                        onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                    <ajaxToolkit:AutoCompleteExtender ID="aceSearch" runat="server" BehaviorID="bidSearch"
                                                        CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_ListItem"
                                                        CompletionSetCount="20" MinimumPrefixLength="1" ServiceMethod="GetAllCoupons"
                                                        ServicePath="~/DhiiCommonService.asmx" TargetControlID="txtSearch" DelimiterCharacters=""
                                                        Enabled="True">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteSearch" runat="server" TargetControlID="txtSearch"
                                                        FilterMode="InvalidChars" InvalidChars=";|~!@#$%^*<>/\{}:';/*-+_~`[]=&quot;">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="wmeSearch" runat="server" TargetControlID="txtSearch"
                                                        WatermarkCssClass="watermarked" WatermarkText="Coupon">
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
                                        <asp:Button ID="btnAddCoupon" runat="server" CssClass="button_Add" Text="Add New"
                                            ToolTip="Add New" CausesValidation="False" OnClick="btnAddCoupon_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:GridView ID="gvCoupon" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                            AllowSorting="True" EmptyDataText="No Records Found" OnPageIndexChanging="gvCoupon_PageIndexChanging"
                                            OnRowDataBound="gvCoupon_RowDataBound" OnSorting="gvCoupon_Sorting" PageSize="15"
                                            SkinID="GridMasters" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="60px" />
                                                    <ItemStyle Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Batch Name" SortExpression="BatchName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBatchName" runat="server" Text='<%# Eval("BatchName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Coupon No" SortExpression="CouponNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCouponNo" runat="server" Text='<%# Eval("CouponNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valid From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValidFrom" runat="server" Text='<%# Eval("ValidFrom","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valid To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValidTo" runat="server" Text='<%# Eval("ValidTo","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# (bool)Eval("Status")?"Active":"Inactive" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hfCouponId" runat="server" />
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
