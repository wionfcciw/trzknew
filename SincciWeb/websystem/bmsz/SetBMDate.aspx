<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetBMDate.aspx.cs" Inherits="SincciKC.websystem.bmsz.SetBMDate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置报名时间</title>
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
             
              ymPrompt.alert({ message: '开始时间不能为空！', title: '提示', winPos: [200, 5] });
              setTimeout(function () { ymPrompt.close(); }, 2000);
              //  alert("开始时间不能为空");
            
              return false;
          }
          if (strend.length == 0) {

              ymPrompt.alert({ message: '结束时间不能为空！', title: '提示', winPos: [200, 5] });
              setTimeout(function () { ymPrompt.close(); }, 2000);
              // alert("结束时间不能为空");
             
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

          // if (sdate.getTime() > edate.getTime()) {
          //   alert("开始时间不能大于结束时间");
          // return false;
          //  }

      }
</script>
    <style type="text/css">
        .btn
        {
            cursor: hand;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="tb" class="datagrid-toolbar">
        &nbsp; 区县名称：<asp:DropDownList ID="ddlxqdm" runat="server"  >
        </asp:DropDownList>
         &nbsp;
      开始时间：<asp:TextBox  ID="StartTime"  CssClass="searchbox" onFocus="WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"  runat="server"></asp:TextBox> &nbsp;
      结束时间：<asp:TextBox  ID="EndTime" CssClass="searchbox"  onFocus="WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"  runat="server"></asp:TextBox>
        <asp:Button ID="btnSetDate" runat="server" Text="  设置" CssClass="icon-ok" 
            onclick="btnSetDate_Click"  OnClientClick="return checknull(); "  />
    </div>
    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
        cellpadding="2" cellspacing="0">
        <asp:Repeater ID="repDisplay" runat="server"  >
            <HeaderTemplate>
                <tr class="datagrid-header" style="height: 26px">
                    
                    <td>
                        <b>区县</b>
                    </td>
                   
                    <td>
                        <b>开始时间</b>
                    </td>
                    <td>
                        <b>结束时间</b>
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                     
                    <td align="left">
                        <%# Eval("xqdm") %>
                    </td>
                   
                     <td>
                        <%# Eval("kssj")%>
                    </td>
                     <td>
                        <%# Eval("jssj")%>
                    </td>
                    
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
      <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px; padding-top:10px; color:#ff0000" >
      注：设置报名开始时间和报名结束时间必须在大市规定时间范围内。
      </div>
    </form>
</body>
</html>
