<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kczw_Manage.aspx.cs" Inherits="SincciKC.websystem.kwgl.Kczw_Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考生考场座位查询</title>

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
            ymPrompt.win({ message: 'KaoDian_AddEdit.aspx?kddm=' + ID, width: 620, height: 500, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function opdg2(ID, Title) {
            ymPrompt.win({ message: 'KaoDian_Show.aspx?kddm=' + ID, width: 580, height: 480, title: Title, iframe: true, fixPosition: true, dragOut: false })
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
       &nbsp; 县区：<asp:DropDownList ID="ddlxqdm" runat="server"  AutoPostBack="true"
            onselectedindexchanged="ddlxqdm_SelectedIndexChanged">
        </asp:DropDownList>
 &nbsp;毕业中学：<asp:DropDownList ID="ddlkd" runat="server">
 <asp:ListItem Value="">请选择</asp:ListItem>
        </asp:DropDownList> 
        &nbsp;报名号/姓名：
        <asp:TextBox ID="txtName" runat="server" CssClass="searchbox" Width="118px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
    </div>
    <div id="Div2" class="datagrid-toolbar">
      <asp:Button ID="btndbf"  CssClass="icon-reload"  runat="server" 
            Text=" 导出DBF " onclick="btndbf_Click"  />
    <asp:Button ID="btnksh" runat="server" Text="   单个考生打印" CssClass="icon-print" 
            onclick="btnksh_Click"  />
        <asp:Button ID="btnPrint" runat="server" Text="   毕业中学打印" CssClass="icon-print" 
            onclick="btnPrint_Click"  />
        </div>

    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
        cellpadding="2" cellspacing="0">
        <asp:Repeater ID="repDisplay" runat="server"  >
            <HeaderTemplate>
                <tr class="datagrid-header" style="height: 26px">            
                    <td>
                        <b>序号</b>
                    </td>
                    <td>
                        <b>报名号</b>
                    </td>
                     <td>
                        <b>姓名</b>
                    </td>
                     <td>
                        <b>性别</b>
                    </td>  
                    <td>
                        <b>考点</b>
                    </td>
                     <td>
                        <b>考试号</b>
                    </td>
                     <td>
                        <b>考场代码</b>
                    </td>  
                    <td  >
                        <b>座位代码</b></td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                    
                    <td> 
                       <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.ItemIndex + 1%>
                       
                    </td>
                     <td>
                        <%# Eval("ksh")%>
                    </td>
                    <td  >
                        <%# Eval("xm")%>
                    </td>
                    <td >
                        <%# Eval("xbmc")%>
                    </td>
                     <td >
                        [<%# Eval("kddm").ToString() %>]<%# Eval("kdmc").ToString() %></td>
                     <td >
                        <%# Eval("zkzh").ToString() %>
                        
                    </td>
                    <td >
                         <%# Eval("kcdm")%> 
                    </td>
                     <td>
                       <%# Eval("zwh")%> 
                    </td>
                    
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div id="tbpage" class="datagrid-toolbar" style="padding-left: 10px">

     <table width="100%">
            <tr> <td>
                &nbsp;</td>
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