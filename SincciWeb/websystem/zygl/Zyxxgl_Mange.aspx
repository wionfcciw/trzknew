<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zyxxgl_Mange.aspx.cs" Inherits="SincciKC.websystem.zygl.Zyxxgl_Mange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考生志愿信息</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />
    <link rel="stylesheet" type="text/css" href="../../css/subModal.css" />
    <script src="../../js/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/subModal.js"></script>
    <style type="text/css">
        body, td, th
        {
            /*font-size: 9px;*/
        }
        td
        {
             height:2px;
        }
        .btn
        {
            cursor: hand;
        }
        .tblcss
        {
            text-align: center;
            border-collapse: collapse;
            vertical-align: middle;
            border-style: solid;
            
        }
        
        .input1
        {
            
            background-color: #FFFFFF;
            border-top-width: 0px;
            border-right-width: 0px;
            border-bottom-width: 1px;
            border-left-width: 0px;
            border-top-style: none;
            border-right-style: none;
            border-bottom-style: dashed;
            border-left-style: none;
            border-bottom-color: #FF0066;
        }
        .input2
        {
            
            background-color: #FFFFFF;
            border-top-width: 0px;
            border-right-width: 0px;
            border-bottom-width: 0px;
            border-left-width: 0px;
            border-top-style: none;
            border-right-style: none;
            border-bottom-style: none;
            border-left-style: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color: #FFFFFF" id="divShow" runat="server">
      
        <table width="700" border="0"  align="center" cellpadding="0"
            cellspacing="0">
            <tr>
                <td  style=" height:8px">
                    <table  width="100%" >
                     
                    <tr><td colspan="5"  align="right"   >
                        <b style=" font-size:16px; padding-right:40px"><%= info.Kaocimc %>铜仁市中招考生志愿表(<%= info.xqmc %>)</b></td><td colspan="1" align="right"> <%=DateTime.Now%></td>     </tr>
                        
                        <tr>
                            <td align="right" width="80">
                                <b>姓名：</b>
                            </td>
                            <td align="left" >
                                <%= info.Xm %>
                            </td>
                            <td align="right" width="120">
                                <b>报名号：</b>
                            </td>
                            <td align="left"  >
                                <%= info.Ksh %>
                            </td>
                            <td align="right" width="110">
                                <b>毕业中学名称：</b>
                            </td>
                            <td align="left">
                                <%= info.Bmdmc %>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <span id="zyspan" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
               <%--             <table style="width: 700px;">
                            <tr>
                                <td align="left">
                                    <b>考生签名:______________</b>
                                      <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日</b>
                              
                                </td>

                                <td align="right">
                                    <b>家长签名:______________</b>
                                     <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日</b>
                              
                                </td>

                                <td align="right"> <%--<b>学校盖章:______________</b></td>
                            </tr>
                         
                        </table>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        
          <%--  <tr>
                <td align="left">
                <table>
                        <tr>
                            <td rowspan="3" valign="top" style="width: 50px;">
                                说明:
                            </td>
                            <td>
                                本表为考生中考志愿最终确认表，修改无效。相关成绩仅作核对使用，如有问题请与学校联系，由学校汇总后集中上报，在本表修改无效。
                            </td>
                        </tr>
                        <tr>
                            <td>
                                本表由毕业学校指定专人负责，考生和家长校核，确认无误后签字确认。
                            </td>
                        </tr>
                        <tr>
                            <td>
                                本表由毕业学校收齐后交当地招办留存。
                            </td>
                        </tr>
                    </table> 
                </td>
            </tr>--%>
            <%--<tr>
                <td align="right">
                    <div id="divprint" runat="server"  >
                        <table style="width: 800px;">
                            <tr>
                                <td align="left">
                                    <b>考生签名:______________</b>
                                </td>

                                <td align="right">
                                    <b>家长签名:______________</b>
                                </td>

                                <td> <b>学校盖章:______________</b></td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日</b>
                                </td>
                                <td align="right">
                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日</b>
                                </td>
                                 <td align="right">
                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日</b>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>--%>
        </table>
    </div>
    <div id="divYin" runat="server" visible="false">
        <font color='red'>该考生尚未填报!</font>
    </div>
    </form>
</body>
</html>
