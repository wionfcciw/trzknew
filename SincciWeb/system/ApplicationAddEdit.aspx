<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationAddEdit.aspx.cs" Inherits="SincciWeb.system.ApplicationAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>增加修改应用</title>

<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
 
   <link href="../../css/style.css" rel="stylesheet" type="text/css" />
 
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />


</head>
<body>
    <form id="form1" runat="server">
    <div> 
    <table class="windowTable" width="99%" border="1" align="center" style="border-collapse:collapse; margin-top:15px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
  <tr>
    <td align="right" style=" width:30%">选择模块：</td>
    <td align="left"><asp:DropDownList ID="ddlModule" runat="server"  >  </asp:DropDownList>

        <asp:Label ID="lblApplicationid" runat="server" Text="0"  Visible="false"></asp:Label>
        <asp:Label ID="lblA_order" runat="server"  Visible="false"></asp:Label>

    </td>
  </tr>
  <tr>
    <td align="right">应用名称：</td>
    <td align="left">
        <asp:TextBox ID="txtA_appname" CssClass="input1" runat="server" Width="216px"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td align="right">应用网址：</td>
    <td align="left">
        <asp:TextBox ID="txtA_url" CssClass="input1" runat="server" Width="288px"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td align="right">应用图标：</td>
    <td align="left">
        <asp:TextBox ID="txtA_picurl" CssClass="input1" runat="server" Width="288px"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td align="right">页面代码：</td>
    <td align="left">
        <asp:TextBox ID="txtA_pagecode" CssClass="input1" runat="server" Width="117px"></asp:TextBox>
      </td>
  </tr>
  <tr>
   <td align="right">状态：</td>
    <td align="left">
        <asp:DropDownList ID="ddlTag" runat="server">
            <asp:ListItem Value="1">开通</asp:ListItem>
            <asp:ListItem Value="0">关闭</asp:ListItem>
        </asp:DropDownList></td>
  </tr>
  <tr>
    <td colspan="2" align="center">
    
        <asp:Button ID="btnSave" runat="server" CssClass="btnStyle"   Text=" 保存 " onclick="btnSave_Click" /> </td>
  </tr>
  
</table>
 
    </div>
    </form>
</body>
</html>
