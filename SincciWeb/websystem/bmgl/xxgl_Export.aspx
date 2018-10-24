<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xxgl_Export.aspx.cs" Inherits="SincciKC.websystem.bmgl.xxgl_Export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
     <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
       <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#txtZsxxdm").val() == "") {
                $("#txtZsxxdm").focus();
                alert("请输学校代码");
                return false;
            }

            if ($("#txtZsxxmc").val() == "") {
                $("#txtZsxxmc").focus();
                alert("请输入学校名称");
                return false;
            }

            return true;
        }
    </script>
  
</head>
<body>
    <form id="form1" runat="server">
    <span></span>
        <table class="windowTable">
            <tr>
                <td class="title">请选择您要导出的方式
</td>
            </tr>

            <tr>
                <td  align="center" >
                    <asp:RadioButton ID="rdoexcel" runat="server" Text="导出EXCEL" 
                       GroupName="one" Checked="true" /> </td>
                    </tr>
<tr>
                <td  align="center">
                    <asp:RadioButton ID="RadioButton2" runat="server" Text="导出DBF" 
                     GroupName="one"  />&nbsp;&nbsp;</td>
                    </tr>
    
     

            <tr >
                <td   class="buttonBar">
                    <asp:Button ID="btnEnter" Text=" 导 出 " CssClass="btnStyle" runat="server" onclick="btnEnter_Click"  />
                </td>
            </tr>
        </table>
        <div  runat="server" id="div"></div>
    </form>
</body>
</html>
