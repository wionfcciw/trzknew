<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserTypeAddEdit.aspx.cs" Inherits="SincciWeb.system.UserTypeAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增修改用户类型</title>

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
    <table class="windowTable" width="99%" border="1" align="center" style="border-collapse:collapse; margin-top:5px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
  <tr>
    <td align="right">用户类型名称：</td>
    <td align="left"><asp:TextBox ID="txtT_Name" CssClass="input1"  runat="server"></asp:TextBox> 
    </td>
  </tr>
  
  <tr>
    <td colspan="2" align="center">
    
        <asp:Button ID="btnSave" runat="server" CssClass="btnStyle" Text=" 保存 " onclick="btnSave_Click" /> </td>
  </tr>
  
</table>
 
    </div>
    </form>
</body>
</html>
