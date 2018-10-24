<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cjMag.aspx.cs" Inherits="SincciKC.webUI.cj.cjMag" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考生成绩信息查询</title>
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
            <div id="menu"   >
                <uc:MenuControl ID="MenuControl1" runat="server"  />
            </div>
        </div>
        <div class="center_content" style=" margin-left:5px" >
             
            <table cellpadding="0" border="0" cellspacing="0" style=" width:860px; margin-left:10px"  >
                <tr>
                    <td class="title">
                        <img src="/images/arrow.gif" />
                      考生成绩信息
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
                
                    <table width="850" border="0"  align="center" cellpadding="0"
                  style="border-collapse: collapse; border: 1px solid #B1CDE3; color: #4f6b72;  padding:0;   margin:0 auto;  border-collapse: collapse; background: #fff;   "       cellspacing="0">
                  <%--<tr>
                            <td class="tbltitle">
                                铜仁市<asp:Label ID="lblSysYear" runat="server" Text="Label"></asp:Label>高中阶段学校招生考试报名信息采集表
                                <br />
                                填 &nbsp;&nbsp;写 &nbsp;&nbsp;说 &nbsp;&nbsp;明
                            </td>
                        </tr>--%>
                        <tr>
                            <td  >
                      <asp:Repeater ID="repDisplay" runat="server">
        <HeaderTemplate>
                              <table  class="tblcss" id="GridView1" border="1" bordercolor="#C1DAD7" style="text-align: left" cellspacing="0" width="100%">
                                    <tr>
                                        <td colspan="6" align="center">
                                           <h4><b>中考考试成绩</b></h4> 
                                        </td>
                                    </tr>
                                     </HeaderTemplate>
         <ItemTemplate> 
                             <tr>
                                                <td width="100" class="sth" >
                                                     姓名：
                                                </td>
                                                <td><%#Eval("xm")%>&nbsp;
                                                </td>
                                               <td width="100" class="sth">
                                                     性别：
                                                </td>
                                                <td><%#Eval("xbmc")%>&nbsp;
                                                </td>
                                               <td width="100"class="sth">
                                                     身份证号：
                                                </td>
                                                <td><%#Eval("sfzh")%>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                   <td width="100" class="sth">
                                             准考证号：
                                        </td>
                                        <td><%#Eval("zkzh")%>&nbsp;
                                        </td>
                                        <td width="100" class="sth">
                                             毕业中学：
                                        </td>
                                        <td><%#Eval("bmdmc")%>&nbsp;
                                        </td>
                                       <td width="100" class="sth">
                                             班级：
                                        </td>
                                        <td><%#Eval("bjdm")%>&nbsp;
                                        </td>
                                      
                                    </tr>
                         <tr>
                              <td width="100" class="sth">
                                             考生类别：
                                        </td>
                                        <td><%#Eval("kslbmc")%>&nbsp;
                                        </td>
                                        <td width="100" class="sth">
                                             报考类别：
                                        </td>
                                        <td><%#Eval("bklb")%>&nbsp;
                                        </td>
                                       <td width="100" class="sth">
                                             联系电话：
                                        </td>
                                        <td><%#Eval("lxdh")%>&nbsp;
                                        </td>
                                    
                                    </tr>
                                    <tr>
                                        <td width="100" class="sth">
                                             语文：
                                        </td>
                                        <td><%#Eval("yw")%>&nbsp;
                                        </td>
                                       <td width="100" class="sth">
                                             数学：
                                        </td>
                                        <td><%#Eval("sx")%>&nbsp;
                                        </td>
                                       <td width="100" class="sth">
                                             英语：
                                        </td>
                                        <td><%#Eval("yy")%>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sth">
                                             理综分数：
                                        </td>
                                        <td><%#Eval("lkzh")%>&nbsp;
                                        </td>
 					<td class="sth">
                                             文综等级：
                                        </td>
                                        <td><%#Eval("wkzh")%>&nbsp;
                                        </td>
                                          <td class="sth">
                                             地生等级：
                                        </td>
                                        <td><%#Eval("dsdj")%>&nbsp;
                                        </td>
                                       
                                      
                                    </tr>
                                    <tr>
                                        <td class="sth">
                                             体育成绩：
                                        </td>
                                        <td><%#Eval("ty")%>&nbsp;
                                        </td>
                                        <td class="sth">
                                             综合等级：
                                        </td>
                                        <td><%#Eval("zhdj")%>&nbsp;
                                        </td>
                                        <td class="sth">
                                             照顾加分：
                                        </td>
                                        <td><%#Eval("jf")%>&nbsp;
                                        </td>
                                       
                                    </tr>
                                   <tr>
                                      
                                        <td class="sth">
                                             总分：
                                        </td>
                                        <td><%#Eval("zzf")%>&nbsp;
                                        </td>
                                        <td class="sth">
                                              
                                        </td>
                                        <td> 
                                        </td>
                                          <td class="sth">
                                             
                                        </td>
                                        <td> 
                                        </td>
                                    </tr>
                             </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater>
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
