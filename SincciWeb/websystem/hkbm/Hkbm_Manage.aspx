<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hkbm_Manage.aspx.cs" Inherits="SincciKC.websystem.hkbm.Hkbm_Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试信息管理</title>
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
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

        //全选、全不选
        function SelectAll(obj) {
            //获取所有的input的元素
            var inputs = document.getElementsByTagName("input");

            for (var i = 0; i < inputs.length; i++) {
                //如果是复选框
                if (inputs[i].type == "checkbox") {
                    inputs[i].checked = obj.checked;
                }
            }
        }

        function opdg3() {
            var ids = "";

            $("[name='CheckBox1']").each(function () {
                if ($(this).attr("checked"))
                    ids = $(this).val();
            });
            // alert(ids);
            if (ids.length > 0) {
                ymPrompt.win({ message: 'xxgl_AddEdit.aspx?ksh=' + ids, width: 500, height: 400, title: '修改考生信息', iframe: true, fixPosition: true, dragOut: false })
                return false;
                // alert(ids);
            }
            else {
                alert("请选择考生。");
                return false;
            }

        }

        function opdg4() {
            var ids = "";

            $("[name='CheckBox1']").each(function () {
                if ($(this).attr("checked"))
                    ids = $(this).val();
            });

            if (ids.length > 0) {
                return true;
            }
            else {
                alert("请选择考生。");
                return false;
            }

        }
        function addMask() {
            var str = "<div id='backgroud' class='mask-backgroud'>";
            str += "<div id='image' class='mask-image'></div>";
            str += "<div id='text' class='mask-text'>请稍等，正在导入数据。。。。。</div>"
            str += "</div>";

            $("body").append(str);
        }

        function opdg2(ID, Title) {
            ymPrompt.win({ message: 'xxgl_AddEdit.aspx', width: 500, height: 420, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function opdg4(ID, Title) {
            ymPrompt.win({ message: 'xxgl_Export.aspx', width: 300, height: 180, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function opdg(ID, Title) {
            window.parent.addTab2(Title, '/websystem/hkbm/HkbmInfo.aspx?ksh=' + ID + '&title=' + Title);
            //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function opUp(ID, Title) {

            window.parent.addTab2(Title, '/websystem/bmgl/xxgl_Updete.aspx?ksh=' + ID + '&title=' + Title);
            return false;
        }
        function opExport(ID, Title) {
            if (confirm('是否导出数据？', '确定', '取消')) {
                window.parent.addTab2(Title, '/websystem/hkbm/Hkbm_ExportSource.aspx');

            }

            //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })

        }
        function opExportExcel(ID, Title) {
            if (confirm('是否导出数据？', '确定', '取消')) {
                window.parent.addTab2(Title, '/websystem/hkbm/Hkbm_ExportExcel.aspx');

            }

            //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })

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
                        <asp:ListItem Value="">请选择</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    班级:
                </td>
                <td>
                    <asp:DropDownList ID="dlistBj" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="">请选择</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    状态:
                    <asp:DropDownList ID="dlistZt" runat="server">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="1">学校已确定</asp:ListItem>
                        <asp:ListItem Value="2">学校未确定</asp:ListItem>
                        <asp:ListItem Value="3">县区已确定</asp:ListItem>
                        <asp:ListItem Value="4">县区未确定</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="Div1" class="datagrid-toolbar">
        报名号/姓名/身份证号：
        <asp:TextBox ID="txtName" CssClass="searchbox" runat="server" Width="150px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
    </div>
    <div class="datagrid-toolbar">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
                        OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
                </td>
                <td>
                    <asp:Button ID="btndaoAll" runat="server" CssClass="icon-reload" 
                        Text="  导出数据(DBF)" OnClientClick="opExport('', '导出数据') ;" 
                          />
                </td>
                 <td>
                    <asp:Button ID="btnexcel" runat="server" CssClass="icon-reload" Text="  导出数据(EXCEL)" OnClientClick="opExportExcel('', '导出数据') ;" />
                </td>
                <td>
                    <asp:Button ID="butDaochu" runat="server" CssClass="icon-reload" Text="  导出数据(报名号)"
                        OnClientClick="return opdg4('','导出考生信息')" Visible="false" />
                </td>
                <td>
                    <asp:Button ID="btnResetTag" runat="server" CssClass="icon-reload" Text="  状态重置"
                        OnClientClick="return confirm('是否重置您所选择考生的状态？', '确定', '取消')" OnClick="btnResetTag_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAllQr" runat="server" CssClass="icon-reload" Text="  全部确认" OnClientClick="return confirm('是否全部确认？', '确定', '取消')"
                        OnClick="btnAllQr_Click" />
                </td>
                <td>
                    <asp:Button ID="btnQr" runat="server" CssClass="icon-reload" Text="  确认" OnClientClick="return confirm('是否确认您所选择考生的状态？', '确定', '取消')"
                        OnClick="btnQr_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
                cellpadding="2" cellspacing="0">
                <tr class="datagrid-header" style="height: 26px">
                    <th>
                        全选
                        <input type="checkbox" name="checkboxid" id="checkboxid" onclick="SelectAll(this)" />
                    </th>
                    <th>
                        考次
                    </th>
                    <th>
                        报名号
                    </th>
                    <th>
                        姓名
                    </th>
                    <th>
                        学籍号
                    </th>
                    <th>
                        身份证号
                    </th>
                    <th>
                        毕业中学
                    </th>
                    <th>
                        班级
                    </th>
                    <th>
                        学校确认
                    </th>
                    <th>
                        县区确认
                    </th>
                    <th>
                        是否照相
                    </th>
                    <th>
                        详情
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                <td>
                    <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("ksh") %>'>
                </td>
                <td>
                    <%#Eval("kaocimc")%>
                </td>
                <td>
                    <%#Eval("ksh")%>
                </td>
                <td>
                    <%#Eval("xm")%>
                </td>
                <td>
                    <%#Eval("xjh")%>
                </td>
                <td>
                    <%#Eval("sfzh")%>
                </td>
                <td>
                    <%#Eval("bmdmc")%>
                </td>
                <td>
                    <%#Eval("bjmc")%>
                </td>
                <td>
                    <%#Eval("xxqr").ToString() == "1" ? "已确认" :"未确认"%>
                </td>
                <td>
                    <%#Eval("xqqr").ToString() == "1" ? "已确认" :"未确认"%>
                </td>
                <td>
                    <%#Eval("pic").ToString() == "1" ? "是" :"否"%>
                </td>
                <td>
                    <a href="#" onclick='return opdg(<%#Eval("ksh")%>,"考生信息详情");'>详情</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </asp:Repeater>
    <div id="tbpage" class="datagrid-toolbar" style="padding-left: 10px">
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
    </div>
    <div   runat="server" id="divdaor" class="datagrid-toolbar" style="padding-left: 10px">
        <table>
            <tr>
                <td>
                    <asp:FileUpload ID="fuExcelFileImport" runat="server" CssClass="searchbox" />
                    <asp:Button ID="btnImport" runat="server" CssClass="icon-reload" Text="  导入数据" OnClientClick="return addMask();"
                        OnClick="btnImport_Click" /><asp:Button CssClass="icon-reload" ID="btnrizhi" runat="server"
                            Text="  下载日志" OnClick="btnrizhi_Click" />
                    <a href="/Template/hkKsTemplate.xls">模板下载1</a> <a href="/Template/ksKshTemplate.xls">
                        模板下载2(含报名号)</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false"
        collapsible="false" minimizable="false" style="width: 435px; height: 270px;"
        runat="server">
    </div>
    </form>
</body>
</html>
