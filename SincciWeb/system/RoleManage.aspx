<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleManage.aspx.cs" Inherits="SincciWeb.system.RoleManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色管理</title>

<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
    <script src="../js/addTableListener.js" type="text/javascript"></script>
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function MsgYes() {
        if (confirm('确定要删除信息吗？')) {
            return true;
        }
        else {
            return false;
        }
    }

    function opdg(Roleid, Title) {
     ymPrompt.win({ message: 'RoleAddEdit.aspx?Roleid=' + Roleid, width: 300, height: 200, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }
    function opdg1(Roleid, Title, R_name) {
    ymPrompt.win({ message: 'RoleModuleSet.aspx?Roleid=' + Roleid + '&R_name=' + R_name, width: 440, height: 450, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }
    function opdg2(Roleid, Title, R_name) {
        parent.addTab2(Title, '/system/RolePermissionSet.aspx?Roleid=' + Roleid + '&R_name=' + R_name);
      //  ymPrompt.win({ message: 'RolePermissionSet.aspx?Roleid=' + Roleid + '&R_name=' + R_name, width: 780, height: 520, title: Title, iframe: true, fixPosition: false, dragOut: false })
    
        return false;
    }
    function opdg3(Roleid, Title, R_name) {
        ymPrompt.win({ message: 'RoleUserTypeSet.aspx?Roleid=' + Roleid + '&R_name=' + R_name, width: 440, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }
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
          <asp:Button ID="btnDelete" runat="server" Text="  删除"  CssClass="icon-remove btn" OnClientClick="return MsgYes();" onclick="btnDelete_Click" />
          &nbsp;&nbsp;
          <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn"  OnClientClick="return opdg(0,'新增模块');"/>
    </div> 

    <asp:GridView id="GridView1" runat="server"  CssClass="tbColor"   OnRowDataBound="GridView1_RowDataBound" 
    AutoGenerateColumns="False"  >
        <Columns>                 
            <asp:TemplateField HeaderText="选择"  > 
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"   />  
                </ItemTemplate>                    
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序号"  >
                <ItemTemplate><%# (this.AspNetPager1.CurrentPageIndex-1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%></ItemTemplate>                    
            </asp:TemplateField>
            <asp:BoundField HeaderText="角色ID" DataField="Roleid"/>
            <asp:BoundField HeaderText="角色名称" DataField="R_name"/> 
            <asp:BoundField HeaderText="角色描述"  DataField="R_descript" /> 
            <asp:TemplateField HeaderText="设置模块">
                <ItemTemplate>
                    <a href="#"  onclick="return opdg1(<%# Eval("Roleid")%>,'设置模块','<%# Eval("R_name")%>');"><image src="../images/set.gif" alt="设置模块" border="0">设置模块</image></a>  
                </ItemTemplate>                    
            </asp:TemplateField>
            <asp:TemplateField HeaderText="设置应用权限">
                <ItemTemplate>
                    <a href="#" onclick="return opdg2(<%# Eval("Roleid")%>,'设置应用权限','<%# Eval("R_name")%>');"><image src="../images/set.gif" alt="设置应用权限" border="0">设置应用权限</image></a>  
                </ItemTemplate>                    
            </asp:TemplateField>
             <asp:TemplateField HeaderText="设置用户类型">
                <ItemTemplate>
                    <a href="#" onclick="return opdg3(<%# Eval("Roleid")%>,'设置用户类型','<%# Eval("R_name")%>');"><image src="../images/set.gif" alt="设置用户类型" border="0">设置用户类型</image></a>  
                </ItemTemplate>                    
            </asp:TemplateField>


            <asp:TemplateField HeaderText="修改">
                <ItemTemplate>
                    <a href="#" onclick="return opdg(<%# Eval("Roleid")%>,'角色');"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
                </ItemTemplate>                    
            </asp:TemplateField>
            
                                
        </Columns>                 
        <HeaderStyle  Height="25px" CssClass="datagrid-header"/>
        <RowStyle Height="23px" CssClass="datagrid-body" />
        <AlternatingRowStyle BackColor="#F7F7F7" />                
    </asp:GridView>

    <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px" >
        <table Width="100%">
            <tr>
             <td > 
                <asp:CheckBox ID="ckbFull" runat="server" Text="全选/全不选" AutoPostBack="True" OnCheckedChanged="ckbFull_CheckedChanged" />&nbsp;</td>
           <td    >
             <webdiyer:aspnetpager id="AspNetPager1" runat="server"  Width="100%"  OnPageChanged="AspNetPager1_PageChanged">
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
