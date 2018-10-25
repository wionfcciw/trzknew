<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tyxm_AddEdit.aspx.cs" Inherits="SincciKC.websystem.tyxm.Tyxm_AddEdit" %>

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
            if ($("#txtXqdm").val() == "") {
                $("#txtXqdm").focus();
                alert("请输入县区代码");
                return false;
            }

            if ($("#txtXqmc").val() == "") {
                $("#txtXqmc").focus();
                alert("请输入县区名称");
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
                <td colspan="2" class="title">体育考试信息设置</td>
            </tr>

            <tr>
                <td class="labelRedTD">项目类型：</td>
                <td class="contentTD"> 
                <asp:DropDownList ID="dlistxx" runat="server"
        >
             <asp:ListItem Value="3">自选项目1</asp:ListItem>
            <asp:ListItem Value="4">自选项目2</asp:ListItem>
            <asp:ListItem Value="5">自选项目3</asp:ListItem>
        </asp:DropDownList></td>
            </tr>

            <tr>
                <td class="labelRedTD">项目名称：</td>
                <td class="contentTD"><asp:TextBox ID="txtXqmc" runat="server" CssClass="input1" MaxLength="20"></asp:TextBox></td>
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
