<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bmgl_bktj_Print.aspx.cs" Inherits="SincciKC.websystem.bmgl.bmgl_bktj_Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
              <div id="noprint" align="center"   >
         <asp:Button ID="btndaoc" runat="server" Text="导出PDF" onclick="btndaoc_Click" Visible="false" />
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
