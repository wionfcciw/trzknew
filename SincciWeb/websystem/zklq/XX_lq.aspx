<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XX_lq.aspx.cs" Inherits="SincciKC.websystem.zklq.XX_lq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
       <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function checkInput() {
            if ($("#ddlzy").val() == "") {
                $("#ddlzy").focus();
                alert("请选择专业!");
                return false;
            }
        }
    </script>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="windowTable">
            <tr>
                <td colspan="2" class="title">考生录取</td>
            </tr>

            <tr>
                <td class="labelRedTD" style=" width:160px">报名号：</td>
                <td class="contentTD">
                    <asp:Label ID="lblksh" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="labelRedTD">姓名：</td>
                <td class="contentTD">
                    <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                                      </td>
            </tr>
                <tr>
                <td class="labelRedTD">批次：</td>
                <td class="contentTD">
                  <asp:DropDownList ID="ddlXpcInfo" runat="server"  AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlXpcInfo_SelectedIndexChanged">
                  </asp:DropDownList>
                </td>
            </tr>
              
            <tr>
                <td class="labelRedTD">招生学校：</td>
                <td class="contentTD">
                     <asp:DropDownList ID="ddlzsxx" runat="server"  AutoPostBack="true"
                          onselectedindexchanged="ddlzsxx_SelectedIndexChanged">
                      </asp:DropDownList>
                </td>
            </tr>
               <tr>
                <td class="labelRedTD">专业：</td>
                <td class="contentTD">
                     <asp:DropDownList ID="ddlzy" runat="server" >
                         <asp:ListItem Value="">-请选择-</asp:ListItem>
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
