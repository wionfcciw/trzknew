<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SincciKC.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>铜仁市高中阶段学校招生考试管理系统</title>
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <meta name="description" content="高中阶段学校招生考试管理系统" />
    <meta name="keywords" content="高中阶段学校招生考试管理系统，高中阶段学校招生考试管理系统" />
    <script type="text/javascript" src="/js/jquery-1.3.2.min.js"></script>
    <script src="js/Jquery.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="style.css" />

</head>
<body>
    <div id="wrap">
        <div class="header">
            <div class="logo" style="height: 100px"></div>
            <div id="menu">
                <uc:MenuControl ID="MenuControl1" runat="server" />
                
            </div>
        </div>
        <div class="center_content">
            <div class="left_content">

                <div class="county_c">
                    <div class="county_l">
                        <div class="county_r">
                            <h2>最新公告</h2>
                            <div>
                                <a href="webUI/news/NewsList.aspx?id=50">更多&gt;&gt;</a>
                            </div>
                        </div>
                    </div>
                    <div class="county_bot">
                        <dl class="county_dl" id="gonggao">
                           <%-- <font color="blue"> （注意：最低填报志愿资格线为400分，达不到资格线的考生无法登录填报志愿。）</font>--%>
                            <%=new BLL.BLL_news().ShowArticle(50, 7, 40).ToString() %>
                            <%--<dt><a href='/webUI/NewsInfo.aspx?NewsID=<%# Eval("N_NewID")%>'>
                                <%# Eval("Title")%>
                            </a></dt>--%>
                        </dl>
                    </div>
                </div>

            </div>

            <!--end of left content-->
            <div class="right_content">


                <div class="login">
                    <div class="topic  ">
                        <h2>考生登录</h2>
                        
                        <div class="clear">
                        </div>

                    </div>
                    <div class="rightarea h205">
                        <asp:Panel ID="Panel1" runat="server">

                            <table class="tabs1" cellpadding="4" cellspacing="3" style="margin: 0 auto;">
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="" runat="server" ID="lblname" />:
                                    </td>
                                    <td align="left">
                                        <input type="text" class="input3" maxlength="18" style="width: 140px" id="ksh" name="ksh" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">姓&nbsp;&nbsp;&nbsp;&nbsp;名:
                                    </td>
                                    <td align="left">
                                        <input type="text" class="input3" maxlength="15" style="width: 140px" id="xm" name="xm" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">密&nbsp;&nbsp;&nbsp;&nbsp;码:
                                    </td>
                                    <td align="left">
                                        <input type="password" class="input3" maxlength="12" style="width: 140px" id="pwd" name="pwd" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">验证码:
                                    </td>
                                    <td align="left">
                                        <input type="text" class="input2" maxlength="4" style="width: 60px; height: 17px" name="code"
                                            id="code" />
                                        <img src="/webUI/Image.aspx" height="26" width="70" align="middle" onclick="this.src=this.src+'?'" alt="看不清,换张图">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <input type="submit" name="btnLogin"
                                            value=" 登 录 " id="btnLogin" class="register" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" Visible="false" runat="server">
                            <table class="tabs1" cellpadding="4" cellspacing="3" style="margin: 0 auto;">
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <br />
                                        报名号:
                                    </td>
                                    <td align="left">
                                        <br />
                                        <asp:Label ID="lblksh" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <br />
                                        姓&nbsp;&nbsp;&nbsp;&nbsp;名:
                                    </td>
                                    <td align="left">
                                        <br />
                                        <asp:Label ID="lblxm" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <br />
                                        <input type="button" name="btnExit" class="register" onclick="javascript: window.location.href = '/webUI/Exit.aspx'"
                                            value="退出系统">
                                        &nbsp;&nbsp; 
                            <input type="button" name="btnPwdEdit" class="register" onclick="javascript: window.location.href = '/webUI/Ks_PwdEdit.aspx'"
                                value="修改密码">
                                    </td>
                                </tr>
                            </table>

                        </asp:Panel>
                    </div>
                </div>
            </div>

            <!--end of center content-->
        </div>
        <div class="center_content2">

            <div class="county_e">
                <div class="county_le">
                    <div class="county_re">
                        <h2>政策规定</h2>
                        <div>
                            <a href="webUI/news/NewsList.aspx?id=51">更多&gt;&gt;</a>
                        </div>
                    </div>
                </div>
                <div class="county_bote">
                    <dl class="county_dl" id="zhengce">
                        <%=new BLL.BLL_news().ShowArticle(51, 7, 20).ToString()%>
                    </dl>
                </div>
            </div>
            <div class="ge10"></div>
            <div class="county_e">
                <div class="county_le">
                    <div class="county_re">
                        <h2>中招宣传</h2>
                        <div>
                            <a href="webUI/news/NewsList.aspx?id=52">更多&gt;&gt;</a>
                        </div>
                    </div>
                </div>
                <div class="county_bote">
                    <dl class="county_dl" id="xuanchuan">
                        <%=new BLL.BLL_news().ShowArticle(52, 7, 20).ToString()%>
                    </dl>
                </div>
            </div>
            <div class="ge10"></div>
            <div class="county_e">
                <div class="county_le">
                    <div class="county_re">
                        <h2>常见问题</h2>
                        <div>
                            <a href="webUI/news/NewsList.aspx?id=54">更多&gt;&gt;</a>
                        </div>
                    </div>
                </div>
                <div class="county_bote">
                    <dl class="county_dl" id="wenti">
                        <%=new BLL.BLL_news().ShowArticle(54, 7, 20).ToString()%>
                    </dl>
                </div>
            </div>
        </div>

        <uc1:FootControl ID="FootControl1" runat="server" />
        <!--end of footer-->
    </div>
</body>
</html>
