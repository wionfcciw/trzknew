<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cj_Manage.aspx.cs" Inherits="SincciKC.websystem.cjgl.cj_Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>中考成绩管理</title>
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
            ymPrompt.win({ message: 'cj_AddEdit.aspx?ksh=' + ID, width: 360, height: 330, title: Title, iframe: true, fixPosition: true, dragOut: false })
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
             <tr> <td>
                 <asp:Button ID="btnEdit" runat="server" CssClass="icon-edit" Text="  修改" 
                    onclick="btnEdit_Click"   /></td>
             <td><asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
            OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
            <asp:Button ID="btndaoAll" runat="server" CssClass="icon-reload" Text="  更新成绩"  
                              OnClientClick="return confirm('您确定要根据条件更新成绩吗?'); " onclick="btndaoAll_Click"  
                         />
             </td>
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
                        <b>道德品质与公民素养</b>
                    </td>
                    <td>
                        <b>交流与合作能力</b>
                    </td>
                    <td>
                        <b>学习习惯与学习能力</b>
                    </td>
                    <td>
                        <b>运动与健康</b>
                    </td>
                    <td>
                        <b>审美与表现</b>
                    </td>
                    <td>
                        <b>创新意识与实践能力</b>
                    </td><td>
                        <b>地理</b>
                    </td> 
                    <td>
                        <b>生物</b>
                    </td>
                    
                    <td>
                        <b>听力与口语</b>
                    </td>
                    <td>
                        <b>体育校</b>
                    </td> 
                    <td>
                        <b>体育统</b>
                    </td>
                    <td>
                        <b>体育总</b>
                    </td> 
                     
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
                   <tr style="height:24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %> >
                      <td> 
                     <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("ksh") %>'    > </td>      
                    
                 
                     <td><asp:Label ID="lblksh" runat="server" Text='<%#Eval("ksh")%>'></asp:Label>  </td> 
                      <td><%#Eval("xm")%></td>
                  
                    
                      <td><%#Eval("zp1")%> </td> 
                     <td><%#Eval("zp2")%> </td> 
                      <td><%#Eval("zp3")%> </td> 
                       <td><%#Eval("zp4")%> </td> 
                        <td><%#Eval("zp5")%> </td> 
                         <td><%#Eval("zp6")%> </td> 
                          <td><%#Eval("dl7")%> </td>
                          <td><%#Eval("sw8")%> </td>
                          <td><%#Eval("tl9")%> </td>
                          <td><%#Eval("tyx10")%> </td>
                          <td><%#Eval("tyt11")%> </td>
                          <td><%#Eval("tyz12")%> </td>          
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
     <div class="datagrid-toolbar"  runat="server" id="divdaor">&nbsp;&nbsp;数据导入：<asp:FileUpload ID="fuExcelFileImport" runat="server" CssClass="searchbox"  />
        <asp:Button ID="btnExcelFileImport" runat="server" Text="  导入数据"
            CssClass="icon-reload btn"   onclick="btnExcelFileImport_Click"  OnClientClick="return addMask();" />
           <asp:Button ID="Button1" runat="server" Text="  下载日志 "
            CssClass="icon-reload btn" onclick="Button1_Click"   />
        <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false" collapsible="false" minimizable="false" style="width:385px;height:270px;" runat="server">
        </div>
        <a href="../../Template/cjTemplate.xls">模板下载</a>
    </div>

    <asp:HiddenField ID="hfDelIDS" runat="server" />
    </form>
</body>
</html>