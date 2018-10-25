<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KSGJForm.aspx.cs"
    Inherits="SincciKC.KSGJForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><html xmlns="http://www.w3.org/1999/xhtml"><head id="Head1" runat="server"><title></title><link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" /><link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" /><link rel="stylesheet" type="text/css" href="../../css/page.css" /><script src="../../js/addTableListener.js" type="text/javascript"></script><script src="../../prompt/ymPrompt.js" type="text/javascript"></script><link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
 </head><body><form id="form1" runat="server">
    <div>
        <div style="text-align: center; vertical-align: bottom; height: 26px; font-size: 18px;
            font-weight: bold; padding-top: 8px" class="datagrid-toolbar">
            考生轨迹</div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 98%;">
    
               
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <table border="1" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse;
                        height: 400px;">
                        
                     
                      
                        
                        <tr>
                            <td style="text-align: left; vertical-align: top; width: 100%; height: 350px;">
                              <asp:GridView ID="repDisplay" runat="server" ForeColor="#333333" AutoGenerateColumns="False"
                                       Font-Size="13px"  BorderWidth="1px"
                                    Width="100%"  >
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E9EBEF" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle   HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    <Columns>
                                       
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
                                        <asp:TemplateField HeaderText="操作员">
                                            <ItemTemplate>
                                                <%# Eval("username")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="状态"  >
                                            <ItemTemplate>
                                            <%#Eval("type").ToString() == "2" ? "取消" : Eval("type").ToString() == "3" ? "预退" : Eval("type").ToString() == "4" ? "预录" : Eval("type").ToString() == "5" ? "录取" : Eval("type").ToString() == "1" ? "填报" : "已投"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="填报批次">
                                            <ItemTemplate>
                                                <%# Eval("xpcMc")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="填报学校">
                                            <ItemTemplate>
                                                <%# Eval("zsxxmc")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作时间">
                                            <ItemTemplate>
                                                <%# Eval("times")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                         
                                      
                                    </Columns>
                                </asp:GridView>
                            <%--    <asp:Repeater ID="repDisplay" runat="server" ViewStateMode="Disabled">
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
                                                    <b>志愿顺序</b>
                                                </td>
                                                <td align="center">
                                                    <b>专业1</b>
                                                </td>
                                                <td align="center">
                                                    <b>专业2</b>
                                                </td>
                                                <td align="center">
                                                    <b>是否专业服从</b>
                                                </td>
                                                <td align="center">
                                                    <b>状态</b>
                                                </td>
                                                  <td align="center">
                                                    <b>录取专业</b>
                                                </td>
                                                <td align="center">
                                                    <b>预退原因</b>
                                                </td>
                                                 <td align="center">
                                                    <b>审核意见</b>
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
                                                <%# Eval("zysx")%>
                                            </td>
                                            <td align="center">
                                                <%# Eval("zy1mc")%>
                                            </td>
                                            <td align="center">
                                                <%# Eval("zy2mc")%>
                                            </td>
                                            <td align="center">
                                                 <%#Eval("zyfc").ToString() == "1" ? "服从" : "不服从"%>
                                            </td>
                                            <td align="center">
                                              <%#Eval("xq_zt").ToString() == "2" ? "已发档" : Eval("xq_zt").ToString() == "3" ? "预退" : Eval("xq_zt").ToString() == "4" ? "预录" : Eval("xq_zt").ToString() == "5" ? "录取" : "已投"%>
                                              
                                            </td>
                                              <td align="center">
                                                <%# Eval("lqzymc")%>
                                            </td>
                                            <td align="center">
                                                <%# Eval("xxbz")%>
                                            </td>
                                              <td align="center">
                                                <%# Eval("xqbz")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>--%>
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
