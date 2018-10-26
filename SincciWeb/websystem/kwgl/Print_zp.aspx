<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_zp.aspx.cs" Inherits="SincciKC.websystem.kwgl.Print_zp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试证打印</title>
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../css/page.css"  />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="tb" runat="server" class="datagrid-toolbar" >
          &nbsp;&nbsp;
           <asp:Button ID="Export" runat="server" Text="导出PDF"   onclick="Export_Click" /> <font color="red">注：如果在线打印慢，可以选择导出PDF在本地打印。</font>

    </div> 
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="510px" 
            ShowBackButton="False" ShowFindControls="False" ShowRefreshButton="False" 
            ShowZoomControl="False" Width="100%" ShowExportControls="False">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
