<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuAddEdit.aspx.cs" Inherits="SincciKC.system.MenuAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>增改菜单</title>

<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
 
 
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
 <link href="../../css/style.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div> 
    <table   class="windowTable" width="99%" border="1" align="center" style="border-collapse:collapse; margin-top:5px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
   
  <tr>
    <td align="right">名称：</td>
    <td align="left"><asp:HiddenField   ID="Hid" runat="server" />
        <asp:TextBox ID="txtName" runat="server" CssClass="input1" Width="216px"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td align="right">网址：</td>
    <td align="left">
        <asp:TextBox ID="txtUrl" runat="server" CssClass="input1" Width="288px"></asp:TextBox>
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
    
        <asp:Button ID="btnSave" runat="server"  Text=" 保存 " CssClass="btnStyle" onclick="btnSave_Click" /> </td>
  </tr>
  
</table>
 
    </div>
    </form>
</body>
</html>
