<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tj_xx.aspx.cs" Inherits="SincciKC.websystem.zklq.Tj_xx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报名人数统计</title>
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
        function opdg(ID, Title, kaoci) {

            window.parent.addTab2(Title, '/websystem/bmgl/bmgl_bktj_xx.aspx?kaoci='+kaoci+'&xqdm=' + ID + '&title=' + Title);
            return false;
        }
        function hideTest(Title,kaoci) {
            window.parent.addTab2(Title, '/websystem/bmgl/bmgl_bktj_Print.aspx?kaoci=' + kaoci + '&type=1&title=' + Title);
            return false;
        }
    </script>
      

 
   
    
</head>
<body>
    <form id="form1" runat="server"  >
     
     
      <div id="divAll" runat="server"  >
     <div id="tb" class="datagrid-toolbar" style="text-align:center" >
         <span class="font20" >招生学校录取统计</span>
             </div>
             <div>批次:
               <asp:DropDownList ID="ddlXpcInfo" runat="server"  AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlXpcInfo_SelectedIndexChanged">
                  </asp:DropDownList></div>
      <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table class="tbColor" id="tb_test" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >
                <tr   style="height: 26px">
                    <td  >
                     
                     <b>招生学校</b></td>
                    <td>
                        <b>专业</b>
                    </td>
                    <td>
                        <b>专业计划数</b>
                    </td>
                    <td>
                        <b>专业预录人数</b>
                    </td>
                   
                    <td>
                        <b>专业已录人数</b>
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
                   <tr style="height:24px"   >
                      <td> 
                     <%#Eval("xxdm")%><%#Eval("zsxxmc")%></td>      
                
              
                      
                     <td><%#Eval("zydm")%><%#Eval("zymc")%> </td> 
                      <td><%#Eval("jhs")%></td> 
                 
                     
                      <td><%#Eval("lqzynum")%> </td> 
                
                      <td><%#Eval("lqnum")%> </td>
                      <td><%#Eval("maxcj")%> </td>                     
                      <td>  <%#Eval("mincj")%> </td> 
                            
                     
                  <%--  <td> <a href="#" onclick="return opdg( ,'修改数据');"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
                    </td>--%>
                    </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater> 
    
    <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px" >
        <asp:Button ID="btnExcel" runat="server" Text="导出excel" 
            onclick="btnExcel_Click"   />
        <asp:Button ID="btnAll" runat="server" Text="导出" onclick="btnAll_Click" Visible="false" />
        <asp:Button ID="btnPrint" runat="server" Text="打印"   Visible="false" 
            onclick="btnPrint_Click"   />
    </div> 
  </div>
    </form>
</body>
</html>
