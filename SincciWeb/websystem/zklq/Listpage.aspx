<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listpage.aspx.cs" Inherits="SincciKC.websystem.zklq.Listpage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>区县管理</title>

    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
 
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
        &nbsp;<input type="checkbox" runat="server" id="ischeked" />是否分县区
        <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
    </div>
    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
        cellpadding="2" cellspacing="0">
        <asp:Repeater ID="repDisplay" runat="server"   >
            <HeaderTemplate>
                <tr class="datagrid-header" style="height: 26px">
              
                    <td>
                        <b>区县代码</b>
                    </td>
                    <td>
                        <b>招生学校代码</b>
                    </td>
                        <td>
                        <b>剩余计划数</b>
                    </td>
                 
                 
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                  
                    <td> 
                        <%# Eval("xqdm") %>
                    </td>
                    <td>
                        <%# Eval("zsxxdm") %>
                    </td>
                    <td>
                        <%# Eval("f_zbssl")%>
                    </td>
                 
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div id="tbpage" class="datagrid-toolbar" style="padding-left: 10px">

    </div>
   
    </form>
</body>
</html>
