<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PiLiangTouDangWebForm.aspx.cs"
    Inherits="SincciKC.PiLiangTouDangWebForm" %>

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
       <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
 
 
    <script language="javascript" type="text/javascript">
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
        function checkInput() {
        
            if ($("#txtbl").val() == "") {
                $("#txtbl").focus();
                alert("请输入投档比例!");
                return false;
            }
        }
        function checkInput2() {
            if ($("#ddl_xx").val() == "") {
                $("#ddl_xx").focus();
                alert("请选择招生学校!");
                return false;
            }
            if ($("#txtdxnum").val() == "") {
                $("#txtdxnum").focus();
                alert("请输入投档数!");
                return false;
            }
        }

        function checkInput3() {
            if ($("#ddl_xx").val() == "") {
                $("#ddl_xx").focus();
                alert("请选择招生学校!");
                return false;
            }
          
        }
        function opdg(pcdm, xqdm, xxdm, title) {
            window.parent.addTab2(title, '/websystem/zklq/ByXxLqInfo.aspx?pcdm=' + pcdm + '&xqdm=' + xqdm + '&xxdm=' + xxdm);
            return false;
        }
        function opUp(ID, Title) {
            ymPrompt.win({ message: 'XX_FD.aspx?pcdm=' + ID, width: 500, height: 250, title: Title, iframe: true, fixPosition: true, dragOut: false })
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
            批量投档</div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 98%;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 120px; text-align: right;">
                                当前投档批次:
                            </td>
                            <td style="width: 250px; text-align: left;">
                                <asp:DropDownList ID="ddlXpcInfo" runat="server" Width="280px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlXpcInfo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 90px; text-align: right;">
                                投档算法:
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddl_tdsf" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                             
                            <td style="text-align: left;">
                            <div runat="server" id="divzy">
                                志愿:<asp:DropDownList ID="ddl_zy" runat="server" >
                                </asp:DropDownList></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <table border="1" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse;
                        height: 400px;">
                         <tr>
                            <td  >
                            投档比例1:<asp:TextBox ID="txtbl" runat="server"  ></asp:TextBox>
                                <asp:TextBox ID="txtNum" runat="server" Visible="False" ></asp:TextBox>

                                   &nbsp;&nbsp;

                                   <asp:Button ID="btn_ql"   runat="server" Text="完成该批次执行下一批次" OnClick="btn_ql_Click" 
                                                      />
                                &nbsp;&nbsp;
                                   <asp:Button ID="Button5"   runat="server" Text="一次性提交录取状态" OnClick="Button5_Click"  
                                                      />
                            
                            </td>
                        </tr>
                       
                        <tr style="height: 45px;">
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: left; width: 75%;">
                                            <asp:Button ID="btn_beginTd" runat="server" Text="统招生投档"   OnClick="btn_beginTd_Click"
                                                Width="90px" OnClientClick="return checkInput();" />
                                              &nbsp;
                                               <asp:Button ID="btn_pesTd" runat="server" Text="配额生投档"    Visible="false"  
                                                Width="90px" OnClientClick="return checkInput();" OnClick="btn_pesTd_Click" />
                                              &nbsp;
                                            <asp:Button ID="btnCancel_Td" runat="server" Text="取消投档"   Width="90px"
                                                OnClick="btnCancel_Td_Click" />
                                                &nbsp;
                                            <asp:Button ID="btnPl_Fd"   runat="server" Text="批量发档"  Visible="false"  
                                                Width="90px" onclick="btnPl_Fd_Click" />
                                              &nbsp;
                                           <asp:Button ID="btn_daochu"   runat="server" Text="导出"  
                                                Width="90px" onclick="btn_daochu_Click"     />
                                                &nbsp;
                                             <asp:Button ID="Button6"   runat="server" Text="更新计划(5-14)" OnClick="Button6_Click"  
                                                   />&nbsp;
                                        
                                           <asp:Button ID="btnpzt"   runat="server" Text="更新计划(17-38||41-44)" OnClick="btnpzt_Click"  
                                                   />

                                             <asp:CheckBox ID="chxq" runat="server" Text="是否分县区" OnCheckedChanged="chxq_CheckedChanged" AutoPostBack="true" Visible="false" />
                                            <asp:Button ID="btn_zbstd" runat="server" Text="指标生投档"   Width="110px"
                                                OnClientClick="return checkInput();" OnClick="btn_zbstd_Click" Visible="false" />
                                         
                                        
                                            &nbsp;
                                           <asp:Button ID="btn_dxFd"   runat="server" Text="单校发档"  
                                                Width="90px" onclick="btn_dxFd_Click" Visible="false"  />
                                           
                                            &nbsp;
                                           <asp:Button ID="btn_jhs"   runat="server" Text="更新计划库"  
                                                Width="110px" onclick="btn_jhs_Click"    Visible="false"   />
                                                  &nbsp;
                                            <asp:Button ID="btn_jq" runat="server" Text="结清"   
                                                Width="80px" onclick="btn_jq_Click"  
                                                  />
                                            &nbsp;
                                            <asp:Button ID="btn_jqqx" runat="server" Text="取消结清"   
                                                Width="100px" onclick="btn_jqqx_Click" 
                                                />
                                            &nbsp;
                                                </td>
                                         
                                        <td style="text-align: right;">
                                               <span id="span_TongJiInfo" runat="server" style="color: Blue;">统计提示信息！</span>
                                  
                                           &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>

                          <tr style="height: 25px;">
                            <td  >
                                <div id="divdxtoud" runat="server"  >
                                    招生学校:
                                    <asp:DropDownList ID="ddl_xx" runat="server">
                                    </asp:DropDownList>
                                    投档总数:<asp:TextBox ID="txtdxnum" runat="server" Width="60px"></asp:TextBox>
                                  
                                       &nbsp;
                                       <asp:Button ID="Button1" runat="server" Text="生成录取统计表"  Visible="false"  Width="120px" OnClick="Button1_Click"
                                         />
                                       &nbsp;
                                       <asp:Button ID="Button4" runat="server" Text="修改配额生录取控制线"     OnClientClick="return checkInput3();" OnClick="Button4_Click" 
                                         />  &nbsp;  &nbsp;
                                    <asp:TextBox ID="txtfsx" runat="server" Width="250px" ></asp:TextBox>
                                </div>
                            </td>
                          
                          </tr>
                        <tr>    <td>  <asp:Button ID="btn_xxfd" runat="server" Text="单校统招生投档(本县)"   Width="140px"
                                        OnClientClick="return checkInput2();" OnClick="btn_xxfd_Click" />
                                       &nbsp;
                                        <asp:Button ID="Button2" runat="server" Text="单校统招生投档(外县)"   Width="140px"
                                        OnClientClick="return checkInput2();" OnClick="Button2_Click" />
                                       &nbsp;
                                       <asp:Button ID="btn_xxfd_pe" runat="server" Text="单校配额生投档"   Width="120px"
                                        OnClientClick="return checkInput2();" OnClick="btn_xxfd_pe_Click"  />
                                       &nbsp;
                                       <asp:Button ID="btn_xxfd_pzt" runat="server" Text="单校配转统投档(本县)"   Width="140px"
                                        OnClientClick="return checkInput2();" OnClick="btn_xxfd_pzt_Click"  />
                              &nbsp;
                                       <asp:Button ID="Button3" runat="server" Text="单校配转统投档(外县)"   Width="140px"
                                        OnClientClick="return checkInput2();" OnClick="Button3_Click"    />
</td>




                        </tr>

   <tr>    <td>  <asp:Button ID="Button7" runat="server" Text="选择学校统招生投(本县)"   Width="150px"
                                          OnClick="Button7_Click"   />
                                       &nbsp;
                                        <asp:Button ID="Button8" runat="server" Text="选择学校统招生投(外县)"   Width="150px" OnClick="Button8_Click"
                                          />
                                       &nbsp;
                                       <asp:Button ID="Button9" runat="server" Text="选择学校配额生投档"   Width="130px" OnClick="Button9_Click"
                                            />
                                       &nbsp;
                                       <asp:Button ID="Button10" runat="server" Text="选择学校配转统投档(本县)"   Width="160px" OnClick="Button10_Click"
                                          />
                              &nbsp;
                                       <asp:Button ID="Button11" runat="server" Text="选择学校配转统投档(外县)"   Width="160px" OnClick="Button11_Click"
                                             />
</td>




                        </tr>
                        <tr><td>
                              <div runat="server" id="bmdinfo" class="datagrid-toolbar" visible="false"></div>

                            </td></tr>
                        <tr style="height: 25px;">
                            <td style="color: Blue; vertical-align:bottom; font-weight:bold;">
                                最低控制线：
                            </td>
                        </tr>
                        <tr style="height: 45px;">
                            <td>
                                <span id="span_showXqInfo" runat="server"></span>
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Button ID="btnImport_td_ksxx" runat="server" Text="  导出所有考生当前的投档状态"  
                                    OnClientClick="meg();" Width="196px" OnClick="btnImport_td_ksxx_Click" Visible="false" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top; width: 100%; height: 350px;">
                                <asp:Repeater ID="repDisplay" runat="server" 
                                    onitemcommand="repDisplay_ItemCommand" >
                                    <HeaderTemplate>
                                        <table id="tab_Repeater" border="1" style="border-collapse: collapse; width: 100%;"
                                            cellpadding="2" cellspacing="0">
                                            <tr class="datagrid-header" runat="server" style="height: 26px">
                                                <td>
                                                    <b>全选
                                                        <input type="checkbox" name="checkboxid" id="checkboxid" onclick="SelectAll(this)" /></b>
                                                </td>
                                                <td style="width: 300px;" align="center">
                                                    <b>学校名称</b>
                                                </td>
                                              <%-- <td align="center">
                                                    <b>志愿</b>
                                                </td>--%>
                                                <td align="center">
                                                    <b>投档分数线</b>
                                                </td>
                                              <%--  <td align="center">
                                                    <b>县区名称</b>
                                                </td>--%>
                                                <td align="center">
                                                    <b>总计划数</b>
                                                </td>
                                              <%--  <td align="center">
                                                    <b>计划数</b>
                                                </td>--%>
                                                <td align="center">
                                                    <b>已投档数</b>
                                                </td>
                                                <td align="center">
                                                    <b>余缺数</b>
                                                </td>
                                              <%--    <td align="center">
                                                    <b>发档数</b>
                                                </td>
                                             --%>
                                                 <td align="center">
                                                    <b>录取数</b>
                                                </td>
                                              <%--  <td align="center">
                                                    <b>预录数</b>
                                                </td>
                                                <td align="center">
                                                    <b>预退数</b>
                                                </td>--%>
                                                 <td align="center">
                                                    <b>录取最高分</b>
                                                </td>
                                                  <td align="center">
                                                    <b>录取最低分</b>
                                                </td>
                                               <td align="center">
                                                    <b>操作</b>
                                                </td>
                                                
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="height: 24px"  >
                                       <td align="center">
                                                <input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("xxdm") %>'>
                                            </td>
                                            <td>
                                              <a href="#" onclick='return opdg("<%# Eval("pcdm")%>","<%# Eval("xqdm")%>","<%# Eval("xxdm")%>"," <%# Eval("xxmc")%>"+"招生情况");'>
                                                    <%# Eval("xxmc")%></a>
                                                
                                            </td>
                                         <%--  <td align="center">
                                               <%# Eval("zysx")%></td>--%>
                                            <td align="center">
                                                <%# Eval("zaosenFsx")%>
                                            </td>
                                            <%--<td align="center">
                                                <%# Eval("xqmc")%>
                                            </td>--%>
                                            <td align="center">
                                                <%# Eval("jhs")%>
                                            </td>
                                           <%-- <td align="center">
                                                <%# Eval("tz_sl")%>
                                            </td>--%>
                                            <td align="center">
                                                <%# Eval("yitd_fzbs_sl")%> 
                                            </td>
                                            <td align="center">
                                                <%# Eval("fzbs_xcsl")%>
                                            </td>
                                           <%--   <td align="center">
                                                <%# Eval("fdnum")%>
                                            </td>--%>
                                            <td align="center">
                                                <%# Eval("lqnum")%>
                                            </td>
                                       <%--     <td align="center">
                                                <%# Eval("ylnum")%>
                                            </td>
                                            <td align="center">
                                                <%# Eval("ytnum")%>
                                            </td>--%>
                                             <td align="center">
                                                <%# Eval("maxlqfsx")%>
                                             </td>
                                            <td align="center">
                                                <%# Eval("minlqfsx")%>
                                            </td>
                                            <td align="center">
                                              <font color="red" ><%#Eval("jqxx").ToString() == "" ? "" : "已结清"%></font> 
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
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
