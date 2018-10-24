<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XX_ytui.aspx.cs" Inherits="SincciKC.websystem.zklq.XX_ytui" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
       <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function checkInput() {
            if ($("#txtqk").val() == "") {
                $("#txtqk").focus();
                alert("请输入预退原因!");
                return false;
            }
        }
    </script>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="windowTable">
            <tr>
                <td colspan="2" class="title">考生预退</td>
            </tr>

            <tr>
                <td class="labelRedTD" style=" width:160px">报名号：</td>
                <td class="contentTD">
                    <asp:Label ID="lblksh" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="labelRedTD">姓名：</td>
                <td class="contentTD">
                    <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                                      </td>
            </tr>

            <tr>
                <td class="labelRedTD">预退原因：</td>
                <td class="contentTD">
 
                         <asp:TextBox Width="300" Height="60" TextMode="MultiLine" ID="txtqk" runat="server" Text=""> </asp:TextBox> 
                </td>
            </tr>
             
            <tr >
                <td colspan="2" class="buttonBar">
                  <asp:Button ID="btnEnter" Text=" 保 存 " CssClass="btnStyle" runat="server" onclick="btnEnter_Click" OnClientClick="return checkInput()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
