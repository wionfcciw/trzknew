<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HegeKs_AddEdit.aspx.cs" Inherits="SincciKC.websystem.zysz.HegeKs_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
        <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#txtksh").val() == "") {
                $("#txtksh").focus();
                alert("请输入报名号。");
                return false;
            }

            if ($("#txtxm").val() == "") {
                $("#txtxm").focus();
                alert("请输入姓名。");
                return false;
            }
        

            return true;
        }


        function checknum(theform) {
            if ((fucCheckNUM(theform.value) == 0)) {
                theform.value = "";
                //theform.newprice.focus();
                return false;
            }
        }
        function fucCheckNUM(NUM) {
            var i, j, strTemp;
            strTemp = "0123456789-";
            if (NUM.length == 0)
                return 0
            for (i = 0; i < NUM.length; i++) {
                j = strTemp.indexOf(NUM.charAt(i));
                if (j == -1) {
                    //说明有字符不是数字
                    return 0;
                }
            }
            //说明是数字
            return 1;
        }
    </script>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="windowTable">
            <tr>
                <td colspan="2" class="title">合格名单</td>
            </tr> 
                  <tr>
                <td class="labelRedTD">类型：</td>
                <td class="contentTD">
                                       <asp:DropDownList ID="dlistXx" runat="server" >
                   
                     <asp:ListItem Value="1">男儿幼儿师范</asp:ListItem>
                         <asp:ListItem Value="2">配额生</asp:ListItem>
                         <asp:ListItem Value="3">统招生</asp:ListItem>
                     
                       <%--  <asp:ListItem Value="6">三星普高国际班</asp:ListItem>--%>
                     </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="labelRedTD">报名号：</td>
                <td class="contentTD">
                    
                    <asp:TextBox ID="txtksh" runat="server"  onKeyUp="checknum(this);" MaxLength="12" CssClass="input1"></asp:TextBox>
                     
                </td>
            </tr> 
            <tr><td class="labelRedTD">姓名：</td>
                <td class="contentTD"><asp:TextBox ID="txtxm" runat="server" CssClass="input1"></asp:TextBox></td>
            </tr>
        
         
          
            <tr >
                <td colspan="2" class="buttonBar">
                 <asp:Button ID="btnEnter" Text=" 保 存 " CssClass="btnStyle" runat="server" onclick="btnEnter_Click" OnClientClick="return checkInput()" />
                
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
