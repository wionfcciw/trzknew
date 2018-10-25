<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="SincciKC.webUI.news.NewsList1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

 <title>铜仁市高中阶段学校招生考试管理系统</title>
 <meta content="IE=7" http-equiv="X-UA-Compatible" />
<meta name="description" content="高中阶段学校招生考试管理系统" />
<meta name="keywords" content="高中阶段学校招生考试管理系统，高中阶段学校招生考试管理系统" />
   <script type="text/javascript" src="/js/jquery-1.3.2.min.js"></script>
<script src="/js/Jquery.js" type="text/javascript"></script>
 
<link rel="stylesheet" type="text/css" href="/style.css" />

</head>
<body>
    <div id="wrap">
         <div class="header">
            <div class="logo" style="height:100px"> </div>
            <div id="menu"><uc:MenuControl ID="MenuControl1" runat="server" /> </div>
        </div>

        <div class="center_content" style="margin-left:5px ; ">

                    <table cellpadding="0"   border="0" cellspacing="0" style=" width:866px; margin-left:10px"  >
                <tr>
                    <td  >
                        <img src="/images/arrow.gif" />  <a href="/">首页</a> &gt;
         <a href="NewsClass.aspx?ID=<%=categoryId %>"><asp:Label ID="lblCategoryName" runat="server" Text="Label"></asp:Label></a> 
                    </td> 
                </tr>
                <tr><td style=" height:5px"></td></tr>
            </table>

         <div  style="border:2px solid #DFF3FE; margin-left:10px ; padding:2px; width:858px" >
               <table style=" width:845px" align="center"><tr><td>
                <div class="listcontent" style="height:450px">
				<dl class="clearfix" runat="server" id="NewsContent">
                     
                </dl>
			</div></td></tr><tr><td>
            <div class="page">
                <webdiyer:aspnetpager id="AspNetPager1" runat="server" width="100%"  OnPageChanged="AspNetPager1_PageChanged">
                </webdiyer:aspnetpager>
		    </div></td></tr></table>
         </div>

        <uc1:FootControl ID="FootControl1" runat="server" /> 
        <!--end of footer--> 
    </div>
</body>
</html>

  