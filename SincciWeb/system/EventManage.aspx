<%@ Page Language="C#"   AutoEventWireup="true" CodeBehind="EventManage.aspx.cs" Inherits="SincciKC.system.EventManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看操作记录</title>

<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
<script src="../js/addTableListener.js" type="text/javascript"></script>
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    
</script>

<style type="text/css">
.btn
{
	 cursor: hand; 
 }
  
</style>

</head>
<body>
    <form id="form1" runat="server">

     <div id="tb" class="datagrid-toolbar" >
          &nbsp;
        用户名： <asp:TextBox ID="txtUserName" CssClass="searchbox" runat="server"></asp:TextBox>&nbsp;
        类型：<asp:DropDownList ID="ddlType" runat="server">
        <asp:ListItem Value="">全部</asp:ListItem>
        <asp:ListItem Value="1">操作日记</asp:ListItem>
        <asp:ListItem Value="2">安全日志</asp:ListItem>
         </asp:DropDownList>
          &nbsp;&nbsp;
          <asp:Button ID="btnSearch" runat="server" Text="  查询" 
              CssClass="icon-search btn" onclick="btnSearch_Click"  />
    </div> 

    <asp:GridView id="GridView1" runat="server"  CssClass="tbColor"   
    AutoGenerateColumns="False"   >
        <Columns> 
            <asp:TemplateField HeaderText="序号"  >
                <ItemTemplate><%# (page-1) * pagesize + Container.DataItemIndex + 1%></ItemTemplate>                    
            </asp:TemplateField> 
           
             <asp:BoundField HeaderText="用户名"   DataField="E_U_LoginName"/> 
            <asp:BoundField HeaderText="操作时间"   DataField="E_DateTime"/> 
            <asp:BoundField HeaderText="操作来源"  ItemStyle-Width="200px" DataField="E_From" />  
            <asp:TemplateField HeaderText="日记类型">
                <ItemTemplate><%# Eval("E_Type").ToString() == "1" ? "操作日记" : "安全日志"%> </ItemTemplate>
            </asp:TemplateField>
              <asp:BoundField HeaderText="IP地址"   DataField="E_IP" />  
   
             
              <asp:TemplateField HeaderText="详细描述" ItemStyle-Width="400px">
                <ItemTemplate>
                    <asp:TextBox Width="400" Height="30" TextMode="MultiLine" ID="TextBox1" runat="server" Text='<%# Eval("E_Record")%>'> </asp:TextBox>   </ItemTemplate>
            </asp:TemplateField>
        </Columns>                 
        <HeaderStyle  Height="25px" CssClass="datagrid-header"/>
        <RowStyle Height="23px" CssClass="datagrid-body" />
        <AlternatingRowStyle BackColor="#F7F7F7" />                
    </asp:GridView> 
    
    <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px" >
        <table  width="100%">
            <tr>  <td>
             <webdiyer:aspnetpager id="AspNetPager1" runat="server" Width="100%"   >
               </webdiyer:aspnetpager></td>
               <td>每页：<asp:DropDownList ID="ddlPageSize" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="ddlPageSize_SelectedIndexChanged">                     
            </asp:DropDownList>条</td>
         </tr>
       </table> 
    </div>  
    </form>
</body>
</html>
