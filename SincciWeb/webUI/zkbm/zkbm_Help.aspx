<%@ Page Language="C#" ViewStateMode="Disabled" AutoEventWireup="true" CodeBehind="zkbm_Help.aspx.cs" Inherits="SincciKC.webUI.zkbm.zkbm_Help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信息采集表填表说明</title>    
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />


</head>
<body>
    <form id="form2" runat="server">
     <div id="wrap">
        <div class="header">
            <div class="logo" style="height:100px"> </div>
            <div id="menu"><uc:MenuControl ID="MenuControl1" runat="server" /> </div>
        </div>
        <div class="center_content" style=" margin-left:5px" >
             
            <table cellpadding="0" border="0" cellspacing="0" style=" width:860px; margin-left:10px"  >
                <tr>
                    <td class="title">
                        <img src="/images/arrow.gif" />
                        填写说明
                    </td>
                    <td class="title" align="right">
                        <input type="button" name="btnExit" class="register" onclick="javascript:window.location.href='/webUI/Exit.aspx'"
                            value="退出系统">
                    </td>
                </tr>
                <tr><td style=" height:5px"></td></tr>
            </table>
            <div  style="border:1px solid #DFF3FE; background-color:#DFF3FE; margin-left:10px ; padding:1px; width:855px" >
                <div  style="background-color:#FFFFFF;">
                
                    <table width="850" border="0" style="border-collapse: collapse;" align="center" cellpadding="0"
                        cellspacing="0">
                        <%--<tr>
                            <td class="tbltitle">
                                铜仁市<asp:Label ID="lblSysYear" runat="server" Text="Label"></asp:Label>高中阶段学校招生考试报名信息采集表
                                <br />
                                填 &nbsp;&nbsp;写 &nbsp;&nbsp;说 &nbsp;&nbsp;明
                            </td>
                        </tr>--%>
                        <tr>
                            <td height="400">
                                <table width="820" align="center">
                                    <tr>
                                        <td class="tblcontent" runat="server" id="Content"> 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnNext" CssClass="register2" runat="server" Text="报名信息采集" OnClick="btnNext_Click" />
                              
                              
                            </td>
                        </tr>
                    </table>
             </div>
             </div>
            <!--end of center content-->
        </div>
        

       <uc1:FootControl ID="FootControl1" runat="server" /> 
        <!--end of footer--> 
    </div>
    </form>
</body>
</html>
 