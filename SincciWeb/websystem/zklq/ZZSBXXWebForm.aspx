<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZZSBXXWebForm.aspx.cs"
    Inherits="SincciKC.ZZSBXXWebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
 
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function MsgYes() {
           
           
            if (confirm('确定将您选择的考生预录到专业 ' + document.getElementById("ddlzy").value+ " 吗?")) {
                return true;
            }
            else {
                return false;
            }
        }

        var prev = null;
        function RowClick(obj) {
            var chkColl = obj.all;
            obj.style.backgroundColor = '';
            prev = obj;
            for (var i = 0; i < chkColl.length; i++) {
                if (chkColl[i].type == "checkbox") {
                    if (chkColl[i].checked) {
                        chkColl[i].checked = false;
                        prev.style.backgroundColor = '';
                    } else {
                        chkColl[i].checked = true;
                        prev.style.backgroundColor = '#EEF5FF';
                    }
                }
            }
        }
        //全选、全不选
        function SelectAll(obj) {
            //获取所有的input的元素
            var inputs = document.getElementsByTagName("input");
            var elem = document.getElementById('<%= repDisplay.ClientID%>').getElementsByTagName('input');

            for (var i = 0; i < inputs.length; i++) {
                //如果是复选框
                if (inputs[i].type == "checkbox") {
                    inputs[i].checked = obj.checked;
                    try {
                        if (inputs[i].checked) {
                            if (inputs[i].id != "checkboxid")
                                elem[i - 9].parentNode.parentNode.style.backgroundColor = '#EEF5FF';
                        }
                        else {
                            if (inputs[i].id != "checkboxid")
                                elem[i - 9].parentNode.parentNode.style.backgroundColor = '';
                        }
                    } catch (e) {
                         
                    }
                }
            }
        }
        function opdg(pcdm, xqdm, xxdm, title) {
            window.parent.addTab2(title, '/websystem/zklq/ByXxLqInfo.aspx?pcdm=' + pcdm + '&xqdm=' + xqdm + '&xxdm=' + xxdm);
            return false;
        }
        function opUp(ID, Title) {
            ymPrompt.win({ message: 'XX_ytui.aspx?ksh=' + ID, width: 420, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function meg() {
            ymPrompt.alert({ message: '正在导出数据，请稍候...<br /><img src="/images/busy.gif"/> ', title: '提示' });
            return false;
        }  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: center; vertical-align: bottom; height: 26px; font-size: 18px;
            font-weight: bold; padding-top: 8px" class="datagrid-toolbar">
            自主申报</div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 98%;">
     <%--       <tr>
                <td>
                    <table>
                        <tr>
                            <td style=" text-align: right;">
                                当前批次:
                            </td>
                            <td style="  text-align: left;">
                                <asp:DropDownList ID="ddlXpcInfo" runat="server"  AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlXpcInfo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                          
                        </tr>
                    </table>
                </td>
            </tr>--%>
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <table border="1" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse;
                        height: 400px;">
                           <tr style="height: 25px;">
                            <td  >
                             未处理的批次:  <asp:DropDownList ID="ddl_wcl" 
                                    runat="server" onselectedindexchanged="ddl_wcl_SelectedIndexChanged" 
                                    AutoPostBack="True"   >
                                </asp:DropDownList>          
                            </td>
                        </tr>
                    
                    <tr style="height: 45px;">
                            <td>
                                         <asp:Button ID="btn_beginTd" runat="server" Text="预录" CssClass="icon-reload"
                                                OnClick="btn_beginTd_Click" Width="80px"    />
                                            &nbsp;   录取专业:  <asp:DropDownList ID="ddlzy" runat="server"   >
                                </asp:DropDownList>
                                          
                                          &nbsp;
                                            <asp:Button ID="btnPl_Fd"   runat="server" Text="提交" CssClass="icon-reload"
                                                Width="80px" onclick="btnPl_Fd_Click" />
                              
                                          
                            </td>
                        </tr>
                          <tr style="height: 25px;">
                            <td  >
                            <asp:Button ID="btnCancel_Td" runat="server" Text="预退" CssClass="icon-reload" 
                             Width="80px" onclick="btnCancel_Td_Click"     />
                                   &nbsp;        
                              预退原因:<asp:TextBox ID="txtyij" runat="server" Width="180px"></asp:TextBox>         
                            </td>
                        </tr>
                      
                        
                        <tr>
                            <td style="text-align: left; vertical-align: top; width: 100%; height: 350px;">
                              <div id="mess_box" style=" height:400px; OVERFLOW-y:auto;"> 
                                <asp:GridView ID="repDisplay" runat="server" ForeColor="#333333" AutoGenerateColumns="False"
                                    AllowSorting="True"   OnSorting="repDisplay_Sorting" Font-Size="13px"  BorderWidth="1px"
                                    Width="100%" onrowdatabound="repDisplay_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E9EBEF" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle  HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <input type="checkbox" name="checkboxid" id="checkboxid" onclick="SelectAll(this);" />全选</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("ksh") %>'>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="报名号" SortExpression="ksh">
                                            <ItemTemplate>
                                                <%# Eval("ksh")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名">
                                            <ItemTemplate>
                                                <%# Eval("xm")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总分" SortExpression="cj">
                                            <ItemTemplate>
                                                <%# Eval("cj")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="志愿顺序">
                                            <ItemTemplate>
                                                <%# Eval("zysx")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="专业1" SortExpression="zy1">
                                            <ItemTemplate>
                                                <%# Eval("zy1mc")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="专业2" SortExpression="zy2">
                                            <ItemTemplate>
                                                <%# Eval("zy2mc")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="专业服从" SortExpression="zyfc">
                                            <ItemTemplate>
                                                <%#Eval("zyfc").ToString() == "True" ? "服从" : "不服从"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="状态" SortExpression="xx_zt">
                                            <ItemTemplate>
                                              <%#Eval("xx_zt").ToString() == "2" ? "已发档" : Eval("xx_zt").ToString() == "3" ? "预退" : Eval("xx_zt").ToString() == "4" ? "预录" : Eval("xx_zt").ToString() == "5" ? "录取" : "已投"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="录取专业">
                                            <ItemTemplate>
                                                <%# Eval("lqzymc")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="预退原因">
                                            <ItemTemplate>
                                                <%# Eval("xxbz")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="审核意见">
                                            <ItemTemplate>
                                                <%# Eval("xqbz")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
