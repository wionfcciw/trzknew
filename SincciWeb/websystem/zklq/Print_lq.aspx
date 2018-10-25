<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_lq.aspx.cs" Inherits="SincciKC.websystem.zklq.Print_lq" %>


 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    
  <script type="text/javascript" language="javascript">
      function checknull() {

          var strat = document.getElementById("StartTime").value;
          var strend = document.getElementById("EndTime").value;

          if (strat.length == 0) {
              if (strend.length > 0) {

                  ymPrompt.alert({ message: '开始时间不能为空！', title: '提示', winPos: [200, 5] });
                  setTimeout(function () { ymPrompt.close(); }, 2000);
                  //  alert("开始时间不能为空");
                  return false;
              } else {
                  return true;
              }
          } else {
              if (strend.length == 0) {

                  ymPrompt.alert({ message: '结束时间不能为空！', title: '提示', winPos: [200, 5] });
                  setTimeout(function () { ymPrompt.close(); }, 2000);
                  return false;
              }
              var d1, d2, s, arr, arr1, arr2;
              if (strat.length > 10) {

                  arr = strat.split(" ");
                  arr1 = arr[0].split("-");
                  arr2 = arr[1].split(":");
                  d1 = new Date(arr1[0], arr1[1] - 1, arr1[2], arr2[0], arr2[1], arr2[2]);

              }
              if (strend.length > 10) {
                  arr = strend.split(" ");
                  arr1 = arr[0].split("-");
                  arr2 = arr[1].split(":");
                  d2 = new Date(arr1[0], arr1[1] - 1, arr1[2], arr2[0], arr2[1], arr2[2]);
              }
              s = d2 - d1;
              if (s < 0) {
                  ymPrompt.alert({ message: '开始时间不能大于结束时间！', title: '提示' });
                  //  alert("开始时间不能大于结束时间");
                  return false;
              }
          }
      }
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div> 开始时间：<asp:TextBox  ID="StartTime"  CssClass="searchbox" onFocus="WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"  runat="server"></asp:TextBox> &nbsp;
      结束时间：<asp:TextBox  ID="EndTime" CssClass="searchbox" onFocus="WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"  runat="server"></asp:TextBox>
        <asp:Button ID="btn_sel" runat="server" Text="查询" 
            OnClientClick="return checknull(); " onclick="btn_sel_Click" />
    </div>
     <div id="tb" runat="server"  >
    </div>
    <div id="pdf" runat="server" >
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
              <div id="noprint" align="center"   >
         <asp:Button ID="btndaoc" runat="server" Text="导出PDF" onclick="btndaoc_Click"  />
         <div align="center" runat="server" id="Message"> </div>
         </div>
          <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="450px" 
            ShowBackButton="False" ShowFindControls="False" ShowRefreshButton="False" 
            ShowZoomControl="False" Width="100%" ShowExportControls="False">
        </rsweb:ReportViewer>
    </div>
        
    </form>
</body>
</html>
