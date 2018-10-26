<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsType_AddEdit.aspx.cs" Inherits="SincciWeb.websystem.News.NewsType_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
     <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
       <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="300px" border="1" align="center" style="border-collapse:collapse; margin-top:15px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
  <tr>
    <td align="right" style="width:35%;">类型名称：</td>
    <td align="left"><asp:TextBox ID="txtName" CssClass="input1" runat="server"  ></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfv1" runat="server" 
            ForeColor="Red" ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:Label ID="lblLevel" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="lblParentID" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="lblID" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
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
