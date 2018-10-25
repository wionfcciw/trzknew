<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kaoci_AddEdit.aspx.cs" Inherits="SincciKC.websystem.jcsj.Kaoci_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增、修改考次</title>
    
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

        <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#txtKcdm").val() == "") {
                $("#txtKcdm").focus();
                alert("请输考次");
                return false;
            }

            if ($("#txtKcmc").val() == "") {
                $("#txtKcmc").focus();
                alert("考次名称");
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
                <td colspan="2" class="title">考次信息设置</td>
            </tr>

            <tr>
                <td class="labelRedTD">考次：</td>
                <td class="contentTD"><asp:TextBox ID="txtKcdm" MaxLength="2" onKeyUp="checknum(this);" CssClass="input1" 
                        runat="server" Width="67px"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="labelRedTD">考次名称：</td>
                <td class="contentTD"><asp:TextBox ID="txtKcmc" runat="server"  CssClass="input1" 
                        MaxLength="50" Width="177px"></asp:TextBox></td>
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
