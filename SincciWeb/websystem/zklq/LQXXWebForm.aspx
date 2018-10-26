<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LQXXWebForm.aspx.cs"
    Inherits="SincciKC.LQXXWebForm" %>

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

        function hideTest(Title, pcdm,lqxx,leix,sfpc) {
            window.parent.addTab2(Title, '/websystem/zklq/Print_lq.aspx?pcdm=' + pcdm + '&lqxx=' + lqxx + '&title=' + Title + '&leix=' + leix + '&sfpc=' + sfpc);
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
        function hideTest4(Title, ksh) {
            window.parent.addTab2(Title, '/websystem/zklq/KSGJForm.aspx?ksh=' + ksh + '&title=' + Title);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: center; vertical-align: bottom; height: 26px; font-size: 18px;
            font-weight: bold; padding-top: 8px" class="datagrid-toolbar">
            考生录取信息查询</div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 98%;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style=" text-align: right;">
                                当前批次:
                            </td>
                            <td  colspan="5" style="  text-align: left;">
                                <asp:DropDownList ID="ddlXpcInfo" runat="server"  AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlXpcInfo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                         
                   
                         
                        </tr>
                        <tr>  <td style=" text-align: right;">
                                学校:
                            </td>
                            <td style="  text-align: left;">
                                <asp:DropDownList ID="dllxx" runat="server"    >
                              <asp:ListItem  Value="">-请选择-</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                                      <td style=" text-align: right;">
                                录取状态:
                            </td>
                            <td style="  text-align: left;">
                                <asp:DropDownList ID="ddlzt" runat="server">
                                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                                    <asp:ListItem Value="0">在库</asp:ListItem>
                                    <asp:ListItem Value="1">已投</asp:ListItem>
                                 <%--   <asp:ListItem Value="2">已发档</asp:ListItem>
                                    <asp:ListItem Value="3">预退</asp:ListItem>
                                    <asp:ListItem Value="4">预录</asp:ListItem>--%>
                                    <asp:ListItem Value="5">录取</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                         <td style=" text-align: right;">
                                录取类型:
                            </td>
                            <td style="  text-align: left;">
                                <asp:DropDownList ID="ddllx" runat="server"    >
                              <asp:ListItem  Value="">-请选择-</asp:ListItem>
                               <asp:ListItem  Value="1">统招生</asp:ListItem>
                                 <asp:ListItem  Value="2">配额生</asp:ListItem>
                                   <asp:ListItem  Value="3">配转统</asp:ListItem>
                                   <asp:ListItem  Value="4">特长生</asp:ListItem>
                                      <asp:ListItem  Value="5">补录生</asp:ListItem>
                                </asp:DropDownList>

                                  <asp:DropDownList ID="listpc" runat="server"    >
                              <asp:ListItem  Value="">-请选择-</asp:ListItem>
                               <asp:ListItem  Value="11">11批次</asp:ListItem>
                                 <asp:ListItem  Value="21">21批次</asp:ListItem>
                                
                                </asp:DropDownList>
                            </td>
                            </tr>
                    </table>
                </td>
            </tr>
            <tr><td>报名号/准考号：
                     <asp:TextBox ID="txtName" CssClass="searchbox" runat="server" 
                    Width="100px" MaxLength="12"></asp:TextBox>
                     <asp:Button ID="btn_beginTd" runat="server" Text="  查询" CssClass="icon-search"  OnClick="btn_beginTd_Click"  />
                      
                     <asp:Button ID="btn_hs" runat="server" Text="  回收" CssClass="icon-cancel" onclick="btn_hs_Click"  OnClientClick="confirm('确定回收你选中的考生吗?回收后不可恢复!');"  />
                     <asp:Button ID="btn_lq" runat="server" Text="  指录" CssClass="icon-add" onclick="btn_lq_Click"  Visible="false"
                      />
                      <asp:Button ID="btn_print_lq" runat="server" Text="  打印三联单"   
                    CssClass="icon-print" onclick="btn_print_lq_Click" 
                      />
                                            <asp:Button ID="btn_spb" runat="server" Text="  打印审批表"  Visible="false"
                    CssClass="icon-print" onclick="btn_spb_Click" 
                      />
  <asp:Button ID="btn_daoclq" runat="server" Text="  导出录取名册" 
                    CssClass="icon-redo" onclick="btn_daoclq_Click"   
                      />
                     
                      <asp:Button ID="btn_xxtj" runat="server" Text="  查看该校统计" 
                    CssClass="icon-search" onclick="btn_xxtj_Click"    />

                      <asp:Button ID="btn_all" runat="server" Text="  导出所有" 
                    CssClass="icon-redo" onclick="btn_all_Click"  
                      />
                         <asp:Button ID="btn_selgj" runat="server" Text="  查看轨迹" 
                    CssClass="icon-search" onclick="btn_selgj_Click"  
                      />
                     </td></tr>
         
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <table border="1" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse;
                        ">
                        
                        
                        <tr>
                            <td style="text-align: left; vertical-align: top; width: 100%;  ">
                                    <asp:GridView ID="repDisplay" runat="server" ForeColor="#333333" AutoGenerateColumns="False"
                                    AllowSorting="True"   OnSorting="repDisplay_Sorting" Font-Size="13px"  BorderWidth="1px"
                                    Width="100%" EmptyDataText="查询不到任何信息!" AllowPaging="True" 
                                        onpageindexchanged="repDisplay_PageIndexChanged" 
                                        onpageindexchanging="repDisplay_PageIndexChanging" PageSize="20">

                                    <AlternatingRowStyle BackColor="White" />

                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E9EBEF" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
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
                                        <asp:TemplateField HeaderText="总分" SortExpression="zzf">
                                            <ItemTemplate>
                                                <%# Eval("zzf")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="体育">
                                            <ItemTemplate>
                                                <%# Eval("ty")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="文综" >
                                            <ItemTemplate>
                                                <%# Eval("wkzh")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="地生" >
                                            <ItemTemplate>
                                                <%# Eval("dsdj")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="综合等级" >
                                            <ItemTemplate>
                                                <%# Eval("zhdj")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="加分" >
                                            <ItemTemplate>
                                                <%# Eval("jf")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="录取学校">
                                            <ItemTemplate>
                                               <%# Eval("lqxxmc")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                      <%--  <asp:TemplateField HeaderText="录取专业">
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
                                        </asp:TemplateField>--%>
                                           <asp:TemplateField HeaderText="录取类型" >
                                            <ItemTemplate>
                                               <%#Eval("sftzs").ToString() == "1" ? "统招生" : Eval("sftzs").ToString() == "2"? "配额生":Eval("sftzs").ToString() == "3"? "配转统":Eval("sftzs").ToString() == "4"? "特长生":Eval("sftzs").ToString() == "5"? "补录生":""%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="状态" SortExpression="td_zt">
                                            <ItemTemplate>
                                              <%#Eval("td_zt").ToString() == "2" ? "已发档" : Eval("td_zt").ToString() == "3" ? "预退" : Eval("td_zt").ToString() == "4" ? "预录" : Eval("td_zt").ToString() == "5" ? "录取" : Eval("td_zt").ToString() == "1" ? "已投" : "在库"%>
                                              </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <%--  <asp:TemplateField HeaderText="志愿详情">
                                            <ItemTemplate>
                                                 <a href="#" onclick='return opdg(<%#Eval("ksh")%>,"学生志愿填报信息详情",1);'>详情</a>
                                           </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="个人信息">
                                            <ItemTemplate>
                                                <a href="#" onclick='return opdg2(<%#Eval("ksh")%>,"考生信息详情");'>详情</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                        <PagerTemplate>
      <table width="100%">
             <tr>
                 <td style="text-align: center">
                                        第<asp:Label ID="lblPageIndex" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' ForeColor="red" />页
                                        共<asp:Label ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>' />页
                                        <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First"
                                         Visible="<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>"   CommandName="Page" Text="首页"  ForeColor="White"  />
                                        <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev" 
                                         Visible="<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>" CommandName="Page" Text="上一页"   ForeColor="White" />
                                        <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next"
                                         Visible="<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>" CommandName="Page" Text="下一页"   ForeColor="White" />
                                        <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last"
                                         Visible="<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>" CommandName="Page" Text="尾页"   ForeColor="White" />
                  </td>
             </tr>
      </table>
</PagerTemplate>
                                </asp:GridView>
                         
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
