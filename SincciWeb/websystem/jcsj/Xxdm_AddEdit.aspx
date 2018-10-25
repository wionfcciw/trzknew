<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xxdm_AddEdit.aspx.cs" Inherits="SincciKC.websystem.jcsj.Xxdm_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学校新增修改</title>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
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
     
</head>
<body>
    <form id="form1" runat="server">
        <table class="windowTable">
            <tr>
                <td colspan="2" class="title">学校信息设置</td>
            </tr>

            <tr>
                <td class="labelRedTD">县区代码：</td>
                <td class="contentTD"> 
                <asp:DropDownList ID="ddlXqdm" runat="server"   AutoPostBack="true"
                        onselectedindexchanged="ddlXqdm_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>

            <tr>
                <td class="labelRedTD">学校代码：</td>
                <td class="contentTD"><asp:TextBox ID="txtByxxdm" onKeyUp="checknum(this);" 
                        CssClass="input1" MaxLength="6"  runat="server" Width="90px"></asp:TextBox>
                                      <asp:Label ID="lblByxxdm" runat="server"></asp:Label></td>
            </tr>

            <tr>
                <td class="labelRedTD">学校名称：</td>
                <td class="contentTD"><asp:TextBox ID="txtXqmc" CssClass="input1"  MaxLength="40" 
                        runat="server" Width="265px"></asp:TextBox></td>
            </tr>
              <tr>
                <td class="labelRedTD">学校类型：</td>
                <td class="contentTD">  
                  <asp:DropDownList ID="xxList" runat="server">  </asp:DropDownList></td>
            </tr>
            <tr >
                <td colspan="2" class="buttonBar">
                    <asp:Button ID="btnEnter" Text=" 保 存 "  CssClass="btnStyle" runat="server" onclick="btnEnter_Click" OnClientClick="return checkInput()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
