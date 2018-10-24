<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loginzxc.aspx.cs" Inherits="SincciWeb.ht.Loginzxc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>铜仁市高中阶段学校招生考试管理系统</title> 
 <link href="css.css" rel="stylesheet" type="text/css" /> 
<link rel="stylesheet" type="text/css" href="../css/main.css" /> 
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

<script  type="text/javascript" language="javascript">

    function change() {
        var img = document.getElementById("checkImg");
        img.src = img.src + '?';
    }

    function YY_checkform() {


        if (document.getElementById("txtUserName").value.length == 0) {
            ymPrompt.alert({ title: '提示', message: '请输入用户名！' });
            document.forms["form1"].txtUserName.focus();
            return false;
        }
        if (document.getElementById("txtPassword").value.length == 0) {
            ymPrompt.alert('请输入用户密码！');
            document.getElementById("txtPassword").focus();
            return false;
        }

        if (document.getElementById("txtcheck").value.length != 4) {
            ymPrompt.alert('请输入验证码！');
            document.getElementById("txtcheck").focus();
            return false;
        }
    }

</script>

</head>
<body  >
    <form id="form1" runat="server"  > 
 <div class="login_a">
    <div class="login_kuang">
         <div class="login_denglu" style=" width:270px">
             <table cellpadding="1" cellspacing="2" border="0">
                <tr><td colspan="3" style="height:10px"></td></tr> <tr>
                     <td>
                         登录帐号：
                     </td>
                     <td colspan="2">
                         <asp:TextBox ID="txtUserName" runat="server" Height="18px" Width="155px" MaxLength="12"></asp:TextBox>
                     </td> 
                 </tr>
                 <tr>
                     <td>
                         登录密码：
                     </td>
                     <td colspan="2">
                         <asp:TextBox ID="txtPassword" runat="server" Width="155px" Height="18px" TextMode="Password"></asp:TextBox>
                     </td> 
                 </tr>
                 <tr>
                     <td>
                         验&nbsp;证&nbsp;码：
                     </td>
                     <td>
                         <asp:TextBox ID="txtcheck" runat="server" Height="18px" Width="60px" MaxLength="4"
                             Style="margin-left: 0px"></asp:TextBox>
                         <td>
                             <img src="Image.aspx" id="checkImg" runat="server" style="width: 70px; height: 22px;"
                                 alt="看不清,换张图" onclick="this.src=this.src+'?'" />
                         </td>  
                     </td>
                 </tr>
                 <tr>
                     <td colspan="3" align="center">
                         <asp:ImageButton ID="btnLogin" ImageUrl="images/bt_login.gif" OnClick="btnLogin_Click"
                             OnClientClick="return YY_checkform();" runat="server" />
                     </td>  
                 </tr>
                 
             </table>
         </div>
       </div>
     </div>
    </form>
</body>
</html>
