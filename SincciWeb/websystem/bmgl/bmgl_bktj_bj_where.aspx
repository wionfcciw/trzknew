<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bmgl_bktj_bj_where.aspx.cs" Inherits="SincciKC.websystem.bmgl.bmgl_bktj_bj_where" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        function hideTest(Title, kaoci, where, xqdm,bmddm) {
            window.parent.addTab2(Title, '/websystem/bmgl/bmgl_bktj_Print_where.aspx?bmddm=' + bmddm + '&xqdm=' + xqdm + '&kaoci=' + kaoci + '&where=' + where + '&type=3&title=' + Title);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"  >
       <div id="Div1" class="datagrid-toolbar" style="text-align:center" >
          <span class="font20" >报考人数分析</span>

       
             </div>
    
     <div id="tb" class="datagrid-toolbar" >
        

         <table border="0" cellpadding="0" cellspacing="0">
             <tr>   <td>   考次:  <asp:DropDownList ID="drpKaoci" runat="server">
          </asp:DropDownList> 
       </td>
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
                     
                     <b>班级</b></td>
                    <td>
                        <b>总人数</b>
                    </td>
                    <td>
                        <b>报名人数</b>
                    </td>
                    <td>
                        <b>已确认人数</b>
                    </td>
                    <td>
                        <b>未确认人数</b>
                    </td>
                    
                  
                    <%-- <td><b>修改</b></td>--%>
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
                   <tr style="height:24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %> >
                      <td> 
                     <%#Eval("bjmc")%> </td>      
                    
                  <%--  <td><%# (page - 1) * pagesize + Container.ItemIndex + 1%></td>   --%>
                     <td>  
                         <asp:Label ID="lblksh" runat="server" Text='<%#Eval("zjallnum")%>'></asp:Label>  </td> 
                      
                     <td><%#Eval("zjbmrs")%> </td> 
                                          <td><%#Eval("zjqrrs")%></td> 
                  <%--  <td><%#Eval("xbdm").ToString()=="1"? "男":"女"%> </td>
                      <td><%#Eval("csrq")%> </td> --%>
                     
                      <td><%#Eval("zjwqrrs")%> </td> 
                  <%--  <td> <a href="#" onclick="return opdg( ,'修改数据');"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
                    </td>--%>
                    </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater> 

    <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px" >
      <asp:Button ID="btnExcel" runat="server" Text="导出excel" 
            onclick="btnExcel_Click" />
        <asp:Button ID="btnPrint" runat="server" Text="打印" onclick="btnPrint_Click" />
    </div> 
  
    </form>
</body>
</html>
