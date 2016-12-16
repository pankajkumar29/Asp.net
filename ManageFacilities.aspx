<%@ Page Title="Manage Branches " Language="C#" MasterPageFile="~/Application/Common/Common.Master"
    AutoEventWireup="true" CodeFile="ManageFacilities.aspx.cs" Inherits="DhiiLifeSpring.Application.Masters.ManageFacilities"
    StylesheetTheme="Controls" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
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
            $get("<%=txtFacility.ClientID%>").focus();
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                $find('mdlPopup').hide();
                document.getElementById('<%=btnAddFacility.ClientID %>').focus();
            }
        }
        var TotalChkBx;
        var Counter;
        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvWards.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }
        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvWards.ClientID %>');
            var TargetChildControl = "ChkSelect";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;
            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function Click(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvWards.ClientID %>');
            var TargetChildControl = "ChkSelect";

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
        function AddClick() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            if (document.getElementById('<%=txtFacility.ClientID%>').value.trim().length == 0) {
                msg.innerHTML = "Required Branch";
                msg.style.color = "red";
                $get("<%=txtFacility.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtAddress.ClientID%>').value.trim().length == 0) {
                msg.innerHTML = "Required address";
                msg.style.color = "red";
                $get("<%=txtAddress.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtPhoneNo.ClientID%>').value.trim().length == 0) {
                msg.innerHTML = "Required phone no.";
                msg.style.color = "red";
                $get("<%=txtPhoneNo.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtPhoneNo.ClientID%>').value.trim().length > 0) {
                if (document.getElementById('<%=txtPhoneNo.ClientID%>').value.trim().length < 10) {
                    msg.innerHTML = "Invalid phone no.";
                    msg.style.color = "red";
                    $get("<%=txtPhoneNo.ClientID%>").focus();
                    return false;
                }
            }
            if (document.getElementById('<%=txtMobileNo.ClientID%>').value.trim().length > 0) {
                if (document.getElementById('<%=txtMobileNo.ClientID%>').value.trim().length < 10) {
                    msg.innerHTML = "Invalid mobile no.";
                    msg.style.color = "red";
                    $get("<%=txtMobileNo.ClientID%>").focus();
                    return false;
                }
            }
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
            if (document.getElementById('<%=ddlColony.ClientID %>').selectedIndex == 0) {
                msg.innerHTML = "Select colony";
                msg.style.color = "red";
                document.getElementById('<%=ddlColony.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlStreet.ClientID %>').selectedIndex == 0) {
                msg.innerHTML = "Select street";
                msg.style.color = "red";
                document.getElementById('<%=ddlStreet.ClientID %>').focus();
                return false;
            }
            else {
                msg.innerHTML = "";
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="btnShow" runat="server" CausesValidation="False" />
            <ajaxToolkit:ModalPopupExtender ID="modlpopup" BehaviorID="mdlPopup" runat="server"
                TargetControlID="btnShow" PopupControlID="pnlPopup" BackgroundCssClass="modalBackground"
                DynamicServicePath="" Enabled="True" />
            <asp:Panel ID="pnlPopup" runat="server" Style="text-align: center; display: none;
                width: 650px;">
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
                                                    <asp:Label ID="lblmdlheadertext" runat="server" Text="Add/Update Branch"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="padding-left: 12px;">
                                                    <table width="96%" align="center">
                                                        <tr>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <span class="star">*</span><asp:Label ID="lblFacility" runat="server" Text="Branch:"></asp:Label>
                                                            </td>
                                                            <td colspan="3" align="left" style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtFacility" runat="server" SkinID="Txt_Skin" Style="width: 300px;"
                                                                    MaxLength="50" onfocus="Change(this, event)" onblur="Change(this, event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteFacility" runat="server" FilterMode="InvalidChars"
                                                                    TargetControlID="txtFacility" InvalidChars=";|~!$%^&'*<>/\{}:';/*+_?~`()[]=,1234567890&quot;">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td rowspan="2" align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label>
                                                            </td>
                                                            <td rowspan="2" align="left" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtAddress" runat="server" SkinID="Txt_Skin" TextMode="MultiLine"
                                                                    Style="height: 50px" onblur="Change(this, event)" onfocus="Change(this, event)"
                                                                    onkeyup="return CheckFirstChar(this);" onkeyDown="checkTextAreaMaxLength(this,event,'250');"></asp:TextBox>
                                                            </td>
                                                            <td valign="top" align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblPhoneNo" runat="server" Text="Phone No.:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtPhoneNo" runat="server" SkinID="Txt_Skin" MaxLength="15" onblur="Change(this, event)"
                                                                    onfocus="Change(this, event)" onkeyup="return CheckFirstCharLogin(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftePhoneNo" runat="server" TargetControlID="txtPhoneNo"
                                                                    ValidChars="1234567890" Enabled="True">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="left" style="padding-top: 5px;">
                                                                <span class="star">&nbsp;&nbsp;</span><asp:Label ID="lblMobileNo" runat="server"
                                                                    Text="Mobile No.:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:TextBox ID="txtMobileNo" runat="server" SkinID="Txt_Skin" MaxLength="10" onblur="Change(this, event)"
                                                                    onfocus="Change(this, event)" onkeyup="return CheckFirstChar(this);"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteMobileNo" runat="server" TargetControlID="txtMobileNo"
                                                                    ValidChars="1234567890" Enabled="True">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="4" style="padding-top: 10px; font-weight: bold; color: Maroon">
                                                                Default Settings:
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <span class="star">*</span><asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <asp:DropDownList ID="ddlCity" runat="server" SkinID="Dpd_Skin" onblur="Change(this, event);"
                                                                    onfocus="Change(this, event)" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <span class="star">*</span><asp:Label ID="lblMunicipalWard" runat="server" Text="Municipal Ward:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 10px;">
                                                                <asp:DropDownList ID="ddlMunicipalWard" runat="server" SkinID="Dpd_Skin" onblur="Change(this, event);"
                                                                    onfocus="Change(this, event)" AutoPostBack="true" OnSelectedIndexChanged="ddlMunicipalWard_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblColony" runat="server" Text="Colony:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:DropDownList ID="ddlColony" runat="server" SkinID="Dpd_Skin" onblur="Change(this, event);"
                                                                    onfocus="Change(this, event)" AutoPostBack="true" OnSelectedIndexChanged="ddlColony_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <span class="star">*</span><asp:Label ID="lblStreet" runat="server" Text="Street:"></asp:Label>
                                                            </td>
                                                            <td align="left" style="padding-top: 5px;">
                                                                <asp:DropDownList ID="ddlStreet" runat="server" SkinID="Dpd_Skin" onblur="Change(this, event);"
                                                                    onfocus="Change(this, event)">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="4" style="padding-top: 10px; font-weight: bold; color: Maroon">
                                                                Wards and Beds:
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="4" style="padding-top: 10px;">
                                                                <asp:GridView ID="gvWards" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                                    PageSize="15" AllowSorting="True" EmptyDataText="No Records Found" SkinID="GridMasters"
                                                                    Width="96%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-Width="20px">
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkHeadSelect" runat="server" onclick="javascript:HeaderClick(this);" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ChkSelect" runat="server" onclick="javascript:Click(this);" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ward" SortExpression="Ward">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblWardName" runat="server" Text='<%# Eval("Ward") %>'></asp:Label>
                                                                                <asp:Label ID="lblWardId" runat="server" Text='<%# Eval("WardId") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblShortCode" runat="server" Text='<%# Eval("ShortCode") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblOrder" runat="server" Text='<%# Eval("Ordering") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="headerBorderLine" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No. Of Beds">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtBeds" runat="server" SkinID="Txt_SkinSmall" AutoPostBack="true"
                                                                                    OnTextChanged="txtBeds_TextChanged"></asp:TextBox>
                                                                                <asp:Label ID="lblFacilityWardId" runat="server" Text='<%# Eval("FacilityWardId") %>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblFacilityNoOfBeds" runat="server" Text='<%# Eval("FacilityNoOfBeds") %>'
                                                                                    Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="130px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="4" style="padding-top: 10px; font-weight: bold; color: Maroon">
                                                                Messages to be printed on bill:
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="4" style="padding-top: 10px;">
                                                                <asp:GridView ID="gvMessages" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                                    PageSize="15" AllowSorting="True" EmptyDataText="No Records Found" SkinID="GridMasters"
                                                                    Width="96%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-Width="80px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Message">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtMessages" runat="server" SkinID="Txt_Skin" Style="width: 450px;"
                                                                                    MaxLength="100" Text='<%# Eval("Message") %>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2" style="padding-top: 10px;">
                                                                Copy Service cost from:
                                                            </td>
                                                            <td align="left" colspan="2" style="padding-top: 10px;">
                                                                <asp:DropDownList ID="ddlbarnch" runat="server" SkinID="Dpd_Skin" onfocus="Change(this, event)"
                                                                    onblur="Change(this, event)">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="4" style="padding-top: 10px;">
                                                                <asp:Button ID="btnCreate" runat="server" CssClass="button_Add" Text="Create" ToolTip="Create Facility"
                                                                    OnClientClick="return AddClick();" OnClick="btnCreate_Click" /> <%----%>
                                                                <asp:Button ID="btnClear" runat="server" CssClass="button_Add" CausesValidation="False"
                                                                    Text="Clear" ToolTip="Text Clear" OnClick="btnClear_CLick"  />
                                                                <asp:LinkButton ID="lnkClose" runat="server" ToolTip="Close" CausesValidation="False"
                                                                    CssClass="close" OnClientClick="$find('mdlPopup').hide();return false;" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="padding-top: 10px; height: 15px;">
                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True"></asp:Label>
                                                    <asp:HiddenField ID="hfFacilityId" runat="server" />
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
            <fieldset style="border: 1px dashed #ccc; margin-left: 10px; margin-top: 0px; text-align: right;
                padding-bottom: 10px;">
                <legend class="legend_right" style="margin-right: 10px;" align="right">
                    <asp:Label ID="lblDoc" runat="server" Text="Manage Branches"></asp:Label>
                </legend>
                <table width="100%" align="center">
                    <tr>
                        <td align="right" style="padding-top: 10px; padding-right: 10px;">
                            <asp:Button ID="btnAddFacility" runat="server" CssClass="button_big_Add" Text="Add Branch" Width="90px"
                                ToolTip="Add Branch" CausesValidation="False" OnClick="btnAddFacility_Click"
                                 />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" >
                            <asp:GridView ID="gvFacilities" runat="server" SkinID="GridMasters" AutoGenerateColumns="False"
                                AllowPaging="True" PageSize="5" AllowSorting="True" Width="98%" EmptyDataText="No Records Found"
                                OnRowCommand="gvFacilities_RowCommand" OnPageIndexChanging="gvFacilities_PageIndexChanging"
                                OnSorting="gvFacilities_Sorting" OnRowDataBound="gvFacilities_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="S. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch" SortExpression="FacilityName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("FacilityName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="headerBorderLine" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="160px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton CausesValidation="False" ID="lnkedit" runat="server" Width="20px"
                                                Height="20px" ImageUrl="~/Images/edit-notes.png" ToolTip="Edit" CommandName="Modify"
                                                CommandArgument='<%# Eval("FacilityId") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
