<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LQDAOForm.aspx.cs"
    Inherits="SincciKC.LQDAOForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
      <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css"  />
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <link href="../../easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../easyui/themes/icon.css" rel="stylesheet" type="text/css" />
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

    function opdg(ID, Title) {
        ymPrompt.win({ message: 'Zyk_AddEdit.aspx?ID=' + ID, width: 440, height: 250, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }

    function opdy(ID, Title) {

        //window.parent.addTab2(Title, '/websystem/bmgl/Printxx.aspx?ksh=' + ID + '&title=' + Title);
        //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
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

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: center; vertical-align: bottom; height: 26px; font-size: 18px;
            font-weight: bold; padding-top: 8px" class="datagrid-toolbar">
            导入录取</div>
        <div class="datagrid-toolbar">
            Excel数据导入<asp:FileUpload ID="fuExcelFileImport" runat="server" />
            <asp:Button ID="btnExcelFileImport" runat="server" Text="导入" CssClass="icon-reload btn"
                Width="60" OnClick="btnExcelFileImport_Click" OnClientClick="addMask();" />
            <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false"
                collapsible="false" minimizable="false" style="width: 385px; height: 270px;"
                runat="server">
            </div>
            <a href="../../Template/lqdao.xls">模板下载</a>
        </div>
        <asp:HiddenField ID="hfDelIDS" runat="server" />
    </div>
    </form>
</body>
</html>
