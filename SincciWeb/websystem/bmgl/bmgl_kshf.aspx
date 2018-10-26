<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bmgl_kshf.aspx.cs" Inherits="SincciKC.websystem.bmgl.bmgl_kshf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学校代码管理</title>
 <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script src="../../easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function MsgYes() {
            var ids = "";
            $("[name='CheckBox1']").each(function () {
                if ($(this).attr("checked"))
                    ids += $(this).val() + ",";
            });

            if (ids.length > 0)
                $("#hfDelIDS").val(ids.substr(0, ids.length - 1));
            else {
                alert("请选择需要恢复的数据。");
                return false;
            }

            if (confirm('您确定要恢复选中的信息吗？')) {
                return true;
            }
            else {
                return false;
            }
        }

        function opdg(ID, Title) {
            ymPrompt.win({ message: 'Hkcj_AddEdit.aspx?ksh=' + ID, width: 350, height: 295, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function SelectAll() {
            var checkboxs = document.getElementsByName("CheckBox1");

            for (var i = 0; i < checkboxs.length; i++) {
                var e = checkboxs[i]; e.checked = !e.checked;
            }
        }
        function opUp(ID, Title) {

            window.parent.addTab2(Title, '/tmpUpLoadFile/' + ID);
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
   <div id="tb" class="datagrid-toolbar"  >
         <table>
             <tr> 
             <td><asp:Button ID="btnDelete" runat="server" Text="  恢复" CssClass="icon-remove btn"
            OnClientClick="return MsgYes();" OnClick="btnDelete_Click" /> </td>
                 <td style="width: 60px">
                     市(区)：
                 </td>
                 <td style="width: 100px">
                     <asp:DropDownList ID="dlistSq" runat="server" OnSelectedIndexChanged="dlistSq_SelectedIndexChanged"
                         AutoPostBack="True">
                     </asp:DropDownList>
                 </td>
                 <td style="width: 45px">
                     学校：
                 </td>
                 <td>
                     <asp:DropDownList ID="dlistXx" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlistXx_SelectedIndexChanged">
                     <asp:ListItem  Value="">请选择</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                 <td>
                     班级:
                 </td>
                 <td>
                     <asp:DropDownList ID="dlistBj" runat="server" AutoPostBack="True">
                      <asp:ListItem  Value="">请选择</asp:ListItem>
                     </asp:DropDownList>
                 </td>
        
                 <td>
                     报名号/姓名：
                 </td>
                 <td>
                    <asp:TextBox ID="txtName" runat="server" Width="108px" CssClass="searchbox"></asp:TextBox>
        
                     <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
                 </td>
                 
             </tr>
             </table>
             </div>
     
    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
        cellpadding="2" cellspacing="0">
        <asp:Repeater ID="repDisplay" runat="server">
        <HeaderTemplate>
            <table class="tbColor" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >
                <tr class="datagrid-header" style="height: 26px">
                    <td>
                            <b> <input type="checkbox" name="checkboxid" id="checkboxid"  onclick="SelectAll();"   />全选</b>
                    </td>
                    <td>
                        <b>报名号</b>
                    </td>
                     <td>
                        <b>姓名</b>
                    </td>
                    <%-- <td>
                        <b>毕业中学</b>
                    </td>--%>
                   
                    <td>
                        <b>县区代码</b>
                    </td>
                    <td>
                        <b>学校代码</b>
                    </td> 
                     <td><b>删除用户</b></td>
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
                   <tr style="height:24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %> >
                      <td> 
                     <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("id") %>'   > </td>      
                    
                 
                    
                      <td><%#Eval("ksh")%></td>
                      <%-- <td align="left"><%#Eval("bmd")%></td>--%>
                    
                      <td><%#Eval("xm")%> </td> 
                      <td><%#Eval("bmdxqdm")%> </td>
                      
                     <td><%#Eval("bmddm")%> </td>
               
                        <td><%#Eval("delUser")%> </td>
               
                 
                    </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater>
    </table>
    <div id="tbpage" class="datagrid-toolbar" style="padding-left: 10px">

     <table width="100%">
            <tr>
           <td>
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
     

    <asp:HiddenField ID="hfDelIDS" runat="server" />
    </form>
</body>
</html>
