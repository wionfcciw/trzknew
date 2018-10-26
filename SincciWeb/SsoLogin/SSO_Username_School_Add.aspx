<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SSO_Username_School_Add.aspx.cs" Inherits="SincciKC.SsoLogin.SSO_Username_School_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../css/page.css"  />
    <script src="../prompt/ymPrompt.js" type="text/javascript"></script>
    <script src="../js/Jquery183.js" type="text/javascript"></script>
    <link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <title>平台管理新增</title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
        .auto-style2 {
            height: 23px;
            width: 287px;
        }
        .auto-style3 {
            width: 287px;
            height: 22px;
        }
        .auto-style4 {
            height: 22px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="windowTable" width="98%" border="1" align="center" style="border-collapse:collapse; margin-top:5px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
            <tr>
                <td align="right" class="auto-style2" >
                    <asp:Label ID="Label4" runat="server" Text="区县"></asp:Label>
                </td>
                <td align="left" class="auto-style1">
                    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="auto-style2" >
                    <asp:Label ID="Label1" runat="server" Text="学校名称"></asp:Label>
                </td>
                <td align="left" class="auto-style1">
                    <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" align="right">
                    <asp:Label ID="Label2" runat="server" Text="账号"></asp:Label>
                </td>
                <td class="auto-style4" align="left">
                    <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" Text="平台schoolId"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="40" Width="339px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
      <asp:Button ID="btnSave" runat="server"  CssClass="btnStyle"  Text=" 保存 "  onclick="btnSave_Click" /> </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
