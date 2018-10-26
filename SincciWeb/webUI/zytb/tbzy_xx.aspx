<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tbzy_xx.aspx.cs" Inherits="SincciKC.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" type="text/css" href="/style.css" />     
       <script src="../../js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../../js/layer/layer.js"  type="text/javascript"></script>
    <link href="../../js/layer/skin/layer.css" rel="stylesheet" />
     <script type="text/javascript" language="javascript">
         var index = parent.layer.getFrameIndex(window.name); //获取窗口索引
         function myfunction(id, name, zyid) {
             
             if (id == "quxiao")
                 window.parent.MessageBox(name); // window.returnVal = name;        
             else
                 window.parent.MessageBox(id + ";" + name + ";" + zyid);//window.returnVal = id + ";" + name + ";" + zyid;
                
          //   window.parent.hidePopWin(true);
             parent.layer.close(index);
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
                 window.parent.MessageBox(document.getElementById("Text1").value);
                 parent.layer.close(index);
                 //window.returnVal = document.getElementById("Text1").value;
                 //window.parent.hidePopWin(true);
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
