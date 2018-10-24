<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cj_AddEdit.aspx.cs" Inherits="SincciKC.websystem.cjgl.cj_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
       <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        
    </script>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="windowTable">
            <tr>
                <td colspan="2" class="title">考生成绩修改</td>
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
                <td class="labelRedTD">道德品质与公民素养：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="ddpzList" runat="server">
                        <asp:ListItem Value="合格">合格</asp:ListItem>
                        <asp:ListItem Value="不合格">不合格</asp:ListItem>

                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">交流与合作能力：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="jlList" runat="server">
                       <asp:ListItem Value="合格">合格</asp:ListItem>
                        <asp:ListItem Value="不合格">不合格</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">学习习惯与学习能力：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="xxxgList" runat="server">
                       <asp:ListItem Value="A">A</asp:ListItem>
                        <asp:ListItem Value="B">B</asp:ListItem>
                           <asp:ListItem Value="C">C</asp:ListItem>
                        <asp:ListItem Value="D">D</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">运动与健康：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="ydList" runat="server">
                      <asp:ListItem Value="A">A</asp:ListItem>
                        <asp:ListItem Value="B">B</asp:ListItem>
                           <asp:ListItem Value="C">C</asp:ListItem>
                        <asp:ListItem Value="D">D</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">审美与表现：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="smList" runat="server">
                  <asp:ListItem Value="A">A</asp:ListItem>
                        <asp:ListItem Value="B">B</asp:ListItem>
                           <asp:ListItem Value="C">C</asp:ListItem>
                        <asp:ListItem Value="D">D</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">创新意识与实践能力：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="cxysList" runat="server">
                      <asp:ListItem Value="A">A</asp:ListItem>
                        <asp:ListItem Value="B">B</asp:ListItem>
                           <asp:ListItem Value="C">C</asp:ListItem>
                        <asp:ListItem Value="D">D</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">地理：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="diliList" runat="server">
                      <asp:ListItem Value="A">A</asp:ListItem>
                        <asp:ListItem Value="B">B</asp:ListItem>
                           <asp:ListItem Value="C">C</asp:ListItem>
                        <asp:ListItem Value="D">D</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td class="labelRedTD">生物：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="swList" runat="server">
                      <asp:ListItem Value="A">A</asp:ListItem>
                        <asp:ListItem Value="B">B</asp:ListItem>
                           <asp:ListItem Value="C">C</asp:ListItem>
                        <asp:ListItem Value="D">D</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td class="labelRedTD">英语听力口语：</td>
                <td class="contentTD"><asp:TextBox ID="txttl" runat="server">
                        </asp:TextBox>
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">体育校考：</td>
                <td class="contentTD"><asp:TextBox ID="txtty1" runat="server">
                        </asp:TextBox>
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">体育统考：</td>
                <td class="contentTD"><asp:TextBox ID="txtty2" runat="server">
                        </asp:TextBox>
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">体育总分：</td>
                <td class="contentTD"><asp:TextBox ID="txtty3" runat="server">
                        </asp:TextBox>
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
