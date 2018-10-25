<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSDAOForm.aspx.cs"
    Inherits="SincciKC.HSDAOForm" %>

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
        function MsgYes() {


            if (confirm('确定将您选择的考生预录到专业 ' + document.getElementById("ddlzy").value + " 吗?")) {
                return true;
            }
            else {
                return false;
            }
        }
        function opdg2(ID, Title) {
            window.parent.addTab2(Title, '/websystem/bmgl/xxglMange.aspx?ksh=' + ID + '&title=' + Title);
            return false;
        }
        //全选、全不选
        function SelectAll(obj) {
            //获取所有的input的元素
            var inputs = document.getElementsByTagName("input");

            for (var i = 0; i < inputs.length; i++) {
                //如果是复选框
                if (inputs[i].type == "checkbox") {
                    inputs[i].checked = obj.checked;
                }
            }
        }
        function opdg(ID, Title, type) {
            window.parent.addTab2(Title, '/websystem/zygl/Zyxxgl_Mange.aspx?ksh=' + ID + '&title=' + Title + '&type=' + type);
            return false;
        }
        function opUp(ID, Title) {
            ymPrompt.win({ message: 'XX_ytui.aspx?ksh=' + ID, width: 420, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function opLq(ID, Title) {
            ymPrompt.win({ message: 'XX_lq.aspx?ksh=' + ID, width: 420, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function meg() {
            ymPrompt.alert({ message: '正在导出数据，请稍候...<br /><img src="/images/busy.gif"/> ', title: '提示' });
            return false;
        }

        function hideTest(Title, pcdm, lqxx) {
            window.parent.addTab2(Title, '/websystem/zklq/Print_lq.aspx?pcdm=' + pcdm + '&lqxx=' + lqxx + '&title=' + Title);
            return false;
        }
        function hideTest2(Title, pcdm, lqxx) {
            window.parent.addTab2(Title, '/websystem/zklq/Print_shb.aspx?pcdm=' + pcdm + '&lqxx=' + lqxx + '&title=' + Title);
            return false;
        }
        function hideTest3(Title, type, lqxx) {
            window.parent.addTab2(Title, '/websystem/zklq/Tj_xx.aspx?type=' + type + '&lqxx=' + lqxx + '&title=' + Title);
            return false;
        }
        function addMask() {
            var str = "<div id='backgroud' class='mask-backgroud'>";
            str += "<div id='image' class='mask-image'></div>";
            str += "<div id='text' class='mask-text'>请稍等，正在导入数据。。。。。</div>"
            str += "</div>";

            $("body").append(str);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: center; vertical-align: bottom; height: 26px; font-size: 18px;
            font-weight: bold; padding-top: 8px" class="datagrid-toolbar">
            导入回收</div>
        <table border="0" cellpadding="0" cellspacing="0" width="98%">
             <tr>
                <td>
                    <div class="datagrid-toolbar">
            Excel数据导入<asp:FileUpload ID="fuExcelFileImport" runat="server" />
            <asp:Button ID="btnExcelFileImport" runat="server" Text="导入" CssClass="icon-reload btn"
                Width="60" OnClick="btnExcelFileImport_Click" OnClientClick="addMask();" />
            <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false"
                collapsible="false" minimizable="false" style="width: 385px; height: 270px;"
                runat="server">
            </div>
            <a href="../../Template/hsdao.xls">模板下载</a>
        </div>
                </td>
            </tr>
             <tr>
                <td>
                                      
         <asp:Button ID="btn_hs" runat="server" Text="  回收" CssClass="icon-cancel" onclick="btn_hs_Click"  OnClientClick="confirm('确定回收你选中的考生吗?回收后不可恢复!');"  />
                     
                </td>
            </tr>
             <tr>
                <td>
                 <div>
             <asp:GridView ID="repDisplay" runat="server" ForeColor="#333333" AutoGenerateColumns="False"
                                    AllowSorting="True" Font-Size="13px"  BorderWidth="1px"
                                    Width="100%" EmptyDataText="查询不到任何信息!" 
                 PageSize="20">

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
                                        <asp:TemplateField HeaderText="报名号" >
                                            <ItemTemplate>
                                                <%# Eval("ksh")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名">
                                            <ItemTemplate>
                                                <%# Eval("xm")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总分"  >
                                            <ItemTemplate>
                                                <%# Eval("cj")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="录取学校">
                                            <ItemTemplate>
                                               <%# Eval("lqxxmc")%>
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
                                          <asp:TemplateField HeaderText="类型"  >
                                            <ItemTemplate>
                                                 <%#Eval("types").ToString() == "2" ? "注册" : Eval("types").ToString() == "1" ? "申报" : Eval("types").ToString() == "0" && Eval("zczt").ToString() == "-1" ? "普通" : "注册"%>
                                              </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="状态"  >
                                            <ItemTemplate>
                                              <%#Eval("td_zt").ToString() == "2" ? "已发档" : Eval("td_zt").ToString() == "3" ? "预退" : Eval("td_zt").ToString() == "4" ? "预录" : Eval("td_zt").ToString() == "5" ? "录取" : Eval("td_zt").ToString() == "1" ? "已投" : "在库"%>
                                              </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                          <asp:TemplateField HeaderText="志愿详情">
                                            <ItemTemplate>
                                                 <a href="#" onclick='return opdg(<%#Eval("ksh")%>,"学生志愿填报信息详情",1);'>详情</a>
                                           </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="个人信息">
                                            <ItemTemplate>
                                                <a href="#" onclick='return opdg2(<%#Eval("ksh")%>,"考生信息详情");'>详情</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

       </div>
                </td>
            </tr>
           
        </table>
    
       
        <asp:HiddenField ID="hfDelIDS" runat="server" />
    </div>
    </form>
</body>
</html>
