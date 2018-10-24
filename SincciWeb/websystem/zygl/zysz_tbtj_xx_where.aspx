<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zysz_tbtj_xx_where.aspx.cs" Inherits="SincciKC.websystem.zysz.zysz_tbtj_xx_where" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        function opdg(where,xqdm,ID, Title,tiaoj) {
           // alert(where + "__" + xqdm + "__" + ID);
            window.parent.addTab2(Title, '/websystem/zygl/zysz_tbtj_bj_where.aspx?tiaojian=' + tiaoj + '&bmddm=' + ID + '&where=' + where + '&xqdm=' + xqdm + '&title=' + Title);
          
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"  >
     
      <div id="Div1" class="datagrid-toolbar" style="text-align:center" >
          <span class="font20" >报考人数统计</span>

       
             </div>
     
             <div  class="datagrid-toolbar"  >
                  <table border="0" cellpadding="0" cellspacing="0">
             <tr>
                 <td>性别: 
                     <asp:DropDownList ID="listxb" runat="server">
                     </asp:DropDownList> 民族: <asp:DropDownList ID="listmz" runat="server">
                     </asp:DropDownList>   考生类别: <asp:DropDownList ID="listlb" runat="server">
                     </asp:DropDownList> 
                     <asp:Button ID="Button1" runat="server" Text="分析" onclick="Button1_Click" />
                 </td>
             </tr>
         </table>
          
    </div> 
      <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table class="tbColor" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >
                <tr class="datagrid-header" style="height: 26px">
                    <td>
                     
                     <b>学校</b></td>
                    <td>
                        <b>总人数</b>
                    </td>
                    <td>
                        <b>填报人数</b>
                    </td>
                    <td>
                        <b>已确认人数</b>
                    </td>
                    <td>
                        <b>未确认人数</b>
                    </td>
                     <%-- <td>
                        <b>指标生人数</b>
                    </td>
                    <td>
                        <b>非指标生人数</b>
                    </td>
                    <td>
                        <b>地理不合格人数</b>
                    </td>
                    <td>
                        <b>生物不合格人数</b>
                    </td>--%>
                    <td>
                        <b>详情</b>
                    </td>
                    <%-- <td><b>修改</b></td>--%>
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
                   <tr style="height:24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %> >
                      <td  style="width:200px; text-align:left" > 
                     <%#Eval("bmdmc")%> </td>      
                    
                  <%--  <td><%# (page - 1) * pagesize + Container.ItemIndex + 1%></td>   --%>
                     <td>  
                         <asp:Label ID="lblksh" runat="server" Text='<%#Eval("zjallnum")%>'></asp:Label>  </td> 
                      
                     <td><%#Eval("zjbmrs")%> </td> 
                                          <td><%#Eval("zjqrrs")%></td> 
                  <%--  <td><%#Eval("xbdm").ToString()=="1"? "男":"女"%> </td>
                      <td><%#Eval("csrq")%> </td> --%>
                     
                      <td><%#Eval("zjwqrrs")%> </td> 
                  <%--     <td><%#Eval("szbs")%> </td> 
                     <td><%#Eval("fzbs")%> </td> 
                     <td><%#Eval("Dldj")%> </td> 
                     <td><%#Eval("Swdj")%> </td> --%>
                      <td>  <a href="#" onclick='return opdg("<%=str %>","<%=xqdm %>","<%#Eval("bmddm")%>","班级志愿填报分析","<%=gotj %>");'><%#Eval("bmdmc").ToString() == "合计" ? "" : "详情"%></a>
                      </td>                 
                     
                  <%--  <td> <a href="#" onclick="return opdg( ,'修改数据');"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
                    </td>--%>
                    </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater> 

    <div id="tbpage" class="datagrid-toolbar" style="padding-left: 10px">
        <asp:Button ID="btnExport" runat="server" CssClass="btnexit" Text="导出数据" OnClick="btnExport_Click" />
         &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExportHKcj" runat="server" CssClass="btnexit"  Visible="false"
           Text="不合格名单" onclick="btnExportHKcj_Click" />
    </div>

    </form>
</body>
</html>
