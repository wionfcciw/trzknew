<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TouDangDinZiWebForm.aspx.cs"
    Inherits="SincciKC.TouDangDinZiWebForm" %>

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
     
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0px" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; border-collapse:collapse"> 
            <tr style="height: 400px;">            
                <td style="text-align: left; vertical-align:top; width: 200px;">
                   <asp:Panel ID="Panel2" ScrollBars="Both" Height="540px" BorderStyle="Solid" 
                                    Width="250px" runat="server" BorderColor="Silver" BorderWidth="1px">   
                    <table cellpadding="0" border="0" cellspacing="0" style="width: 100%; height: 100%;">
                        <tr style="height: 20px;">
                            <td  style="font-size:18px; font-weight:bold;" class="datagrid-toolbar">
                                <img src="/images/nav_01.gif" alt="" /> 批次信息
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align:top;" >
                             <asp:TreeView ID="tvPcInfo" runat="server" Font-Size="15px" 
                                        onselectednodechanged="tvPcInfo_SelectedNodeChanged" ImageSet="Arrows">
                                        <HoverNodeStyle ForeColor="#CC00FF" />
                                        <NodeStyle Font-Bold="True" ForeColor="#000099" />
                                        <SelectedNodeStyle BackColor="#666699" Font-Bold="True" ForeColor="#99FF33" />
                                    </asp:TreeView>
                                
                            </td>
                        </tr>
                    </table></asp:Panel>
                </td>
                <td style="width: 200px; text-align: left; vertical-align: top;">
                   <asp:Panel ID="Panel3" ScrollBars="Both" Height="540px" BorderStyle="Solid" 
                                    Width="240px" runat="server" BorderColor="Silver" BorderWidth="1px">  
                    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
                        <tr style="height: 20px;">
                            <td  style="font-size:18px; font-weight:bold;" class="datagrid-toolbar">
                                <img src="/images/nav_02.gif" alt="" /> 批次投档条件类型
                            </td> 
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top;">
                                       
                                <asp:TreeView ID="tvIfType" runat="server" ShowExpandCollapse="False" OnSelectedNodeChanged="tvIfType_SelectedNodeChanged"
                                    Font-Bold="True" Font-Size="14px" ImageSet="BulletedList2">
                                    <HoverNodeStyle ForeColor="#CC00FF" />
                                    <Nodes>
                                        <asp:TreeNode Text="基本条件" Value="0"></asp:TreeNode>
                                        <asp:TreeNode Text="同分跟进" Value="1"></asp:TreeNode>
                                        <asp:TreeNode Text="指标生" Value="2"></asp:TreeNode>
                                        <asp:TreeNode Text="往届生" Value="3"></asp:TreeNode>
                                     <%--   <asp:TreeNode Text="其他条件" Value="4"></asp:TreeNode>--%>
                                      <%--  <asp:TreeNode Text="国际班控档线" Value="5"></asp:TreeNode>--%>
                                    </Nodes>
                                    <NodeStyle ChildNodesPadding="20px" NodeSpacing="8px" ForeColor="#0066FF" />
                                    <SelectedNodeStyle BackColor="#666699" ForeColor="#99FF33" />
                                </asp:TreeView>
                               
                            </td>
                        </tr>
                    </table> </asp:Panel>
                </td>
                <td style="text-align: left; vertical-align: top;">
                
                 <asp:Panel ID="Panel4" ScrollBars="Both" Height="540px" BorderStyle="Solid" 
                                    Width="400px" runat="server" BorderColor="Silver" BorderWidth="1px">            
                    <!--基本条件--> 
                    <table id="tab_0" runat="server" visible="true" width="100%" cellpadding="2" >
                        <tr  >
                            <td colspan="2" style="font-size: 18px; font-weight: bold;" class="datagrid-toolbar">
                                <img src="/images/nav_03.gif" alt="" /> 基本条件设置
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold; color: Blue;">
                                批量投档算法
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server"  RepeatColumns="1">
                                    <asp:ListItem Value="0" Selected="True" Text="平行志愿算法"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="志愿优化算法"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold; color: Blue;">
                                最低控制线:
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:RadioButton ID="RadioButton3" runat="server" Text="普高最低控档线有效" GroupName="ZdKzx"
                                    Checked="true" AutoPostBack="True" OnCheckedChanged="RadioButton5_CheckedChanged" />
                                <br />
                                <asp:RadioButton ID="RadioButton4" runat="server" Text="普高最低控档线无效" GroupName="ZdKzx"
                                    AutoPostBack="True" OnCheckedChanged="RadioButton5_CheckedChanged" />
                               <br />
                                <asp:RadioButton ID="RadioButton5" runat="server" Text="指定最低分数" GroupName="ZdKzx"  Visible="false"
                                    AutoPostBack="True" OnCheckedChanged="RadioButton5_CheckedChanged" />
                                &nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="80px" Height="18px" Enabled="False"  Visible="false"></asp:TextBox>
                                &nbsp;<%--分--%>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold; color: Blue;">
                             <%--   是否自动发档:--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatColumns="1"  Visible="false" >
                                    <asp:ListItem Value="0" Text="自动发档" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="手动发档"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold; color: Blue;">
                          <%--      同一批次同一招生学校在各县区的计划数不同:--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatColumns="1"   Visible="false"> 
                                    <asp:ListItem Value="0" Text="是" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="否"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <!--同分跟进-->
                    <table id="tab_1" runat="server" visible="false" style="width: 100%; font-size: 11pt;">
                        <tr  >
                            <td style="font-size: 18px; font-weight: bold;" class="datagrid-toolbar">
                                <img src="/images/nav_03.gif" alt="" /> 同分跟进设置
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="font-weight: bold; color: Blue;">
                                是否同分跟进:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatColumns="1"
                                    OnSelectedIndexChanged="RadioButtonList5_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                    <asp:ListItem Value="0" Selected="True" Text="同分不跟进"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="同分直接跟进"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="同分比较各科成绩"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top; height: 20px;">
                                ---------------------------------------
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; color: Blue;">
                               <%-- 选择要比较的科目:--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="2"  
                                    Enabled="False" RepeatDirection="Horizontal"  AutoPostBack="true"
                                    onselectedindexchanged="CheckBoxList1_SelectedIndexChanged"  Visible="false"
                                  >
                                    <asp:ListItem Value="1" Text="语数外三科"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="语数两科"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="语文"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="数学"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="英语"></asp:ListItem>

                                  <%--  <asp:ListItem Value="4" Text="物理"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="化学"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="思想品德"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="历史"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="体育"></asp:ListItem>--%>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr><td>
                            <%--比较顺序:--%><asp:Label Text="" runat="server" ID="lblcj" />
                            <asp:Label Text="" runat="server" ID="lblcjid"  Visible="false"   />
                            </td></tr>
                    </table>
                    <!--指标生-->
                    <table id="tab_2" runat="server" visible="false" style="width: 100%; font-size: 11pt;">
                        <tr  >
                            <td colspan="2" style="font-size: 18px; font-weight: bold;" class="datagrid-toolbar">
                                <img src="/images/nav_03.gif" alt="" /> 指标生设置
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2" style="font-weight: bold; color: Blue;">
                                有无指标生:
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButtonList ID="RadioButtonList6" runat="server"   RepeatColumns="1"
                                    OnSelectedIndexChanged="RadioButtonList6_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="0" Text="有指标生"></asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True" Text="无指标生"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold; color: Blue;">
                              <%--  指标生录取分数限制:--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="abc" Text="无分数限制" AutoPostBack="True"
                                        OnCheckedChanged="RadioButton2_CheckedChanged" />
                                   <br />
                                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="abc" Text="不小于统招线下"
                                        AutoPostBack="True" OnCheckedChanged="RadioButton2_CheckedChanged" />
                                    <asp:TextBox ID="TextBox2" runat="server" Width="80px"></asp:TextBox>&nbsp;分
                                </asp:Panel>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left; vertical-align: top; height: 20px;">
                            <%--    -----------------------------------------%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold; color: Blue;">
                           <%--     剩余指标生处理:--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
                                <asp:RadioButtonList ID="RadioButtonList8" runat="server"  RepeatColumns="1"  Visible="false"
                                    Enabled="False">
                                    <asp:ListItem Value="0" Text="无剩余指标"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="可用其他中学的剩余指标"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <!--素质评价-->
                    <table id="tab_3" runat="server" visible="false" style="width: 100%; font-size: 11pt;">
                        <tr  >
                            <td colspan="2" style="font-size: 18px; font-weight: bold;" class="datagrid-toolbar">
                                <img src="/images/nav_03.gif" alt="" /> 素质评价设置
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2" style="font-weight: bold; color: Blue;">
                                往届生降低分数投档:
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButtonList ID="RadioButtonList9" runat="server"   RepeatColumns="1"
                                    AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList9_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True" Text="无"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="有"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                   <%--     <tr>
                            <td colspan="2" style="font-weight: bold; color: Blue;">
                                请输入评价等级:
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="width: 120px; text-align: right;">
                             <%--   选择关系:--%>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="120px"  Visible="false"
                                    Enabled="False">
                                    <asp:ListItem Value="0" Text="大于[>]"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="小于[<]"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="等于[=]"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="不等于[!=]"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="大于等于[>=]"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="小于等于[<=]"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; text-align: right;">
                            <%--    等级个数:--%>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="TextBox3" runat="server" Width="115px" Enabled="False" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; text-align: right;">
                                <%--选择等级:--%>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="DropDownList3" runat="server" Width="120px" Visible="false"
                                    Enabled="False">
                                    <asp:ListItem Value="0" Text="A"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="B"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="C"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="D"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <!--其他条件-->
                    <table id="tab_4" runat="server" visible="false"  width="100%"   >
                         <tr  >
                            <td style="font-size: 18px; font-weight: bold;" class="datagrid-toolbar">
                                <img src="/images/nav_03.gif" alt="" /> 其他条件设置
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="font-weight: bold; color: Blue;">
                                加试成绩合格:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList10" runat="server"   RepeatColumns="1">
                                    <asp:ListItem Value="0" Text="艺术类"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="师范类"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="免费师范(男生)类"></asp:ListItem>
                                    <asp:ListItem Value="3" Selected="True" Text="不限制"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; color: Blue;">
                                与文化成绩合并的加试成绩:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList11" runat="server"  RepeatColumns="1">
                                    <asp:ListItem Value="0" Text="艺术类"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="师范类"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="免费师范(男生)类"></asp:ListItem>
                                    <asp:ListItem Value="3" Selected="True" Text="不限制"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; color: Blue;">
                                会考成绩合格:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList12" runat="server"   RepeatColumns="1">
                                    <asp:ListItem Value="0" Selected="True" Text="会考成绩必须合格"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="不限制"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; color: Blue;">
                                性别限制:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList13" runat="server" RepeatColumns="1">
                                    <asp:ListItem Value="0" Text="必须男性"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="必须女性"></asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True" Text="男女不限"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <!--国际班控档线-->
                    <table id="tab_5" runat="server" visible="false" style="width: 100%; font-size: 11pt;">
                        <tr  >
                            <td colspan="2" style="font-size: 18px; font-weight: bold;" class="datagrid-toolbar">
                                <img src="/images/nav_03.gif" alt="" /> 国际班控档线设置
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="width: 250px; text-align: left;">
                                [8188]江苏省兴化中学国际班:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_1" runat="server" Width="60px"></asp:TextBox>&nbsp;分
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 250px; text-align: left;">
                                [8288]江苏省靖江高级中学:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_2" runat="server" Width="60px"></asp:TextBox>&nbsp;分
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 250px; text-align: left;">
                                [8388]泰兴市第一高级中学:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_3" runat="server" Width="60px"></asp:TextBox>&nbsp;分
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 250px; text-align: left;">
                                [8488]江堰市第二中学:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_4" runat="server" Width="60px"></asp:TextBox>&nbsp;分
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 250px; text-align: left;">
                                [0288]江苏省泰州中学:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_5" runat="server" Width="60px"></asp:TextBox>&nbsp;分
                            </td>
                        </tr>
                    </table>
                    
                    <table style="width:100%; background-color:#f5f5f5" ><tr><td align="center"><asp:Button ID="btnSave" runat="server" Text="保存"  CssClass="btnStyle" OnClick="btnSave_Click" /></td></tr></table>
                
                </asp:Panel>
                </td>
            </tr>
            
        </table>
        <asp:HiddenField ID="hf_xpcId" runat="server"/>
        <asp:HiddenField ID="hf_pcdm" runat="server"/>
    </div>
    </form>
</body>
</html>
