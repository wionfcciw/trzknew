<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zsxx_AddEdit.aspx.cs" Inherits="SincciKC.websystem.zysz.Zsxx_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
     <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
       <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#txtZsxxdm").val() == "") {
                $("#txtZsxxdm").focus();
                alert("请输学校代码");
                return false;
            }

            if ($("#txtZsxxmc").val() == "") {
                $("#txtZsxxmc").focus();
                alert("请输入学校名称");
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
                <td colspan="2" class="title">招生学校信息设置</td>
            </tr>

            <tr>
                <td class="labelRedTD">学校代码：</td>
                <td class="contentTD"><asp:TextBox ID="txtZsxxdm" CssClass="input1" runat="server" MaxLength="10"></asp:TextBox>
                                      <asp:Label ID="lblZsxxdm" runat="server"></asp:Label></td>
            </tr>

            <tr>
                <td class="labelRedTD">学校名称：</td>
                <td class="contentTD"><asp:TextBox ID="txtZsxxmc" CssClass="input1" runat="server" MaxLength="100"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="labelRedTD">备注：</td>
                <td class="contentTD"><asp:TextBox ID="txtBz" CssClass="input1" runat="server" MaxLength="100"></asp:TextBox></td>
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
