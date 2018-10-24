<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XXWebForm.aspx.cs"
    Inherits="SincciKC.XXWebForm" %>

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
            if ($("#txtyij").val() == "") {
                $("#txtyij").focus();
                alert("请输入预退原因!");
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
        function MsgYes() {
            if (confirm('确定将您选择的考生预录到专业 ' + document.getElementById("ddlzy").value+ " 吗?")) {
                return true;
            }
            else {
                return false;
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
                        if (inputs[i].checked  ) {
                            if (inputs[i].id != "checkboxid")
                                elem[i - 13].parentNode.parentNode.style.backgroundColor = '#EEF5FF';
                        }
                        else {
                            if (inputs[i].id != "checkboxid")  
                            elem[i - 13].parentNode.parentNode.style.backgroundColor = '';
                        }
                    } catch (e) {
                     
                    }
                }
            }
        }
      
        function opUp(ID, Title) {
            ymPrompt.win({ message: 'XX_ytui.aspx?ksh=' + ID, width: 420, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: center; vertical-align: bottom; height: 26px; font-size: 18px;
            font-weight: bold; padding-top: 8px" class="datagrid-toolbar">
            阅档</div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 98%;">
        <%--    <tr>
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
                            <td style=" text-align: right;">
                                发档批次:
                            </td>
                            <td style="  text-align: left;">
                                <asp:DropDownList ID="ddlfapc" runat="server"  AutoPostBack="True" 
                                    onselectedindexchanged="ddlfapc_SelectedIndexChanged"  >
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>--%>
                    <tr style="height: 25px;">
                            <td  >
                             未处理的批次:  <asp:DropDownList ID="ddl_wcl" 
                                    runat="server" onselectedindexchanged="ddl_wcl_SelectedIndexChanged" 
                                    AutoPostBack="True"   >
                                </asp:DropDownList>     
                                 
                                            
                            </td>
                        </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <table border="1" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse;
                        height: 400px;">
                        
                      
                        <tr style="height: 45px;">
                            <td>
                                         <asp:Button ID="btn_beginTd" runat="server" Text="预录" CssClass="icon-reload"
                                                OnClick="btn_beginTd_Click" Width="80px"    />
                                            &nbsp;   录取专业:  <asp:DropDownList ID="ddlzy" runat="server"   >
                                </asp:DropDownList>
                                          
                                          &nbsp;
                                            <asp:Button ID="btnPl_Fd"   runat="server" Text="提交" CssClass="icon-reload"
                                                Width="80px" onclick="btnPl_Fd_Click" />
                                            &nbsp;
                                           <asp:Button ID="btn_jy"   runat="server" Text="  按建议专业预录" CssClass="icon-reload"
                                                Width="140px" onclick="btn_jy_Click"   />
                                                   &nbsp;
                                           <asp:Button ID="Button1"   runat="server" Text="  导出Excel" CssClass="icon-reload"
                                                Width="100px" onclick="Button1_Click"    />
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
                      
                           <tr style="height: 25px;">
                            <td  >
                           提示:<br/>
                           1.选择考生,只需单击该考生信息任意位置即可.<br/>
                           2.进行预录操作,首先选择您要操作的考生,然后选择需要录取到的专业,再点击"预录"按钮即可.<br/>
                           3.进行预退操作,首先选择您要操作的考生,然后填写预退原因,再点击"预退"按钮即可.<br/>
                           4."按建议专业预录",无需选择考生,按照专业计划自动预录,剩下未预录的考生,需自己手工操作.<br/>
                           5.点击有"_"下划线的列头,可以对该列进行排序.<br/>
                           6.选中不了考生的,点击考生姓名即可.

                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top; width: 100%; height: 350px;">
                              <div id="mess_box" style=" height:400px; OVERFLOW-y:auto;"> 
                       
                                <asp:GridView ID="repDisplay" runat="server" ForeColor="#333333" AutoGenerateColumns="False"
                                    AllowSorting="True"   OnSorting="repDisplay_Sorting" Font-Size="13px"  BorderWidth="1px"
                                    Width="100%" EmptyDataText="该批次暂无数据!" 
                                      onrowdatabound="repDisplay_RowDataBound" AllowPaging="True" 
                                      onpageindexchanged="repDisplay_PageIndexChanged" 
                                      onpageindexchanging="repDisplay_PageIndexChanging" PageSize="100">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle HorizontalAlign="Center" />
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
                                           <asp:TemplateField HeaderText="性别">
                                            <ItemTemplate>
                                                <%# Eval("xbmc")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="联系电话1">
                                            <ItemTemplate>
                                                <%# Eval("lxdh")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="联系电话2">
                                            <ItemTemplate>
                                                <%# Eval("yddh")%>
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
                                                <%#Eval("xx_zt").ToString() == "2" ? "已发档" : Eval("xx_zt").ToString() == "3" ? "预退" : Eval("xx_zt").ToString() == "4" ? "预录" : Eval("xx_zt").ToString() == "5" ? "录取" : Eval("xx_zt").ToString() == "1"? "已投":"在库"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="录取专业" SortExpression="lqzymc">
                                            <ItemTemplate>
                                                <%# Eval("lqzymc")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="建议专业">
                                            <ItemTemplate>
                                                <%# Eval("jyzy").ToString() == "1" ? "分数优先" : Eval("jyzy").ToString() == "2" ? "专业优先":""%>
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
