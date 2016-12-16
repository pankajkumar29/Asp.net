<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Login.aspx.cs" Inherits="DhiiLifeSpring.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LifeSpring Application</title>
    <meta content='IE=EmulateIE7' http-equiv='X-UA-Compatible' />
    <link href="Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Styles/cms_styles.css" rel="stylesheet" type="text/css" />
    <script src="Javascripts/focusncheckchar.js" type="text/javascript"></script>
    <link href="Images/favicon.ico" rel="SHORTCUT ICON" />
    <script type="text/javascript">
        function getCookie(c_name) {
            var i, x, y, ARRcookies = document.cookie.split(";");
            for (i = 0; i < ARRcookies.length; i++) {
                x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                x = x.replace(/^\s+|\s+$/g, "");
                if (x == c_name) {
                    return unescape(y);
                }
            }
        }
        function checkCookie() {
            var username = getCookie("username");
            if (username != null && username != "") {
                //alert("Welcome again " + username);
            }
            else {
                self.location = "AccessDenied.aspx";
            }
        }
        function Validate() {
            var msg = document.getElementById('<%=lblMsg.ClientID %>');
            var uname = document.getElementById('<%=txtUserName.ClientID %>');
            var pwd = document.getElementById('<%=txtPassword.ClientID %>');
            if (document.getElementById('<%=txtUserName.ClientID %>').value.length == 0) {
                var msg = document.getElementById('<%=lblMsg.ClientID %>');
                msg.innerHTML = "Please Enter user name ";
                msg.style.color = "red";
                document.getElementById('<%=txtUserName.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtUserName.ClientID %>').value.length < 5) {
                var msg = document.getElementById('<%=lblMsg.ClientID %>');
                msg.innerHTML = "User Name Should Contain Atleast 5 Chars ";
                msg.style.color = "red";
                document.getElementById('<%=txtUserName.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtPassword.ClientID %>').value.length == 0) {
                var msg = document.getElementById('<%=lblMsg.ClientID %>');
                msg.innerHTML = "Please Enter Password ";
                msg.style.color = "red";
                document.getElementById('<%=txtPassword.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtPassword.ClientID %>').value.length < 4) {
                var msg = document.getElementById('<%=lblMsg.ClientID %>');
                msg.innerHTML = "Password Should Contain Atleast 4 Chars ";
                msg.style.color = "red";
                document.getElementById('<%=txtPassword.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlBranch.ClientID %>').selectedIndex == 0) {
                msg.innerHTML = "Select branch";
                msg.style.color = "red";
                document.getElementById('<%=ddlBranch.ClientID %>').focus();
                return false;
            }
            else {
                msg.innerHTML = "";
            }
        }    
    </script>
</head>
<body>
    <%--onload="checkCookie();"--%>
    <form id="form1" runat="server">
    <center>
        <div id="main_wrapper">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="content_wrapper">
                <tr>
                    <td width="278" height="95" align="left" valign="top">
                        &nbsp;
                    </td>
                    <td width="422" height="188px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" class="cms">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="login_wrapper">
                            <tr>
                                <td width="151" align="right">
                                    <img src="Images/lock.gif" width="60" height="58" alt="lock" class="lock" />
                                </td>
                                <td width="48" align="center" valign="middle">
                                    <img src="Images/saperator.gif" width="10" height="179" alt="saperator" />
                                </td>
                                <td align="left" width="370">
                                    <asp:Panel ID="PnlLogin" runat="server">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="actual_login">
                                            <tr>
                                                <td width="35%" align="right" style="color: White">
                                                    <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
                                                </td>
                                                <td align="left" class="textbox">
                                                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="100" onblur="Change(this, event)"
                                                        onfocus="Change(this, event)" onkeyup="CheckFirstCharLogin(this);" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="color: White">
                                                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                                                </td>
                                                <td align="left" class="textbox">
                                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="12" onblur="Change(this, event)"
                                                        onfocus="Change(this, event)" onkeyup="CheckFirstChar(this);" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="color: White">
                                                    <asp:Label ID="Label1" runat="server" Text="Branch"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlBranch" runat="server" onfocus="Change(this, event)" Width="180px"
                                                        onblur="Change(this, event)">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="right">
                                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn_login" Text="Login" ToolTip="Click Here To Login"
                                                        OnClientClick="return Validate();" OnClick="btnLogin_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn_login" Text="Reset" ToolTip="Text Clear"
                                                        OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center" style="width: 250px; height: 10px;">
                                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Size="10px" Font-Bold="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>
