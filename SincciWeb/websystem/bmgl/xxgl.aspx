<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xxgl.aspx.cs" Inherits="SincciKC.websystem.bmgl.xxgl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试信息管理</title>
<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../css/page.css"  />
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
        window.parent.addTab2(Title, '/websystem/bmgl/xxglMange.aspx?ksh=' + ID + '&title=' + Title);
      //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }
    function opUp(ID, Title) {
      
        window.parent.addTab2(Title, '/websystem/bmgl/xxgl_Updete.aspx?ksh=' + ID + '&title=' + Title);
        return false;
    }
    function opExport(ID, Title) {
        if (confirm('是否导出数据？', '确定', '取消')) {
            window.parent.addTab2(Title, '/websystem/bmgl/xxgl_ExportDBF.aspx');
           
        }
      
        //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
      
    }
    function opdy(ID, Title) {
        window.parent.addTab2(Title, '/websystem/bmgl/Printxx.aspx?ksh=' + ID + '&title=' + Title);
      
        return false;
    }
</script>
 <script type="text/javascript">
     function addMask2() {
         if (confirm('确定要检查关联服务器上的相片吗？')) {
             ymPrompt.alert({ message: '正在检查相片，请稍候...<br /><img src="/images/busy.gif"/> ', title: '提示' });
         }
         else {
             return false;
         }
     } </script>
<style type="text/css">
.btn
{
	 cursor: hand; 
 }
  
</style>

