<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tbzy_xx.aspx.cs" Inherits="SincciKC.websystem.zygl.tbzy_xx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" type="text/css" href="/style.css" />     
        
     <script type="text/javascript" language="javascript">
         function myfunction(id, name, zyid) {
              
             if (id == "quxiao")
                 window.returnVal = name;
             else
                 window.returnVal = id + ";" + name + ";" + zyid;
                
             window.parent.hidePopWin(true);
         }
         function CheakValue(id,name,zyid) {
       
             if (id == "quxiao") {
                 document.getElementById("Text1").value = name;
             } else {
                 document.getElementById("Text1").value = id + ";" + name + ";" + zyid;
             }
         }
         function MyCL() {
             try {

                 if (document.getElementById("Text1").value == "") {
                     if (document.getElementById("txtlab").value=="pass") {
                         alert("请选择其中一项!");
                         return;
                     } else {
                         document.getElementById("Text1").value = document.getElementById("txtlab").value;
                     }
                  
                   
                  //   return;
                 }
            //     alert(document.getElementById("Text1").value);
                 window.returnVal = document.getElementById("Text1").value;
                 //window.top.hidePopWin(true);
               
                 window.parent.hidePopWin(true);
             } catch (e) {
                // alert(e);
             }
         }
     
     </script>
</head>
<body>
  

    <span id="xxSpan" runat="server"  >
    
        
    </span>
  

    &nbsp;<br/>
   
  <input id="Text1" type="text" runat="server"  style=" display:none" />
 <%--   <input type="button" value=" 确 定 " onclick="MyCL();" style=" margin-left:200px; "/>--%>
  
</body>
</html>
