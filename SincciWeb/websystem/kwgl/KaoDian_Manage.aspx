<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KaoDian_Manage.aspx.cs" Inherits="SincciKC.websystem.kwgl.KaoDian_Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考点管理</title>
<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />

    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function MsgYes() {
            var ids = "";
            $("[name='CheckBox1']").each(function () {
                if ($(this).attr("checked"))
                    ids += $(this).val() + ",";
            });

            if (ids.length == 0) {
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

        function opgEdit() {
            var ids = "";

            $("[name='CheckBox1']").each(function () {
                if ($(this).attr("checked"))
                    ids = $(this).val();
            });
            if (ids.length > 0) {
                opdg(ids, "修改数据");
                return false;
            }
            else {
                ymPrompt.alert({ message: '请选择数据！', title: '提示' });
                return false;
            }
        }

        function opdg(ID, Title) {
            ymPrompt.win({ message: 'KaoDian_AddEdit.aspx?kddm=' + ID, width: 750, height: 440, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function opdg2(ID, Title) {
            ymPrompt.win({ message: 'KaoDian_Show.aspx?kddm=' + ID, width: 580, height: 480, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function opdgksz(ID, Title) {
            window.parent.addTab2(Title, '/websystem/kwgl/ksz_print.aspx?kddm=' + ID + '&title=' + Title);
            
            return false;
        }
        function opdgzp(ID, Title) {
            window.parent.addTab2(Title, '/websystem/kwgl/kc_print.aspx?kddm=' + ID + '&title=' + Title);

            return false;
        }
        function opdgbmd(ID, Title) {
            window.parent.addTab2(Title, '/websystem/kwgl/KaoDian_Bmd.aspx?kddm=' + ID + '&title=' + Title);

            return false;
        }
        

        
        function opdg3(ID, Title) {
            ymPrompt.win({ message: 'KaoDian_Cancel.aspx?kddm=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }

        function SelectAll() {
            var checkboxs = document.getElementsByName("CheckBox1");

            for (var i = 0; i < checkboxs.length; i++) {
                var e = checkboxs[i]; e.checked = !e.checked;
            }
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
    <div id="Div1" class="datagrid-toolbar">
       &nbsp; 县区：<asp:DropDownList ID="ddlxqdm" runat="server">
        </asp:DropDownList>
        &nbsp;考点代码/名称：
        <asp:TextBox ID="txtName" runat="server" CssClass="searchbox" Width="118px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
    </div>
    <div id="tb" class="datagrid-toolbar">
        &nbsp;
        <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
            OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
        &nbsp; 
        <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn" OnClientClick="return opdg('','新增数据');" />
        &nbsp;
        <%--  <asp:Button ID="btndbf"  CssClass="icon-reload"  runat="server" 
            Text=" 导出DBF " onclick="btndbf_Click"  />--%>
        &nbsp;&nbsp;&nbsp;混编方式：<asp:DropDownList ID="ddltype" runat="server">
            <asp:ListItem Value="">--请选择--</asp:ListItem>
            <asp:ListItem Value="3">相同学校不能坐一起混编</asp:ListItem>
             <asp:ListItem Value="4">相同学校相同班级不能坐一起混编</asp:ListItem>
            <asp:ListItem Value="1">考点为单位混编</asp:ListItem>
            <asp:ListItem Value="2">考点内以毕业中学为单位混编</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnKCBP" runat="server" Text=" 编排考场 " OnClick="btnKCBP_Click" />
         <asp:Button ID="btnquxiao" runat="server" Text=" 取消编排 " 
            onclick="btnquxiao_Click"   />
        <%--（请先选择下面一个或多个考点进行编排。）--%>
    </div>
    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
        cellpadding="2" cellspacing="0">
        <asp:Repeater ID="repDisplay" runat="server" 
            onitemdatabound="repDisplay_ItemDataBound"  >
            <HeaderTemplate>
                <tr class="datagrid-header" style="height: 26px">
                <td>
                        <b> <input type="checkbox" name="checkboxid" id="checkboxid"  onclick="SelectAll();"  />全选</b>
                    </td>
                    <td>
                        <b>序号</b>
                    </td>
                    <td>
                        <b>所属县区</b>
                    </td>
                     <td>
                        <b>考点代码</b>
                    </td>
                     <td>
                        <b>考点名称</b>
                    </td>  
                    <td>
                        <b>状态</b>
                    </td>
                    <td width="250px" ><b>考生来源</b></td>
                    <%-- <td>
                        <b>混编方式</b>
                    </td>--%>
                    <td>总数 </td>
                      <td> 考场数</td>
                    <%-- <td>
                        <b>查看考点信息</b>
                    </td>--%>
                     <td>
                        <b>设置毕业中学</b>
                    </td>
                    <td colspan="2">
                        <b>打印表格</b>
                    </td>
                   <%-- <td>
                        <b>取消编排</b>
                    </td>--%>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr  style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                   <td><input type="checkbox" name="CheckBox1" id="CheckBox1"    <%# Eval("isbp").ToString()=="1"?"disabled":""%>  value='<%# Eval("kddm") %>'> </td>
                    <td> 
                       <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.ItemIndex + 1%>
                       
                    </td>
                     <td>
                        <%# Eval("xqdm")%>
                    </td>
                    <td  >
                        <%# Eval("kddm")%>
                    </td>
                    <td >
                        <%# Eval("kdmc")%>
                    </td>
                     <td >
                        <%# Eval("isbp").ToString()=="1"?"已编排":"未编排"%>
                    </td>
                    <td>
                        <table  width="250px" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                             <td>
                                <asp:Repeater runat="server" ID="Repeater2">
                                    <ItemTemplate>
                                       
                                            <%#Eval("bmdmc")%><%#Eval("rens")%>、
                                        
                                    </ItemTemplate>
                                </asp:Repeater>
                                 </td>
                            </tr>
                        </table>
                  </td>
                  <%--   <td >
                        <%# Eval("isxx").ToString() == "1" ? "考点为单位混编" :""%>
                        <%# Eval("isxx").ToString() == "2" ? "毕业中学为单位混编" : ""%>
                        <%# Eval("isxx").ToString() == "3" ? "相同学校不能坐一起混编" : ""%>
                        <%# Eval("isxx").ToString() == "4" ? "相同学校相同班级不能坐一起混编" : ""%>
                    </td>--%>
                    <td><%#Eval("zs")%> </td>
                         <td> <%#Eval("kcs")%></td>
                   <%-- <td >
                        <a href="#" onclick="return opdg2('<%# Eval("kddm")%>','查看考点信息');">查看考点信息</a>
                    </td>--%>
                      <td  >
                         <%# Eval("isbp").ToString()!= "1" ? "<a href=\"#\" onclick=\"return opdgbmd('" + Eval("kddm") + "','"+ Eval("kdmc")+"设置毕业中学');\">设置</a> " : "设置"%>
                        
                    </td>

                   <%--  <td>
                      <%# Eval("isbp").ToString() == "1" ? "<a href=\"#\" onclick=\"return opdgksz('" + Eval("kddm") + "','考点信息');\">考试证</a> " : "考试证"%>
                    </td>--%>
                   <%-- <td>
                          <a href="#" onclick="return opdgzp('<%# Eval("kddm")%>','考场信息');">签到表</a>    
                            
                    </td>   --%>
                    <td colspan="2">
                         <%# Eval("isbp").ToString() == "1" ? "<a href=\"#\" onclick=\"return opdgzp('" + Eval("kddm") + "','考场信息');\">其他报表</a> " : "其他报表"%>
                        
                    </td>
                 <%--   <td> <%#  (BLL.SincciLogin.Sessionstu().UserType == 2 || BLL.SincciLogin.Sessionstu().UserType == 1) ? (Eval("isbp").ToString() == "1" ? " <a href=\"#\" onclick=\"return opdg3('" + Eval("kddm") + "','取消编排');\">取消编排</a>" : "取消编排") : "取消编排"%> </td>
              --%>  </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div id="tbpage" class="datagrid-toolbar" style="padding-left: 10px">

     <table width="100%">
            <tr> <td>
                <asp:Button ID="btnEdit" runat="server" CssClass="icon-edit" Text="  修改"  OnClientClick="return opgEdit();"/></td>
           <td>
              <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" 
                ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到" 
        RecordCount="299"  CurrentPageButtonPosition="Beginning" 
                FirstPageText="首页" LastPageText="尾页" PrevPageText="上页" NextPageText="下页" 
                onpagechanged="AspNetPager1_PageChanged" PagingButtonSpacing="8px" 
                AlwaysShow="True" NumericButtonCount="5">
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