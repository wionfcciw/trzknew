<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zytb_Help.aspx.cs" Inherits="SincciKC.webUI.zytb.Zytb_Help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考生志愿填报填表说明</title>    
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
            <div id="menu">
                <uc:MenuControl ID="MenuControl1" runat="server" />
            </div>
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
                            <td   style="vertical-align:top">
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

                                <table width="820" align="center">
                                   
                                    <tr>
                                           <td id="Td1" class="tblcontent">
                                               &nbsp;
<p style="text-align:center;background:white;" align="center">
	<span style="font-family:Arial;color:black;font-size:15pt;"></span> 
</p>
<p style="text-indent:21.1pt;background:white;">
	<b><span style="font-family:宋体;color:black;">一、</span></b><b><span style="font-family:Arial;color:black;">2018</span></b><b><span style="font-family:宋体;color:black;">年中考实时录取学校</span></b><b><span style="font-family:Arial;color:black;font-size:15pt;"></span></b> 
</p>
<p style="text-align:left;text-indent:21pt;" align="left">
	<span style="font-family:Arial;color:black;">&nbsp;&nbsp;&nbsp; &nbsp; </span><span style="font-family:宋体;color:black;">全市所有普通高中（省级示范性高中、普通高中、民办高中）纳入网上实时录取。</span><span style="font-family:Arial;color:black;"> </span> 
</p>
<p style="text-align:left;text-indent:21pt;" align="left">
	<b><span style="font-family:宋体;color:black;">二、高中录取安排：</span></b><span style="font-family:Arial;color:black;">2018</span><span style="font-family:宋体;color:black;">年</span><span style="font-family:Arial;color:black;">7</span><span style="font-family:宋体;color:black;">月</span><span style="font-family:Arial;color:black;">14</span><span style="font-family:宋体;color:black;">日</span><span style="font-family:宋体;color:black;">—</span><span style="font-family:Arial;color:black;">7</span><span style="font-family:宋体;color:black;">月</span><span style="font-family:Arial;color:black;">18</span><span style="font-family:宋体;color:black;">日</span><span style="font-family:宋体;color:black;">考生在网上实时填报高中志愿，并实时录取。</span><span style="font-family:Arial;color:black;"> </span> 
</p>
<p style="text-align:left;text-indent:21pt;" align="left">
	<span style="font-family:Arial;color:black;">&nbsp;&nbsp;&nbsp; <b><span>&nbsp;</span>&nbsp;&nbsp;&nbsp; </b></span><b><span style="font-family:宋体;color:black;">中职</span></b><b><span style="font-family:宋体;color:black;">录取安排：</span></b><span style="font-family:Arial;color:black;">2018</span><span style="font-family:宋体;color:black;">年</span><span style="font-family:Arial;color:black;">7</span><span style="font-family:宋体;color:black;">月</span><span style="font-family:Arial;color:black;">21</span><span style="font-family:宋体;color:black;">日</span><span style="font-family:宋体;color:black;">—</span><span style="font-family:Arial;color:black;">7</span><span style="font-family:宋体;color:black;">月</span><span style="font-family:Arial;color:black;">24</span><span style="font-family:宋体;color:black;">日</span><span style="font-family:宋体;color:black;">，由市招生考试院按传统志愿方式组织录取。</span><span style="font-family:Arial;color:black;"> </span> 
</p>
<p style="text-align:left;text-indent:21pt;" align="left">
	<b><span style="font-family:宋体;color:black;">三、网上实时录取批次安排：</span></b><span style="font-family:Arial;color:black;"> </span> 
</p>
<p style="text-align:left;text-indent:42pt;" align="left">
	<span style="font-family:宋体;color:black;">第一批次：提前录取铜仁一中、思南中学、铜仁市民族中学、铜仁二中、铜仁八中的精准扶贫生，市民族中学的民族班学生，以及民大附中的单独招生。<span></span></span> 
</p>
<p style="text-align:left;text-indent:42pt;" align="left">
	<span style="font-family:宋体;color:black;">第二批次：铜仁一中、思南中学的统招生（配额生）及其他省级示范性高中和铜仁市十五中、思南县民族中学的奖励性指标（含市民族中学面向县域外的配额生）。<span> </span></span> 
