<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zyk_AddEdit.aspx.cs" Inherits="SincciKC.websystem.zysz.Zyk_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
     <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#ddlXxdm").val() == "" && getParameter("ID") == "0") {
                $("#ddlXxdm").focus();
                alert("请选择学校");
                return false;
            }

            if ($("#txtZydm").val() == "" && getParameter("ID") == "0") {
                $("#txtZydm").focus();
                alert("请输入专业代码");
                return false;
            }

            if ($("#txtZymc").val() == "") {
                $("#txtZymc").focus();
                alert("请输入专业名称");
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
            <td colspan="2" class="title">
                招生学校信息设置
            </td>
        </tr>
        <tr>
            <td class="labelRedTD">
                学校名称：
            </td>
            <td class="contentTD">
                <asp:DropDownList ID="ddlXxdm" runat="server">
                </asp:DropDownList>
                <asp:Label ID="lblXxdm" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="labelRedTD">
                专业代码：
            </td>
            <td class="contentTD">
                <asp:TextBox ID="txtZydm" CssClass="input1" runat="server" MaxLength="4"></asp:TextBox>
                <asp:Label ID="lblZydm" CssClass="input1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="labelRedTD">
                专业名称：
            </td>
            <td class="contentTD">
                <asp:TextBox ID="txtZymc" CssClass="input1" runat="server" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="labelRedTD">
                备注：
            </td>
            <td class="contentTD">
                <asp:TextBox ID="txtBz" CssClass="input1" runat="server" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="buttonBar">
                <asp:Button ID="btnEnter" Text=" 保 存 " runat="server" CssClass="btnStyle" OnClick="btnEnter_Click"
                    OnClientClick="return checkInput()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
