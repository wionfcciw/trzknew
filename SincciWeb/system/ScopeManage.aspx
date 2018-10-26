<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScopeManage.aspx.cs" Inherits="SincciKC.system.ScopeManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>范围管理</title>
<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
 
 
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />


</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table id="table1" cellspacing="0" cellpadding="4" width="100%" style="border-collapse:collapse; border-color:#D3D3D3" border="1">
     <tr>
      <td valign="top">
      <asp:Panel ID="Panel1"  Width="400px" Height="500px" ScrollBars="Auto" runat="server">
         
          <asp:TreeView ID="TreeView1" runat="server" ShowLines="true"  
              BorderWidth="0px" OnDataBound ="TreeView1_DataBound">
              <NodeStyle BorderWidth="0px" />
              <SelectedNodeStyle BackColor="LightGray" /> 
          </asp:TreeView>
           </asp:Panel>
        </td>
      <td valign="top">
       <table id="table2" cellspacing="1" cellpadding="1"  border="0">
        <tr>
         <td>  节点名称
          <asp:textbox id="txtName" runat="server"></asp:textbox>
            节点代码：<asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
         <td align="center">
          <asp:button id="btnNew" runat="server" text="添　加" onclick="addbutton_Click1"></asp:button>  
          <asp:button id="btnEdit" runat="server" text="修　改" onclick="editbutton_Click1"></asp:button>       
          <asp:button id="btnDelete" runat="server" text="删　除" onclick="delbutton_Click1"></asp:button></td>
        </tr>
        <tr>
         <td>移动节点</td>
        </tr>
        <tr>
         <td>要移动到的节点 <asp:dropdownlist id="dropdownlist1" runat="server"></asp:dropdownlist></td>
        </tr>
        <tr>
         <td align="center">
          <asp:button id="movebutton" runat="server" text="移 动"></asp:button></td>
        </tr>
       </table>
      </td>
     </tr>
     </table>
    </div>
    </form>
</body>
</html>
