<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="SincciKC.system.main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../css/page.css" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <base target="_self"> 
</head>
<body>
    <form id="form1" runat="server">
 <%--     <h4 style=" color:Red">招生学校操作说明!</h4> 
   下载：
    <br />
    1、<a href="../Template/招生学校操作说明.doc">招生学校操作说明。 </a>--%>
   
   <%--<h4 style=" color:Red">请不要将考生全部删除后重新导入,已经生成的报名号不可以重新生成!</h4>  --%>
  <%-- <h4 style=" color:Red">若需要打印中考志愿报表,请下载组件,否则打印不了!</h4> 
 
    <h5 style=" color:Red">  温馨提示：  打印时请设定页眉页脚为空，左右边距为5毫米，上下边距为5毫米。  </h5> 
   组件下载：
    <br />
    1、<a href="../Template/Print.rar">志愿报表打印组件下载。 </a><br />
    2、<a href="../Template/志愿填报操作说明.doc">志愿填报操作说明。 </a>  --%>
    
    </form>

</body>
</html>

<script language="JavaScript">
    function myrefresh() {
        window.location.reload();
    }
    setTimeout('myrefresh()', 1000 * 60 * 3); //指定1秒刷新一次 
</script> 
 