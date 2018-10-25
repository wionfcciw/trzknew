<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SSO_Username_School_Manage.aspx.cs" Inherits="SincciKC.SsoLogin.SSO_Username_Manage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../css/page.css"  />
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
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
        //新增学校平台对应信息
        function option_add(ID, Title) {
            ymPrompt.win({ message: 'SSO_Username_School_Add.aspx', width: 500, height: 420, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        //修改学校平台对应信息
        function option_update(ID, Title) {

            ymPrompt.win({ message: 'SSO_Username_School_Edit.aspx?xxdm=' + ID + '&title=' + Title, width: 500, height: 420, title: Title, iframe: true, fixPosition: true, dragOut: false });
            return false;
        }
    </script>
    <title>学校用户平台账号维护</title>
    <style type="text/css">
        
            .btn
            {
	             cursor: hand; 
             }
  
            .icon-add{
	            background:url('../easyui/themes/icons/edit_add.png') no-repeat;
                    }
            .icon-edit{
	            background:url('../easyui/themes/icons/pencil.png') no-repeat;
                    }
    

 
            .tbColor
            {
	            font-size: 14px;
	            border: 1px solid f5f5f5;
	            width:100%;
	            text-align:center;
            }
 
            .datagrid-header{
	            overflow:hidden;
	            background:#fafafa url('../easyui/themes/default/images/datagrid_header_bg.gif') repeat-x left bottom;
	            border-bottom:1px solid #ccc;
	            cursor:default;
    
            }
            .datagrid-body{
	            margin:0;
	            padding:0;
	            overflow:auto;
	            zoom:1;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
             <tr>
                 <td style="width: 60px">
                     市(区):
                 </td>
                 <td style="width: 100px">
                     <asp:DropDownList ID="dlistSq" runat="server" OnSelectedIndexChanged="dlistSq_SelectedIndexChanged"
                         AutoPostBack="True">
                     </asp:DropDownList>
                 </td>
                 <td style="width: 45px">
                     学校:
                 </td>
                 <td>
                     <asp:DropDownList ID="dlistXx" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlistXx_SelectedIndexChanged">
                     <asp:ListItem  Value="">请选择</asp:ListItem>
                     </asp:DropDownList>
                 </td>
             </tr>
        </table>
        <div id="Div1" class="datagrid-toolbar"   >
              
                
                     学校名：
                
                     <asp:TextBox ID="txtName" CssClass="searchbox" runat="server" Width="150px"></asp:TextBox>
                     <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
           
        </div>
         <div  class="datagrid-toolbar"  style="height:21px;" >
            <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn" OnClientClick="return option_add('','新增学校平台信息')"  />
             <asp:Button ID="btnEdit" runat="server" CssClass="icon-edit" Text="  修改" onclick="btnEdit_Click"  /> 
             <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
                      OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
                
          
        </div> 
    </div>
  
      <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table class="tbColor" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >
                <tr class="datagrid-header" style="height: 26px">
                    <td>
                        <b>全选
                           <input type="checkbox" name="checkboxid" id="checkboxid"  onclick="SelectAll(this)"  /></b>
                    </td>
                     <td>
                        <b>名称</b>
                    </td>
                    <td>
                        <b>账号</b>
                    </td>
                    <td>
                        <b>平台学校schoolId</b>
                    </td>
                    <td>
                        <b>用户类别</b>
                    </td>
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
             <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                 <td>
                     <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("sys_name") %>'>
                 </td>
                  <td>
                      <%#Eval("xxmc")%> 
                 </td>
                 <td>
                      <%#Eval("sys_name")%> 
                 </td>
                  <td>
                      <%#Eval("organization_id")%> 
                 </td>
                 <td>
                     <%#Eval("user_type")%>
                 </td>
             </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater> 

      <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px" >
        <table width="100%">
            <tr><td>
              <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" 
                ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到" 
        RecordCount="299"  CurrentPageButtonPosition="Beginning" 
                FirstPageText="首页" LastPageText="尾页" PrevPageText="上页" NextPageText="下页" 
                onpagechanged="AspNetPager1_PageChanged" PagingButtonSpacing="8px" 
                AlwaysShow="True">
            </webdiyer:AspNetPager></td>
               <td>每页：<asp:DropDownList ID="ddlPageSize" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="ddlPageSize_SelectedIndexChanged">                     
            </asp:DropDownList>条</td>
         </tr>
       </table> 
    </div> 

    </form>
</body>
</html>
