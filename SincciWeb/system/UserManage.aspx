<%@ Page Language="C#"   AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="SincciWeb.system.UserManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户管理</title>
<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
<script src="../js/addTableListener.js" type="text/javascript"></script>
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
<script src="../../js/Jquery183.js" type="text/javascript"></script>
 <script src="../../easyui/jquery.easyui.min.js" type="text/javascript"></script>
<script type="text/javascript">
    function MsgYes() {
        if (confirm('确定要删除信息吗？')) {
            return true;
        }
        else {
            return false;
        }
    }

    function MsgYes2() {
        if (confirm('确定要初始化用户密码吗？')) {
            return true;
        }
        else {
            return false;
        }
    }

    function opdg(Userid, Title, UserTypeID) {

      //  window.parent.addTab(Title, 'UserAddEdit.aspx?Userid=' + Userid + '&title=' + Title);

        ymPrompt.win({ message: 'UserAddEdit.aspx?Userid=' + Userid+'&UserTypeID='+UserTypeID , width: 580, height: 460, title: Title, iframe: true,  dragOut: false })
        return false;
    }
    function addMask() {
        var str = "<div id='backgroud' class='mask-backgroud'>";
        str += "<div id='image' class='mask-image'></div>";
        str += "<div id='text' class='mask-text'>请稍等，正在导入数据。。。。。</div>"
        str += "</div>";

        $("body").append(str);
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
          <asp:Button ID="btnReset" runat="server" Text=" 初始化密码" 
              CssClass="icon-reload btn"  OnClientClick="return MsgYes2();" 
              onclick="btnReset_Click" />
          &nbsp;&nbsp;
          <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn"  OnClientClick="return opdg(0,'新增用户',0);"/>
  
          用户类型：<asp:DropDownList ID="ddlType" runat="server">
         </asp:DropDownList> 帐号：
         <asp:TextBox ID="txtUser" runat="server" CssClass="searchbox"></asp:TextBox> 
         <asp:Button ID="btnSearch" runat="server" CssClass="icon-search"  Text="  查 询 " 
              onclick="btnSearch_Click" />
  
    </div> 

    <asp:GridView id="GridView1" runat="server"  CssClass="tbColor"   OnRowDataBound="GridView1_RowDataBound" 
    AutoGenerateColumns="False" DataKeyNames="Userid"  >
        <Columns>                 
            <asp:TemplateField HeaderText="选择"  > 
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"  />  
                </ItemTemplate>                    
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序号"  >
                <ItemTemplate><%# (this.AspNetPager1.CurrentPageIndex-1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%></ItemTemplate>                    
            </asp:TemplateField>
            <asp:BoundField HeaderText="用户ID" DataField="Userid"  Visible="false"   />
             <asp:BoundField HeaderText="登录帐号"   DataField="U_loginname"/> 
            <asp:BoundField HeaderText="用户名称"   DataField="U_xm"/> 
            <asp:BoundField HeaderText="性别"   DataField="U_xb" /> 
            <asp:BoundField HeaderText="联系电话"   DataField="U_phone" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate><%# Eval("U_tag").ToString()=="1"?"开通":"<font color='red'>关闭</font>"%> </ItemTemplate>
            </asp:TemplateField> 
          
             <asp:TemplateField HeaderText="用户类型">
                <ItemTemplate>
                        <%# new BLL.Method().Sys_UserTypeDisp((int)Eval("U_usertype")).T_Name%>                     
                </ItemTemplate>
            </asp:TemplateField>  

              <asp:TemplateField HeaderText="所属部门" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                        <%# fanweimc((int)Eval("U_usertype"),  Eval("U_department").ToString())%>                     
                </ItemTemplate>
            </asp:TemplateField>  
           

            <asp:BoundField HeaderText="登录时间"   DataField="U_datetime" />
             <asp:TemplateField HeaderText="在线状态">
                <ItemTemplate>
                        <%# new BLL.OnlineDataBase().OnlineCheck(Eval("U_loginname").ToString())==true? "在线":"<font color='red'>离线</font>"%>                     
                </ItemTemplate>
            </asp:TemplateField>  

           
            
            <asp:TemplateField HeaderText="修改" >
                <ItemTemplate>
                    <a href="#" onclick="return opdg(<%# Eval("Userid")%>,'修改用户',<%# Eval("U_usertype")%>);"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
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
             <webdiyer:aspnetpager id="AspNetPager1" runat="server" Width="100%" 
                   onpagechanged="AspNetPager1_PageChanged"   >
               </webdiyer:aspnetpager></td>
               <td>每页：<asp:DropDownList ID="ddlPageSize" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="ddlPageSize_SelectedIndexChanged">                     
            </asp:DropDownList>条</td>
         </tr>
       </table> 
    </div> 
               <div id="divdaoru" class="datagrid-toolbar" runat="server" style="padding-left:10px" >
         <table  >
             <tr>
                
                   <td> 
                       <asp:FileUpload ID="fuExcelFileImport" runat="server" CssClass="searchbox"/>
                       <asp:Button ID="btnImport" runat="server" CssClass="icon-reload" Text="  导入数据" 
                           onclick="btnImport_Click"  OnClientClick="addMask();"  /> <a href="/Template/UserTemplate.xls">模板下载</a>
                        
                 </td>
             </tr>
         </table>
     </div>    
      <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false" collapsible="false" minimizable="false" style="width:385px;height:270px;" runat="server">
        </div>
    </form>
</body>
</html>
