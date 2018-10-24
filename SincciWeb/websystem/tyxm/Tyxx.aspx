<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tyxx.aspx.cs" Inherits="SincciKC.websystem.tyxm.Tyxx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>体育信息详情</title>
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
                alert("请选择需要删除的数据。");
                return false;
            }

            if (confirm('您确定要删除选中的信息吗？本次操作将不可恢复。')) {
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
            <td> 状态:
                     <asp:DropDownList ID="dlistkslx" runat="server">
                         <asp:ListItem Value="">全部</asp:ListItem>
                         <asp:ListItem Value="0">未登录</asp:ListItem>
                         <asp:ListItem Value="1">已保存</asp:ListItem>
                        <asp:ListItem Value="2">已确认</asp:ListItem>
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
     <div id="Div1" class="datagrid-toolbar"  >
         <table>
             <tr>
                 <td>
                     <asp:Button ID="btnEdit" runat="server" CssClass="icon-edit" Text="  修改" OnClick="btnEdit_Click"
                         Visible="False" />
                 </td>
                 <td>
                     <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
                         OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
                 </td>
                 <td>
                     <asp:Button ID="btndaoAll" runat="server" CssClass="icon-reload" Text="  导出数据" OnClick="btndaoAll_Click" />
                 </td>
                 <td>
                     <asp:Button ID="btnAllOK" runat="server" CssClass="icon-reload" Text="  全部确认" OnClick="btnAllOK_Click"
                         OnClientClick="return confirm('是否确认全部考生的状态？', '确定', '取消')" />
                 </td>
                 <td>
                     <asp:Button ID="btndanOK" runat="server" CssClass="icon-reload" Text="  确认" OnClick="btndanOK_Click"
                         OnClientClick="return confirm('是否确认您所选择考生的状态？', '确定', '取消')" />
                 </td>
                 <td>
                     <asp:Button ID="btnResetTag" runat="server" CssClass="icon-reload" Text="  状态重置"
                         OnClientClick="return confirm('是否重置您所选择考生的状态？', '确定', '取消')" OnClick="btnResetTag_Click" />
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
                        <b>
                            <input type="checkbox" name="checkboxid" id="checkboxid" onclick="SelectAll();" />全选</b>
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
                  <%--  <td>
                        <b>必考项目</b>
                    </td>
                    <td>
                        <b>抽定项目</b>
                    </td>--%>
                    <td>
                        <b>自选项目1</b>
                    </td>
                    <td>
                        <b>自选项目2</b>
                    </td>
                     <td>
                        <b>自选项目3</b>
                    </td>
                    <td>
                        <b>状态</b>
                    </td>
                    <td>
                        <b>学校确认</b>
                    </td>
                      <td>
                        <b>县区确认</b>
                    </td>
                </tr>
        </HeaderTemplate>
         <ItemTemplate>
             <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                 <td>
                     <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("ksh") %>'>
                 </td>
                 <td>
                     <asp:Label ID="lblksh" runat="server" Text='<%#Eval("ksh")%>'></asp:Label>
                 </td>
                 <td>
                     <%#Eval("xm")%>
                 </td>
                 <%-- <td align="left"><%#Eval("bmd")%></td>--%>
                 
                 <td>
                     <%#Eval("zxmc")%>
                 </td>
                 <td>
                     <%#Eval("bmc")%>
                 </td>
                  <td>
                     <%#Eval("zxmc3")%>
                 </td>
                 <td>
                     <%#Eval("kstyqr").ToString() == "2" ? "已确认" : Eval("kstyqr").ToString() == "1" ? "已保存" : "未登录"%>
                 </td>
                 <td> <%#Eval("xxtyqr").ToString() == "1" ? "已确认" : "未确认"%></td>
                 <td> <%#Eval("xqtyqr").ToString() == "1" ? "已确认" : "未确认"%></td>
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