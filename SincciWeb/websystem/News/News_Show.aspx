<%@ Page Language="C#" EnableViewState="false"  AutoEventWireup="true" CodeBehind="News_Show.aspx.cs" Inherits="SincciWeb.websystem.News.News_Show" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <style type="text/css">
 body,td,th {
	font-size: 14px;
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
 html 
{
	overflow-x: hidden;   /*- 横滚动条 -*/
	 
}
</style>

</head>
<body> 
    <div>
        <table border="0" align="center" cellpadding="4" style="border-collapse: collapse;
            margin: 1px; " cellspacing="1" width="100%">
            <tr>
                <td align="left" height="25" bgcolor="#F7F7F7" style="border-bottom:1px solid #FFFFFF">
                    &nbsp;&nbsp;<asp:Label ID="lblCreate" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" height="25" bgcolor="#F7F7F7" style="border-bottom:1px solid #FFFFFF">
                    &nbsp;&nbsp;标&nbsp;&nbsp;&nbsp;&nbsp;题：<asp:Label ID="lblTitle" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" height="25" bgcolor="#F7F7F7">
                    &nbsp;&nbsp;链接地址：<asp:Label ID="lblurl" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            <%--
<tr>
<td align="left" height="25" bgcolor="#F7F7F7">&nbsp;&nbsp;文章备注：<asp:Label ID="lblbz" runat="server" Text=" "></asp:Label></td>
</tr>--%>
            <tr>
                <td>
                    <asp:Label ID="lblContent" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
        </table>
 
    </div>
    
</body>
</html>