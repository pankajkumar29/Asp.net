<%@ Page Title="Access Denied" Language="C#" AutoEventWireup="true" CodeFile="AccessDenied.aspx.cs"
    Inherits="AccessDenied" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.01 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LifeSpring Application</title>
    <link href="~/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/cms_innerpage_styles.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="~/Styles/accordin_style.css" />
    <link href="~/SpryAssets/SpryMenuBarHorizontal.css" rel="stylesheet" type="text/css" />
    <link href="~/Images/logo.ico" rel="SHORTCUT ICON" />
    <%--<script type="text/javascript" language="JavaScript1.2">
        if (document.all) {
            document.onkeydown = function () {
                var key_f5 = 116; // 116 = F5  
                if (key_f5 == event.keyCode) {
                    event.keyCode = 27;
                    return false;
                }
            }
        }
        window.history.forward();
        function checkShortcut() {
            if (event.keyCode == 13) {
                return false;
            }
        }
    </script>
    <style type="text/css">
        .info
        {
            border: none;
            text-decoration: none;
        }
        .info img
        {
            border: none;
            text-decoration: none;
        }
        .info:hover
        {
            border: none;
            text-decoration: none;
        }
        .imginfo
        {
            border: none;
            padding-top: 2px;
            padding-bottom: -2px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            function adPostWidth() {
                var ovWid = $('#header').width(); /*We use the header width with min-height of 700px in the style*/
                var postN = Math.floor(ovWid / 250);
                var widFix = Math.floor(ovWid / postN);
                $(".post_cont").css({ 'width': widFix });
            }
            function WidthHead() {
                var ovWid = $('#header').width(); /*We use the header width with min-height of 700px in the style*/
                var widFixHead = Math.floor(ovWid / 3);
                $(".head_info").css({ 'width': widFixHead - 2 });
            }
            adPostWidth();
            WidthHead();
            $(window).resize(function () {
                adPostWidth();
                WidthHead();
            });
            //hide toolbar and make visible the 'show' button
            $("span.downarr a").click(function () {
                $("#toolbar").slideToggle("fast");
                $("#toolbarbut").fadeIn("slow");

            });
            //show toolbar and hide the 'show' button
            $("span.showbar a").click(function () {
                $("#toolbar").slideToggle("fast");
                $("#toolbarbut").fadeOut();
            });
        });

    </script>--%>
</head>
<body>
    <form id="frmMaster" runat="server">
    <center>
        <div id="main_wrapper">
            <div id="logo">
            </div>
            <div id="content" class="clear">
                <div id="content_right">
                    <div style="padding-top: 200px;">
                        <h1>
                            <asp:Image ID="imgDeny" runat="server" ImageUrl="~/Images/Access Denied.jpg" />
                            <br />
                            <asp:Label ID="lblMsg" Text="This application cannot be accessed on this system."
                                runat="server" Font-Names="Miriam Fixed"
                                Font-Size="X-Large" ForeColor="Red" Font-Bold="True"></asp:Label>
                        </h1>
                    </div>
                </div>
            </div>
        </div>
        <div style="display: block;" id="toolbar">
        </div>
    </center>
    </form>
</body>
</html>
