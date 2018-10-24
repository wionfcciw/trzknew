<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tyxm_ShowInfo.aspx.cs" Inherits="SincciKC.webUI.tyxm.tyxm_ShowInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>铜仁市高中阶段学校招生考试管理系统</title>

 <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />
 

<script  type="text/javascript" language="javascript">
    function cheak() {
        if (confirm("是否要确认您所填写的信息?")) {
            if (confirm("一旦确认后将不能在修改所有信息，是否要确认?")) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
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
                <div  class="tbltitle" >铜仁市<asp:Label runat="server" ID="SysYear"></asp:Label>中等学校体育考试考生报名信息采集表</div>
                
                 <table  width="850" border="0" style="border-collapse: collapse;" align="center" cellpadding="0" cellspacing="0">
   <tr>
                         <td>
                             <table width="100%" border="1" bordercolor="#B0DFFD" style="border-collapse: collapse;   font-size:12px"
                                 align="center" cellpadding="3" cellspacing="0">
 
                                 <tr>
                                     <td align="right">
                                        毕业中学：</td>
                                     <td>
                                     <asp:Label ID="lblbmd" runat="server" Text=""></asp:Label>
                                     </td>
                                     
                                    <td align="right">
                                        考次：</td>
                                     <td>
                                    
                                               <asp:Label ID="lblkaoci" runat="server" Text=""></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                       报名号：</td>
                                     <td>
                                        <asp:Label ID="lblksh" runat="server" Text=""></asp:Label> </td>
                                   <td align="right">
                                      姓名：  </td> 
                                     <td>
                                    
                                                 <asp:Label ID="lblxm" runat="server" Text=""></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                              <td align="right">
                                       身份证号：</td>
                                     <td>
                                       <asp:Label ID="lblsfzh" runat="server" Text=""></asp:Label>  </td>
                                   <td align="right">
                                      班级：  </td> 
                                     <td>
                                    
                                             <asp:Label ID="lblbj" runat="server" Text=""></asp:Label>   </td>
                                 </tr>

                                 <tr><td colspan="4">体育考试信息</td></tr>
                               <%--   <tr>
                                              <td align="right">
                                    必考项目：</td>
                                       <td colspan="3">
                                             <asp:Label ID="lblbikao" runat="server" Text=""></asp:Label>  </td>
                                 </tr>
                                  <tr>
                                              <td align="right">
                                       抽定项目：</td>
                                       <td colspan="3">    <asp:Label ID="lblchoud" runat="server" Text=""></asp:Label>
                                        </td>
                                 </tr>--%>
                                  <tr>
                                              <td align="right">
                                   自选项目1：</td>
                                       <td colspan="3">    <asp:Label ID="lblzixuan" runat="server" Text=""></asp:Label>
                                            </td>
                                 </tr>
                                  <tr>
                                              <td align="right">
                                       自选项目2：</td>
                                       <td colspan="3">   <asp:Label ID="lblbeixuan" runat="server" Text=""></asp:Label> </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         自选项目3：
                                     </td>
                                     <td colspan="3">
                                         <asp:Label ID="lblzixuan3" runat="server" Text=""></asp:Label>
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
                             <asp:Button ID="btnKSQueren" runat="server" CssClass="register" Text=" 确 认 "  OnClientClick="return cheak()"
                                 Visible="false" onclick="btnKSQueren_Click"   />  
                                  &nbsp;&nbsp;
                             <asp:Button ID="btnBack" runat="server" CssClass="register" Text=" 返 回 "  Visible="false"
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