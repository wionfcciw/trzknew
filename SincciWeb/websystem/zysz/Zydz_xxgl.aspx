<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zydz_xxgl.aspx.cs" Inherits="SincciKC.websystem.zysz.Zydz_xxgl" %>

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



    function opdg(ID, Title) {
        window.parent.addTab2(Title, '/websystem/zysz/Zydz_xxglDetails.aspx?ksh=110&title=' + Title);
        //window.parent.addTab2(Title, '/websystem/bmgl/Zydz_xxglDetails.aspx?ksh=' + ID + '&title=' + Title);
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
     
    
     <div id="tb" class="datagrid-toolbar"  >
         <table>
             <tr>
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
                     <asp:ListItem Value="">-请选择-</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                 <td>
                     班级:
                 </td>
                 <td>
                     <asp:DropDownList ID="dlistBj" runat="server" AutoPostBack="True">
                     <asp:ListItem Value="">-请选择-</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                 <td>
                     <asp:DropDownList ID="dlistZt" runat="server">
                         <asp:ListItem Value="99">全部</asp:ListItem>
                         <asp:ListItem Value="0">未报名</asp:ListItem>
                         <asp:ListItem Value="1">已报名</asp:ListItem>
                         <asp:ListItem Value="2">已确认</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                 <td>
                     准考证号/姓名/身份证号：
                 </td>
                 <td>
                     <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                     <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
                 </td>
                 
             </tr>
             </table>
             </div>
             <div  class="datagrid-toolbar"  >
             <table>
             <tr>
                 <td>
                     <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
                         OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
                 </td>
                 <td>
                     <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn"  />
                     <asp:Button ID="btndayin" runat="server" CssClass="icon-print" Visible="false" Text="  打印确认" OnClick="btndayin_Click" />
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
                        <b>学校确认</b>
                    </td>
                    <td>
                        <b>县区确认</b>
                    </td>
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
                   <tr style="height:24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %> >
                      <td> 
                      <asp:CheckBox ID="CheckBox1" runat="server" /> </td>      
                    
                  <%--  <td><%# (page - 1) * pagesize + Container.ItemIndex + 1%></td>   --%>
                     <td>  
                         <asp:Label ID="lblksh" runat="server" Text='<%#Eval("ksh")%>'></asp:Label>  </td> 
                      
                     <td><%#Eval("xm")%> </td> 
                                          <td><%#Eval("xjh")%></td> 
                  <%--  <td><%#Eval("xbdm").ToString()=="1"? "男":"女"%> </td>
                      <td><%#Eval("csrq")%> </td> --%>
                     
                      <td><%#Eval("sfzh")%> </td> 
                      <td><%#Eval("bmdmc")%> </td>
                      <td><%#Eval("kslbmc")%> </td>                     
                      <td> <%#Eval("zyksqr").ToString() == "1" ? "已确认" : "未确认"%></td>
                      <%--<td> <%#Eval("ksqr").ToString() == "2" ? "已确认" : Eval("ksqr").ToString() == "1" ? "已报名" : "未报名"%></td> --%>
                      <td><%#Eval("zyxxqr").ToString() == "1" ? "已确认" : "未确认"%></td> 
                      <td><%#Eval("zyxqqr").ToString() == "1" ? "已确认" : "未确认"%> </td> 
                      <td><%#Eval("zyxxdy").ToString() == "1" ? "已打印" : "未打印"%></td> 
                      <td><a href="#" onclick='return opdg(<%#Eval("ksh")%>,"学生志愿填报信息详情");'>详情</a>
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
