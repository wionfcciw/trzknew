<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LqTJS_tdsd.aspx.cs" Inherits="SincciKC.websystem.zklq.LqTJS_tdsd" %>

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
            ymPrompt.win({ message: 'HegeKs_AddEdit.aspx?type=1&ksh=' + ID, width: 350, height: 295, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }

        function opdg2(ID, Title) {
            ymPrompt.win({ message: 'HegeKs_AddEdit.aspx?type=2&ksh=' + ID, width: 350, height: 295, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function SelectAll() {
            var checkboxs = document.getElementsByName("CheckBox1");

            for (var i = 0; i < checkboxs.length; i++) {
                var e = checkboxs[i]; e.checked = !e.checked;
            }
        }

        function addMask() {
            var s = document.getElementById("dlistXx").value;
            switch (s) {
                case "1":
                    s = "男儿幼儿师范";
                    break;
                case "2":
                    s = "配额生";
                    break;
                case "3":
                    s = "统招生";
                    break;
                case "4":
                    s = "普高国际班";
                    break;
                case "5":
                    s = "师范(音乐、美术、学前教育专业)";
                    break;
//                case "6":
//                    s = "三星普高国际班";
//                    break;
            }
            if (s == "") {
                alert("请先选择类型!");
                return;
            }
            if (confirm("您确定要将数据导入至[" + s + "]吗?")) {
            }
            else {
                return false;
            }
//            var str = "<div id='backgroud' class='mask-backgroud'>";
//            str += "<div id='image' class='mask-image'></div>";
//            str += "<div id='text' class='mask-text'>请稍等，正在导入数据。。。。。</div>"
//            str += "</div>";

//            $("body").append(str);
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
             <td><asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
            OnClientClick="return MsgYes();" OnClick="btnDelete_Click" /> </td>
           
               
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
                     <td>
                        <b>县区</b>
                    </td>
 
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
                   <tr style="height:24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %> >
                      <td style=" width:60px"> 
                     <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("id") %>'    > </td>      
                    
                 
                     <td><%#Eval("ksh")%></td> 
                      <td><%#Eval("xm")%></td> 
                     <td><%#Eval("xqdm")%></td> 
                  
                     

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
     <div runat="server"  class="datagrid-toolbar">
         <span id="ImportData" runat="server" >&nbsp;&nbsp;数据导入：<asp:FileUpload ID="fuExcelFileImport" runat="server" />
             <asp:Button ID="btnExcelFileImport" runat="server" Text="  导入数据" CssClass="icon-reload btn"
                  OnClick="btnExcelFileImport_Click" /> 
               <asp:Button ID="btnDTxt" runat="server"  Text="下载日志" 
             onclick="btnDTxt_Click" Visible="false" />        <a href="../../Template/tjs5.xls">模板下载</a></span>
          <span  id="ExPortData" runat="server" > &nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnExport" runat="server" 
             Text="  导出数据" CssClass="icon-reload btn" onclick="btnExport_Click" Visible="false"
                />    </span>
    </div>
    <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false"
                 collapsible="false" minimizable="false" style="width: 385px; height: 270px;"
                 runat="server">
             </div>
    <asp:HiddenField ID="hfDelIDS" runat="server" />
    </form>
</body>
</html>