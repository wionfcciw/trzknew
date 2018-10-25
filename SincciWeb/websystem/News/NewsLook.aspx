<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsLook.aspx.cs" Inherits="SincciWeb.NewsList.NewsLook" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>信息查看</title>
<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../css/page.css"  />
<script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />


</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="1" align="center" cellpadding="4" style="border-collapse:collapse; margin-top:1px" cellspacing="0" width="100%" height="100%" >
  <tr>
    <td align="left" height="25" bgcolor="#F7F7F7">&nbsp;&nbsp;<asp:Label ID="lblTitle" runat="server" Text=" "></asp:Label></td>
    
  </tr>
   
  <tr>
    <td><asp:Label ID="lblContent" runat="server" Text=" "></asp:Label></td>
   
  </tr>
  <tr><td align="center">
  <asp:Button ID="Button1" runat="server" Text="  返回" CssClass="icon-add btn" OnClientClick="javascript:window.opener=window.location.href;window.close()"/></td></tr>
</table>
 
    </div>
    </form>
</body>
</html>
