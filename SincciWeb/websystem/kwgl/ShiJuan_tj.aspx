<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShiJuan_tj.aspx.cs" Inherits="SincciKC.websystem.kwgl.ShiJuan_tj" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试卷需求统计</title>
   <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../css/page.css"  />

<style type="text/css">
.btn
{
	 cursor: hand; 
 }
  
</style>
    <script type="text/javascript">
        function opdg(ID, Title) {

            window.parent.addTab2(Title, '/websystem/kwgl/ShiJuan_tj_kd.aspx?xqdm=' + ID + '&title=' + Title);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"  > 
    <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <div id="tb" class="datagrid-toolbar" style="text-align: center">
                <span class="font20">试卷需求统计</span>
            </div> 
            <table class="tbColor" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >               
                

                <tr  class="datagrid-header" style="height: 26px">
                    <td style=" width:10%">
                     
                     <b>县区</b></td>
                    <td>
                        <b>考生数</b>
                    </td>
                    <td>
                        <b>考场数</b>
                    </td>
                    <td>
                        <b>试卷数</b>
                    </td> 
                    <td>
                        <b>备用试卷数</b>
                    </td>
                    <td>
                        <b>试卷合计</b>
                    </td>
                      
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
             <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                 <td>
                    <a href="#" onclick="return opdg('<%#Eval("xqdm")%>','考点试卷需求统计');">
                        [<%#Eval("xqdm")%>]<%#Eval("xqmc")%></a>
                 </td>
                 
                 <td>
                     <%#Eval("ksnum")%> 
                 </td>
                 <td>
                     <%#Eval("kcdm")%>
                 </td>
                 <td>
                     <%#Eval("kcdm")%>
                 </td>
                 
                 <td>
                     <%# int.Parse(Eval("kdnum").ToString()) + 2%>
                 </td>
                 <td>
                     <%# int.Parse(Eval("kdnum").ToString()) + int.Parse(Eval("kcdm").ToString()) + 2%>
                 </td>
                 
             </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater>

    <div id="tbpage" class="datagrid-toolbar" style="padding-left: 10px">
      <asp:Button ID="btnExport" runat="server" CssClass="btnexit" Text="导出数据" OnClick="btnExport_Click" />
    </div>

    </form>
</body>
</html>