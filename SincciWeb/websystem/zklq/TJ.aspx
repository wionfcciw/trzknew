<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TJ.aspx.cs" Inherits="SincciKC.websystem.zklq.TJ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
              学校:  <asp:DropDownList ID="dllxx" runat="server"    >
                              <asp:ListItem  Value="">-请选择-</asp:ListItem>
                                </asp:DropDownList> 

             类型:
               <asp:DropDownList ID="ddllx" runat="server"    >
                              <asp:ListItem  Value="">-请选择-</asp:ListItem>
                               <asp:ListItem  Value="1">统招生</asp:ListItem>
                                 <asp:ListItem  Value="2">配额生</asp:ListItem>
                                   <asp:ListItem  Value="3">配转统</asp:ListItem>
                                   
                                </asp:DropDownList>
           <asp:Button ID="btntj" runat="server" Text=" 统 计 " onclick="btntj_Click"  CssClass="btnStyle" />    </div>
         
     
            
    <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <div id="tb" class="datagrid-toolbar" style="text-align: center">
                <span class="font20">录取统计</span>
            </div> 
            <table class="tbColor" width="100%" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >               
               

                <tr class="datagrid-header" style="height: 26px">
                   
                     <td>
                        <b>毕业学校</b>
                    </td>
                    <td>
                        <b>计划总数</b>
                    </td>
                    <td>
                        <b>已录取数</b>
                    </td>
                    <td>
                        <b>余缺数</b>
                    </td>
                     <td>
                        <b>最高分</b>
                    </td>
                      <td>
                        <b>最低分</b>
                    </td>
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
             <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
             
                 <td>
                       [<%#Eval("xxdm")%>]<%#Eval("xxmc")%>
                 </td>
                 <td>
                     <%#Eval("allnum")%>
                 </td>
                 <td>
                     <%#Eval("lqnum")%>
                 </td>
                 <%--  <td><%#Eval("xbdm").ToString()=="1"? "男":"女"%> </td>
                      <td><%#Eval("csrq")%> </td> --%>
                 <td>
                     <%#Eval("yqnum")%>
                 </td>
                  <td>
                     <%#Eval("lqmax")%>
                 </td>
                  <td>
                     <%#Eval("lqmin")%>
                 </td>
             </tr>
         </ItemTemplate>



         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater>

    <div id="tbpage" class="datagrid-toolbar" style="padding-left: 10px">
      <asp:Button ID="btnExport" runat="server" CssClass="btnexit" Text="导出报表" OnClick="btnExport_Click"  /> &nbsp;&nbsp;
      
    </div>

    </form>
</body>
</html>
