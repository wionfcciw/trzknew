<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ks_PwdEdit.aspx.cs" Inherits="SincciKC.webUI.Ks_PwdEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />

    
<script  type="text/javascript" language="javascript">

    function change() {
        var img = document.getElementById("checkImg");
        img.src = img.src + '?';
    }

    function YY_checkform() {

        if (document.getElementById("txtOld").value.length == 0) {
            alert('请输入原密码！');
            return false;
        }  
        if (document.getElementById("txtpwd").value.length == 0) {
            alert('请输入新密码！');
            return false;
        }
        if (document.getElementById("txtpwd").value.length < 6 ) {
            alert('密码必须大于等于6位！');
            return false;
        }  

        if (document.getElementById("txtpwd1").value.length == 0) {
           alert('请再次输入新密码！');
            return false;
        }
        if (document.getElementById("txtpwd1").value != document.getElementById("txtpwd").value) {
             alert('输入的两次密码不相同，请重新输入！'); 
            return false;
        }
    }

    function checknum(theform) {
        if ((fucCheckNUM(theform.value) == 0)) {
            theform.value = "";
            //theform.newprice.focus();
            return false;
        }
    }
    function fucCheckNUM(NUM) {
        if (NUM.length == 0)
            return 0
        for (i = 0; i < NUM.length; i++) {
            n = name.charCodeAt(i);
            //把字符串中第i个字符的ASCALL值赋给变量n       
            if (!((n >= 48 && n <= 57) || (n >= 65 && n <= 90) || (n >= 97 && n <= 122) || n == 95)) {   //48=0，57=9,65=A,90=Z,97=a,122=z,95=_,.=46,@=64           

                return 0;

            }
        }
        //符合要求
        return 1;
         
    }

</script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="wrap">
        <div class="header">
            <div class="logo"  style="height:108px">
                </div>
            <div id="menu">
                <ul>
                    <%--<li class="selected"><a href="/index.htm">首页</a></li>--%>
                    <%-- <li><a href="/webUI/zkbm/zkbm_Input.aspx">考生报名</a></li>
                    <li><a href="/webUI/zytb/zytb_Input.aspx">志愿填报</a></li>--%>
                </ul>
            </div>
        </div>
        <div class="center_content" style="height: 330px">
            <div class="title">
                <img src="/images/arrow.gif" />
                修改密码</div>
            <table align="center" width="510" height="230" style="border:1px solid #209797;background-color:#E0F3FD">
                <tr>
                    <td>
                        <table width="500" align="center"   >
                            <tr>
                                <td align="right">
                                    准考证号：
                                </td>
                                <td>
                                    <asp:Label ID="lblksh" runat="server" Text=" "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    姓名：
                                </td>
                                <td>
                                    <asp:Label ID="lblxm" runat="server" Text=" "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    原密码：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOld" CssClass="input1" MaxLength="12" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    新密码：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpwd" CssClass="input1" onkeyup="value=value.replace(/[^\w\.\/]/ig,'')"
                                        MaxLength="12" runat="server" TextMode="Password"></asp:TextBox>
                                    6-12位的数字、字母、下划线
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    确认密码：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpwd1" CssClass="input1" onkeyup="value=value.replace(/[^\w\.\/]/ig,'')"
                                        MaxLength="12" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSave" runat="server" CssClass="register" Text=" 保存 " OnClientClick="return YY_checkform()"
                                        OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
        </div>
        </td></tr></table>
        <!--end of center content-->
    </div>
   
    <!--end of footer-->
 
    </form>
</body>
</html>
