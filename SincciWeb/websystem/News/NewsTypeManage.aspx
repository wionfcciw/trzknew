<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsTypeManage.aspx.cs" Inherits="SincciWeb.websystem.News.NewsTypeManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新闻类型管理</title>

<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../css/page.css"  />
 <script src="../../js/addTableListener.js" type="text/javascript"></script>
<script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function MsgYes() {
        if (confirm('确定要删除信息吗？')) {
            return true;
        }
        else {
            return false;
        }
    }

    function opdg(Moduleid, Title) {
        ymPrompt.win({ message: 'NewsType_AddEdit.aspx?TypeID=' + Moduleid, width: 350, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }
   
</script>

<style type="text/css">
.btn
{ 
cursor:hand; 
 }
  .btnT
{
background:#F0F0F0 repeat-x;
padding-top:3px;
border-top:0px solid #708090;
border-right:0px solid #708090;
border-bottom:0px solid #708090;
border-left:0px solid #708090;  
height:auto;
font-size:10pt;
cursor:hand; 

 }
</style>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="tb" class="datagrid-toolbar" style=" padding-top:5px;">
          &nbsp;
          <asp:Button ID="btnDelete" runat="server" Text="  删除"  CssClass="icon-remove btn" OnClientClick="return MsgYes();" onclick="btnDelete_Click" />
          &nbsp;&nbsp;
          <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn" OnClientClick="return opdg(0,'新增类型')"/>
 
    </div> 
    <asp:GridView id="GridView1" runat="server"  CssClass="tbColor"  OnRowDataBound="GridView1_RowDataBound" 
    AutoGenerateColumns="False" DataKeyNames="PCID" >
        <Columns>                 
        <asp:TemplateField HeaderText="选择"  > 
            <ItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="server"  />                 
            </ItemTemplate>                    
        </asp:TemplateField>
        <asp:TemplateField HeaderText="序号" >
            <ItemTemplate><%# (this.AspNetPager1.CurrentPageIndex-1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%></ItemTemplate>                    
        </asp:TemplateField>
        <asp:BoundField HeaderText="类型名称" DataField="CategoryName" /> 
        <asp:TemplateField HeaderText="修改">
            <ItemTemplate>
                <a href="#" onclick="return opdg(<%# Eval("PCID") %>,'修改类型');"><image src="../../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
            </ItemTemplate>                    
        </asp:TemplateField> 
                    
        </Columns>                 
        <HeaderStyle  Height="25px" CssClass="datagrid-header"/>
        <RowStyle Height="23px" CssClass="datagrid-body" />
        <AlternatingRowStyle BackColor="#F7F7F7" />                
    </asp:GridView>

    <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px" >
         <table width="100%">
            <tr>
             <td  width="130"> 
                <asp:CheckBox ID="ckbFull" runat="server" Text="全选/全不选" AutoPostBack="True" OnCheckedChanged="ckbFull_CheckedChanged" />&nbsp;</td>
           <td  align="left">          
             <webdiyer:aspnetpager id="AspNetPager1" runat="server" width="100%"  OnPageChanged="AspNetPager1_PageChanged">
               </webdiyer:aspnetpager></td>
               <td align="left">每页：<asp:DropDownList ID="ddlPageSize" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="ddlPageSize_SelectedIndexChanged">                     
            </asp:DropDownList>条</td>
         </tr>
         </table> 
      </div> 
    </div>
    </form>
</body>
</html>
