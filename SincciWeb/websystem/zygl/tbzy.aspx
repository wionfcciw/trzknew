<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="tbzy.aspx.cs" Inherits="SincciKC.websystem.zygl.tbzy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考生志愿填报</title>
    
<meta content="IE=7" http-equiv="X-UA-Compatible" />
<link rel="stylesheet" type="text/css" href="/style.css" /> 
<script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../../css/subModal.css" /> 
<script src="../../js/common.js" type="text/javascript"></script>
<script type="text/javascript" src="../../js/subModal.js"></script>
 
<style type="text/css">
.btn
{
	 cursor: hand; 
 }
.tblcss
{
     text-align: center;  border-collapse: collapse; vertical-align: middle; border-style: solid;  
     
} 
 body,td,th {
font-size: 13px;
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
	width:150px;
	 color: Blue ;
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
	width:150px;
	 color:Blue ;
}
  
</style>
 <script language="javascript" type="text/javascript">

     function chilkSave() { //保存判断
         //           if (document.getElementById("xpcIs").value == "") {
         //               alert("保存失败,至少要填报一个学校!");
         //               return false;
         //           }
         var str = document.getElementById("xpcIs").value;
         if (str.length == 0) {
             alert("保存失败,至少填报一个类别！");
             return false;
         }

         var ispass = 0;
         for (var i = 0; i < str.split(",").length - 1; i++) {
             var ccc = document.getElementById(str.split(",")[i] + "_" + str.split(",")[i] + 1 + "_dm").value;
             // alert(str.split(",")[i]);
             //               if (str.split(",")[i].substring(0, 4) == "0684") { //海门除外
             //                   if (str.split(",")[i].substring(4) != "01") {
             //                       if (ccc.length == 0) {
             //                           ispass = 1;
             //                       }
             //                   }
             //               } else {
             //                   if (ccc.length == 0) {
             //                       ispass = 1;
             //                   }
             //               }
             if (str.split(",")[i].substring(4) == "01") {
                 var a1 = document.getElementById(str.split(",")[i] + "_" + str.split(",")[i] + 1 + "_dm").value;
                 var a2 = document.getElementById(str.split(",")[i] + "_" + str.split(",")[i] + 2 + "_dm").value;
                 var a3 = document.getElementById(str.split(",")[i] + "_" + str.split(",")[i] + 3 + "_dm").value;
                 if (a1.length == 0 && a2.length == 0 && a3.length == 0) {
                     ispass = 1;
                 }
             }
             else if ((str.split(",")[i].substring(4) == "12" || str.split(",")[i].substring(4) == "13") && str.split(",")[i].substring(0, 4) == "0684") { //海门
                 var a1 = document.getElementById(str.split(",")[i] + "_" + str.split(",")[i] + 1 + "_dm").value;
                 var a2 = document.getElementById(str.split(",")[i] + "_" + str.split(",")[i] + 2 + "_dm").value;
                 if (a1.length == 0 && a2.length == 0) {
                     ispass = 1;
                 }
             }
             else {
                 if (ccc.length == 0) {
                     ispass = 1;
                 }
             }
         }

         if (ispass == 1) {
             alert("保存失败,选择了报考类型时,至少要填报一个学校!");
             return false;
         }
         ispass = 0;
         for (var i = 0; i < str.split(",").length - 1; i++) {

             for (var j = 1; j < 7; j++) {
                 try {
                     var ccc = document.getElementById(str.split(",")[i] + "_" + str.split(",")[i] + j + "_zy0").value;
                     var bbb = document.getElementById(str.split(",")[i] + "_" + str.split(",")[i] + j + "_dm").value;
                     if (ccc.length == 0 && bbb.length != 0) {
                         ispass = 1;
                     }
                 } catch (e) {

                 }
             }
         }
         if (ispass == 1) {
             alert("保存失败,有学校专业未填报！");
             return false;
         }
     }
     function showPop(Title, url, width, height, returnFunc, showCloseBox, showScrolling, str) {//点击学校代码
         var shunx = 0;
         var xpcid = str.split("_")[0];
         var num = str.split("_")[1].substring(xpcid.length);
         var pcDm = xpcid.substring(4);


         if (num > 1) {
             //    alert(document.getElementById(xpcid + "_" + xpcid + (num - 1) + "_dm").value);

             if ((pcDm == "12" || pcDm == "13") && xpcid.substring(0, 4) == "0684") { //海门

             }
             else if (pcDm == "01") {


             }
             else {
                 if (document.getElementById(xpcid + "_" + xpcid + (num - 1) + "_dm").value == "") {
                     alert("请按顺序填报志愿!");
                     return;
                 }
             }

         }
         showPopWin(Title, url, width, height, MessageBox, showCloseBox, showScrolling);
     }
     function MessageBox(ReturnTxt) {//选择学校返回值
         //alert(ReturnTxt);
         var tdid = ReturnTxt;
         if (ReturnTxt.indexOf(";") == -1) { //
             //document.getElementById(tdid).innerText = " ";
             // document.getElementById(tdid + "_dm").innerText = "";
             //  document.getElementById(tdid + "_mc").innerText = " ";
             document.getElementById("isSave").value = "";
             for (var i = 0; i < 7; i++) {
                 try {
                     document.getElementById(tdid + "_zy" + i).innerText = "";
                 } catch (e) {
                     //alert(e+"1");
                     break;
                 }
             }
             var xpcid = tdid.split("_")[0]; //xpcid
             var zyid = tdid.split("_")[1];
             var num = zyid.substring(xpcid.length); //顺序号
             for (var i = num; i > 0; i++) {  //for清除信息
                 try {
                     //alert(xpcid + "_" + xpcid + num + "_mc");
                     document.getElementById(xpcid + "_" + xpcid + i + "_mc").innerText = "";
                     document.getElementById(xpcid + "_" + xpcid + i + "_dm").innerText = "";
                     for (var j = 0; j < 4; j++) {
                         try {
                             document.getElementById(xpcid + "_" + xpcid + i + "_zy" + j).innerText = "";
                             document.getElementById(xpcid + "_" + xpcid + i + "_zy" + j).disabled = "disabled";
                             document.getElementById(xpcid + "_" + xpcid + i + "_zy" + j).className = "input2";

                         } catch (e) {
                             break;
                         }
                     }
                     for (var j = 0; j < 4; j++) {
                         try {
                             document.getElementById(xpcid + "_" + xpcid + i + "_fc1").disabled = "disabled";
                             document.getElementById(xpcid + "_" + xpcid + i + "_fc2").disabled = "disabled";
                             document.getElementById(xpcid + "_" + xpcid + i + "_fc2").checked = "checked";
                         } catch (e) {
                             break;
                         }
                     }
                     for (var j = 0; j < 4; j++) {
                         try {
                             document.getElementById(xpcid + "_" + xpcid + i + "_xxfc1").disabled = "disabled";  //学校服从
                             document.getElementById(xpcid + "_" + xpcid + i + "_xxfc2").disabled = "disabled";
                             document.getElementById(xpcid + "_" + xpcid + i + "_xxfc2").checked = "checked";
                         } catch (e) {
                             break;
                         }
                     }
                 } catch (e) {
                     //alert(e);
                     break;
                 }
             }
         } else {
             var str = tdid.split(";")[2]; //ID
             var xpcid = str.split("_")[0]; //xpcid
             //alert(str + "a");
             // alert(xpcid + "b");

             var ispass = 0; //判断是否报考过相同学校
             //    alert(xpcid.substring(4));
             if (xpcid.substring(4) != "01") {
                 for (var i = 1; i > ispass; i++) {
                     try {
                         if (document.getElementById(xpcid + "_" + xpcid + i + "_dm").value == tdid.split(";")[0]) //查到已报考过学校
                             ispass = 1;
                     } catch (e) {
                         //alert(e + "3");
                         break;
                     }
                 }
             }
             if (ispass == 1) {
                 alert("您已填报过该学校!");
             }
             else {
                 document.getElementById(str + "_dm").innerText = tdid.split(";")[0];
                 document.getElementById(str + "_mc").innerText = tdid.split(";")[1];
                 document.getElementById("isSave").value = "Save";
                 //     alert(document.getElementById("isSave").value);
                 for (var i = 0; i < 7; i++) {
                     try {
                         document.getElementById(str + "_zy" + i).innerText = "";
                         document.getElementById(str + "_zy" + i).disabled = "";
                         document.getElementById(str + "_zy" + i).className = "input1";


                     } catch (e) {
                         //alert(e + "4");
                         break;
                     }
                 }
                 try {
                     document.getElementById(str + "_xxfc1").disabled = "";  //学校服从
                     document.getElementById(str + "_xxfc2").disabled = "";
                 } catch (e) {

                 }
                 try {
                     document.getElementById(str + "_fc1").disabled = "";
                     document.getElementById(str + "_fc2").disabled = "";
                 } catch (e) {

                 }
             }
         }
     }
     function MessageBoxZY(ReturnTxt) {  //获取选择专业返回值
         // alert(ReturnTxt);
         var tdid = ReturnTxt;
         if (ReturnTxt.indexOf(";") == -1) {
             document.getElementById(tdid).innerText = "";
             try {//删除后面的专业信息
                 var zhuanye = tdid.split("_")[0] + "_" + tdid.split("_")[1];
                 var zy = tdid.split("_")[2].substring(0, 2);
                 var num = tdid.split("_")[2].substring(2);
                 for (var i = (num + 1); i < 7; i++) {
                     document.getElementById(zhuanye + "_" + zy + i.substring(1)).innerText = "";
                 }

             } catch (e) {
                 // alert(e + "5");

             }
         } else {
             var str = tdid.split(";")[2]; //专业id全称
             var zhuanye = tdid.split(";")[0] + ":" + tdid.split(";")[1]; //名称
             var zyid = str.split("_")[0] + "_" + str.split("_")[1]; //专业id
             var zy = tdid.split("_")[2].substring(0, 2); //专业num
             var ispass = 0;
             for (var i = 0; i < 7; i++) {
                 try {
                     if (document.getElementById(zyid + "_" + "zy" + i).value == zhuanye)
                         ispass = 1;
                 } catch (e) {
                     // alert(e + "6");
                     break;
                 }
             }
             if (ispass == 0) {
                 document.getElementById(str).innerText = zhuanye;
             } else {
                 alert("您已填报该专业!");
             }
         }
     }
     //专业弹出
     function ShowPc(name, path, wh, he, fanhui, xxdm) {

         var str = document.getElementById(xxdm + "_dm").value;
         if (str == "") {
             alert("请先选择学校!");
         } else {
             var xpcid = path.split("=")[3];
             var num = xpcid.split("_")[2].substring(2);
             path = path + "&xxdm=" + str;
             if (num > 0) {
                 //    alert(document.getElementById(xpcid + "_" + xpcid + (num - 1) + "_dm").value);
                 if (document.getElementById(xxdm + "_zy" + (num - 1)).value == "") {
                     alert("请按顺序填报专业!");
                     return;
                 }
             }
             showPopWin(name + "--" + document.getElementById(xxdm + "_mc").innerText, path, wh, he, fanhui, true, true);
         }

     }
     function cheakbk(xpcid, type) { //是否报考控制

         //alert("test");
         var zyidSum = document.getElementById("pd_" + xpcid).value.split(",").length; //志愿数量
         var zyid = document.getElementById("pd_" + xpcid).value; //志愿 用 , 分割
         if (type == 1) {  //报考
             // disabled = "disabled"
             var xpcPass = document.getElementById("xpcIs").value; //小批次ID集合用于判断填报状态时.是否有选择学校
             xpcPass = xpcPass.replace(xpcid + ",", "");
             //  alert(xpcPass);
             document.getElementById("xpcIs").value = xpcPass + xpcid + ",";
             //   alert(document.getElementById("xpcIs").value);
             for (var i = 0; i < zyidSum; i++) {
                 document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_dm").disabled = "";
                 document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_dm").className = "input1";

                 //状态启用
                 //                for (var j = 0; j < 7; j++) {  
                 //                    try {
                 //                        document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_zy" + j).disabled = "";
                 //                        document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_zy" + j).className = "input1";
                 //                    } catch (e) {
                 //                    break;
                 //                    }
                 //                }
                 //                try {
                 //                    document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_fc1").disabled = "";
                 //                    document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_fc2").disabled = "";
                 //                } catch (e) { }
             }
         } else {
             var xpcPass = document.getElementById("xpcIs").value;
             document.getElementById("xpcIs").value = xpcPass.replace(xpcid + ",", "");
             //  alert(document.getElementById("xpcIs").value);
             for (var i = 0; i < zyidSum; i++) {
                 document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_dm").disabled = "disabled";
                 document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_dm").className = "input2";
                 document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_dm").innerText = "";
                 document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_mc").innerText = "";
                 for (var j = 0; j < 7; j++) {
                     try {
                         document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_zy" + j).disabled = "disabled";
                         document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_zy" + j).className = "input2";
                         document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_zy" + j).innerText = "";
                     } catch (e) {
                         //alert(e + "7");
                         break;
                     }
                 }
                 try {
                     document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_fc1").disabled = "disabled";
                     document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_fc2").disabled = "disabled";
                     document.getElementById(xpcid + "_" + zyid.split(",")[i] + "_fc2").checked = "checked";

                 } catch (e) {
                     //alert(e + "8");

                 }
             }
         }
     }
   </script>
