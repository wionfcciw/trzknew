<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZBS_Import.aspx.cs" Inherits="SincciKC.websystem.zklq.ZBS_Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
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
        function SelectAll() {
            var checkboxs = document.getElementsByName("CheckBox1");

            for (var i = 0; i < checkboxs.length; i++) {
                var e = checkboxs[i]; e.checked = !e.checked;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: center; vertical-align: bottom; height: 26px; font-size: 18px;
            font-weight: bold; padding-top: 8px" class="datagrid-toolbar">
            配额生名额分配</div>
        <div id="tb" class="datagrid-toolbar">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
                            OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
                    </td>
                    <td>
                    县区：     <asp:DropDownList ID="dlistSq" runat="server" OnSelectedIndexChanged="dlistSq_SelectedIndexChanged"
                         AutoPostBack="True">
                     </asp:DropDownList>
                    </td>
                    <td>毕业学校： <asp:DropDownList ID="dlistXx" runat="server" AutoPostBack="True"  >
                     <asp:ListItem  Value="-1">请选择</asp:ListItem>
                     </asp:DropDownList>
                    </td>
                    <td>
                      <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
                    </td>
                    <%--  <td>类型：
                     <asp:DropDownList ID="dlistXx" runat="server" >
                     <asp:ListItem  Value="">请选择</asp:ListItem>
                         <asp:ListItem Value="1">推荐生</asp:ListItem>
                         <asp:ListItem Value="2">配额生</asp:ListItem>
                         <asp:ListItem Value="3">统招生</asp:ListItem>
                         
                     </asp:DropDownList>
                      <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
                 </td>--%>
                    <%--<td>
                     班级:
                 </td>
                 <td>
                     <asp:DropDownList ID="dlistBj" runat="server" AutoPostBack="True">
                      <asp:ListItem  Value="">请选择</asp:ListItem>
                     </asp:DropDownList>
                 </td>--%>
                </tr>
            </table>
        </div>
        <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
            cellpadding="2" cellspacing="0">
            <asp:Repeater ID="repDisplay" runat="server">
                <HeaderTemplate>
                    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
                        cellpadding="2" cellspacing="0">
                        <tr class="datagrid-header" style="height: 26px">
                            <td>
                                <b>
                                    <input type="checkbox" name="checkboxid" id="checkboxid" onclick="SelectAll();" />全选</b>
                            </td>
                            <td>
                                <b>毕业学校代码</b>
                            </td>
                            <td>
                                <b>毕业学校名称</b>
                            </td>
                            <td>
                                <b>招生学校代码</b>
                            </td>
                            <td>
                                <b>招生学校名称</b>
                            </td>
                            <td>
                                <b>配额生分配数量</b>
                            </td>
                            <td>
                                <b>批次代码</b>
                            </td>
                            <td>
                                <b>县区代码</b>
                            </td>
                          <%--  <td>
                                <b>生源学校类型</b>
                            </td>--%>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                        <td style="width: 60px">
                            <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("lsh") %>'>
                        </td>
                        <td>
                            <%#Eval("xxdm")%>
                        </td>
                        <td>
                            <%#Eval("xxmc")%>
                        </td>
                        <td>
                            <%#Eval("zsxxdm")%>
                        </td>
                        <td>
                            <%#Eval("zsxxmc")%>
                        </td>
                        <td>
                            <%#Eval("zbssl")%>
                        </td>
                        <td>
                            <%#Eval("pcdm")%>
                        </td>
                        <td>
                            <%#Eval("xqdm")%>
                        </td>
                      <%--  <td>
                            <%#Eval("type").ToString() == "1" ? "城市" : Eval("type").ToString() == "2"?"农村":"无"%>
                        </td>--%>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </table>
        <div id="tbpage" class="datagrid-toolbar"  >
            <table width="100%">
                <tr>
                    <td>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
                            PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到" RecordCount="299"
                            CurrentPageButtonPosition="Beginning" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
                            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged" PagingButtonSpacing="8px"
                            AlwaysShow="True">
                        </webdiyer:AspNetPager>
                    </td>
                    <td>
                        每页：<asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        </asp:DropDownList>
                        条
                    </td>
                </tr>
            </table>
            <div class="datagrid-toolbar">
                <asp:FileUpload ID="ImportExecl" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                    ID="ImportBtn" runat="server" Text="配额生信息导入" OnClick="ImportBtn_Click" />&nbsp;&nbsp;
                <a href="../../Template/zbs_import.xls">模板下载</a><font style="color: Red"  >
                    注意事项：导入文件类型为Execl文件（xls或xlsx）
                </font>
            </div>
        </div>
           <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false" collapsible="false" minimizable="false" style="width:385px;height:270px;" runat="server">
        </div>
        <asp:HiddenField ID="hfDelIDS" runat="server" />
    </div>
    </form>
</body>
</html>
