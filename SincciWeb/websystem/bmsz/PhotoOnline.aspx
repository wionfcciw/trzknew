<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoOnline.aspx.cs" Inherits="SincciKC.websystem.bmsz.PhotoOnline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在线照相</title>
<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
    
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
   <script  language="javascript" type="text/javascript">  
function openwin(ksh,xqdm,bmddm,pic)
{

    window.ksh2name.location.href = "Photograph.aspx?ksh=" + ksh + "&xqdm=" + xqdm + "&bmddm=" + bmddm + "&pic=" + pic + "";
}
</script>
</head>
<body >
    <form id="form1" runat="server">
      
       
 <div id="tb" class="datagrid-toolbar">
     &nbsp; 区县名称：<asp:DropDownList ID="dlistSq" runat="server" OnSelectedIndexChanged="dlistSq_SelectedIndexChanged"
         AutoPostBack="True">
     </asp:DropDownList>
     &nbsp;学校：
     <asp:DropDownList ID="dlistXx" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlistXx_SelectedIndexChanged">
         <asp:ListItem Value="">-请选择-</asp:ListItem>
     </asp:DropDownList>
     班级:<asp:DropDownList ID="dlistBj" runat="server">
         <asp:ListItem Value="">-请选择-</asp:ListItem>
     </asp:DropDownList> 报名号/姓名：
     <asp:TextBox ID="txtksh" runat="server"  CssClass="searchbox"></asp:TextBox>
     <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
    </div>

    <table width="100%" height="450px" border="0"  cellpadding="0" cellspacing="0">
  <tr>
    <td valign="top" width="300">
    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse; width:300px"
        cellpadding="2" cellspacing="0">
        <asp:Repeater ID="Repeater1" runat="server" >
            <HeaderTemplate>
                <tr class="datagrid-header" style="height: 26px">
                    
                    <td width="128">
                        <b>准考证号</b>
                    </td>
                   
                    <td width="86">
                        <b>姓名</b>
                    </td>
                    <td>
                        <b>是否照相</b>
                    </td>
                </tr>
                <tr><td colspan="3">
                    
                  <div style="OVERFLOW-Y: auto; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 437px">
                <table cellpadding="2" width="100%" cellspacing="0"  style="border-collapse: collapse;">
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                    <td width="130">
                     <span style='cursor:hand;' onclick="openwin('<%# Eval("ksh") %>','<%# Eval("bmdxqdm") %>','<%# Eval("bmddm") %>','<%# Eval("pic") %>')" ><font color='#0000FF'>
                      <%# Eval("ksh") %></font></span> 
                    </td>
                    <td width="90">
                        <%# Eval("xm") %>
                    </td>
                    <td>
                        <%# Eval("pic").ToString()=="1"?"是":"否" %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></table> </div></td></tr></FooterTemplate>
        </asp:Repeater>
    </table>
    
    </td>
    <TD valign="top" align="left">
       <iframe src="Photograph.aspx" name="ksh2name" id="ksh2name" width="570" height="470" marginwidth="0" marginheight="0" frameborder="0" ></iframe>
    </TD>
    </tr>
    <tr><td colspan="2">
      <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px; " >
       <table width="100%">
            <tr> <td>
             <webdiyer:aspnetpager id="AspNetPager1" runat="server"  Width="100%"  onpagechanged="AspNetPager1_PageChanged" >
               </webdiyer:aspnetpager></td>
               <td>每页：<asp:DropDownList ID="ddlPageSize" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="ddlPageSize_SelectedIndexChanged">                     
            </asp:DropDownList>条</td>
         </tr>
       </table> 
      </div>
   </td>
   
  </tr>
  
</table>
    
 
 

    </form>
</body>
</html>
