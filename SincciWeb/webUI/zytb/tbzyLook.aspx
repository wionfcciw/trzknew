<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tbzyLook.aspx.cs" Inherits="SincciKC.webUI.zytb.tbzyLook" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考生志愿填报</title>

    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />

    <link rel="stylesheet" type="text/css" href="../../css/subModal.css" />
    <script src="../../js/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/subModal.js"></script>

    <style type="text/css">
        .btn {
            cursor: hand;
        }

        .tblcss {
            text-align: center;
            border-collapse: collapse;
            vertical-align: middle;
            border-style: solid;
        }

        body, td, th {
            font-size: 13px;
        }

        .input1 {
            padding-left: 2px;
            padding-top: 5px;
            background-color: #FFFFFF;
            border-top-width: 0px;
            border-right-width: 0px;
            border-bottom-width: 1px;
            border-left-width: 0px;
            border-top-style: none;
            border-right-style: none;
            border-bottom-style: dashed;
            border-left-style: none;
            border-bottom-color: #FF0066;
        }

        .input2 {
            padding-left: 2px;
            padding-top: 5px;
            background-color: #FFFFFF;
            border-top-width: 0px;
            border-right-width: 0px;
            border-bottom-width: 0px;
            border-left-width: 0px;
            border-top-style: none;
            border-right-style: none;
            border-bottom-style: none;
            border-left-style: none;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div id="wrap">
            <div class="header">
                <div class="logo" style="height: 100px">
                </div>
                <div id="menu">
                    <uc:menucontrol id="MenuControl1" runat="server" />
                </div>
            </div>
            <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
            <div class="center_content" style="margin-left: 5px">

                <table cellpadding="0" border="0" cellspacing="0" style="width: 860px; margin-left: 10px">
                    <tr>
                        <td class="title">
                            <img src="/images/arrow.gif" />
                            志愿详情
                        </td>
                        <td class="title" align="right">
                            <input type="button" name="btnExit" class="register" onclick="javascript: window.location.href = '/webUI/Exit.aspx'"
                                value="退出系统">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px"></td>
                    </tr>
                </table>
                <div style="border: 5px solid #DFF3FE; background-color: #DFF3FE; margin-left: 10px; padding: 2px; width: 855px">
                    <div style="background-color: #FFFFFF">
                        <div class="tbltitle"><%= info.Kaocimc %>铜仁市中招考生志愿表(<%= info.xqmc %>)</div>
                        <table width="850" border="0" style="border-collapse: collapse;" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td align="right" width="80"><b>姓名：</b></td>
                                            <td align="left" width="120"><%= info.Xm %>  </td>
                                            <td align="right" width="80"><b>报名号：</b></td>
                                            <td align="left" width="180"><%= info.Ksh %>  </td>
                                            <td align="right" width="110"><b>毕业中学名称：</b></td>
                                            <td align="left"><%= info.Bmdmc %>  </td>

                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>

                                <td>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lblpc" Visible="false" />
                                                <span id="zyspan" runat="server"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                  
                                    <asp:label id="lblksh" runat="server" text="" visible="false"></asp:label>
                                    <asp:updatepanel id="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border: 1px solid #95DDFF; border-collapse: collapse">
                                                <tr>
                                                   
                                                    <td height="55" align="center"><br/>
                                                        <asp:Label Text="填报该批次志愿成功，可前往“录取实时查询”页面查询您的排名、录取情况！" runat="server" ForeColor="Red" Font-Size="18px" />

                                                        <asp:Repeater ID="Repeater1" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="tblcss" id="GridView1" border="1" bordercolor="#C1DAD7" style="text-align: center" cellspacing="0">

                                                                    <tr class="datagrid-header" style="height: 26px">
                                                                        <td>

                                                                            <b>学校代码</b></td>
                                                                        <td>
                                                                            <b>学校名称</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>类别</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>排名情况</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>最高分</b>
                                                                        </td>
                                                                         <td>
                                                                            <b>最低分</b>
                                                                        </td>
                                                                         <td>
                                                                            <b>填报人数</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>填报时间</b>
                                                                        </td>

                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="height: 24px">
                                                                    <td>
                                                                        <%#Eval("xxdm")%> </td>
                                                                    <td><%#Eval("zsxxmc")%> </td>
                                                                    <td><%#Eval("xpcMc")%></td>
                                                                    <td> 第<font color='red'><%#Eval("pm")%></font>名 </td>
                                                                    <td><%#Eval("maxnum")%></td>
                                                                     <td><%#Eval("minnum")%></td>
                                                                     <td><%#Eval("num")%></td>
                                                                     <td><%#Eval("lrsj")%></td>          
                                                                </tr>
                                                            </ItemTemplate>
                                                            
                                                            <FooterTemplate>
                                                                </table></FooterTemplate>
                                                        </asp:Repeater>



                                                        <asp:Timer ID="tim" runat="server" Interval="30000" OnTick="tim_Tick" Enabled="false"></asp:Timer>
 
                                                </td>
                                       </tr>
                                       </table>
                                       </ContentTemplate>
                                    </asp:updatepanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label Text="" runat="server" ID="lblxxdm" Visible="false" />
                                    <input id="isSave" type="hidden" />
                                    <asp:button id="btnSave" runat="server" cssclass="register" text="修改志愿" onclick="btnSave_Click" />
                                    &nbsp;&nbsp;
                             <asp:button id="btnKSQueren" runat="server" cssclass="register" text=" 确 认 "
                                 visible="false" onclick="btnKSQueren_Click" onclientclick="return confirm('一经确认资料就不能再修改,是否确认志愿信息?');" />
                                    &nbsp;&nbsp;
                             <asp:button id="btnBack" runat="server" cssclass="register" text=" 返 回 "  visible="false" 
                                 onclick="btnBack_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <!--end of center content-->
            </div>


           <uc1:FootControl ID="FootControl1" runat="server" />
            <!--end of footer-->
        </div>
    </form>
</body>
</html>
