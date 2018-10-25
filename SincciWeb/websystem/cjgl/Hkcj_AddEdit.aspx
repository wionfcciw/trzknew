<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hkcj_AddEdit.aspx.cs" Inherits="SincciKC.websystem.cjgl.Hkcj_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
        <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#txtByxxdm").val() == "") {
                $("#txtByxxdm").focus();
                alert("请输入学校代码。");
                return false;
            }

            if ($("#txtXqmc").val() == "") {
                $("#txtXqmc").focus();
                alert("请输入学校名称。");
                return false;
            }

            return true;
        }
    </script>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="windowTable">
            <tr>
                <td colspan="2" class="title">考生会考成绩修改</td>
            </tr>

            <tr>
                <td class="labelRedTD">报名号：</td>
                <td class="contentTD">
                    <asp:Label ID="lblksh" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>

          
 
              <tr>
                <td class="labelRedTD">生物：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="smList" runat="server">
                  <asp:ListItem Value="A">A</asp:ListItem>
                        <asp:ListItem Value="B">B</asp:ListItem>
                           <asp:ListItem Value="C">C</asp:ListItem>
                        <asp:ListItem Value="D">D</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">地理：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="cxysList" runat="server">
                      <asp:ListItem Value="A">A</asp:ListItem>
                        <asp:ListItem Value="B">B</asp:ListItem>
                           <asp:ListItem Value="C">C</asp:ListItem>
                        <asp:ListItem Value="D">D</asp:ListItem>
                    </asp:DropDownList>
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
