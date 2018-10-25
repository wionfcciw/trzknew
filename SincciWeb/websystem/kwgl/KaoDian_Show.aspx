<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KaoDian_Show.aspx.cs" Inherits="SincciKC.websystem.kwgl.KaoDian_Show" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看考点信息</title> 
<link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
<link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="windowTable"  >           
             <tr>
                <td class="labelRedTD" style="width:100px" >选择县区：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="ddlxqdm" runat="server"  >                         
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="labelRedTD">考点代码：</td>
                <td class="contentTD">
                    <asp:Label ID="lbllsh" runat="server" Visible="false"  Text=""></asp:Label>
                    <asp:TextBox ID="txtkddm" runat="server"  onKeyUp="checknum(this);" 
                        MaxLength="4" CssClass="input1" Width="97px"></asp:TextBox> 
                </td>
            </tr> 
            <tr><td class="labelRedTD">考点名称：</td>
                <td class="contentTD"><asp:TextBox ID="txtkdmc" runat="server" CssClass="input1" 
                        Width="295px"></asp:TextBox></td>
            </tr> 
            <tr>
                <td class="labelRedTD">毕业中学：</td>
                <td >
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="2">
                    </asp:CheckBoxList>
                </td>
            </tr> 
        </table>
    </form>
</body>
</html>
