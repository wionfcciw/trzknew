<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zyxxgl.aspx.cs" Inherits="SincciKC.websystem.zygl.Zyxxgl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学生志愿管理</title>
   <%-- <object id="GzzkWebPrint1" style="visibility: hidden" classid="clsid:6755B401-850C-4910-B60C-0A32155BC31C"
       >
        <param name="Visible" value="-1">
        <param name="AutoScroll" value="0">
        <param name="AutoSize" value="0">
        <param name="AxBorderStyle" value="1">
        <param name="Caption" value="铜仁市招生考试院">
        <param name="Color" value="2147483663">
        <param name="Font" value="宋体">
        <param name="KeyPreview" value="0">
        <param name="PixelsPerInch" value="96">
        <param name="PrintScale" value="1">
        <param name="Scaled" value="-1">
        <param name="DropTarget" value="0">
        <param name="HelpFile" value="">
        <param name="DoubleBuffered" value="0">
        <param name="Enabled" value="-1">
        <param name="Cursor" value="0">
        <param name="HelpType" value="0">
        <param name="HelpKeyword" value="">
    </object>--%>
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
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
            var pcdm = "<%=pcdmstr%>";
            window.parent.addTab2(Title, '/websystem/zygl/Zyxxgl_Mange.aspx?ksh=' + ID + '&title=' + Title + '&type=' + type + '&pcdm=' + pcdm);
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
    <form id="form1" runat="server">
    <table width="100%" class="datagrid-toolbar">
        <tr>
            <td>
                市(区)：
                <asp:DropDownList ID="dlistSq" runat="server" OnSelectedIndexChanged="dlistSq_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
                学校：
                <asp:DropDownList ID="dlistXx" runat="server"  OnSelectedIndexChanged="dlistXx_SelectedIndexChanged">
                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                </asp:DropDownList>
                 
                <asp:DropDownList ID="dlistBj" runat="server"  Visible="false"   >
                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                </asp:DropDownList>
                状态:
                <asp:DropDownList ID="dlistZt" runat="server">
                    <asp:ListItem Value="">全部</asp:ListItem>
                    <asp:ListItem Value="0">未填报</asp:ListItem>
                    <asp:ListItem Value="1">已填报</asp:ListItem>
                    <asp:ListItem Value="2">已录取</asp:ListItem>
                  <%--  <asp:ListItem Value="3">考生未确认</asp:ListItem>
                    <asp:ListItem Value="9">考生已确认未打印</asp:ListItem>
                    <asp:ListItem Value="4">学校未打印</asp:ListItem>
                    <asp:ListItem Value="5">学校已打印</asp:ListItem>
                    <asp:ListItem Value="7">学校已确认</asp:ListItem>
                    <asp:ListItem Value="8">学校未确认</asp:ListItem>
                    <asp:ListItem Value="6">县区未确认</asp:ListItem>--%>
                </asp:DropDownList>
         
                                 志愿批次:
                          
                                <asp:DropDownList ID="ddlXpcInfo" runat="server" Width="280px"   >
                                </asp:DropDownList>
                            </td>
        </tr>
        <tr>
            <td>
                报名号/准考证号/姓名：
                <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <div class="datagrid-toolbar">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btndayin" runat="server" CssClass="icon-print" Text="  打印" OnClick="btndayin_Click" Visible="false" />
                </td>
                <td>
                    <asp:Button ID="btnResetTag" runat="server" CssClass="icon-reload" Text="  状态重置"  Visible="false" 
                        OnClick="btnResetTag_Click" />
                </td>
                <td>
                    <asp:Button ID="btndaoAll" runat="server" CssClass="icon-reload" Text="  导出数据" OnClick="btndaoAll_Click"  Visible="false"  />
                </td>
                <td>
                    <asp:Button ID="btnxxqr" runat="server" CssClass="icon-reload" Text="  学校确认" OnClientClick="return confirm('确定要确认考生信息吗？', '确定', '取消')"
                        OnClick="btnxxqr_Click"  Visible="false" />
                </td>
                <td>
                    <asp:Button ID="btnXqQr" runat="server" CssClass="icon-reload" Text="  县区确认" OnClick="btnXqQr_Click"
                        OnClientClick="return xqqr();"  Visible="false" />
                </td>
                <td>
                    <asp:Button ID="btnReset" runat="server" CssClass="icon-reload" Text="  密码重置" OnClientClick="return confirm('是否密码重置？', '确定', '取消')"
                        OnClick="btnReset_Click" />
                </td>
                <td>
                    <asp:Button ID="btnyuans" runat="server" CssClass="icon-reload" Text="  重置回已填报状态"
                        OnClick="btnyuans_Click"  Visible="false" />
                </td>
                <td>
                    <asp:Button ID="btnwtb" runat="server" CssClass="icon-reload" Text="  重置回未填报状态" OnClick="btnwtb_Click"  Visible="false" />
                </td>
                <td>
                    <asp:Button ID="btnZyUp" runat="server" CssClass="icon-reload" Text="  志愿修改" OnClick="btnZyUp_Click"  Visible="false" />
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
                        <b>班级</b>
                    </td>
                      <td>
                        <b>民族</b>
                    </td>
                    <td>
                        <b>考生类别</b>
                    </td>
                    <td>
                        <b>报考类别</b>
                    </td>
                     <td>
                        <b>精准扶贫</b>
                    </td>
                       <td>
                        <b>三学籍</b>
                    </td>
                     <td>
                        <b>联系电话</b>
                    </td>
                     <td>
                        <b>成绩</b>
                    </td>
                     <td>
                        <b>体育</b>
                    </td>
                     <td>
                        <b>文综</b>
                    </td>
                     <td>
                        <b>地生</b>
                    </td>
                     <td>
                        <b>综合</b>
                    </td>
                     <td>
                        <b>加分</b>
                    </td>
                    <td>
                        <b>考生状态</b>
                    </td>
                    <td>
                        <b>录取学校</b>
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
                     <%#Eval("bjdm")%>
                 </td>
                 <td>
                     <%#Eval("mzdm")%>
                 </td>
                 <td>
                     <%#Eval("kslbmc")%>
                 </td>
                  <td>
                     <%#Eval("bklb")%>
                 </td>
                
                   <td>
                  <%# Eval("jzfp").ToString()=="1"?"是": "否"%>
                    </td>
                    <td>
                  <%# Eval("xjtype").ToString()=="1"?"否": "是"%>
                    </td>
                   <td>
                    <%#Eval("lxdh")%>
                    </td>
                   <td>
                    <%#Eval("zzf")%>
                    </td>
                   <td>
                    <%#Eval("ty")%>
                    </td>
                   <td>
                    <%#Eval("wkzh")%>
                    </td>
                   <td>
                    <%#Eval("dsdj")%>
                    </td>
                   <td>
                    <%#Eval("zhdj")%>
                    </td>
                   <td>
                    <%#Eval("jf")%>
                    </td>
                 <td>
                     <%# Eval("td_zt").ToString()=="5"?"已录取": (Eval("xxdm").ToString() != "" ? "已填报" : "未填报")%>
                    
                 </td>
                     <td>
                     <%# Eval("td_zt").ToString()=="5"?Eval("lqxx").ToString()+Eval("zsxxmc").ToString():""%>
                    
                 </td>
                 <%--<td> <%#Eval("ksqr").ToString() == "2" ? "已确认" : Eval("ksqr").ToString() == "1" ? "已报名" : "未报名"%></td> --%>
              <%--   <td>
                     <%#Eval("xxqr").ToString() == "1" ? "已确认" :"未确认"%>
                 </td>--%>
                <%-- <td>
                     <%#Eval("xqqr").ToString() == "1" ? "已确认" :"未确认"%>
                 </td>--%>
               
                 <td>
                     <a href="#" onclick='return opdg(<%#Eval("ksh")%>,"学生志愿填报信息详情",1);'>详情</a>
                 </td>
                 <%--  <td> <a href="#" onclick="return opdg( ,'修改数据');"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
                    </td>--%>
             </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater> 
    <div id="tbpage" class="datagrid-toolbar" style="padding-left: 10px">
        <table width="100%">
            <tr>
                <td>
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        Width="100%">
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
    </form>
</body>
</html>
