<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZXLQXXWebForm.aspx.cs"
    Inherits="SincciKC.ZXLQXXWebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css"  />
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <link href="../../easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function checkInput() {
            if ($("#txtksh").val() == "") {
                $("#txtksh").focus();
                alert("请输入报名号!");
                return false;
            }
            if ($("#txtyy").val() == "") {
                $("#txtyy").focus();
                alert("请输入注销原因!");
                return false;
            }
        }

        function addMask() {
            var str = "<div id='backgroud' class='mask-backgroud'>";
            str += "<div id='image' class='mask-image'></div>";
            str += "<div id='text' class='mask-text'>请稍等，正在导入数据。。。。。</div>"
            str += "</div>";

            $("body").append(str);
        }
        function MsgYes() {
           
           
            if (confirm('确定将您输入的考生注册到专业 ' + document.getElementById("ddlzy").value+ " 吗?")) {
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
            考生录取注销</div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 98%;">
      <%--      <tr>
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
                           
                            </td>
                        </tr>

                     
                        <tr style="height: 45px;">
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: left; width: 75%;">
                                      报名号:  
                              <asp:TextBox ID="txtksh" runat="server"></asp:TextBox>
                                注销原因:  
                              <asp:TextBox ID="txtyy" runat="server" Width="200px"></asp:TextBox>
                        
                                        </td>
                                        
                                    </tr>
                                    <tr><td>
                                      <asp:Button ID="btn_beginTd" runat="server" Text="添加" CssClass="icon-reload"
                                                OnClick="btn_beginTd_Click" Width="80px"  OnClientClick="return checkInput()" />
                                            &nbsp;
                                            <asp:Button ID="btnCancel_Td" runat="server" Text="删除" CssClass="icon-reload" 
                                                Width="80px" onclick="btnCancel_Td_Click"    />
                                          
                                            &nbsp;
                                            <asp:Button ID="btnPl_Fd"   runat="server" Text="提交" CssClass="icon-reload"
                                                Width="80px" onclick="btnPl_Fd_Click" />
                                            &nbsp;
                                         
                                    </td> </tr>
                                </table>
                            </td>
                        </tr>
                        
                      
                        
                        <tr>
                            <td style="text-align: left; vertical-align: top; width: 100%; height: 350px;">
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
                                        
                                         
                                       
                                        <asp:TemplateField HeaderText="录取专业" SortExpression="lqzy">
                                            <ItemTemplate>
                                                 <%# Eval("lqzy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="注销原因"  >
                                            <ItemTemplate>
                                                 <%# Eval("zxyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                  
                                    </Columns>
                                </asp:GridView>
                       <%--         <asp:Repeater ID="repDisplay" runat="server" ViewStateMode="Disabled">
                                    <HeaderTemplate>
                                        <table id="tab_Repeater" border="1" style="border-collapse: collapse; width: 100%;"
                                            cellpadding="2" cellspacing="0">
                                            <tr id="Tr1" class="datagrid-header" runat="server" style="height: 26px">
                                                <td align="center">
                                                    <b>
                                                        <input type="checkbox" name="checkboxid" id="checkboxid" onclick="SelectAll(this);" />全选</b>
                                                </td>
                                               <td   align="center">
                                                    <b>序号</b>
                                                </td>
                                                <td   align="center">
                                                    <b>报名号</b>
                                                </td>
                                                <td  align="center">
                                                    <b>姓名</b>
                                                </td>
                                                <td align="center">
                                                    <b>总分</b>
                                                </td>
                                                 
                                                <td align="center">
                                                    <b>录取专业</b>
                                                </td>
                                                <td align="center">
                                                    <b>类型</b>
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                                            <td align="center">
                                                 <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("ksh") %>'>
                                            </td>
                                             <td align="center">
                                                <%# Eval("序号")%>
                                            </td>
                                           <td align="center">
                                                <%# Eval("ksh")%>
                                            </td>
                                            <td align="center">
                                                <%# Eval("xm")%>
                                            </td>
                                            
                                            <td align="center">
                                                <%# Eval("cj")%>
                                            </td>
                                            <td align="center">
                                                <%# Eval("lqzy")%>
                                            </td>
                                            <td align="center">
                                             注册  </td>
                                            
                                           
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    Excel数据导入<asp:FileUpload ID="fuExcelFileImport" runat="server" />
                                    <asp:Button ID="btnExcelFileImport" runat="server" Text="  导入数据" OnClientClick="addMask();"
                                        CssClass="icon-reload btn" OnClick="btnExcelFileImport_Click" />
                                    <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false"
                                        collapsible="false" minimizable="false" style="width: 385px; height: 270px;"
                                        runat="server">
                                    </div>
                                    <a href="../../Template/zxlq.xls">模板下载</a>
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
