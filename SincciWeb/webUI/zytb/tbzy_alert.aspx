<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tbzy_alert.aspx.cs" Inherits="SincciKC.webUI.zytb.tbzy_alert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
         <link rel="stylesheet" type="text/css" href="/style.css" />     
        
     <script type="text/javascript" language="javascript">
   
         function MyCL() {
             try {

              
               //  window.returnVal = document.getElementById("Text1").value;
                 //window.top.hidePopWin(true);

                 window.parent.hidePopWin(true);
             } catch (e) {
                 // alert(e);
             }
         }
     
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    保存失败,至少填报一个类别！
      <input type="button" value=" 确 定 " onclick="MyCL();" style=" margin-left:200px; "/>
    </div>
    </form>
</body>
</html>