</p>
<p style="text-align:left;text-indent:42pt;" align="left">
	<span style="font-family:宋体;color:black;">第三批次：其它省级示范性高中面向本县域内的统招生（配额生）、普通高中及民办高中统招生。<span></span></span> 
</p>
<p style="text-align:left;text-indent:21pt;" align="left">
	<b><span style="font-family:宋体;color:black;">四、考生网上实时填报时间安排：</span></b><span style="font-family:宋体;color:black;">志愿填报和录取分<span>3</span>个批次，共<span>5</span>天。从第一天开始，每天上午<span>8</span>：<span>00</span>—<span>12</span>：<span>00</span>及<span>14</span>：<span>00</span>—<span>18</span>：<span>00</span>为考生填报阶段，考生可上网填报志愿。从第一天开始，每天<span>12</span>：<span>00</span>—<span>14</span>：<span>00</span>及<span>18</span>：<span>00</span>—<span>20</span>：<span>00</span>为录取阶段，在录取时间段内，考生只能查询信息，不能上网办理志愿填报。其余时间考生均可在网上了解各校考生填报、招生录取信息。第<span>5</span>天下午<span>20</span>：<span>00</span>，普通高中网上实时录取工作结束。</span><span style="font-family:Arial;color:black;"> </span> 
</p>
<p style="text-align:center;" align="center">
	<b><span style="font-family:仿宋_GB2312;color:black;font-size:16pt;">铜仁市中考实时录取考生填报录取时间表<span></span></span></b> 
</p>
<div align="center">
	<table style="border-collapse:collapse;" border="1" cellspacing="0" bordercolor="#000000" cellpadding="0">
		<tbody>
			<tr>
				<td width="79">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">填报批次<span></span></span> 
					</p>
				</td>
				<td width="144">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">填报类别<span></span></span> 
					</p>
				</td>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">填报节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">填报开始时间<span></span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">填报结束时间<span></span></span> 
					</p>
				</td>
				<td width="110">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">录取时间<span></span></span> 
					</p>
				</td>
			</tr>
			<tr>
				<td width="79">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第一批次<span></span></span> 
					</p>
				</td>
				<td width="144">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">精准扶贫生、民族生<span></span></span> 
					</p>
				</td>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第一节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p>
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>14</span>日</span><span style="font-family:宋体;color:black;">8</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>14</span>日</span><span style="font-family:宋体;color:black;">12</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td rowspan="10" width="110">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;"></span> 
					</p>
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">每天中午<span></span></span> 
					</p>
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">12</span><span style="font-family:宋体;color:black;">：<span>00</span>—<span>14</span>：<span>00</span></span> 
					</p>
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;"></span> 
					</p>
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">每天下午<span></span></span> 
					</p>
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">18</span><span style="font-family:宋体;color:black;">：<span>00</span>—<span>20</span>：<span>00</span></span> 
					</p>
				</td>
			</tr>
			<tr>
				<td rowspan="4" width="79">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第二批次<span></span></span> 
					</p>
				</td>
				<td rowspan="4" width="144">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">统招生、配额生、配额生转统招生<span></span></span> 
					</p>
				</td>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第一节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p>
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>14</span>日</span><span style="font-family:宋体;color:black;">14</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>14</span>日</span><span style="font-family:宋体;color:black;">18</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
			</tr>
			<tr>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第二节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p>
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>15</span>日</span><span style="font-family:宋体;color:black;">8</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>15</span>日</span><span style="font-family:宋体;color:black;">12</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
			</tr>
			<tr>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第三节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p>
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>15</span>日</span><span style="font-family:宋体;color:black;">14</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>15</span>日</span><span style="font-family:宋体;color:black;">18</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
			</tr>
			<tr>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第四节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p>
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>16</span>日</span><span style="font-family:宋体;color:black;">8</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>16</span>日</span><span style="font-family:宋体;color:black;">12</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
			</tr>
			<tr>
				<td rowspan="5" width="79">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第三批次<span></span></span> 
					</p>
				</td>
				<td rowspan="5" width="144">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">面向本辖区统招生、配额生、配转统计划<span></span></span> 
					</p>
				</td>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第一节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p>
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>16</span>日</span><span style="font-family:宋体;color:black;">14</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>16</span>日</span><span style="font-family:宋体;color:black;">18</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
			</tr>
			<tr>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第二节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p>
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>17</span>日</span><span style="font-family:宋体;color:black;">8</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>17</span>日</span><span style="font-family:宋体;color:black;">12</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
			</tr>
			<tr>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第三节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p>
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>17</span>日</span><span style="font-family:宋体;color:black;">14</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>17</span>日</span><span style="font-family:宋体;color:black;">18</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
			</tr>
			<tr>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第四节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p>
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>18</span>日</span><span style="font-family:宋体;color:black;">8</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>18</span>日</span><span style="font-family:宋体;color:black;">12</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
			</tr>
			<tr>
				<td width="84">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">第五节点<span></span></span> 
					</p>
				</td>
				<td width="126">
					<p>
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>18</span>日</span><span style="font-family:宋体;color:black;">14</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
				<td width="113">
					<p style="text-align:center;" align="center">
						<span style="font-family:宋体;color:black;">7</span><span style="font-family:宋体;color:black;">月<span>18</span>日</span><span style="font-family:宋体;color:black;">18</span><span style="font-family:宋体;color:black;">：<span>00</span></span> 
					</p>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<p style="text-align:left;text-indent:21pt;" align="left">
	<b><span style="font-family:宋体;color:black;">五、请考生必须在规定的时间内上网进行填报或改报志愿，错过时间节点未提交或未报名的考生没有被录取，由考生自已负责。</span></b><span style="font-family:Arial;color:black;"> </span> 
