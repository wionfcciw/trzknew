<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zdybz_Manage.aspx.cs" Inherits="SincciKC.websystem.bmsz.Zdybz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自定义备注字段</title>
<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../css/page.css"  />
<script src="../../js/addTableListener.js" type="text/javascript"></script>
<script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
<script src="../../js/Jquery183.js" type="text/javascript"></script>
<script src="../../easyui/jquery.easyui.min.js" type="text/javascript"></script>
     <script type="text/javascript">
         function opdg(ID,name, Title) {
             ymPrompt.win({ message: 'Zdybz_AddEdit.aspx?bzmc=' + name + '&xqdm=' + ID, width: 350, height: 260, title: Title, iframe: true, fixPosition: true, dragOut: false })
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
       <div class="datagrid-toolbar" style="height:0px"></div>
           
      <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table class="tbColor" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >
                <tr class="datagrid-header" style="height: 26px">
                    <td>
                        <b>选择
                            </b>
                    </td>
                     <td>
                        <b>县区</b>
                    </td>
                    <td>
                        <b>备注名称</b>
                    </td>
                  
                    <%-- <td><b>修改</b></td>--%>
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
             <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                 <td>
                     <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("xqmc") %>|<%#Eval("bzmc")%> '>
                 </td>
                 <%--  <td><%# (page - 1) * pagesize + Container.ItemIndex + 1%></td>   --%>
                  <td>
                      <%#Eval("xqmc")%> 
                 </td>
                 <td>
                      <%#Eval("bzmc")%> 
                 </td>
                
                 <%--  <td> <a href="#" onclick="return opdg( ,'修改数据');"><image src="../easyui/themes/icons/pencil.png" alt="修改" border="0"></image></a>  
                    </td>--%>
             </tr>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater>
    <div class="datagrid-toolbar">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnEdit" runat="server" CssClass="icon-edit" Text="  修改" OnClick="btnEdit_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
