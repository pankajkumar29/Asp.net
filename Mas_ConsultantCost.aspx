<%@ Page Title="Consultant Cost" Language="C#" MasterPageFile="~/Application/Common/Common.Master"
    AutoEventWireup="true" CodeFile="Mas_ConsultantCost.aspx.cs" Inherits="DhiiLifeSpring.Application.Masters.Mas_ConsultantCost"
    StylesheetTheme="Controls" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="Server">
    <script src="../../Javascripts/focusncheckchar.js" type="text/javascript"></script>
    <link href="../../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript" language="JavaScript">
        function SearchConsultantCost() {
            var msg = document.getElementById('<%=lblError.ClientID %>');
            if (document.getElementById('<%=ddlBranch.ClientID %>').selectedIndex == 0) {
                msg.innerHTML = "Select branch";
                msg.style.color = "red";
                $get("<%=ddlBranch.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=ddlConsultant.ClientID %>').selectedIndex == 0) {
                msg.innerHTML = "Select consultant";
                msg.style.color = "red";
                $get("<%=ddlConsultant.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <fieldset style="border: 1px dashed #ccc; margin-left: 10px; margin-top: 0px; text-align: right;
                padding-bottom: 10px;">
                <legend class="legend_right" style="margin-right: 10px;" align="right">
                    <asp:Label ID="lblDoc" runat="server" Text="Consultant Cost"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td align="right">
                            <span class="star">*</span><asp:Label ID="Label1" runat="server" Text="Branch:"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlBranch" runat="server" onfocus="Change(this, event)" Width="180px"
                                onblur="Change(this, event)" AutoPostBack="True" SkinID="Dpd_Skin" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="padding-top: 5px;">
                            <span class="star">*</span>Consultant:
                        </td>
                        <td align="left" style="padding-top: 5px;">
                            <asp:DropDownList ID="ddlConsultant" runat="server" onblur="Change(this, event)"
                                onfocus="Change(this, event)" SkinID="Dpd_Skin" ToolTip="Select Consultant" Width="170px"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="padding-top: 5px;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="button_Add" Text="Search" ToolTip="Click to search"
                                OnClientClick="return SearchConsultantCost()" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="5">
                            <asp:Panel ID="PanOp" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td align="left" colspan="4" style="padding-top: 5px;">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <img alt="" src="../../Images/white-strp-left.png" />
                                                    </td>
                                                    <td class="tdHeading" valign="top">
                                                        OP Consultation Fee
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <img alt="" src="../../Images/white-strp-rt.png" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding-top: 5px;" colspan="4">
                                            <asp:GridView ID="gvOpConsultantcost" runat="server" AutoGenerateColumns="false"
                                                ShowFooter="true" AllowPaging="false" SkinID="GridTransactions" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S. No." HeaderStyle-Width="40px" ItemStyle-VerticalAlign="Top">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Specilization" HeaderStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSPECIALIZATIONID" Visible="false" runat="server" Text='<%# Bind("SPECIALIZATIONID") %>'></asp:Label>
                                                            <asp:Label ID="lblSPECIALIZATION" runat="server" Text='<%# Bind("SPECIALIZATION") %>'
                                                                Visible="true"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Consultant Fee" HeaderStyle-Width="85px" ItemStyle-VerticalAlign="Top">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtConsultantCost" runat="server" SkinID="Txt_Skin" Style="width: 70px"
                                                                MaxLength="4" Text='<%# Bind("ConsultantCost") %>' onkeyup="return CheckFirstCharLogin(this);"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteInactiveSince" runat="server" FilterMode="ValidChars"
                                                                ValidChars="0123456789." TargetControlID="txtConsultantCost">
                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="85px" />
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Consultant Share" HeaderStyle-Width="120px" ItemStyle-VerticalAlign="Top">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtConsultantAmount" runat="server" SkinID="Txt_Skin" Style="width: 70px"
                                                                MaxLength="4" Text='<%# Bind("DoctorShareAmt") %>' onkeyup="return CheckFirstCharLogin(this);"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="flttxtConsultantAmount" runat="server" FilterMode="ValidChars"
                                                                ValidChars="0123456789." TargetControlID="txtConsultantAmount">
                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="85px" />
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valid Days" HeaderStyle-Width="85px" ItemStyle-VerticalAlign="Top">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtValidDays" runat="server" SkinID="Txt_Skin" Style="width: 50px"
                                                                MaxLength="2" Text='<%# Bind("ValidDays") %>' onkeyup="return CheckFirstCharLogin(this);"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fltValidDays" runat="server" FilterMode="ValidChars"
                                                                ValidChars="0123456789." TargetControlID="txtValidDays">
                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="85px" />
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" align="center" id="trbutton" runat="server" visible="false">
                            <asp:Button ID="btnSave" runat="server" CssClass="button_Add" Text="Save" ToolTip="Click to save"
                                onblur="Change(this, event)" onfocus="Change(this, event)" OnClientClick="return SearchConsultantCost()"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="button_Add" Text="Clear" ToolTip="Click to clear"
                                CausesValidation="False" onblur="Change(this, event)" onfocus="Change(this, event)"
                                OnClick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" align="center">
                            <asp:Label ID="lblError" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
