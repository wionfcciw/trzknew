<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xxdm_Manage.aspx.cs" Inherits="SincciKC.websystem.jcsj.Xxdm_Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        function addMask() {
            var str = "<div id='backgroud' class='mask-backgroud'>";
            str += "<div id='image' class='mask-image'></div>";
            str += "<div id='text' class='mask-text'>请稍等，正在导入数据。。。。。</div>"
            str += "</div>";

            $("body").append(str);
        }
        function MsgYes() {
            var ids = "";
            $("[name='CheckBox1']").each(function () {
                if ($(this).attr("checked"))
                    ids += $(this).val() + ",";
            });

            if (ids.length == 0) {
                ymPrompt.alert({ message: '请选择需要删除的数据！', title: '提示' });
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
            ymPrompt.win({ message: 'Xxdm_AddEdit.aspx?xxdm=' + ID, width: 400, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
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
    <div id="tb" class="datagrid-toolbar">
        &nbsp;
        <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
            OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn" OnClientClick="return opdg(0,'新增数据');" />
            &nbsp;县区：<asp:DropDownList ID="ddlxqdm" runat="server"  >
        </asp:DropDownList>
        &nbsp; 学校代码/名称：
        <asp:TextBox ID="txtName" runat="server" Width="174px" CssClass="searchbox"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
    </div>
    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
        cellpadding="2" cellspacing="0">
        <asp:Repeater ID="repDisplay" runat="server" >
            <HeaderTemplate>
                <tr class="datagrid-header" style="height: 26px">
                <td style="width:70px">
                        <b> <input type="checkbox" name="checkboxid" id="checkboxid"  onclick="SelectAll();"  />全选</b>
                    </td>
                    <td>
                        <b>序号</b>
                    </td>
                    <td>
                        <b>所属县区</b>
                    </td>
                     <td>
                        <b>学校代码</b>
                    </td>
                     <td>
                        <b>学校名称</b>
                    </td> 
               <%--       <td>
                        <b>学校类型</b>
                    </td> --%>
                   <%-- <td>
                        <b>修改</b>
                    </td>--%>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                   <td><input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("xxdm") %>'> </td>
                    <td> 
                       <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.ItemIndex + 1%>
                       
                    </td>
                     <td>
                        <%# Eval("xqdm")%>
                    </td>
                    <td>
                        <%# Eval("xxdm")%>
                    </td>
                    <td align="left">
                        <%# Eval("xxmc")%>
                    </td>
                 <%--    <td>
                        <%# Eval("zlbmc")%>
                    </td>--%>
                   <%-- <td>
                        <a href="#" onclick="return opdg('<%# Eval("xxdm") %>' ,'修改数据');">
                            <image src="/easyui/themes/icons/pencil.png" alt="修改" border="0"></image>
                        </a>
                    </td>--%>
                </tr>
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
                AlwaysShow="True">
            </webdiyer:AspNetPager></td>
            <td>每页：<asp:DropDownList ID="ddlPageSize" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="ddlPageSize_SelectedIndexChanged">                     
            </asp:DropDownList>条</td>
         </tr>
       </table> 
    </div>
     <div class="datagrid-toolbar" runat="server" id="divdaor">&nbsp;&nbsp;数据导入：<asp:FileUpload ID="fuExcelFileImport" runat="server"  CssClass="searchbox"/>
        <asp:Button ID="btnExcelFileImport" runat="server" Text="  导入数据"
            CssClass="icon-reload btn"   onclick="btnExcelFileImport_Click"  OnClientClick="addMask();"/>
        <asp:CheckBox ID="chkIsZL" runat="server" Checked="true" Text="是否增量导入" />
        <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false" collapsible="false" minimizable="false" style="width:385px;height:270px;" runat="server">
        </div>
        <a href="../../Template/xxTemplate.xls">模板下载</a>
    </div>

    
    </form>
</body>
</html>