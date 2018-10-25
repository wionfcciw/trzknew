<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kslq_Help.aspx.cs" Inherits="SincciKC.webUI.kslq.kslq_Help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考生录取信息查询</title>    
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />
     
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
            <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
        <div class="center_content" style=" margin-left:5px" >
             
            <table cellpadding="0" border="0" cellspacing="0" style=" width:860px; margin-left:10px"  >
                <tr>
                    <td class="title">
                        <img src="/images/arrow.gif" />
                      考生录取信息
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
                
                        <tr>
                            <td  >     
                                <asp:Label Text="0" runat="server" ID="sfload" Visible="false"  />
                                    <asp:Label Text="" runat="server" ID="lblpc" Visible="false" />
                                 <asp:Label Text="" runat="server" ID="lblxxdm" Visible="false" />
                                  <asp:label id="lblksh" runat="server" text="" visible="false"></asp:label>
                                    <asp:updatepanel id="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border: 1px solid #95DDFF; border-collapse: collapse">
                                                <tr>
                                                    <td align="center">
                                                        <div runat="server" id="div1">
                                                            请输入您的准考证号：
                                                        <asp:TextBox ID="txtpwd" runat="server" Height="18px" MaxLength="12" Width="100px"></asp:TextBox>
                                                            <asp:Button Text=" 查询情况 " runat="server" OnClick="Unnamed1_Click" ID="btnsel" Height="30" />
                                                            <asp:Label Text="(准考证号有误)" runat="server" ID="lblzkzh" ForeColor="Red" Visible="false" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td height="55" align="center">
                                                        <asp:Repeater ID="Repeater1" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="tblcss" id="GridView1" border="1" bordercolor="#C1DAD7" style="text-align: center" cellspacing="0">

                                                                    <tr class="datagrid-header" style="height: 26px">
                                                                        <td>

                                                                            <b>学校代码</b></td>
                                                                        <td>
                                                                            <b>学校名称</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>类别</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>排名情况</b>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <b>填报时间</b>
                                                                        </td>
                                                                          <td>
                                                                            <b>录取状态</b>
                                                                        </td>
                                                                          <td>
                                                                            <b>录取时间</b>
                                                                        </td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="height: 24px">
                                                                    <td>&nbsp;<%#Eval("xxdm")%>&nbsp;</td>
                                                                    <td>&nbsp;<%#Eval("zsxxmc")%>&nbsp;</td>
                                                                    <td>&nbsp;<%#Eval("xpcMc")%>&nbsp;</td>
                                                                    <td>&nbsp;第<font color='red'><%#Eval("pm")%></font>名&nbsp;<%#  Convert.ToInt32(Eval("pm"))<=Convert.ToInt32(Eval("jhs"))? (Eval("xxdm").ToString().Length==3? "（统招计划内）":"") :(Eval("xxdm").ToString().Length==3? "（统招计划外）":"") %></td>
                                                                  <%--  <td>&nbsp;<%#Eval("maxnum")%>&nbsp;</td>
                                                                     <td>&nbsp;<%#Eval("minnum")%>&nbsp;</td>
                                                                     <td>&nbsp;<%#Eval("num")%>&nbsp;</td>--%>
                                                                     <td>&nbsp;<%#Eval("lrsj")%>&nbsp;</td>    
                                                                      <td>&nbsp;<%#Eval("td_zt").ToString()=="5"?"已被录取":"尚未录取"%>&nbsp;</td>
                                                                     <td>&nbsp;<%#Eval("lqtime")%>&nbsp;</td>          
                                                                </tr>
                                                            </ItemTemplate>
                                                            
                                                            <FooterTemplate></table></FooterTemplate>
                                                        </asp:Repeater>
                                                          <asp:Repeater ID="Repeater2" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="tblcss" id="GridView1" border="1" bordercolor="#C1DAD7" style="text-align: center" cellspacing="0">

                                                                    <tr class="datagrid-header" style="height: 26px">
                                                                        <td>

                                                                            <b>学校代码</b></td>
                                                                        <td>
                                                                            <b>学校名称</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>录取类别</b>
                                                                        </td>
                                                                          <td>
                                                                            <b>录取状态</b>
                                                                        </td>
                                                                          <td>
                                                                            <b>录取时间</b>
                                                                        </td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="height: 24px">
                                                                    <td>&nbsp;<%#Eval("lqxx")%>&nbsp;</td>
                                                                    <td>&nbsp;<%#Eval("zsxxmc")%>&nbsp;</td>
                                                                    <td>&nbsp;<%#Eval("sftzs").ToString()=="1"?"统招生":(Eval("sftzs").ToString()=="2"?"配额生":"配转统")%>&nbsp;</td>
                                                                      <td>&nbsp;<font color='red'><%#Eval("td_zt").ToString()=="5"?"已被录取":"尚未录取"%></font>&nbsp;</td>
                                                                     <td>&nbsp;<%#Eval("lqtime")%>&nbsp;</td>          
                                                                </tr>
                                                            </ItemTemplate>
                                                            
                                                            <FooterTemplate></table></FooterTemplate>
                                                        </asp:Repeater>

                                                        <asp:Label Text="" runat="server"  Visible="false" ID="lblshow"  ForeColor="Red"  />
                                                        <asp:Label Text="排名情况默认120秒刷新一次,请勿频繁刷新页面!" runat="server"  id="lblfont" ForeColor="Red" Visible="false"/> 
                                                        <asp:Label Text="119" runat="server"  Visible="false" ID="lblnum"/>
                                                        <asp:Label Text="您尚未填报志愿，暂无数据!" runat="server"  id="lblbsj" ForeColor="Red" Visible="false"/> 
                                                        <asp:Timer ID="tim" runat="server" Interval="1000" OnTick="tim_Tick"  Enabled="false"></asp:Timer><br />
                                        
                                                   
                            </td>
                                       </tr>
                                       </table>
                                       </ContentTemplate>
                                   <Triggers><asp:AsyncPostBackTrigger ControlID="tim" /></Triggers>
                                    </asp:updatepanel>
                                           
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
 