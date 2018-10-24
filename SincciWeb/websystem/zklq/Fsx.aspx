<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fsx.aspx.cs" Inherits="SincciKC.websystem.zklq.Fsx" %>

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
     
           <asp:Button ID="btntj" runat="server" Text=" 统 计 " onclick="btntj_Click"  CssClass="btnStyle"  />    </div>
         
     
            
    <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <div id="tb" class="datagrid-toolbar" style="text-align: center">
                <span class="font20">录取统计</span>
            </div> 
            <table class="tbColor" width="100%" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >               
                <tr class="datagrid-header" style="height: 26px">
                   
                     <td>
                        <b>招生学校</b>
                    </td>
                    <td>
                        <b>片区</b>
                    </td>
                    <td>
                        <b>类型</b>
                    </td>
                    <td>
                        <b>计划数</b>
                    </td>
                     <td>
                        <b>录取数</b>
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
             <tr  >
             
                 <td>
                       [<%#Eval("lqxx")%>]<%#Eval("zsxxmc")%>
                 </td>
                 <td>
                     <%#Eval("name")%>
                 </td>
                 <td>
                     <%#Eval("ts")%>
                 </td>
              
                 <td>
                     <%#Eval("jhs")%>
                 </td>
                   <td>
                     <%#Eval("num")%>
                 </td>
                   <td>
                     <%#Eval("qe")%>
                 </td>
                   <td>
                     <%#Eval("x")%>
                 </td>
                  <td>
                     <%#Eval("n")%>
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
