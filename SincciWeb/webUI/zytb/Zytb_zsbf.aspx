<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zytb_zsbf.aspx.cs" Inherits="SincciKC.webUI.zytb.Zytb_zsbf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考生志愿填报填表说明</title>    
    <meta http-equiv=Content-Type content="text/html; charset=gb2312">
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
     <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
 
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
                         院校招生计划
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
                            <td  height="100">
                                
                                <table width="820" align="center">    
                                    <tr>
                                       <td id="Content" class="tblcontent">&nbsp;
<table style="width:700pt;border-collapse:collapse;" cellspacing="0" cellpadding="0" width="820" border="0">
	<tbody>
		<tr>
			<td width="820"  >
				<p style="text-align:center;" align="center">
					<span style="font-size:18pt;color:black;font-family:方正小标宋简体;">铜仁市<span>2016</span>年省级示范性普通高中及部分普通高中<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="820">
				<p style="text-align:center;" align="center">
					<span style="font-size:18pt;color:black;font-family:方正小标宋简体;">网上实时录取招生计划分配表<span></span></span>
				</p>
			</td>
		 
		</tr>
	</tbody>
</table>
&nbsp;
<p style="margin-left:18pt;text-indent:-18pt;">
	<span style="color:black;"><span><strong>1、</strong><span><strong>&nbsp; </strong></span></span></span><span style="color:black;font-family:宋体;"><strong>全部招生计划通过实时录取的学校</strong></span><span style="color:black;"></span>
</p>
<table style="border-collapse:collapse;" bordercolor="#000000" cellspacing="0" cellpadding="0" border="2">
	<tbody>
		<tr>
			<td width="99" rowspan="2">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">招生学校<span><span> </span></span></span>
				</p>
			</td>
			<td width="123" rowspan="2">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">招生类别<span></span></span>
				</p>
			</td>
			<td width="525" colspan="10">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">招生区域<span></span></span>
				</p>
			</td>
			<td width="79" rowspan="2">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">合计<span></span></span>
				</p>
			</td>
			<td width="108" rowspan="2">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">备注<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="63">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">碧江<span></span></span>
				</p>
			</td>
			<td width="68">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">万山<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">江口<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">玉屏<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">石阡<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">松桃<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">思南<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">印江<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">德江<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">沿河<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="99" rowspan="5">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">铜仁一中<span></span></span>
				</p>
			</td>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">统招生<span></span></span>
				</p>
			</td>
			<td width="131" colspan="2">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">510</span>
				</p>
			</td>
			<td width="197" colspan="4">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">340</span></b>
				</p>
			</td>
			<td width="197" colspan="4">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">――――――<span></span></span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">850</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">含特长生<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">配额生<span></span></span>
				</p>
			</td>
			<td width="131" colspan="2">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;"><span>&nbsp;</span></span><span style="font-size:11pt;color:black;font-family:宋体;">总<span>595</span>，（碧江区<span>457</span>、万山区<span>138</span>）<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">36</span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">23</span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">76</span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">120</span>
				</p>
			</td>
			<td width="197" colspan="4">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">――――――<span></span></span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">850</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">合计<span></span></span>
				</p>
			</td>
			<td width="328" colspan="6">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">1700</span>
				</p>
			</td>
			<td width="197" colspan="4">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">――――――<span></span></span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">1700</span></b>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">精准扶贫班<span></span></span></b>
				</p>
			</td>
			<td width="525" colspan="10">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">面向全市招<span>50</span>人<span></span></span></b>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">50</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">招收全市建档立卡贫困生<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">总计<span></span></span>
				</p>
			</td>
			<td width="525" colspan="10">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">1750</span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">1750</span></b>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="99" rowspan="3">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">思南中学<span></span></span>
				</p>
			</td>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">统招生<span></span></span>
				</p>
			</td>
			<td width="328" colspan="6">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">――――――<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">390</span>
				</p>
			</td>
			<td width="148" colspan="3">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">260</span></b>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">650</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">含特长生<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">配额生<span></span></span>
				</p>
			</td>
			<td width="328" colspan="6">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">――――――<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">455</span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">49</span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">64</span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">82</span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">650</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">合计<span></span></span>
				</p>
			</td>
			<td width="328" colspan="6">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">――――――<span></span></span>
				</p>
			</td>
			<td width="197" colspan="4">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">1300</span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">1300</span></b>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="99" rowspan="5">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">铜仁市民中<span></span></span>
				</p>
			</td>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">统招生<span></span></span>
				</p>
			</td>
			<td width="131" colspan="2">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">531</span>
				</p>
			</td>
			<td width="395" colspan="8">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">94</span></b>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">625</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">含体育艺术特长班（<span>3</span>个班）<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">配额生<span></span></span>
				</p>
			</td>
			<td width="131" colspan="2">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">总<span>438</span>，（碧江区<span>337</span>、万山区<span>101</span>）<span></span></span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">26</span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">17</span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">56</span>
				</p>
			</td>
			<td width="49">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">88</span>
				</p>
			</td>
			<td width="197" colspan="4">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">――――――<span></span></span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">625</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">合计<span></span></span>
				</p>
			</td>
			<td width="525" colspan="10">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">1300</span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">1300</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">普通高中民族班<span></span></span></b>
				</p>
			</td>
			<td width="525" colspan="10">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">面向全市招<span>50</span>人<span></span></span></b>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">50</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">省教育厅省民委委托办班<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">总计<span></span></span>
				</p>
			</td>
			<td width="525" colspan="10">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">1350</span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">1350</span></b>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="99" rowspan="5">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">铜仁市二中<span></span></span>
				</p>
			</td>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">统招生</span><span style="font-size:11pt;color:black;"></span>
				</p>
			</td>
			<td width="131" colspan="2">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">388</span>
				</p>
			</td>
			<td width="395" colspan="8">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">212</span></b>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">600</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">含特长生<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">配额生<span></span></span>
				</p>
			</td>
			<td width="131" colspan="2">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">总<span>600</span>，（碧江区<span>461</span>、万山区<span>139</span>）<span></span></span>
				</p>
			</td>
			<td width="395" colspan="8">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">――――――<span></span></span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">600</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">合计<span></span></span>
				</p>
			</td>
			<td width="525" colspan="10">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">1200</span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">1200</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">精准扶贫班<span></span></span></b>
				</p>
			</td>
			<td width="525" colspan="10">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">面向全市招<span>50</span>人<span></span></span></b>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">50</span>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">招收全市建档立卡贫困生<span></span></span>
				</p>
			</td>
		</tr>
		<tr>
			<td width="123">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">合计<span></span></span>
				</p>
			</td>
			<td width="525" colspan="10">
				<p style="text-align:center;" align="center">
					<span style="font-size:11pt;color:black;font-family:宋体;">1250</span>
				</p>
			</td>
			<td width="79">
				<p style="text-align:center;" align="center">
					<b><span style="font-size:11pt;color:black;font-family:宋体;">1250</span></b>
				</p>
			</td>
			<td width="108">
				<p style="text-align:left;" align="left">
					<span style="font-size:10pt;color:black;font-family:宋体;">　<span></span></span>
				</p>
			</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnNext" runat="server" Text="  我已了解今年招生计划  " OnClick="btnNext_Click"   Height="30"/>
                              
                              
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
 