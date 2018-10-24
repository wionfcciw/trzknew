<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhidingtoudang.aspx.cs" Inherits="SincciKC.websystem.zklq.Zhidingtoudang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>批次:<asp:DropDownList ID="ddlXpcInfo" runat="server" Width="280px" AutoPostBack="True"
                                    >
       </asp:DropDownList></td>
            </tr>
            <tr><td>001铜仁一中 <asp:Button ID="btn001_tz"   runat="server" Text="统招投档"     
                                                Width="90px" Height="30px"   />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn001_pes"   runat="server" Text="配额生投档"     
                                                Width="90px" Height="30px"   />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn001_pzt"   runat="server" Text="配转统投档"     
                                                Width="90px" Height="30px"   />
                </td></tr>

        </table>
      
    </div>
    </form>
</body>
</html>
