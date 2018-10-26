<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KaoDian_AddEdit.aspx.cs" Inherits="SincciKC.websystem.kwgl.KaoDian_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增修改考点</title>
 <style type="text/css">
<!--
body,td,th {
	font-size: 12px;
}
-->
</style>

<script src="../../js/Jquery183.js" type="text/javascript"></script>
<script src="../../js/URL.js" type="text/javascript"></script>
<script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            if ($("#ddlxqdm").val() == "") {
                $("#ddlxqdm").focus();
                alert("请选择县区。");
                return false;
            }


            if ($("#txtkddm").val() == "") {
                $("#txtkddm").focus();
                alert("请输入考点代码。");
                return false;
            }
            var str = $("#txtkddm").val().toString();

            if (str.length !=4 ) {
                $("#txtkddm").focus();
                alert("请输入考点代码,考点代码为4位。");
                return false;
            }

            if ($("#txtkdmc").val() == "") {
                $("#txtkdmc").focus();
                alert("请输入考点名称。");
                return false;
            }
//            var objs = document.getElementsByTagName("input");
//            var flag = false;
//            for (var i = 0; i < objs.length; i++) {
//                if (objs[i].type == "checkbox" && objs[i].checked) {
//                    flag = true;
//            }
//            }
//            if (!flag) {
//                alert("最少选择一个毕业中学。");
//                return false;
//            }

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
        <table class="windowTable"  >           
             <tr>
                <td class="labelRedTD" style="width:100px" >选择县区：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="ddlxqdm" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlxqdm_SelectedIndexChanged">                         
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="labelRedTD">考点代码：</td>
                <td class="contentTD">
                    <asp:Label ID="lbllsh" runat="server" Visible="false"  Text=""></asp:Label>
                    <asp:TextBox ID="txtkddm" runat="server"  onKeyUp="checknum(this);" 
                        MaxLength="4" CssClass="input1" Width="97px"></asp:TextBox>
                     注：4位，前两位是考区代码，后两位流水号从01开始。
                </td>
            </tr> 
            <tr><td class="labelRedTD">考点名称：</td>
                <td class="contentTD"><asp:TextBox ID="txtkdmc" runat="server" CssClass="input1" 
                        Width="295px"></asp:TextBox></td>
            </tr>
            
             <%-- <tr>
                <td class="labelRedTD">毕业中学：</td>
                <td >
                    <div runat="server" id="bmdinfo"></div> 
                </td>
            </tr>--%>
          
            <tr >
                <td colspan="2" class="buttonBar">
                 <asp:Button ID="btnEnter" Text=" 保 存 " CssClass="btnStyle" runat="server" onclick="btnEnter_Click" OnClientClick="return checkInput()" />
                
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
