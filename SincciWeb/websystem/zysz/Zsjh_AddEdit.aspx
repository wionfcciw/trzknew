<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zsjh_AddEdit.aspx.cs" Inherits="SincciKC.websystem.zysz.Zsjh_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
    <script src="../../js/Number.js" type="text/javascript"></script>
         <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#txtJhs").val() == "") {
                $("#txtJhs").focus();
                alert("请输入计划数。");
                return false;
            }

            if ($("#txtPcdm").val() == "") {
                $("#txtPcdm").focus();
                alert("请输入批次。");
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
                <td colspan="2" class="title">计划信息设置</td>
            </tr>

            <tr>
                <td class="labelRedTD">县区名称：</td>
                <td class="contentTD"><asp:DropDownList ID="ddlXqdm" runat="server"></asp:DropDownList>
                                      </td>
            </tr>

            <tr>
                <td class="labelRedTD">学校名称：</td>
                <td class="contentTD"><asp:DropDownList ID="ddlXxdm" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlXxdm_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>

            <tr>
                <td class="labelRedTD">专业名称：</td>
                <td class="contentTD"><asp:DropDownList ID="ddlZydm" runat="server"></asp:DropDownList></td>
            </tr>

            <tr>
                <td class="labelRedTD">学制：</td>
                <td class="contentTD"><asp:DropDownList ID="ddlXzdm" runat="server"></asp:DropDownList></td>
            </tr>

            <tr>
                <td class="labelRedTD">计划数：</td>
                <td class="contentTD"><asp:TextBox ID="txtJhs" CssClass="input1" runat="server"  ></asp:TextBox>(请输入数字)</td>
            </tr>
            
            <tr>
                <td class="labelRedTD">批次：</td>
                <td class="contentTD"><asp:TextBox ID="txtPcdm" CssClass="input1" runat="server" MaxLength="10"></asp:TextBox></td>
            </tr>
            
       <%--     <tr>
                <td class="labelRedTD">学校类别：</td>
                <td class="contentTD"><asp:TextBox ID="txtXxlbdm" CssClass="input1" runat="server" MaxLength="4"></asp:TextBox></td>
            </tr>--%>
            
            <tr>
                <td class="labelTD">备注：</td>
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
