﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Security.aspx.cs" Inherits="Security" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Security</title>
     <script type="text/javascript" language="javascript">
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

         function setCookie(c_name, value, exdays) {
             var exdate = new Date();
             exdate.setDate(exdate.getDate() + exdays);
             var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
             document.cookie = c_name + "=" + c_value;
         }

         function checkCookie() {
             var username = getCookie("username");
             if (username != null && username != "") {
                 alert("Welcome again " + username);
                 window.navigate("Login.aspx");
             }
             else {
                 username = prompt("Please enter your name:", "");
                 if (username != null && username != "") {
                     setCookie("username", username, 365);

                 }
             }
         }
</script>
</head>
<body onload="checkCookie()">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
