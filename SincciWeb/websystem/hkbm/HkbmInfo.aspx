<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HkbmInfo.aspx.cs" Inherits="SincciKC.websystem.hkbm.HkbmInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="stylesheet" type="text/css" href="/style.css" />
<head runat="server">
    <title></title>
    <style type="text/css">
        tr
        {
            height: 31px;
        }
    </style>
</head>
<body>
    <div style="border: 5px solid #DFF3FE; background-color: #DFF3FE; margin-left: 10px;
        padding: 2px; width: 905px">
        <div style="background-color: #FFFFFF">
            <div class="tbltitle">
                铜仁市<asp:Label runat="server" ID="SysYear"></asp:Label>中等学校会考考生信息</div>
            <table width="900" border="0" style="border-collapse: collapse;" align="center" cellpadding="0"
                cellspacing="0">
                <tr>
                    <td>
                        <table width="100%" border="1" bordercolor="#B0DFFD" style="border-collapse: collapse;
                            font-size: 12px" align="center" cellpadding="3" cellspacing="0">
                            <tr>
                                <td align="right">
                                    报名号：
                                </td>
                                <td>
                                    <asp:Label ID="lblksh" runat="server" Text=" "></asp:Label>
                                </td>
                                <td align="right">
                                    姓名：
                                </td>
                                <td>
                                    <asp:Label ID="lblxm" runat="server"></asp:Label>
                                </td>
                                <td rowspan="4" width="120px">
                                    <asp:Image ID="imgPic" runat="server" Width="120px" Height="160px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    考次：
                                </td>
                                <td>
                                    <asp:Label ID="lblkaocimc" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    学籍号：
                                </td>
                                <td>
                                    <asp:Label ID="lblxjh" runat="server" Text=" "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    县区：
                                </td>
                                <td>
                                    <asp:Label ID="lblxqmc" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    毕业中学名称：
                                </td>
                                <td>
                                    <asp:Label ID="lblbmdmc" runat="server" Text=" "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    班级：
                                </td>
                                <td>
                                    <asp:Label ID="lblbjmc" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    证件号码：
                                </td>
                                <td>
                                    <asp:Label ID="lblsfzh" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    备注 ：
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="lblbz" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
