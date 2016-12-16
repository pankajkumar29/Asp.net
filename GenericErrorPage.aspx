<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/GenericErrorPage.aspx.cs"
    Inherits="DhiiCMS.GenericErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/cms_innerpage_styles.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div id="main_wrapper">
            <div id="logo">
            </div>
            <div id="content" class="clear">
                <div id="content_right">
                    <div style="padding-top: 100px;">
                        <h1>
                            <asp:Label Text="Oops! An error occurred while processing your request. Please "
                                runat="server" Style="font-weight: 700; text-align: center" />
                            <asp:LinkButton Text=" try again " runat="server" PostBackUrl="~/Login.aspx" Font-Underline="false"
                                ForeColor="#007FBC" />
                        </h1>
                    </div>
                    <div style="padding-top: 10px;">
                        <h2>
                            <asp:Label ID="Label1" Text=" Contact administrator for more information." runat="server" />
                        </h2>
                    </div>
                </div>
            </div>
        </div>
    </center>
    </form>
</body>
</html>