</head>
<body>
    <form id="form1" runat="server"  >
     
    
 
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
                     <td> 考生类型:
                     <asp:DropDownList ID="dlistkslx" runat="server">
                         <asp:ListItem Value="">全部</asp:ListItem>
                         <asp:ListItem Value="1">应届生</asp:ListItem>
                         <asp:ListItem Value="2">往届生</asp:ListItem>
                       
                     </asp:DropDownList>
                 </td>
                 <td>状态:
                     <asp:DropDownList ID="dlistZt" runat="server">
                         <asp:ListItem Value="">全部</asp:ListItem>
                         <asp:ListItem Value="0">未报名</asp:ListItem>
                         <asp:ListItem Value="1">已报名未确认</asp:ListItem>
                         <asp:ListItem Value="2">已确认</asp:ListItem>
                         <asp:ListItem Value="3">已打印</asp:ListItem>
                         <asp:ListItem Value="4">未打印</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                
                
             
                 
             </tr>
             </table>
          
              <div id="Div1" class="datagrid-toolbar"   >
              
                
                     报名号/姓名/身份证号：
                
                     <asp:TextBox ID="txtName" CssClass="searchbox" runat="server" Width="150px"></asp:TextBox>
                     <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
           
              </div>
             <div  class="datagrid-toolbar"  style="height:45px" >
            <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn" OnClientClick="return opdg2('','新增考生信息')"  /></td>
             
             <asp:Button ID="btnEdit" runat="server" CssClass="icon-edit" Text="  修改" 
                     onclick="btnEdit_Click"  /> 
                 
                     <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
                         OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
               
                    
                     <asp:Button ID="btndayin" runat="server" CssClass="icon-print" Text="  打印" OnClick="btndayin_Click" />
               <asp:Button ID="btnReset" runat="server" CssClass="icon-reload" Text="  密码重置" 
                         OnClientClick="return confirm('是否密码重置？', '确定', '取消')"  onclick="btnReset_Click" />
                <asp:Button ID="btnResetTag" runat="server" CssClass="icon-reload" Text="  状态重置" OnClientClick="return confirm('是否重置您所选择考生的状态？', '确定', '取消')" 
                         onclick="btnResetTag_Click"   />
                 <asp:Button ID="btndaoAll" runat="server" CssClass="icon-reload" Text="  导出数据"  
                              OnClientClick="opExport('', '导出数据') ;"  
                         />
              <asp:Button ID="butDaochu" runat="server" CssClass="icon-reload" 
                            Text="  导出数据(报名号)"   OnClientClick="return opdg4('','导出考生信息')"  
                         />
                 <br/>
                   <%-- <td><asp:Button ID="btnAllQr" runat="server" CssClass="icon-reload" Text="  全部确认" 
                            OnClientClick="return confirm('是否全部确认？', '确定', '取消')" 
                            onclick="btnAllQr_Click"  />
                 </td>--%>
                    <asp:Button ID="btnQr" runat="server" CssClass="icon-reload" Text="  确认" 
                             OnClientClick="return confirm('是否确认您所选择考生的状态？', '确定', '取消')" 
                             onclick="btnQr_Click"   />
                 
                    <asp:Button ID="btnchuS" runat="server" CssClass="icon-reload" Text="  重置回初始状态" 
                             OnClientClick="return confirm('是否重置回初始状态？', '确定', '取消')" onclick="btnchuS_Click"  
                                 />
                   <asp:Button ID="btnqiangUp" runat="server" CssClass="icon-reload" Text="  强制修改" onclick="btnqiangUp_Click" 
                             
                                 />
                   <asp:Button ID="btnQxqr" runat="server" CssClass="icon-reload" Text="  取消确认" 
                             OnClientClick="return confirm('是否取消确认？', '确定', '取消')" onclick="btnQxqr_Click"  Visible="false"
                                 />
                <asp:Button ID="btnCheckPic" runat="server"  CssClass="icon-ok" Text="  检查相片" 
                           OnClientClick="return addMask2();" 
                         onclick="btnCheckPic_Click"    />
                
          
    </div> 
  
      <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table class="tbColor" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >
                <tr class="datagrid-header" style="height: 26px">
                    <td>
                        <b>全选
                           <input type="checkbox" name="checkboxid" id="checkboxid"  onclick="SelectAll(this)"  /></b>
                    </td>
                     <td>
                        <b>考次</b>
                    </td>
                    <td>
                        <b>报名号</b>
                    </td>
                    <td>
                        <b>准考证号</b>
                    </td>
                    <td>
                        <b>姓名</b>
                    </td>
                 <%--   <td>
                        <b>学籍号</b>
                    </td>--%>
                    <td>
                        <b>身份证号</b>
                    </td>
                    <td>
                        <b>毕业中学</b>
                    </td>
                    <td>
                        <b>考生类别</b>
                    </td>
                    <td>
                        <b>报考类别</b>
                    </td>
                    <td>
                        <b>考生确认</b>
                    </td>
                    <td>
                        <b>相片</b>
                    </td>
                   <%-- <td>
                        <b>学校确认</b>
                    </td>--%>
                   <%-- <td>
                        <b>县区确认</b>
                    </td>--%>
                    <td>
                        <b>打印</b>
                    </td>
                    <td>
                        <b>详情</b>
                    </td>
                    <%-- <td><b>修改</b></td>--%>
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
             <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                 <td>
                     <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("ksh") %>'>
                 </td>
                 <%--  <td><%# (page - 1) * pagesize + Container.ItemIndex + 1%></td>   --%>
                  <td>
                      <%#Eval("kaocimc")%> 
                 </td>
                 <td>
                      <%#Eval("ksh")%> 
                 </td>
                  <td>
                      <%#Eval("zkzh")%> 
                 </td>
                 <td>
                     <%#Eval("xm")%>
                 </td>
                 <%--<td>
                     <%#Eval("xjh")%>
                 </td>--%>
                 <%--  <td><%#Eval("xbdm").ToString()=="1"? "男":"女"%> </td>
                      <td><%#Eval("csrq")%> </td> --%>
                 <td>
                     <%#Eval("sfzh")%>
                 </td>
                 <td>
                     <%#Eval("bmdmc")%>
                 </td>
                 <td>
                     <%#Eval("kslbmc")%>
                 </td>
                  <td>
                     <%#Eval("bklb")%>
                 </td>
                 <td>
                     <%#Eval("ksqr").ToString() == "2" ? "已确认" : Eval("ksqr").ToString() == "1" ? "已报名" : "未报名"%>
                 </td>
                 <td>
                     <%#Eval("pic").ToString() == "1" ? "已照相" : "未照相"%>
                 </td>
                 <%--<td> <%#Eval("ksqr").ToString() == "2" ? "已确认" : Eval("ksqr").ToString() == "1" ? "已报名" : "未报名"%></td> --%>
              <%--   <td>
                     <%#Eval("xxqr").ToString() == "1" ? "已确认" :"未确认"%>
                 </td>--%>
                <%-- <td>
                     <%#Eval("xqqr").ToString() == "1" ? "已确认" :"未确认"%>
                 </td>--%>
                 <td>
                     <%#Eval("xxdy").ToString() == "1" ? "已打印" :"未打印"%>
                 </td>
                 <td>
                     <a href="#" onclick='return opdg(<%#Eval("ksh")%>,"考生信息详情");'>详情</a>
                 </td>
                 <%--  <td> <a href="#" onclick="return opdg( ,'修改数据');"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
                    </td>--%>
             </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater> 

  

    <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px" >
        <table width="100%">
            <tr><td>
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
         <div i runat="server" id="divdaor" class="datagrid-toolbar" style="padding-left:10px" >
         <table  >
             <tr>
                   <td> 
                       <asp:FileUpload ID="fuExcelFileImport" runat="server" CssClass="searchbox"/>
                       <asp:Button ID="btnImport" runat="server" CssClass="icon-reload" Text="  导入数据"   OnClientClick="return addMask();" 
                           onclick="btnImport_Click"   /><asp:Button CssClass="icon-reload" ID="btnrizhi" runat="server" 
                           Text="  下载日志" onclick="btnrizhi_Click" /> <a href="/Template/ksTemplate.xls" >模板下载1</a>
                           <a href="/Template/ksKshTemplate.xls" >模板下载2(含报名号)</a>
                  </td>
             </tr>
         </table>
     </div>  
   <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false" collapsible="false" minimizable="false" style="width:435px;height:270px;" runat="server">
     </div>
    </form>
</body>
</html>
