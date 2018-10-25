<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YcZdKzxWebForm.aspx.cs"
    Inherits="SincciKC.YcZdKzxWebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function GetCheckBoxListValue() {
            var v = new Array();
            var v1 = new Array();
            var CheckBoxList = document.getElementById("cbl_XqXx");
            if (CheckBoxList.tagName == "TABLE") {
                for (i = 0; i < CheckBoxList.rows.length; i++) {
                    for (j = 0; j < CheckBoxList.rows[i].cells.length; j++) {
                        if (CheckBoxList.rows[i].cells[j].childNodes[0]) {
                            if (CheckBoxList.rows[i].cells[j].childNodes[0].checked == true) {
                                v1.push(CheckBoxList.rows[i].cells[j].childNodes[0].value);
                                //v.push(CheckBoxList.rows[i].cells[j].childNodes[1].innerText);
                            }
                        }
                    }
                }
            }
            document.getElementById("hf_HbXqdm").value = v1;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: center; vertical-align: bottom; height: 26px; font-size:18px; font-weight:bold; padding-top:8px" class="datagrid-toolbar"> 预测最低控制线 
        </div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr style="height: 45px;">
                <td align="right" style="width: 120px;">
                    预测方式:
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="30px" RepeatDirection="Horizontal" Enabled="false"
                        Width="153px">
                        <asp:ListItem Selected="True" Value="0">计划</asp:ListItem>
                        <asp:ListItem Value="1">学校</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;">
                    县区合并:
                </td>
                <td>
                    <asp:CheckBoxList ID="cbl_XqXx" runat="server" Height="25px" Width="45%" RepeatColumns="3"
                        onclick="GetCheckBoxListValue()" RepeatDirection="Horizontal" RepeatLayout="Table">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr >
                <td style="text-align:center; height:29px"   class="datagrid-toolbar"  colspan="3"> 
                    <asp:Button ID="btnStartCount" runat="server" Text="开始计算" CssClass="btnexit" OnClick="btnStartCount_Click" />
                </td>
            </tr>
            <tr><td style="height:20px"></td><td></td></tr>
            <tr>
                <td >
                   <div style="text-align: center; vertical-align: bottom; height: 24px; 
                       font-size:16px; font-weight:bold; padding-top:8px" >设置县区预测比例及招生计划数 </div>
                     
                </td>
                <td>
                </td>
                <td  >
                    <div style="text-align: center; vertical-align: bottom; height: 24px; 
                       font-size:16px; font-weight:bold; padding-top:8px" >预测最低控制线结果 </div>
                      
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top; width: 50%; padding-top:1px">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="text-align: left; vertical-align: top;">
                                <asp:GridView ID="gvXqYcBlSz" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnRowDataBound="gvXqYcBlSz_RowDataBound" OnRowCancelingEdit="gvXqYcBlSz_RowCancelingEdit"
                                    OnRowEditing="gvXqYcBlSz_RowEditing" OnRowUpdating="gvXqYcBlSz_RowUpdating">
                                    <HeaderStyle Height="25px" CssClass="datagrid-header" />
                                    <RowStyle Height="30px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="45px">
                                            <ItemTemplate >
                                                <%# Eval("serial") %>
                                            </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="县区名称" >
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("yc_Xqdm")%>'></asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("yc_XqMc")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="yc_ZsJhx" HeaderText="计划数" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="yc_Bl" HeaderText="预测比例(%)" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:CommandField ShowEditButton="True"  HeaderText="编辑" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#999966" BorderStyle="Outset" BorderWidth="2px" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 8px;">
                </td>
                <td style="text-align: left; vertical-align: top; padding-top:1px">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                        <asp:GridView ID="gvZdKzs" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Height="25px" CssClass="datagrid-header" />
                            <RowStyle Height="30px" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" HeaderStyle-HorizontalAlign="Center"  ItemStyle-Width="65px">
                                    <ItemTemplate >
                                        <%# Eval("serial")%>
                                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                    <ItemStyle Width="65px" HorizontalAlign="Center" ></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="县区名称">
                                    <ItemTemplate>
                                        [<%# Eval("yc_Xqdm")%>]&nbsp;<%# Eval("yc_XqMc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="最低控制线" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("yc_ZdFensuKzx")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="预测比例(%)" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("yc_Bl")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="预测方式" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("yc_Fangsi")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#999966" BorderStyle="Outset" BorderWidth="2px" />
                        </asp:GridView>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hf_HbXqdm" runat="server" />
    </form>
</body>
</html>
