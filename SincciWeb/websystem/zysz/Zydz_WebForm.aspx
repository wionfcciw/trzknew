<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zydz_WebForm.aspx.cs" Inherits="SincciKC.Zydz_WebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>志愿定制</title>
  <%--  <meta content="IE=7" http-equiv="X-UA-Compatible" />--%>
<%--    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />--%>
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
       <script src="../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .styHeight
        {
            height: 35px;
        }
        html
        {
            overflow-x: hidden; /*- 横滚动条 -*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--代码-->
        <asp:HiddenField ID="hfDm" runat="server" />
        <!--名称-->
        <asp:HiddenField ID="hfMc" runat="server" />
        <!--标识ID-->
        <asp:HiddenField ID="hfId" runat="server" />
        <!--类型标识-->
        <asp:HiddenField ID="hfFlag" runat="server" />
        <!--操作类型-->
        <asp:HiddenField ID="hfOperationType" runat="server" />
        <!--新增、修改标识-->
        <asp:HiddenField ID="hfEditorType" runat="server" />
        <table id="table1" cellspacing="0" cellpadding="4" width="100%" style="border-collapse: collapse; height:100%;
            border-color: #D3D3D3; margin: 1px" border="1">
            <tr>
                <td width="432px">
                    <asp:Button ID="btnAddXq" runat="server" Text="新增市\县区" OnClick="btnAddXq_Click" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel1" runat="server" Height="510px" ScrollBars="Auto" 
                        Width="100%">
                   
                        <asp:TreeView ID="tvDisplay" runat="server" Font-Size="13px" 
                            OnSelectedNodeChanged="tvDisplay_SelectedNodeChanged" BorderStyle="None" 
                            ImageSet="XPFileExplorer" ShowLines="True">
                            <HoverNodeStyle ForeColor="#CC00FF" />
                            <LeafNodeStyle BorderStyle="None" />
                            <NodeStyle  ForeColor="#000099" />
                            <SelectedNodeStyle BackColor="#666699" Font-Bold="True" ForeColor="#99FF33" />
                        </asp:TreeView>  </asp:Panel>
                </td>
                <td valign="top" id="tdShow" runat="server" visible="false" style="height: 500px">
                    <table id="table3" style="width: 450px;" cellspacing="0" cellpadding="3" border="1">
                        <tr class="styHeight">
                            <td colspan="2" align="center" style="background-color: #f5f5f5">
                                <asp:Button ID="btnAdd" runat="server" Text="新增大批次" OnClick="btnAdd_Click" />
                                &nbsp;
                                <asp:Button ID="btnUpdate" runat="server" Text="修改" OnClick="btnUpdate_Click" />
                                &nbsp;
                                <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_1" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_101" runat="server" Text="大批次代码:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_201" runat="server" Text="大批次代码"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_2" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_102" runat="server" Text="大批次名称:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_202" runat="server" Text="大批次名称"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_3" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_103" runat="server" Text="显示名称:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_203" runat="server" Text="显示名称"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_4" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_104" runat="server" Text="志愿数:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_204" runat="server" Text="志愿数量"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_5" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_105" runat="server" Text="学校服从:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_205" runat="server" Text="学校服从"></asp:Label>&nbsp;
                            </td>
                        </tr>
                          <tr class="styHeight" id="tr_12" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_112" runat="server" Text="其它学校是否服从:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_212" runat="server" Text="服从"></asp:Label>&nbsp;
                            </td>
                        </tr>


                        <tr class="styHeight" id="tr_6" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_106" runat="server" Text="是否启用:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_206" runat="server" Text="是否启用"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_8" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_1_8" runat="server" Text="普高类型:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_208" runat="server" Text="普高类型"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_9" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_109" runat="server" Text="批次类型:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_209" runat="server" Text="批次类型"></asp:Label>&nbsp;
                            </td>
                        </tr>
                           <tr class="styHeight" id="tr_10" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_110" runat="server" Text="开始时间:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_210" runat="server" Text="开始时间"></asp:Label>&nbsp;
                            </td>
                        </tr>
                          <tr class="styHeight" id="tr_11" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_111" runat="server" Text="结束时间:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_211" runat="server" Text="结束时间"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_7" runat="server">
                            <td align="right" style="vertical-align: top;">
                                <asp:Label ID="lbl_107" runat="server" Text="备注:"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txt_207" runat="server" Height="115px" Width="294px" 
                                    TextMode="MultiLine"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                      
                    </table>
                </td>
                <td valign="top" runat="server" id="tdEditor" visible="false">
                    <table id="table2" cellspacing="1" cellpadding="1" border="0">
                        <tr class="styHeight" id="tr_0_0" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_300" runat="server" Text="县区信息:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlXqXx" runat="server" Height="21px" Width="146px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_1_1" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_301" runat="server" Text="大批次代码:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPcDm" runat="server" MaxLength="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_2_2" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_302" runat="server" Text="大批次名称:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPcMc" runat="server" MaxLength="25"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_3_3" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_303" runat="server" Text="显示名称:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtShowName" runat="server" MaxLength="25"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_4_4" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_304" runat="server" Text="志愿数:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCount" runat="server" MaxLength="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_5_5" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_305" runat="server" Text="学校服从:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFc" runat="server" MaxLength="5"></asp:TextBox>
                                <asp:RadioButtonList ID="rbl_Zyfc" runat="server" Height="16px" RepeatColumns="2">
                                    <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                              <tr class="styHeight" id="tr_12_12" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_312" runat="server" Text="其他学校是否服从:"></asp:Label>
                            </td>
                            <td align="left">
                                
                                <asp:RadioButtonList ID="rbl_Sfxxfc" runat="server" Height="16px" RepeatColumns="2">
                                    <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_6_6" runat="server">
                            <td align="right">
                                <asp:Label ID="lbl_306" runat="server" Text="是否启用:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:RadioButtonList ID="rbl_Sfqy" runat="server" Height="16px" RepeatColumns="2">
                                    <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_8_8" runat="server" visible="false">
                            <td align="right">
                                <asp:Label ID="Label1" runat="server" Text="普高类型:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPgLx" runat="server" Height="20px" Width="122px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_9_9" runat="server" visible="false">
                            <td align="right">
                                <asp:Label ID="Label2" runat="server" Text="批次类型:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPcLx" runat="server" Height="19px" Width="124px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                           <tr class="styHeight" id="tr_10_10" runat="server" visible="false">
                            <td align="right">
                                <asp:Label ID="lbl_10" runat="server" Text="开始时间:"></asp:Label>
                            </td>
                            <td align="left">
                              <asp:TextBox  ID="StartTime"  CssClass="searchbox" onFocus="WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"  runat="server"></asp:TextBox> 
                            </td>
                        </tr>
                          <tr class="styHeight" id="tr_11_11" runat="server" visible="false">
                            <td align="right">
                                <asp:Label ID="lbl_11" runat="server" Text="结束时间:"></asp:Label>
                            </td>
                            <td align="left">
                              <asp:TextBox  ID="EndTime"  CssClass="searchbox" onFocus="WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"  runat="server"></asp:TextBox> 
                            </td>
                        </tr>
                        <tr class="styHeight" id="tr_7_7" runat="server">
                            <td align="right" style="vertical-align: top;">
                                <asp:Label ID="lbl_307" runat="server" Text="备注:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBz" runat="server" Height="107px" Width="334px" 
                                    TextMode="MultiLine" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                     
                        <tr class="styHeight">
                            <td colspan="2" align="center">
                                <asp:Button ID="btnOk" runat="server" Text="确定" OnClick="btnOk_Click" 
                                    Height="21px" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
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
