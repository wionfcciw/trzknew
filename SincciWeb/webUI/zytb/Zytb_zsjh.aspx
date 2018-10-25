<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zytb_zsjh.aspx.cs" Inherits="SincciKC.webUI.zytb.Zytb_zsjh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>招生计划</title>
       <link rel="stylesheet" type="text/css" href="/style.css" />
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
     <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
     <script type="text/javascript">

         function opdg(ID, Title) {
             if (ID == "1") {
                 ymPrompt.win({ message: '1.htm', width: 1200, height: 500, title: Title, iframe: true, fixPosition: true, dragOut: false })
                 return false;
             } else if (ID == "3") {
                 ymPrompt.win({ message: '3.htm', width: 1200, height: 500, title: Title, iframe: true, fixPosition: true, dragOut: false })
                 return false;
             }
             else if (ID == "4") {
                 ymPrompt.win({ message: '4.htm', width: 1200, height: 500, title: Title, iframe: true, fixPosition: true, dragOut: false })
                 return false;
             }
             else if (ID == "5") {
                 ymPrompt.win({ message: '5.htm', width: 1200, height: 500, title: Title, iframe: true, fixPosition: true, dragOut: false })
                 return false;
             }
         }
         
     </script>
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
                        招生计划
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
                            <td height="100">
                                 <table class="windowTable">
             
            <tr >
            <td >
            <asp:Button ID="Button1" Width="140px" runat="server" Text="查看提前批次" 
                onclick="Button1_Click"  />
            <asp:Button ID="Button2" Width="140px" runat="server" Text="查看第一批计划"  />
            <asp:Button ID="Button3" Width="140px" runat="server" Text="查看第二批计划" onclick="Button3_Click"  />
            <asp:Button ID="Button4" Width="140px" runat="server" Text="查看第三批计划" 
                    onclick="Button4_Click"  />
            <asp:Button ID="Button5" Width="140px" runat="server" Text="查看第三批中职2计划" 
                    onclick="Button5_Click" Visible="false"  />
            </td>
            
            </tr>
          
          
            
        </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                                                   <asp:Button ID="btnEnter" Text=" 返 回 "  runat="server" 
                                          onclick="btnEnter_Click"  />
                
                
                              
                              
                            </td>
                        </tr>
                    </table>
             </div>
             </div>
            <!--end of center content-->
        </div>
        

        <div class="footer">
            <div class="left_footer">
                Copyright <span class="copy">&copy;</span> 2015-2017,All Rights Reserved. 铜仁市招生考试院
                主办
            </div>
        </div>
        <!--end of footer--> 
    </div>
    </form>
 </body>
</html>
