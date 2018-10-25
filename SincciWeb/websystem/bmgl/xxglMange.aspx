<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xxglMange.aspx.cs" Inherits="SincciKC.websystem.bmgl.xxglMange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <link rel="stylesheet" type="text/css" href="/style.css" />
<head runat="server">
    <title></title>
</head>
<body>
     
       <div  style="border:5px solid #DFF3FE; background-color:#DFF3FE; margin-left:10px ; padding:2px; width:905px" >
                <div  style="background-color:#FFFFFF">
                 <div  class="tbltitle" >铜仁市<asp:Label runat="server" ID="SysYear"></asp:Label>中等学校招生考试考生报名信息采集表</div>
                 <table  width="900" border="0" style="border-collapse: collapse;" align="center" cellpadding="0" cellspacing="0">
                     
                     <tr>
                         <td>
                             <table width="100%" border="1" bordercolor="#B0DFFD" style="border-collapse: collapse;   font-size:12px"
                                 align="center" cellpadding="3" cellspacing="0">
                                 <tr>
                                     <td align="right" width="80px">
                                         报名号：
                                     </td>
                                     <td>
                                         <asp:Label ID="lblksh" runat="server" Text=" "></asp:Label>
                                          
                                     </td>
                                     <td align="right"    width="80px">
                                         姓名：
                                     </td>
                                     <td width="120px">
                                         <asp:Label ID="lblxm"  runat="server"  ></asp:Label>
                                     </td>
                                     <td align="right"  width="80px">
                                         性别：</td>
                                     <td>
                                        <asp:Label ID="lblxb"  runat="server"  ></asp:Label>
                                     </td>
                                     <td rowspan="5">
                                         <img src='ShowKSPic.aspx?ksh=<%=ksh %>&pic=<%=pic %>&bmddm=<%=bmddm %>&Bmdxqdm=<%=Bmdxqdm %>' width="120px" height="160px" />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         &nbsp;民族：
                                     </td>
                                     <td>
                                        <asp:Label ID="lblmzdm"  runat="server"  ></asp:Label>
                                         
                                     </td>
                                     <td align="right"  >
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
                                     <td colspan="3"> 
                                         <asp:Label ID="lblsfzh"   runat="server" ></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         出生日期：</td>
                                     <td>
                                        
                                         <asp:Label ID="lblcsrq"   runat="server" ></asp:Label>
                                     </td>
                                     <td align="right"  >
                                         报考类别：</td>
                                     <td>
                                        <asp:Label ID="lblxjh"  runat="server"  ></asp:Label>
                                        
                                     </td>
                                     <td align="right">
                                        准考证号：</td>
                                     <td>
                                         <asp:Label ID="lblyddh"  runat="server"  ></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         毕业中学：</td>
                                     <td colspan="3">
                                          
                                         <asp:Label ID="lblbmddm" runat="server" Text=" "></asp:Label>
                                     </td>
                                     <td  align="right">
                                         班级：</td>
                                     <td  >
                                         <asp:Label ID="lblxsbh" runat="server" Text=""></asp:Label>
                                         
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">家庭地址：</td>
                                     <td colspan="3"> <asp:Label ID="lbljtdz"  runat="server"  ></asp:Label></td>
                                     <td align="right">联系电话：</td>
                                     <td colspan="2">
                                         <asp:Label ID="lbllxdh"   runat="server"  ></asp:Label>
                                         </td>
                                 </tr>
                               <%--  <tr>
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
                                 </tr>--%>
                             <%--    <tr>
                                     <td align="right">
                                         户籍所在地：
                                     </td>
                                     <td colspan="8">
                                         <asp:Label ID="lblhjdq"  runat="server"  ></asp:Label>
                                           <asp:Label ID="lblhjdz"  runat="server"  ></asp:Label>
                                     </td>
                                 </tr>--%>
                                 <tr>
                                     <td align="right">
                                         邮寄地址：
                                     </td>
                                     <td colspan="6">
                                     <%-- <asp:Label ID="lbljtdq"  runat="server"  ></asp:Label>--%>
                                     <asp:Label ID="lblcrhkh" runat="server" Text=" "></asp:Label>
                                        
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         邮政编码：
                                     </td>
                                     <td>
                                         <asp:Label ID="lblyzbm"   runat="server"  ></asp:Label>
                                     </td>
                                     <td align="right"  >
                                         &nbsp;收件人：</td>
                                     <td colspan="4">
                                         <asp:Label ID="lblsjr" runat="server"  ></asp:Label>
                                     </td>
                                 </tr>
                               <%--  <tr>
                                     <td align="right">
                                         <asp:Label ID="lblbzTitle" runat="server" Text=""></asp:Label> ：
                                     </td>
                                     <td colspan="8">
                                         <asp:Label ID="lblbz" runat="server"  ></asp:Label>
                                     </td>
                                 </tr> --%>
                             </table>
                         </td>
                     </tr>
                     <tr><td >
                      
                     </td></tr>
                  
                 </table>
             </div>
             </div>
    
</body>
</html>
