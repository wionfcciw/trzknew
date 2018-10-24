<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tbzyPwd.aspx.cs" Inherits="SincciKC.webUI.zytb.tbzyPwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     
          <script src="../../js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../../js/layer/layer.js"  type="text/javascript"></script>
    <link href="../../js/layer/skin/layer.css" rel="stylesheet" />
     <script type="text/javascript" language="javascript">
         var index = parent.layer.getFrameIndex(window.name);
       
         function CheakValue() {
             var p = document.getElementById("pwd").value;
             var kshpwd = document.getElementById("Text1").value;
             if (p == "") {
                  alert("请输入密码!");
                // return false;      
             } else if (p != kshpwd) {
                 alert("您输入密码不正确,请重新输入!");
               //  return false;
             } else {
                 window.parent.PwdBox(true);
                 parent.layer.close(index);
                 //window.returnVal = true;
                 //window.parent.hidePopWin(true);
             }
         }
        
     </script>
</head>
<body>
  
    
        <table width="100%"  >
            <tr style=" height:20px"><td></td><td></td></tr>
            <tr>
                <td align="right"><b>请输入登录密码：</b>
                </td>
                <td align="left"><input id="Text1" type="text" runat="server"  style=" display:none" />
                    <input type="password" class="input3" maxlength="20" style="width: 140px" id="pwd" name="pwd" />
                </td>
            </tr>
            <tr><td colspan="2"><br /></td></tr>
            <tr><td colspan="2" align="center"><input type="button" value=" 确 定 " onclick='return CheakValue();'   /></td></tr>
        </table>
  
    
  
</body>
</html>
