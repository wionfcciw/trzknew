<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPermission.aspx.cs" Inherits="SincciKC.system.ApplicationPermission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置页面权限</title>

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

   
</script>

<style type="text/css">
.btn
{
	 cursor: hand;
        height: 21px;
    }
</style>

</head>
<body>
    <form id="form1" runat="server">

     <div id="tb" class="datagrid-toolbar" >
          &nbsp;
          <asp:Button ID="btnDelete" runat="server" Text="  删除"  CssClass="icon-remove btn" OnClientClick="return MsgYes();" onclick="btnDelete_Click" />
          &nbsp;&nbsp;
          权限名称：<asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
          权限值：<asp:TextBox ID="txtValue" runat="server" Width="78px"></asp:TextBox>(只能是2的N次方)
          <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn" 
              onclick="btnNew_Click"  />
           

    </div> 

    <asp:GridView id="GridView1" runat="server"  CssClass="tbColor"  
    AutoGenerateColumns="False"   DataKeyNames="id" ShowHeaderWhenEmpty="True"  >
        <Columns>                 
            <asp:TemplateField HeaderText="选择"  > 
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server"    />  
                    </ItemTemplate>                    
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序号"  >
                    <ItemTemplate><%#   Container.DataItemIndex + 1%></ItemTemplate>                    
            </asp:TemplateField>

            <asp:BoundField HeaderText="页面代码" DataField="PageCode"/>
            <asp:BoundField HeaderText="权限名称" DataField="PermissionName"/>
            <asp:BoundField HeaderText="权限值" DataField="PermissionValue"/>  
             
<%--
            <asp:TemplateField HeaderText="修改">
                    <ItemTemplate>
                     <a href="#" onclick="return opdg(<%# Eval("id")%>,'修改');"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
                    </ItemTemplate>                    
            </asp:TemplateField>     --%>             
           
                    
            </Columns>                 
            <HeaderStyle  Height="25px" CssClass="datagrid-header"/>
            <RowStyle Height="23px" CssClass="datagrid-body" />
            <AlternatingRowStyle BackColor="#F7F7F7" />                
    </asp:GridView>
     
             
    </form>
</body>
</html>
