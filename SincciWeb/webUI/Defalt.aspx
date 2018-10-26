<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Defalt.aspx.cs" Inherits="SincciKC.webUI.Defalt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考生志愿填报填表说明</title>    
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />
<style type="text/css">
        .sth {
 font-weight:bold;
 color: #4f6b72;
 border-right: 1px solid #C1DAD7;
 border-bottom: 1px solid #C1DAD7;
 border-top: 1px solid #C1DAD7;
 letter-spacing: 2px;
 text-transform: uppercase;

 
 background: #CAE8EA  no-repeat;
}

    </style>

</head>
<body>
    <form id="form2" runat="server">
     <div id="wrap">
         <div class="header">
            <div class="logo" style="height: 100px">
            </div>
            <div id="menu">
                <uc:MenuControl ID="MenuControl1" runat="server" />
            </div>
        </div>
        <div class="center_content" style=" margin-left:5px" >
             
            <table cellpadding="0" border="0" cellspacing="0" style=" width:860px; margin-left:10px"  >
                <tr>
                    <td class="title">
                        <img src="/images/arrow.gif" />
                        填报进度
                    </td>
                    <td class="title" align="right">
                        <input type="button"  name="btnExit" class="register" onclick="javascript: window.location.href = '/webUI/Exit.aspx'"
                            value="退出系统" style="display:none" >
                    </td>
                </tr>
                <tr><td style=" height:5px"></td></tr>
            </table>
            <div  style="border:1px solid #DFF3FE;   margin-left:10px ; padding:1px; width:855px" >
               
                      <table width="820" align="center" >
                                   
                                    <tr>
                                        <td  align="center" > 
                                        
                                            <div runat="server" id="Content1"></div>
                                        </td>
                                    </tr>
                                </table>
               
             </div>
            <!--end of center content-->
        </div>
        

         <uc1:FootControl ID="FootControl1" runat="server" />
        <!--end of footer--> 
    </div>
    </form>
</body>
</html>
 
