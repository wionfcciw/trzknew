<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleManage.aspx.cs" Inherits="SincciWeb.system.ModuleManage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模块管理</title>
<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
    <script src="../js/addTableListener.js" type="text/javascript"></script>
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function MsgYes() {
        if (confirm('确定要删除信息吗？')) 
        {
            return true;
        }
        else {
            return false;
        }
    }

    function opdg(Moduleid,Title) {
        //  ymPrompt.win({ message: 'ModuleAddEdit.aspx?Moduleid=' + Moduleid, width: 540, height: 400, title: '添加学校信息', iframe: true })

        ymPrompt.win({ message: 'ModuleAddEdit.aspx?Moduleid=' + Moduleid, width: 250, height: 200, title: Title, iframe: true, fixPosition: true, dragOut: false })
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
    AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" >
        <Columns>                 
            <asp:TemplateField HeaderText="选择"  > 
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server"   />  
                    </ItemTemplate>                    
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序号"  >
                    <ItemTemplate><%# (this.AspNetPager1.CurrentPageIndex-1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%></ItemTemplate>                    
            </asp:TemplateField>
            <asp:BoundField HeaderText="模块ID" DataField="Moduleid"/>
            <asp:BoundField HeaderText="模块名称" DataField="M_modulename"/>    
                        
                <asp:TemplateField HeaderText="向上">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnOderUp"  CommandName="Up"  CommandArgument='<%# Eval("Moduleid")%>'  Visible='<%# (this.AspNetPager1.CurrentPageIndex-1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1==1?false:true%>'  ImageUrl="../images/jts.gif" runat="server" CssClass="btn"   />
                    </ItemTemplate>                    
            </asp:TemplateField>     
                <asp:TemplateField HeaderText="向下">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnOderDown" CommandName="Down"   CommandArgument='<%# Eval("Moduleid")%>'  Visible='<%# (this.AspNetPager1.CurrentPageIndex-1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1==AspNetPager1.RecordCount?false:true%>'   ImageUrl="../images/jtx.gif" runat="server"  CssClass="btn"  />
                    </ItemTemplate>                    
            </asp:TemplateField>
                  
             <asp:BoundField DataField="M_tag" HeaderText="状态" />

          <%--  <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                    <asp:Label ID="M_tag" runat="server" Text='<%# Eval("M_tag").ToString()=="1"?"开通":"关闭"%>'></asp:Label>
                    </ItemTemplate>                    
            </asp:TemplateField>  
--%>
             <asp:TemplateField HeaderText="修改">
                    <ItemTemplate>
                     <a href="#" onclick="return opdg(<%# Eval("Moduleid")%>,'修改模块');"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
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
