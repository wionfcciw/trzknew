<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zsxxzysel.aspx.cs" Inherits="SincciKC.websystem.zygl.Zsxxzysel" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../css/page.css"  />
</head>
<body>
    <form id="form1" runat="server"  > 

         <div id="Div1" class="datagrid-toolbar" style="padding-left: 10px">
         当前批次:  <asp:DropDownList ID="ddlXpcInfo" runat="server"  AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlXpcInfo_SelectedIndexChanged">
                                </asp:DropDownList> 
              招生学校:  <asp:DropDownList ID="dllxx" runat="server"    >
                              <asp:ListItem  Value="">-请选择-</asp:ListItem>
                                </asp:DropDownList> 
               
           <asp:Button ID="btntj" runat="server" Text=" 查 询 " onclick="btntj_Click"  CssClass="btnStyle" />    </div>
           <div runat="server" id="bmdinfo" class="datagrid-toolbar" >
                <asp:CheckBoxList ID="chkxqdm" runat="server" RepeatDirection="Horizontal">
        
         </asp:CheckBoxList>
           </div>
    
            
     <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table class="tbColor" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >
                <tr class="datagrid-header" style="height: 26px">
                      <td>
                        <b>序号</b>
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
                        <b>综合等级</b>
                    </td>
                       <td>
                        <b>加分</b>
                    </td>
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
             <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                <td> 
                    <%# Container.ItemIndex + 1 + (AspNetPager1.CurrentPageIndex - 1) * Convert.ToInt32(this.ddlPageSize.SelectedValue)%>
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
