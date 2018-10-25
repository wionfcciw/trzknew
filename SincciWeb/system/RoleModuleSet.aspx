<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleModuleSet.aspx.cs" Inherits="SincciWeb.system.RoleModuleSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>设置角色模块</title>

<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
 
 
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

<style type="text/css"> 

.ItemBgColor {BACKGROUND-COLOR: #f5f5f5} 

</style> 

<script  type="text/javascript" language="javascript">

    function ChangeSelectedItemColor(checkBoxListId, numOfItems) {

        // Get the checkboxlist object. 

        var objCtrl = document.getElementById(checkBoxListId);

        if (objCtrl == null) {

            return;

        } 

        for (i = 0; i < numOfItems; i++) {

            var name = checkBoxListId + ':' + i;

            var objItem = document.getElementById(checkBoxListId + '_' + i);

            var isCheck = objItem.checked;

            if (isCheck == true) {

                objItem.parentElement.className = 'ItemBgColor';

            }

            else {

                objItem.parentElement.className = '';

            }

        }

    } 

</script> 

     

</head>
<body>
    <form id="form1" runat="server">
    <div> 
   <div style="font-size: medium; font-weight: bold; text-align:center; height:28px" >角色［<asp:Label ID="lblRoleName" runat="server" Text=""></asp:Label>］设置模块</div> 
    <table width="99%" border="1" align="center" style="border-collapse:collapse; margin-top:5px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
   <tr>
    <td > 请选择模块：
    </td>
  </tr>
  <tr>
    <td >  
        
        <asp:CheckBoxList ID="CheckBoxList1"  runat="server" CellPadding="2" 
            CellSpacing="4" RepeatColumns="3">
           
        </asp:CheckBoxList>
    </td>
  </tr>
  <tr>
    <td align="center">
    
        <asp:Button ID="btnSave" runat="server"  CssClass="btnStyle"  Text=" 保存 " onclick="btnSave_Click" /> </td>
  </tr>
  
</table>
 
    </div>
    </form>
</body>
</html>
