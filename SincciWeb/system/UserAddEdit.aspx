<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAddEdit.aspx.cs" Inherits="SincciWeb.system.UserAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改用户信息</title>

<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/page.css"  />
 
  <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#txtU_loginname").val() == "" ) {
                $("#txtU_loginname").focus();
                alert("请输入用户名");
                return false;
            }

            if ($("#ddlUserType").val() == "0") {
              //  $("#ddlUserType").focus();
                alert("请选择用户类型");
                return false;
            }
            if ($("#ddlUserType").val() == "5") {

                if ($("#ddlxqdm").val() == "" | $("#ddlxxdm").val() == "" | $("#ddlbjdm").val() == "") {
                    alert("请选择所属部门");
                    return false;
                }
            } else if ($("#ddlUserType").val() == "4") {

                if ($("#ddlxqdm").val() == "" | $("#ddlxxdm").val() == "") {
                    alert("请选择所属部门");
                    return false;
                }
            }
            else {
                if ($("#ddlxqdm").val() == "" ) {
                    alert("请选择所属部门");
                    return false;
                }
            }
            return true;
        }
    </script>
   
 <link href="../../css/style.css" rel="stylesheet" type="text/css" />
<script src="../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
<style type="text/css" >
 html 
{
	overflow-x: hidden;   /*- 横滚动条 -*/
	 
}
</style>

</head>
<body>
    <form id="form1" runat="server">
    <div> 
    <table class="windowTable" width="98%" border="1" align="center" style="border-collapse:collapse; margin-top:5px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
  <tr>
    <td align="right">登录帐号：</td>
    <td align="left"> 
        <asp:TextBox ID="txtU_loginname"  CssClass="input1" runat="server" Width="200px" MaxLength="15"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td align="right">用户名称：</td>
    <td align="left">
        <asp:TextBox ID="txtU_xm" runat="server" CssClass="input1"  Width="200px" MaxLength="15"></asp:TextBox>
      </td>
  </tr>
  <tr runat="server" id="trpwd">
    <td align="right">密&nbsp;&nbsp;&nbsp;&nbsp;码：</td>
    <td align="left">
        <asp:TextBox ID="txtU_password" runat="server" CssClass="input1" Width="200px" MaxLength="12"   Text="123456" ></asp:TextBox> 默认123456
      </td>
  </tr> 
  <tr>
    <td align="right">性&nbsp;&nbsp;&nbsp;&nbsp;别：</td>
    <td align="left">
        <asp:DropDownList ID="ddlxb" runat="server">
            <asp:ListItem>男</asp:ListItem>
            <asp:ListItem>女</asp:ListItem>
        </asp:DropDownList>
      </td>
  </tr>
  <tr>
   <td align="right">用户类型：</td>
    <td align="left">
        <asp:DropDownList ID="ddlUserType" runat="server"  AutoPostBack="true"
            onselectedindexchanged="ddlUserType_SelectedIndexChanged"  >  </asp:DropDownList>
        </td>
  </tr>
    <tr>
   <td align="right">所属部门：</td>
    <td align="left">
        <asp:DropDownList ID="ddlxqdm" runat="server"  AutoPostBack="true"
            onselectedindexchanged="ddlxqdm_SelectedIndexChanged"  >
            <asp:ListItem Value="">请选择</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlxxdm" runat="server" Visible="false"   AutoPostBack="true"
            onselectedindexchanged="ddlxxdm_SelectedIndexChanged">
        <asp:ListItem Value="">请选择</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlbjdm" runat="server" Visible="false">
        <asp:ListItem Value="">请选择</asp:ListItem>
        </asp:DropDownList>
        </td>
  </tr>
    <tr>
   <td align="right">联系人：</td>
    <td align="left">
        <asp:TextBox ID="txt6" runat="server" CssClass="input1"  Width="100px" MaxLength="10"></asp:TextBox>
      </td>
  </tr>
  <tr>
   <td align="right">移动电话：</td>
    <td align="left">
        <asp:TextBox ID="txtU_phone" runat="server" CssClass="input1"  Width="200px" MaxLength="20"></asp:TextBox>
      </td>
  </tr>
    <tr>
   <td align="right">办公电话：</td>
    <td align="left">
        <asp:TextBox ID="txt1" runat="server" CssClass="input1"  Width="200px" MaxLength="20"></asp:TextBox>
      </td>
  </tr>
    <tr>
   <td align="right">授权移动电话：</td>
    <td align="left">
        <asp:TextBox ID="txt2" runat="server" CssClass="input1"  Width="200px" MaxLength="20"></asp:TextBox>
      </td>
  </tr>
  <tr>
   <td align="right">通讯地址：</td>
    <td align="left">
        <asp:TextBox ID="txt3" runat="server" CssClass="input1"  Width="200px" MaxLength="30"></asp:TextBox>
      </td>
  </tr>
        <tr>
            <td align="right">
                邮政编码：
            </td>
            <td align="left">
                <asp:TextBox ID="txt4" runat="server" CssClass="input1" Width="60px" MaxLength="6"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                职务：
            </td>
            <td align="left">
                <asp:TextBox ID="txt5" runat="server" CssClass="input1" Width="150px" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
  <tr>
   <td align="right">角色：</td>
    <td align="left">
         <table>
            <tr>
                <td rowspan="2"><asp:ListBox ID="lstLeft" runat="server" Height="148px" 
                        Width="110px"></asp:ListBox></td>
                <td valign="top"><asp:Button ID="btnAdd" runat="server" Text="增加&gt;&gt;" 
                        onclick="btnAdd_Click" /></td>
                <td rowspan="2"><asp:ListBox ID="lstRight" runat="server" Height="148px" 
                        Width="110px"></asp:ListBox></td>                
            </tr>
            <tr>                
                <td valign="bottom"><asp:Button ID="btnDel" runat="server" Text="&lt;&lt;删除" 
                        onclick="btnDel_Click" /></td>
            </tr>
        </table>
      </td>
  </tr>
  <tr>
   <td align="right">状&nbsp;&nbsp;&nbsp;&nbsp;态：</td>
    <td align="left">
        <asp:DropDownList ID="ddlTag" runat="server">
            <asp:ListItem Value="1">开通</asp:ListItem>
            <asp:ListItem Value="-1">关闭</asp:ListItem>
        </asp:DropDownList></td>
  </tr>
  <tr>
    <td colspan="2" align="center">
      <asp:Button ID="btnSave" runat="server"  Text=" 保存 "  CssClass="btnStyle" OnClientClick="return checkInput()"  onclick="btnSave_Click" /> </td>
  </tr>
</table>
 
    </div>
    </form>
</body>
</html>
