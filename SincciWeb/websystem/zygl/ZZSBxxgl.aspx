<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZZSBxxgl.aspx.cs" Inherits="SincciKC.websystem.zygl.ZZSBxxgl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学生志愿管理</title>
        <OBJECT id=GzzkWebPrint1 style="VISIBILITY: hidden" 
classid=clsid:6755B401-850C-4910-B60C-0A32155BC31C VIEWASTEXT>
<PARAM NAME="Visible" VALUE="-1"><PARAM NAME="AutoScroll" VALUE="0"><PARAM NAME="AutoSize" VALUE="0">
<PARAM NAME="AxBorderStyle" VALUE="1"><PARAM NAME="Caption" VALUE="铜仁市招生考试院"><PARAM NAME="Color" VALUE="2147483663">
<PARAM NAME="Font" VALUE="宋体"><PARAM NAME="KeyPreview" VALUE="0"><PARAM NAME="PixelsPerInch" VALUE="96">
<PARAM NAME="PrintScale" VALUE="1"><PARAM NAME="Scaled" VALUE="-1"><PARAM NAME="DropTarget" VALUE="0">
<PARAM NAME="HelpFile" VALUE=""><PARAM NAME="DoubleBuffered" VALUE="0"><PARAM NAME="Enabled" VALUE="-1">
<PARAM NAME="Cursor" VALUE="0"><PARAM NAME="HelpType" VALUE="0"><PARAM NAME="HelpKeyword" VALUE="">
</OBJECT> 
<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../css/page.css"  />
<script src="../../js/addTableListener.js" type="text/javascript"></script>
<script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

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
    function xqqr() {
        if (confirm('确定要确认考生信息吗？')) {
            return true;
        }
        else {
            return false;
        }
    }



    function opdg(ID, Title, type) {

        window.parent.addTab2(Title, '/websystem/zygl/ZZSBxxgl_Mange.aspx?ksh=' + ID + '&title=' + Title + '&type=' + type);
        //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }
    function opUp(ID, Title) {

        window.parent.addTab2(Title, '/websystem/zygl/tbzy.aspx?ksh=' + ID + '&title=' + Title);
        //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }
    function opdy(ID, Title) {

        //window.parent.addTab2(Title, '/websystem/bmgl/Printxx.aspx?ksh=' + ID + '&title=' + Title);
        //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
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
    <form id="form1" runat="server"  > 
   
         <table  width="100%" class="datagrid-toolbar" >
             <tr   >
                 <td  >
                     市(区)：
                 
                     <asp:DropDownList ID="dlistSq" runat="server" OnSelectedIndexChanged="dlistSq_SelectedIndexChanged"
                         AutoPostBack="True">
                     </asp:DropDownList>
                 
                     学校：
                
                     <asp:DropDownList ID="dlistXx" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlistXx_SelectedIndexChanged">
                     <asp:ListItem Value="">-请选择-</asp:ListItem>
                     </asp:DropDownList>
                 
                     班级:
                 
                     <asp:DropDownList ID="dlistBj" runat="server" AutoPostBack="True">
                     <asp:ListItem Value="">-请选择-</asp:ListItem>
                     </asp:DropDownList>
                 
                     <asp:DropDownList ID="dlistZt" runat="server">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="0">未填报</asp:ListItem>
                        <asp:ListItem Value="1">已填报未确认</asp:ListItem>
                         <asp:ListItem Value="2">考生已确认</asp:ListItem>
                        
                     
                      
                     </asp:DropDownList>
                 </td> 
             </tr>
             <tr ><td> 
                     报名号/姓名/身份证号：
                 
                     <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                     <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
                 </td></tr>
             </table>
            
             <div  class="datagrid-toolbar"  >
             <table>
                 <tr>
                   
                     <td>
                         <asp:Button ID="btnResetTag" runat="server" CssClass="icon-reload" Text="  状态重置"
                             OnClick="btnResetTag_Click" />
                     </td>
                     <td>
                         <asp:Button ID="btndaoAll" runat="server" CssClass="icon-reload" Text="  导出数据" 
                         
                             onclick="btndaoAll_Click"   />
                     </td>
                     
                   
                      <td><asp:Button ID="btnReset" runat="server" CssClass="icon-reload" Text="  密码重置" 
                         OnClientClick="return confirm('是否密码重置？', '确定', '取消')"  onclick="btnReset_Click" />
                 </td>
                     <td>
                         <asp:Button ID="btnyuans" runat="server" CssClass="icon-reload" Text="  重置回已填报状态"
                             OnClick="btnyuans_Click" />
                     </td>
                       <td>
                         <asp:Button ID="btnwtb" runat="server" CssClass="icon-reload" Text="  重置回未填报状态" onclick="btnwtb_Click"
                              />
                     </td>
                     
                 </tr>
         </table>
          
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
                        <b>报名号</b>
                    </td>
                    <td>
                        <b>姓名</b>
                    </td>
                    <td>
                        <b>学籍号</b>
                    </td>
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
                        <b>考生确认</b>
                    </td>
                   
                  
                    <td>
                        <b>详情</b>
                    </td>
                   
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
                   <tr style="height:24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %> >
                      <td> 
                      <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("ksh") %>'> </td>    
                     <td>  
                         <asp:Label ID="lblksh" runat="server" Text='<%#Eval("ksh")%>'></asp:Label>  </td> 
                      
                     <td><%#Eval("xm")%> </td> 
                     <td><%#Eval("xjh")%></td> 
                                           
                     
                      <td><%#Eval("sfzh")%> </td> 
                      <td><%#Eval("bmdmc")%> </td>
                      <td><%#Eval("kslbmc")%> </td>                     
                      <td>   <%#Eval("sbksqr").ToString() == "2" ? "已确认" : Eval("zyksqr").ToString() == "1" ? "已填报" : "未填报"%></td>
                      
                     
                    
                      <td>      <a href="#" onclick='return opdg(<%#Eval("ksh")%>,"学生自主申报信息详情",1);'>详情</a>
                      </td>                 
                     
                  <%--  <td> <a href="#" onclick="return opdg( ,'修改数据');"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
                    </td>--%>
                    </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater> 

    <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px" >
        <table width="100%">
            <tr> <td>
             <webdiyer:aspnetpager id="AspNetPager1" runat="server"   onpagechanged="AspNetPager1_PageChanged"  Width="100%"   >
               </webdiyer:aspnetpager></td>
               <td>每页：<asp:DropDownList ID="ddlPageSize" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="ddlPageSize_SelectedIndexChanged">                     
            </asp:DropDownList>条</td>
         </tr>
       </table> 
    </div> 
  
    </form>
</body>
</html>
