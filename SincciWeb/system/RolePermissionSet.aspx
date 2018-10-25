<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RolePermissionSet.aspx.cs" Inherits="SincciWeb.system.RolePermissionSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置角色应用权限</title>

<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
 
 
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
<style type="text/css" >
 html 
{
	overflow-x: hidden;   /*- 横滚动条 -*/
	overflow-y: auto;      /*- 竖滚动条 -*/
}
</style>

</head>
<body>
    <form id="form1" runat="server">
    <div> 
    <table width="99%" border="1" align="center" style="border-collapse:collapse; margin-top:5px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
  <tr>
    <td align="right">选择模块：</td>
    <td align="left"><asp:DropDownList ID="ddlModule" runat="server"  
            AutoPostBack="true" onselectedindexchanged="ddlModule_SelectedIndexChanged" >  </asp:DropDownList>

        <asp:Label ID="lblRoleID" runat="server" Text="0"  Visible="false"></asp:Label>
        

    </td>
  </tr>
   <tr>
    <td align="right">角色名称 </td>
    <td align="left"><asp:Label ID="lblRoleName" runat="server"  ></asp:Label></td>
  </tr>
  <tr>
    <td colspan="2" >
           
          <table width="98%"   border="1" align="center" style="border-collapse:collapse; margin-top:5px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
               <tr bgcolor="#F1F1F1">
                <td align="center"><b>应用名称</b></td>
                <td align="center"><b>权限名称</b></td>
              </tr>
               <asp:Repeater ID="Application_Sub" Runat="server" OnItemDataBound="Application_Sub_ItemDataBound"  >
               <ItemTemplate>
                    <tr >
                    <td align="center"><%# Eval("A_appname")%></td>
                    <td align="left">
                        <asp:DataList  CellPadding="3" CellSpacing="3" ID="PermissionList" RepeatDirection="Horizontal" runat="server" RepeatColumns="11">
                            <ItemTemplate>
                                 <%#Eval("DispTxt")%>
                            </ItemTemplate>
                        </asp:DataList></td>
                    </tr>
              </ItemTemplate>
              </asp:Repeater>
          </table> 
        
    </td>
  </tr>  
 
  <tr>
    <td colspan="2" align="center" style="background-color:#ccc">
    
        <asp:Button ID="btnSave" runat="server"  CssClass="btnStyle" Text="  保存 " onclick="btnSave_Click" /> </td>
  </tr>
  
</table>
 
    </div>
    </form>
</body>
</html>
