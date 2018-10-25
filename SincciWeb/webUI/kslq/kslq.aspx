<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kslq.aspx.cs" Inherits="SincciKC.webUI.kslq.kslq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考生录取信息查询</title>    
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />
    <script src="../../js/jquery-1.8.3.min.js" type="text/javascript"></script>
      <script type="text/javascript">
          function Load() {
              if (document.cookie == "") {
                  document.cookie = "num=" + 60;
              } else {
                  time(document.getElementById("btn"));
              }
          }
          function time(o) { 
              var key = $("#key").val();
              var wait = document.cookie.split(";")[0].split("=")[1];
              if (wait == "60") {
                  if (key.length == 0) {
                    //  alert("请先输入您的准考证号!");
                      return false;
                  }
                  var pcdm = $("#hfdpcdm").val();
                  var xxdm = $("#hfdxxdm").val();
                  $.post("kqlqtj.ashx?action=selectLoad&zkzh=" + key + "&pcdm=" + pcdm + "&xxdm=" + xxdm,
                function (data) {
                    var data = eval('(' + data + ')');
                    if (data.Status == "success") {
                        if (data.Type == "1") {
                            $("#tab_Repeater1").hide();
                            $("#tab_Repeater2").show();
                            $("#tab_Repeater2 tr:not(:first)").remove();
                            var datas = data.Dt[0];
                            var td_zt = "<font color='red'>" + (datas["td_zt"] == "5" ? "已被录取" : "尚未录取" )+ "</font>";
                            var lqtime = datas["lqtime"] == null ? "" : datas["lqtime"];
                            var sftzs = datas["sftzs"] == "1" ? (datas["pcdm"] == "01" ? (xxdm == "045" ? "单独招生" : "精准扶贫、民族生") : "统招生") : (datas["sftzs"] == "2" ? "配额生" : (datas["sftzs"] == "3" ? "配转统" : (datas["sftzs"] == "4" ? "特长生" : "补录")));
                            var temp = "<tr  style='height: 24px' id='trtab1' >"
                                   + "<td>" + datas["lqxx"] + "</td>"
                                   + "<td>" + datas["zsxxmc"] + "</td>"
                                   + "<td>" + sftzs + "</td>"
                                     + "<td>" + td_zt + "</td>"
                                   + "<td>" + lqtime + "</td>"
                                   + "</tr>";
                            $("#tab_Repeater2").append(temp);
                        } else {

                            $("#tab_Repeater2").hide();
                            $("#tab_Repeater1").show();
                            $("#tab_Repeater1 tr:not(:first)").remove();
                            var pesfsx = data.Msg;
                            var fs = 0; //判断配转统是否在线上
                            var cj = data.Cj;
                            if (pesfsx != null) {
                                var array = pesfsx.split(";");
                                for (var i = 0; i < array.length; i++) {
                                    var s1 = data.Pzt;
                                    if (array[i].split("=")[0] == s1) {
                                        fs = parseInt(array[i].split("=")[1]);
                                    }
                                }
                            }
                            for (var i = 0; i < data.Dt.length; i++) {
                                var datas = data.Dt[i];
                                var td_zt = datas["td_zt"] == "5" ? "已被录取" : "尚未录取";
                                var lqtime = datas["lqtime"] == null ? "" : datas["lqtime"];
                                var pm = "第<font color='red'>" + datas["pm"] + "</font>名" + (parseInt(datas["pm"]) <= parseInt(datas["jhs"]) ? (datas["xxdm"].length == 3 ? (cj >= fs ? "（计划内）" : "（计划外）") : "") : (datas["xxdm"].length == 3 ? "（计划外）" : ""));
                                var temp = "<tr  style='height: 24px' id='trtab1' >"
                                       + "<td>" + datas["xxdm"] + "</td>"
                                       + "<td>" + datas["zsxxmc"] + "</td>"
                                       + "<td>" + datas["xpcmc"] + "</td>"
                                       + "<td>" + pm + "</td>"
                                       + "<td>" + datas["lrsj"] + "</td>"
                                         + "<td>" + td_zt + "</td>"
                                       + "<td>" + lqtime + "</td>"
                                       + "</tr>";
                                $("#tab_Repeater1").append(temp);
                            }
                        }

                    } else if (data.Status == "man") {
                        $("#tab_Repeater1").hide();
                        $("#tab_Repeater2").hide();
                        alert(data.Msg);
                    } else {
                        $("#tab_Repeater1").hide();
                        $("#tab_Repeater2").hide();
                        document.cookie = "num=" + 0;
                        alert(data.Msg);
                    
                    }
                });
              }
              if (wait == "0") {
                  o.removeAttribute("disabled");
                  o.value = "查询"; document.cookie = "num=" + 60;
              }
              else {
                  o.setAttribute("disabled", true);
                  o.value = "查询(" + wait + ")";
                  wait--;
                  document.cookie = "num=" + wait;
                  setTimeout(function ()
                  { time(o) }, 1000)
              }
          }
      </script>
</head>
<body onload="Load()">
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
                                <asp:HiddenField ID="hfdpcdm" runat="server" />
                                <asp:HiddenField ID="hfdxxdm" runat="server" />
                                <asp:Label Text="0" runat="server" ID="sfload" Visible="false"  />
                                    <asp:Label Text="" runat="server" ID="lblpc" Visible="false" />
                                 <asp:Label Text="" runat="server" ID="lblxxdm" Visible="false" />
                                  <asp:label id="lblksh" runat="server" text="" visible="false"></asp:label>
                             
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border: 1px solid #95DDFF; border-collapse: collapse">
                                                <tr>
                                                    <td align="center">
                                                        <div runat="server" id="div1">
                                                            请输入您的准考证号：<input type="text" id="key"  style="width:100px;height:20px"/>
                                                            <input type="button" id="btn" value="查询" onclick="time(this);"  style="width:60px;height:30px" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td height="55" align="center">
                                                        <table class="tblcss" id="tab_Repeater1" border="1" bordercolor="#C1DAD7" style="text-align: center; display:none" cellspacing="0">
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
                                                        </table>

                                                           <table class="tblcss" id="tab_Repeater2" border="1" bordercolor="#C1DAD7" style="text-align: center; display:none" cellspacing="0">
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
                                                        </table>
                                                        <asp:Label Text="" runat="server" Visible="false" ID="lblshow" ForeColor="Red" />
                                                         <asp:Label Text="您尚未填报志愿，暂无数据!" runat="server" ID="lblbsj" ForeColor="Red" Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                
                                           
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
 