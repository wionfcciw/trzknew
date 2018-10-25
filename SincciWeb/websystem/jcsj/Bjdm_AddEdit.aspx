<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bjdm_AddEdit.aspx.cs" Inherits="SincciKC.websystem.jcsj.Bjdm_AddEdit" %>

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
            if ($("#ddlXqdm").val() == "" && getParameter("lsh") == "0") {
                $("#ddlXqdm").focus();
                alert("请选择县区");
                return false;
            }

            if ($("#ddlXxdm").val() == "" && getParameter("lsh") == "0") {
                $("#ddlXxdm").focus();
                alert("请选择学校");
                return false;
            }

            if ($("#txtBjdm").val() == "" && getParameter("lsh") == "0") {
                $("#txtBjdm").focus();
                alert("请输班级代码");
                return false;
            }

            if ($("#txtBjmc").val() == "") {
                $("#txtBjmc").focus();
                alert("班级名称");
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
                <td colspan="2" class="title">班级信息设置</td>
            </tr>

            <tr>
                <td class="labelRedTD">所属县区：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="ddlXqdm" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlXqdm_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="lblXqdm" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="labelRedTD">所属学校：</td>
                <td class="contentTD">                    
                    <asp:DropDownList ID="ddlXxdm" runat="server"></asp:DropDownList>
                    <asp:Label ID="lblXxdm" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="labelRedTD">班级代码：</td>
                <td class="contentTD">
                    <asp:TextBox ID="txtBjdm" runat="server" MaxLength="2"  CssClass="input1"
                        onKeyUp="checknum(txtBjdm);" Width="70px"></asp:TextBox> 
                    <asp:Label ID="lblBjdm" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="labelRedTD">班级名称：</td>
                <td class="contentTD"><asp:TextBox ID="txtBjmc"  CssClass="input1" runat="server" MaxLength="40"></asp:TextBox></td>
            </tr>

            <tr >
                <td colspan="2" class="buttonBar">
                    <asp:Button ID="btnEnter"   Text="  保 存 " CssClass="btnStyle" runat="server" onclick="btnEnter_Click" OnClientClick="return checkInput()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
