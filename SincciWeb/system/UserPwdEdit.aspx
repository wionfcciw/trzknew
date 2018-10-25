<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPwdEdit.aspx.cs" Inherits="SincciWeb.system.UserPwdEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户密码修改</title>

<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
  <link href="../../css/style.css" rel="stylesheet" type="text/css" />
 
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

 

<style type="text/css" >
 html 
{
	overflow-x: hidden;   /*- 横滚动条 -*/
	overflow-y: hidden;      /*- 竖滚动条 -*/
}
</style>

</head>
<body>
    <form id="form1" runat="server">
      <div id="tb" class="datagrid-toolbar" style="text-align:center" >
         <span class="font20" >修改密码</span>
             </div>
    <div> 
    <table  class="windowTable" width="600" border="1" align="center" style="border-collapse:collapse; margin-top:5px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
  <tr>
    <td align="right">旧密码：</td>
    <td align="left"> 
        <asp:TextBox ID="txtOldpwd" runat="server" CssClass="input1" Width="200px" MaxLength="12" TextMode="Password"></asp:TextBox>
      </td>
  </tr>
  <tr runat="server" id="trpwd">
    <td align="right" style=" width:40%">新密码：</td>
    <td align="left">
        <asp:TextBox ID="txtU_password" runat="server" CssClass="input1" Width="200px" MaxLength="12"
            TextMode="Password"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td align="right">确认密码：</td>
    <td align="left">
        <asp:TextBox ID="txtU_password2" runat="server" CssClass="input1" Width="200px" MaxLength="12" TextMode="Password"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td colspan="2" align="center">
    
        <asp:Button ID="btnSave" runat="server" CssClass="btnStyle"  Text=" 保存 " onclick="btnSave_Click" 
            style="height: 21px" /> </td>
  </tr>
  
</table>
 
    </div>
    </form>
</body>
</html>
