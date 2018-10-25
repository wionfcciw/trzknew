<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SjzdLb_AddEdit.aspx.cs" Inherits="SincciKC.websystem.jcsj.SjzdLb_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#txtZlbdm").val() == "") {
                $("#txtZlbdm").focus();
                alert("请输入类别代码");
                return false;
            }

            if ($("#txtZlbmc").val() == "") {
                $("#txtZlbmc").focus();
                alert("请输入类别名称");
                return false;
            }

            return true;
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
        <table class="windowTable">
            <tr>
                <td colspan="2" class="title">字典类别信息设置</td>
            </tr>

            <tr>
                <td class="labelRedTD">类别代码：</td>
                <td class="contentTD"><asp:TextBox ID="txtZlbdm" runat="server"   CssClass="input1" MaxLength="10"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="labelRedTD">类别名称：</td>
                <td class="contentTD"><asp:TextBox ID="txtZlbmc" runat="server"   CssClass="input1" MaxLength="40"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="labelRedTD">启用状态：</td>
                <td class="contentTD"><asp:CheckBox ID="chkZlbzt" Text="是否启用" runat="server" Checked="true" /></td>
            </tr>

            <tr >
                <td colspan="2" class="buttonBar">
                    <asp:Button ID="btnEnter" Text=" 保 存 " runat="server" CssClass="btnStyle" onclick="btnEnter_Click" OnClientClick="return checkInput()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
