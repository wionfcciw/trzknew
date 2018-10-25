<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewListManage.aspx.cs" Inherits="SincciWeb.websystem.News.NewListManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信息管理</title>

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
        ymPrompt.win({ message: 'News_AddEdit.aspx?TypeID=' + Moduleid, width: 640, height: 500, title: Title, iframe: true, fixPosition: true, dragOut: false })
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
 a:link{ text-decoration:none;color:#000000;}
 a:hover{ text-decoration:none;color:#666;}
 a:visited{ text-decoration:none;color:#666;}
 #tooltip{position:absolute;background:#DDD;padding:20px;opacity:0.9;-moz-border-radius:3px;border-radius:3px;webkit-border-radius:3px;
font-weight:normal;font-size:12px;display:none;color:Red;
}
</style>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="tb" class="datagrid-toolbar" style=" padding-top:5px;">
          &nbsp;
          <asp:Button ID="btnDelete" runat="server" Text="   删除"  CssClass="icon-remove btn" OnClientClick="return MsgYes();" onclick="btnDelete_Click" />
          &nbsp;
          标题：<asp:TextBox ID="txtTitle" runat="server" CssClass="searchbox"  Width="300px"></asp:TextBox>
          类型：<asp:DropDownList ID="ddlTree" runat="server">
          </asp:DropDownList>
             <%--发布人：<asp:TextBox ID="txtAdmin" runat="server" Width="80"></asp:TextBox>--%>
            审核：<asp:DropDownList ID="ddlMarkPass" runat="server">
            <asp:ListItem>全部</asp:ListItem>
            <asp:ListItem Value="1">已审核</asp:ListItem>
            <asp:ListItem Value="0">未审核</asp:ListItem>
          </asp:DropDownList>
          重点：<asp:DropDownList ID="ddlImp" runat="server">
                      <asp:ListItem >全部</asp:ListItem>
                      <asp:ListItem Value="1">是</asp:ListItem>
                     <asp:ListItem Value="0">否</asp:ListItem>
                </asp:DropDownList>
          &nbsp;<asp:Button ID="btnSearch" runat="server" Text="   查询" CssClass="icon-search btn"
              onclick="btnSearch_Click" />
    </div> 
    <asp:GridView id="GridView1" runat="server"  CssClass="tbColor" OnRowDataBound="GridView1_RowDataBound"
    AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" 
            DataKeyNames="NewsID" ShowHeaderWhenEmpty="True" >
        <Columns>                 
            <asp:TemplateField HeaderText="选择"  > 
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  /> 
                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>' Visible="false"></asp:Label>
                </ItemTemplate>                    
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序号" >
                <ItemTemplate><%# (this.AspNetPager2.CurrentPageIndex-1) * this.AspNetPager2.PageSize + Container.DataItemIndex + 1%></ItemTemplate>                    
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="标题" >
                <ItemTemplate>
                  <a href="#" onclick="javascript:parent.addTab2('<%# Eval("Title").ToString().Replace("\"", "”") %>','/websystem/News/News_Show.aspx?NewsID=<%# Eval("NewsID") %>');" title='<%# Eval("Title") %>'><font color="#0000ff"><%# SetSubString(Eval("Title").ToString())%></font></a>  
                </ItemTemplate>                    
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>            
 
             <asp:TemplateField HeaderText="分类">
                <ItemTemplate>  
                  <%#  Eval("CategoryName")%>
                </ItemTemplate>                    
                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>                
            <asp:BoundField HeaderText="发布人" DataField="Editor"  >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="发布时间">
               <ItemTemplate>
               <%# Convert.ToDateTime(Eval("PublishTime")).ToShortDateString()%>
               </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <%-- <asp:BoundField HeaderText="浏览量" DataField="Number" > 
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>--%>
           <asp:TemplateField HeaderText="审核">
                <ItemTemplate>                  
                    <asp:Button ID="btnmarkpass"  CommandName="markpass"   Text='<%# Eval("MarkPass").ToString()=="1" ? "已审核": "否"%>' CommandArgument='<%# Eval("NewsID")+"|"+Eval("MarkPass") %>' runat="server" CssClass="btnT" /> 
                </ItemTemplate>                    
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>             
            <asp:TemplateField HeaderText="显示">
                <ItemTemplate>
                    <asp:Button ID="btnshow"  CommandName="show" Text='<%# Eval("Show").ToString()=="1" ? "是": "否"%>' CommandArgument='<%# Eval("NewsID") +"|"+Eval("Show") %>' runat="server" CssClass="btnT" /> 
                </ItemTemplate>                    
            </asp:TemplateField>       
             <asp:TemplateField HeaderText="置顶">
                <ItemTemplate>
                    <asp:Button ID="btnmarktop"  CommandName="marktop"  Text='<%# Eval("MarkTop").ToString()=="1" ? "是": "否"%>' CommandArgument='<%# Eval("NewsID") +"|"+Eval("MarkTop")%> ' runat="server" CssClass="btnT" /> 
                </ItemTemplate>                    
            </asp:TemplateField>    
             <asp:TemplateField HeaderText="重点">
                <ItemTemplate>
                    <asp:Button ID="btnmarkimp"  CommandName="MarkImp" Text='<%# Eval("MarkImp").ToString()=="1" ? "是": "否"%>' CommandArgument='<%# Eval("NewsID") +"|"+Eval("MarkImp")%> ' runat="server" CssClass="btnT" /> 
                </ItemTemplate>                    
            </asp:TemplateField>
            <%-- <asp:TemplateField HeaderText="滚动">
                <ItemTemplate>
                    <asp:Button ID="btngonggao"  CommandName="gonggao" Text='<%# Eval("GongGao").ToString()=="1" ? "是": "否"%>' CommandArgument='<%# Eval("NewsID") +"|"+Eval("GongGao")%> ' runat="server" CssClass="btnT" /> 
                </ItemTemplate>                    
            </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="修改">
                <ItemTemplate>
                    <a href="#" onclick="javascript:parent.addTab('修改信息','/websystem/News/News_AddEdit.aspx?TypeID=<%# Eval("NewsID") %>');"><image src="../../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
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
              <webdiyer:aspnetpager id="AspNetPager2" runat="server"  AlwaysShow="true" 
               width="100%" onpagechanged="AspNetPager2_PageChanged"    > </webdiyer:aspnetpager> </td>
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
