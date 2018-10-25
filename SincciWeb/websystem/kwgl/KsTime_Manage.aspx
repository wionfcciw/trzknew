<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KsTime_Manage.aspx.cs" Inherits="SincciKC.websystem.kwgl.KsTime_Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试时间设置</title>
 
     <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />

    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table border="1" cellpadding="0" cellspacing="0"  style=" text-align:center">
        <tr >
            <td colspan="4">
                <b>考试时间设置</b> 
            </td>
        </tr>
        <tr>
            <td>
                 </td>
            <td>
                日期
            </td>
            <td>
                上午
            </td>
            <td>
                下午
            </td>
        </tr>
        <tr>
            <td>
                第一天
            </td>
            <td>
                <asp:TextBox ID="t1" runat="server" CssClass="searchbox"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="s1" runat="server" CssClass="searchbox"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="x1" runat="server" CssClass="searchbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                第二天
            </td>
            <td>
                <asp:TextBox ID="t2" runat="server" CssClass="searchbox"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="s2" runat="server" CssClass="searchbox"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="x2" runat="server" CssClass="searchbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                第三天
            </td>
            <td>
                <asp:TextBox ID="t3" runat="server" CssClass="searchbox"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="s3" runat="server" CssClass="searchbox"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="x3" runat="server" CssClass="searchbox"></asp:TextBox>
            </td>
        </tr>
         <tr><td    >说明</td>
            <td colspan="3">
                <asp:TextBox ID="m1" runat="server" 　 Height="30px" 
                    style="margin-left: 0px" TextMode="MultiLine" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text="保存" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>

    </form>
</body>
</html>