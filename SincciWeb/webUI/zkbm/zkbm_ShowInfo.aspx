<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zkbm_ShowInfo.aspx.cs" Inherits="SincciKC.webUI.zkbm.zkbm_ShowInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>铜仁市高中阶段学校招生考试管理系统</title>

 <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />
 

<script  type="text/javascript" language="javascript">
   
</script>
 

</head>
<body>
    <form id="form1" runat="server">
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
                        信息查看
                    </td>
                    <td class="title" align="right">
                        <input type="button" name="btnExit" class="register" onclick="javascript:window.location.href='/webUI/Exit.aspx'"
                            value="退出系统">
                    </td>
                </tr>
                <tr><td style=" height:5px"></td></tr>
            </table>
             <div  style="border:5px solid #DFF3FE; background-color:#DFF3FE; margin-left:10px ; padding:2px; width:855px" >
                <div  style="background-color:#FFFFFF">
                <div  class="tbltitle" >铜仁市<asp:Label runat="server" ID="SysYear"></asp:Label>中等学校招生考试考生报名信息采集表</div>
                
                 <table  width="850" border="0" style="border-collapse: collapse;" align="center" cellpadding="0" cellspacing="0">

                     
                     <tr>
                         <td>
                             <table width="100%" border="1" bordercolor="#B0DFFD" style="border-collapse: collapse;   font-size:12px"
                                 align="center" cellpadding="3" cellspacing="0">
                                 <tr>
                                     <td align="right">
                                         报名号：
                                     </td>
                                     <td>
                                         <asp:Label ID="lblksh" runat="server" Text=" "></asp:Label>
                                          
                                     </td>
                                     <td align="right" colspan="3">
                                         姓名：
                                     </td>
                                     <td>
                                         <asp:Label ID="lblxm"  runat="server"  ></asp:Label>
                                     </td>
                                     <td align="right">
                                         性别：</td>
                                     <td>
                                        <asp:Label ID="lblxb"  runat="server"  ></asp:Label>
                                     </td>
                                     <td rowspan="5" align="center">
                                         <img src='ShowKSPic.aspx?ksh=<%=ksh %>' width="120px" height="160px" />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         &nbsp;民族：
                                     </td>
                                     <td>
                                        <asp:Label ID="lblmzdm"  runat="server"  ></asp:Label>
                                         
                                     </td>
                                     <td align="right" colspan="3">
                                         &nbsp;政治面貌：</td>
                                     <td>
                                        <asp:Label ID="lblzzmmdm"  runat="server"  ></asp:Label>
                                         
                                     </td>
                                     <td align="right">考生类别：</td>
                                     <td>
                                        <asp:Label ID="lblkslbdm"  runat="server"  ></asp:Label>
                                         
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">考次：</td>
                                     <td><asp:Label ID="lblkaoci" runat="server" Text=""></asp:Label>
                                          
                                     </td>
                                     <td align="right">
                                         证件号码：</td>
                                     <td colspan="5"> 
                                         <asp:Label ID="lblsfzh"   runat="server" ></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         出生日期：</td>
                                     <td>
                                        
                                         <asp:Label ID="lblcsrq"   runat="server" ></asp:Label>
                                     </td>
                                     <td align="right" colspan="3">
                                         家庭固定电话：</td>
                                     <td>
                                        
                                         <asp:Label ID="lbllxdh"   runat="server"  ></asp:Label>
                                     </td>
                                     <td align="right">
                                         家长移动电话：</td>
                                     <td>
                                         <asp:Label ID="lblyddh"  runat="server"  ></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         学籍号：</td>
                                     <td>
                                         <asp:Label ID="lblxjh"  runat="server"  ></asp:Label>
                                     </td>
                                     <td colspan="3" align="right">
                                         学生编码：</td>
                                     <td colspan="3">
                                         <asp:Label ID="lblxsbh" runat="server" Text=""></asp:Label> 
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">毕业中学：</td>
                                     <td colspan="5"><asp:Label ID="lblbmddm" runat="server" Text=" "></asp:Label></td>
                                     <td align="right">
                                          初二会考号：</td>
                                     <td colspan="2">
                                        <asp:Label ID="lblcrhkh" runat="server" Text=" "></asp:Label>  
                                      </td>
                                 </tr>
                                 <tr>
                                     <td align="right">毕业中学县区：
                                        </td>
                                     <td>
                                      <asp:Label ID="lblxqdm"  runat="server"  ></asp:Label>    
                                     </td>
                                     <td colspan="2" align="right">
                                         毕业中学：</td>
                                     <td colspan="2"> <asp:Label ID="lblbyzxdm"  runat="server"  ></asp:Label> </td>
                                     <td align="right">
                                          班级：</td>
                                     <td colspan="2">
                                     <asp:Label ID="lblbjdm"  runat="server"  ></asp:Label>
                                          
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         户籍所在地：
                                     </td>
                                     <td colspan="8">
                                         <asp:Label ID="lblhjdq"  runat="server"  ></asp:Label>
                                           <asp:Label ID="lblhjdz"  runat="server"  ></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         通讯地址：
                                     </td>
                                     <td colspan="8">
                                      <asp:Label ID="lbljtdq"  runat="server"  ></asp:Label>
                                     
                                         <asp:Label ID="lbljtdz"  runat="server"  ></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         邮政编码：
                                     </td>
                                     <td>
                                         <asp:Label ID="lblyzbm"   runat="server"  ></asp:Label>
                                     </td>
                                     <td align="right" colspan="3">
                                         &nbsp;收件人：</td>
                                     <td colspan="4">
                                         <asp:Label ID="lblsjr" runat="server"  ></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <asp:Label ID="lblbzTitle" runat="server" Text=""></asp:Label> ：
                                     </td>
                                     <td colspan="8">
                                         <asp:Label ID="lblbz" runat="server"  ></asp:Label>
                                     </td>
                                 </tr> 
                             </table>
                         </td>
                     </tr>
                     <tr><td >
                              <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0"  style=" border:1px solid #95DDFF ;border-collapse:collapse" >
                                <tr> 
                                    <td align="right" style="width:170px" valign="top"   style="color:red"> 提示：</td>
                                  <td height="55" align="left" > 
                                  <span style="color:red">1、请仔细检查你填写的资料，如确认无误请按下面的“确认”按钮。 
                                  <br/>2、资料一经确认就不能再修改。</span> 
                                 </td>
                                </tr>
                            </table>
                     </td></tr>
                     <tr>
                         <td align="center" >
                            <asp:Button ID="btnSave" runat="server" CssClass="register" Text="修改信息"  onclick="btnSave_Click" />
                             &nbsp;&nbsp;
                             <asp:Button ID="btnKSQueren" runat="server" CssClass="register" Text=" 确 认 " 
                                 Visible="false" onclick="btnKSQueren_Click"   />  
                                  &nbsp;&nbsp;
                             <asp:Button ID="btnBack" runat="server" CssClass="register" Text=" 返 回 " 
                                 onclick="btnBack_Click"  />
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