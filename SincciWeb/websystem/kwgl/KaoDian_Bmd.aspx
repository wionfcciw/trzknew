<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KaoDian_Bmd.aspx.cs" Inherits="SincciKC.websystem.kwgl.KaoDian_Bmd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考点添加毕业中学</title>

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
       &nbsp;  市(区): <asp:DropDownList ID="dlistSq" runat="server" OnSelectedIndexChanged="dlistSq_SelectedIndexChanged"
                         AutoPostBack="True">
                     </asp:DropDownList>
 &nbsp;学校： <asp:DropDownList ID="dlistXx" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlistXx_SelectedIndexChanged">
                     <asp:ListItem  Value="">请选择</asp:ListItem>
                     </asp:DropDownList> 
      <%--  班级：
<asp:DropDownList ID="dlistBj" runat="server"  >
                      <asp:ListItem  Value="">请选择</asp:ListItem>
                     </asp:DropDownList>--%>

    <asp:Label ID="lblkddm" runat="server" Text=" " Visible="false" ></asp:Label>
     &nbsp;报名号：<asp:TextBox ID="txtksh" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="btnAdd" runat="server"   Text=" 添加 " onclick="btnAdd_Click" />

     </div>
      <div runat="server" id="bmdinfo" class="datagrid-toolbar" visible="false"></div>
    <div id="Div2" class="datagrid-toolbar">
     &nbsp;
     <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
                         OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />    &nbsp;&nbsp;&nbsp;    报名号/姓名：
        <asp:TextBox ID="txtName" runat="server" CssClass="searchbox" Width="118px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
    </div>
    
     <div class="datagrid-toolbar"  runat="server" id="divdaor">&nbsp;&nbsp;数据导入：<asp:FileUpload ID="fuExcelFileImport" runat="server" CssClass="searchbox"  />
        <asp:Button ID="btnExcelFileImport" runat="server" Text="  导入数据"
            CssClass="icon-reload btn"   onclick="btnExcelFileImport_Click"  OnClientClick="return addMask();" />
           <asp:Button ID="Button1" runat="server" Text="  下载日志 "
            CssClass="icon-reload btn" onclick="Button1_Click"     />
        <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false" collapsible="false" minimizable="false" style="width:385px;height:270px;" runat="server">
        </div>
        <a href="../../Template/kdksh.xls">模板下载</a>
    </div>
      
    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
        cellpadding="2" cellspacing="0">
        <asp:Repeater ID="repDisplay" runat="server"  >
            <HeaderTemplate>
                <tr class="datagrid-header" style="height: 26px">            
                    <td>
                        <b>全选
                           <input type="checkbox" name="checkboxid" id="checkboxid"  onclick="SelectAll(this)"  /></b>
                    </td> 
                      <td>
                        <b>序号</b>
                    </td>
                    <td>
                        <b>报名号</b>
                    </td>
                     <td>
                        <b>姓名</b>
                    </td>
                    <%-- <td>
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
                        <b>座位代码</b></td>--%>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                      <td>
                     <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("ksh") %>'>
                 </td>
                    <td> 
                       <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.ItemIndex + 1%>
                       
                    </td>
                     <td>
                        <%# Eval("ksh")%>
                    </td>
                    <td align="left">
                        <%# Eval("xm")%>
                    </td>
                    <%--<td >
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
                    </td>--%>
                    
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