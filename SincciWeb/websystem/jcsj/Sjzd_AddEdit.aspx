<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sjzd_AddEdit.aspx.cs" Inherits="SincciKC.websystem.jcsj.Sjzd_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>

    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#txtZdlbdm").val() == "" && getParameter("zdlbdm") == "0") {
                $("#txtZdlbdm").focus();
                alert("请输入字典代码。");
                return false;
            }

            if ($("#txtZdlbmc").val() == "") {
                $("#txtZdlbmc").focus();
                alert("请输入字典名称。");
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
                <td colspan="2" class="title">数据字典信息设置</td>
            </tr>

            <tr>
                <td class="labelRedTD">字典代码：</td>
                <td class="contentTD">
                    <asp:TextBox ID="txtZdlbdm" runat="server" MaxLength="15"  CssClass="input1"></asp:TextBox>
                    <asp:Label ID="lblZdlbdm" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="labelRedTD">字典名称：</td>
                <td class="contentTD"><asp:TextBox ID="txtZdlbmc" runat="server"  CssClass="input1" MaxLength="50"></asp:TextBox></td>
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
