<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XX_FD.aspx.cs" Inherits="SincciKC.websystem.zklq.XX_FD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
       <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
       
    </script>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="windowTable">
            <tr>
                <td colspan="2" class="title">单校发档</td>
            </tr>

            <tr>
                <td class="labelRedTD" style=" width:160px">发档学校：</td>
                <td class="contentTD">
                  <asp:DropDownList ID="ddl_xx" runat="server"  >
                  </asp:DropDownList>
                </td>
            </tr>

       

            <tr>
                <td class="labelRedTD">建议专业：</td>
                <td class="contentTD">
                 <asp:DropDownList ID="ddljy" runat="server"  >
                  <asp:ListItem  Value="1">分数优先</asp:ListItem>
                  <asp:ListItem  Value="2">志愿优先</asp:ListItem>
                  </asp:DropDownList>     </td>
            </tr>
             
            <tr >
                <td colspan="2" class="buttonBar">
                  <asp:Button ID="btnEnter" Text=" 发档 " CssClass="btnStyle" runat="server" onclick="btnEnter_Click"   />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