</head>
<body>
    <form id="form1" runat="server">
    
    
             <div  style="border:5px solid #DFF3FE; background-color:#DFF3FE; margin-left:10px ; padding:2px; width:855px" >
                <div  style="background-color:#FFFFFF">
                <div  class="tbltitle" ><asp:Label ID="lblKaocimc" runat="server" Text=""></asp:Label>铜仁市中招考生志愿表(<asp:Label ID="lblxqmc" runat="server" Text=""></asp:Label>)</div>
                 <table  width="850" border="0" style="border-collapse: collapse;" align="center" cellpadding="0" cellspacing="0">
                     <tr  >
                      <td>
                       <table><tr>
                        <td align="right" width="80" ><b>姓名：</b></td>
                        <td align="left"   width="120" ><asp:Label ID="lblXm" runat="server" Text=""></asp:Label> </td>
                         <td align="right"  width="80"><b>报名号：</b></td>
                        <td  align="left"  width="180" ><asp:Label ID="lblKsh" runat="server" Text=""></asp:Label> </td>
                        <td align="right"   width="90"><b>毕业中学名称：</b></td>
                        <td  align="left" ><asp:Label ID="lblBmdmc" runat="server" Text=""></asp:Label> </td>
                       
                       </tr></table> </td>
                     </tr> 
                     <tr>
                         <td >
                             <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                 <tr>
                                     <td  >
                                         <span id="zyspan" runat="server"></span>
                                     </td >
                                 </tr>
                                 <tr>
                                     <td>
                                     </td>
                                 </tr>
                             </table> 
                         </td>
                     </tr>
                     <tr><td >
                              <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0"  style=" border:1px solid #95DDFF ;border-collapse:collapse" >
                                <tr> 
                                    <td align="right" style="width:170px;color:red" valign="top" > 提示：</td>
                                  <td height="55" align="left" > 
                                  <span style="color:red">1、请仔细检查你填写的资料，如确认无误请按下面的“保存”按钮。 
                                  <br/>2、资料保存后如不需要修改，请点“确认”按钮，一经确认资料就不能再修改。</span> 
                                 </td>
                                </tr>
                            </table>
                     </td></tr>
                     <tr>
                         <td align="center" >
                             <input id="isSave" type="hidden" />
                                <input id="xpcIs" type="hidden" />
                         
                         <asp:Button ID="btnSave" OnClientClick="return chilkSave();" runat="server" Text=" 保存 " CssClass="register" OnClick="btnSave_Click" />
                   



                         <%--  &nbsp;&nbsp; &nbsp;&nbsp;
                             <asp:Button ID="btnBack" runat="server" CssClass="register" Text=" 返 回 " 
                                 onclick="btnBack_Click"  />--%>
                         </td>
                     </tr>
                 </table>
             </div>
             </div>
        
    </form>
</body>
</html>