</p>
<p style="text-align:left;text-indent:21pt;" align="left">
	<b><span style="font-family:宋体;color:black;">六、请考生务必保管好密码。</span></b><span style="font-family:宋体;color:black;">如因密码泄露所造成的一切后果由考生自行负责。若考生密码遗忘需及时联系本县招生考试机构或毕业学校重置密码（重置密码为考生身份证后六位）。</span><span style="font-family:Arial;color:black;"> </span> 
</p>
<p style="text-align:left;text-indent:21pt;" align="left">
	<b><span style="font-family:宋体;color:black;">七、填报注意事项。</span></b><span style="font-family:宋体;color:black;">往届生不能填报铜仁一中、思南中学和中央民族大学附属中学（以下称：民大附中）三所学校；文科综合、生物地理、综合素质评价等级，铜仁一中、思南中学和民大附中需<span>B</span>等及以上，其它高中学校（含民办）须达到<span>C</span>等及以上；应届考生必须在所读初中学校有连续三年学籍并实际就读，才能填报省级示范性高中配额生计划和民大附中；省级示范性高中学校只限报考类别为省级示范性高中的考生填报；</span><span style="font-family:Arial;color:black;"> </span><span style="font-family:宋体;color:black;">只有建档立卡贫困考生才能填报精准扶贫计划，只有少数民族考生才能填报民族生计划和民大附中；只有上了省级示范性高中控制线的考生才能填报省级示范性高中学校，只有上了普通高中控制线的考生才能填报普通高中学校，对于精准扶贫生、民族生及铜仁十五中、思南民族中学面向县域外的奖励性指标计划的最低控制线以省级示范性高中的最低填报志愿控制线为准，民办高中将自设控制线。</span><span style="font-family:Arial;color:black;"> </span> 
</p>
<p style="text-align:left;text-indent:21pt;" align="left">
	<b><span style="font-family:宋体;color:black;">八、其它相关要求以市教育局关于印发《铜仁市<span>2018</span>年普通高中招生录取办法》的通知（铜教发<span>[2018]<span>&nbsp;&nbsp; </span>91</span>号）为准。同时请考生随时上网查询公告栏。</span></b><span style="font-family:Arial;color:black;"> </span> 
</p>
<p>
	<span></span> 
</p>
                                    </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnNext" runat="server" Text="  我已核对并确认以上成绩无误  " OnClientClick="return confirm('我已核对并确认以上成绩无误,是否继续?')" OnClick="btnNext_Click"  Height="30"/>
                              
                              
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
 