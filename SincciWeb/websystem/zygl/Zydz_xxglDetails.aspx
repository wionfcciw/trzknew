<%@ Page Language="C#" ViewStateMode="Disabled" AutoEventWireup="true" CodeBehind="Zydz_xxglDetails.aspx.cs" Inherits="SincciKC.websystem.zysz.Zydz_xxglDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>志愿信息表查看</title>
  <link rel="stylesheet" type="text/css" href="/style.css" /> 

<link rel="stylesheet" type="text/css" href="../../css/subModal.css" /> 
 
 
<style type="text/css">
.btn
{
	 cursor: hand; 
 }
.tblcss
{
     text-align: center;  border-collapse: collapse; vertical-align: middle; border-style: solid;  
     
} 
 
.input1{
	padding-left:2px;
	padding-top:5px;
	background-color:#FFFFFF;
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
 .input2{
	padding-left:2px;
	padding-top:5px;
	background-color:#FFFFFF;
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
   <div  style="border:5px solid #DFF3FE; background-color:#DFF3FE; margin-left:10px ; padding:2px; width:855px" >
                <div  style="background-color:#FFFFFF">
                <div  class="tbltitle" >铜仁市<%=info.Kaocimc %>高中阶段教育学校招生考生志愿表</div>
                 <table  width="850" border="0" style="border-collapse: collapse;" align="center" cellpadding="0" cellspacing="0">
                     <tr  >
                      <td ><table><tr>
                        <td align="right" width="80" ><b>姓名：</b></td>
                        <td align="left"   width="120" ><%= info.Xm %>  </td>
                         <td align="right"  width="80"><b>报名号：</b></td>
                        <td  align="left"  width="180" ><%= info.Ksh %>  </td>
                        <td align="right"   width="90"><b>毕业中学名称：</b></td>
                        <td  align="left" ><%= info.Bmdmc %>  </td>
                       
                       </tr></table>  </td>
                     </tr> 
                     <tr>
                         <td >
                             <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                 <tr>
                                     <td >
                                         <span id="zyspan" runat="server"></span>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                     </td>
                                 </tr>
                             </table> 
                         </td>
                     </tr> 
                 </table>
             </div>
             </div>
    </form>
</body>
</html>
