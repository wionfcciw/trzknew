<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebSel.aspx.cs" Inherits="SincciKC.WebSel" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>志愿填报监控</title>
    <link rel="stylesheet" type="text/css" href="easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="css/page.css"  />

<style type="text/css">
.tbColor2
{
	font-size: 20px;
	border: 1px solid f5f5f5;
	width:100%;
	text-align:center;
}
 .tbColor2 td
{
	font-size: 16px;
	 
}
</style>

</head>
<body>
    <form id="form1" runat="server"  > 
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <asp:DropDownList ID="dlistkslx" runat="server">
                         <asp:ListItem Value="60000">1分钟</asp:ListItem>
                         <asp:ListItem Value="120000">2分钟</asp:ListItem>
                          <asp:ListItem Value="300000">5分钟</asp:ListItem>
                         <asp:ListItem Value="600000">10分钟</asp:ListItem>
                         <asp:ListItem Value="900000">15分钟</asp:ListItem>
              </asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="设置" OnClick="Button1_Click" />
            
            <div style="width:100%; text-align:center; font-size:30px" runat="server" id="divs">
           <asp:Label Text="<未到志愿填报时间>" runat="server" ID="lblshow" /></div>
           <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" />
           
                   <div id="divdata" runat="server" style="margin-top:20px"  >
    <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
           
            <table class="tbColor2" id="GridView1" border="1" style="border-collapse:collapse;"  cellpadding="2" cellspacing="0" >               
                <tr class="datagrid-header" style="height: 35px">
                    <td style="width: 20%">
                        <b></b>
                    </td>
                    <td colspan="3">
                        <b>全市</b>
                    </td>
                    <td colspan="3">
                        <b>本县</b>
                    </td>
                    <td colspan="3">
                        <b>外县</b>
                    </td>
                   <td></td>
                   
                </tr>

                <tr class="datagrid-header" style="height: 26px">
                    <td style=" width:10%">
                     
                     <b>学校名称</b></td>
                    <td>
                        <b>填报人数</b>
                    </td>
                    <td>
                        <b>最高分</b>
                    </td>
                 
                    <td>
                        <b>最低分</b>
                    </td>
                     <td>
                        <b>填报人数</b>
                    </td>
                    <td>
                        <b>最高分</b>
                    </td>
                 
                    <td>
                        <b>最低分</b>
                    </td>
                     <td>
                        <b>填报人数</b>
                    </td>
                    <td>
                        <b>最高分</b>
                    </td>
                 
                    <td>
                        <b>最低分</b>
                    </td>
                    <td>
                        <b>状态</b>
                    </td>
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
             <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                 <td>
                     <%#Eval("xxdm")%><%#Eval("xxmc")%>
                 </td>
                 
                 <td>
                     <%#Eval("jqxx").ToString() == "" ? Eval("num") : ""%>
                 </td>
                
                     <td>
                         <%#Eval("jqxx").ToString() == "" ? Eval("maxzf") : ""%>
                   
                 </td>
                   <td>
                        <%#Eval("jqxx").ToString() == "" ? Eval("minzf") : ""%>
                    
                 </td>
                 <td>
                     <%#Eval("jqxx").ToString() == "" ? Eval("num2") : ""%>
                 </td>
                
                     <td>
                         <%#Eval("jqxx").ToString() == "" ? Eval("maxzf2") : ""%>
                   
                 </td>
                   <td>
                        <%#Eval("jqxx").ToString() == "" ? Eval("minzf2") : ""%>
                    
                 </td>
              <td>
                     <%#Eval("jqxx").ToString() == "" ? Eval("num3") : ""%>
                 </td>
                
                     <td>
                         <%#Eval("jqxx").ToString() == "" ? Eval("maxzf3") : ""%>
                 </td>
                   <td>
                        <%#Eval("jqxx").ToString() == "" ? Eval("minzf3") : ""%>
                 </td>
                  <td align="center">
              <font color="red" ><%#Eval("jqxx").ToString() == "" ? "" : "已结清"%></font> 
                </td>
             </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater>

    
        </div>
            </ContentTemplate>
       </asp:UpdatePanel>
    </form>
</body>

</html>
