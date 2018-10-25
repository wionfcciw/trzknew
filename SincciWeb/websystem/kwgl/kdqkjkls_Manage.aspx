<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kdqkjkls_Manage.aspx.cs" Inherits="SincciKC.websystem.kwgl.kdqkjkls_Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考点监考老师登记</title>

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
                    ids +=  + $(this).val() + ",";
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

        

        function opdg(ID, Title) {
            ymPrompt.win({ message: 'kdqkjkls_Add.aspx?ID=' + ID, width: 620, height: 400, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function opUp(ID, Title) {
            ymPrompt.win({ message: 'kdqkjkls_Add.aspx?ID=' + ID, width: 620, height: 400, title: Title, iframe: true, fixPosition: true, dragOut: false })
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

        function checknum(theform) {
            if ((fucCheckNUM(theform.value) == 0)) {
                theform.value = "";
                //theform.newprice.focus();
                return false;
            }
        }
        function fucCheckNUM(NUM) {
            var i, j, strTemp;
            strTemp = "0123456789-";
            if (NUM.length == 0)
                return 0
            for (i = 0; i < NUM.length; i++) {
                j = strTemp.indexOf(NUM.charAt(i));
                if (j == -1) {
                    //说明有字符不是数字
                    return 0;
                }
            }
            //说明是数字
            return 1;
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
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td> 县区:<asp:DropDownList ID="listkemu" runat="server" 
                        onselectedindexchanged="listkemu_SelectedIndexChanged" 
                        AutoPostBack="True"  >
                    
                     </asp:DropDownList>
                </td>
                <td>
                考点:<asp:DropDownList ID="listleib" runat="server"  >
                        <asp:ListItem Value="">-请选择-</asp:ListItem>
                     </asp:DropDownList>
                </td>
                <td>姓名:
                         <asp:TextBox ID="txtksh" runat="server"    MaxLength="12" CssClass="input1"></asp:TextBox>
          
                
                </td>
                <td> <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
     
        
    </div>
    <div id="Div2" class="datagrid-toolbar">
        <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn" 
            OnClientClick="return opdg(0,'新增数据');"   />
        <asp:Button ID="btnEdit" runat="server" CssClass="icon-edit" Text="  修改" 
            onclick="btnEdit_Click"     />
        <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
            OnClientClick="return MsgYes();" onclick="btnDelete_Click" 
               />
            <asp:Button ID="btndaoAll" runat="server" CssClass="icon-reload" 
            Text="  导出数据" onclick="btndaoAll_Click"      />
            
    </div>
    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
        cellpadding="2" cellspacing="0">
        <asp:Repeater ID="repDisplay" runat="server"  >
            <HeaderTemplate>
                <tr class="datagrid-header" style="height: 26px">    
                  <td>
                        <b> <input type="checkbox" name="checkboxid" id="checkboxid"  onclick="SelectAll();"  />全选</b>
                    </td>
                    <td>
                        <b>序号</b>
                    </td>
                     <td>
                        <b>身份证号</b>
                    </td>
                     <td>
                        <b>姓名</b>
                    </td>
                     <td>
                        <b>工作单位</b>
                    </td>
                    <td>
                        <b>评价1</b>
                    </td>
                     
                     <td>
                        <b>评价2</b>
                    </td>  
                     <td>
                        <b>评价3</b>
                    </td>  
                      <td>
                        <b>评价4</b>
                    </td>  
                      <td>
                        <b>评价5</b>
                    </td>  
                      <td>
                        <b>评价6</b>
                    </td>  
                      <td>
                        <b>评价7</b>
                    </td>  
                      <td>
                        <b>备注</b>
                    </td>  
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                     <td><input type="checkbox" name="CheckBox1" id="CheckBox1"   value='<%# Eval("ID") %>'> </td>
                  
                    <td> 
                       <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.ItemIndex + 1%>
                       
                    </td>
                     <td>
                        <%# Eval("sfzh")%>
                    </td>
                      <td>
                        <%# Eval("xm")%>
                    </td>
                   <td>
                        <%# Eval("gzdw")%>
                    </td>
                     <td  >
                      <%# Eval("pj1")%></td>
                       <td  >
                      <%# Eval("pj2")%></td>
                       <td  >
                      <%# Eval("pj3")%></td>
                       <td  >
                      <%# Eval("pj4")%></td>
                       <td  >
                      <%# Eval("pj5")%></td>
                       <td  >
                      <%# Eval("pj6")%></td>
                       <td  >
                      <%# Eval("pj7")%></td>
                      
                         <td >
                      <%# Eval("bz")%> <%--<asp:TextBox Width="300" Height="30" TextMode="MultiLine" ID="TextBox1" runat="server"  Enabled="false"  Text='<%# Eval("bz")%>'> --%></asp:TextBox> </td>
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
      <div class="datagrid-toolbar"  runat="server" id="divdaor">&nbsp;&nbsp;数据导入：<asp:FileUpload ID="fuExcelFileImport" runat="server" CssClass="searchbox"  />
        <asp:Button ID="btnExcelFileImport" runat="server" Text="  导入数据"
            CssClass="icon-reload btn"   onclick="btnExcelFileImport_Click"  OnClientClick="return addMask();" />
          
        <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false" collapsible="false" minimizable="false" style="width:385px;height:270px;" runat="server">
        </div>
        <a href="../../Template/kdjkls.xls">模板下载</a>
    </div>

    </form>
</body>
</html>