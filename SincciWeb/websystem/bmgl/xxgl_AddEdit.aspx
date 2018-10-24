<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xxgl_AddEdit.aspx.cs" Inherits="SincciKC.websystem.bmgl.xxgl_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增、修改考生基本信息</title>

<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../css/page.css"  />
 
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
  <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../js/URL.js" type="text/javascript"></script>
    <script type="text/javascript">
        //检查数据录入的完整性
        function checkInput() {
            var str = "";
            if (document.getElementById("txtxm").value.replace(" ","") == "") {
                str = str + "-  姓名不能为空!\n";//   alert("姓名不能为空!");
                 
            }
//            if (document.getElementById("txtxj").value.replace(" ", "") == "") {
//                str = str + "-  学籍号不能为空!\n";
//            } else {
//              if (document.getElementById("txtxj").value.length != 19) 
//                 str = str + "-  学籍号应为19位!\n";
//            }
            if (document.getElementById("ddsfz").value == "0") {
                if (document.getElementById("txtsfzh").value.replace(" ", "") == "") {
                    str = str + "-  身份证号不能为空!\n";
                } else if (document.getElementById("txtsfzh").value.length!=18) {
                    str = str + "-  身份证号应为18位!\n";
                } else {
                    if (isCardID(document.getElementById("txtsfzh").value) != "") {
                        str = str + "-  " + isCardID(document.getElementById("txtsfzh").value) + "\n";
                    }
                }
            } else {
                if (document.getElementById("txtsfzh").value.replace(" ", "") == "") {
                    str = str + "-  其他证件号不能为空!\n";
                }
            }
            
            if (document.getElementById("ddlxqdm").value == "0") {
                str = str + "-  请选择毕业中学县区!\n";
                if (document.getElementById("ddlxxdm").value == "0") {
                    str = str + "-  请选择毕业中学!\n";
                }
            } else {
                var ddxqdm = document.getElementById("ddlxqdm").value;
                if (ddxqdm.substring(ddxqdm.length - 2) == "99") {

                    if (document.getElementById("txtxxdm").value.replace(" ", "") == "") {

                        str = str + "-  毕业中学不能为空!\n";
                      
                    }
                } else {
                    if (document.getElementById("ddlxxdm").value == "0") {
                        str = str + "-  请选择毕业中学!\n";
                    }
                }
            }
            if (document.getElementById("ddbmdxq").value == "0") {
                str = str + "-  请选择毕业中学县区!\n";
                if (document.getElementById("ddbmdxx").value == "0") {
                    str = str + "-  请选择毕业中学学校!\n";
                }
            } else {
                var ddxqdm = document.getElementById("ddbmdxq").value;
                if (ddxqdm.substring(ddxqdm.length - 2) == "99") {

                    if (document.getElementById("txtbmdxxdm").value.replace(" ", "") == "") {

                        str = str + "-  毕业中学学校不能为空!\n";
                      
                    }
                } else {
                    if (document.getElementById("ddbmdxx").value == "0") {
                        str = str + "-  请选择毕业中学学校!\n";
                    }
                }
            }
          
            if (str != "") {
                str = "保存错误: \n\n" + str;
                alert(str);
                return false;
            }
           
            return true;
        }
        function  isCardID(sId) {

            var aCity = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外" }
            var Errors = new Array(
"你输入的身份证长度或格式错误!",
"你的身份证地区错误!",
"身份证上的出生日期错误!",
"你输入的身份证号校验位错误!"
);
            var iSum = 0;
            var info = "";

            if (!/^\d{17}(\d|x)$/i.test(sId)) {

                return Errors[0];
            }

            sId = sId.replace(/x$/i, "a");
            if (aCity[parseInt(sId.substr(0, 2))] == null) {
                return Errors[1];
            }

            sBirthday = sId.substr(6, 4) + "-" + Number(sId.substr(10, 2)) + "-" + Number(sId.substr(12, 2));
            var d = new Date(sBirthday.replace(/-/g, "/"));
            if (sBirthday != (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate())) {
               
                return Errors[2];
            }
            for (var i = 17; i >= 0; i--) iSum += (Math.pow(2, i) % 11) * parseInt(sId.charAt(17 - i), 11);
            if (iSum % 11 != 1) {
                return Errors[3];
            }

            return "";
        } 
    </script>
<script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
   <link href="../../css/style.css" rel="stylesheet" type="text/css" />
 
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
    <table  class="windowTable" width="98%" border="1" align="center" style="border-collapse:collapse; margin-top:5px;" cellpadding="3" cellspacing="0" bordercolor="#cccccc">
    <tr>
    <td align="right"><asp:Label ID="Label7" Text="*" runat="server" ForeColor="Red" />考次：</td>
    <td align="left">
        <asp:DropDownList ID="ddlkc" runat="server">
            
        </asp:DropDownList>
      </td>
  </tr>
     <tr>
    <td align="right">
        <asp:Label Text="*" runat="server" ForeColor="Red" /> 姓名：</td>
    <td align="left">
        <asp:TextBox ID="txtxm" runat="server" Width="200px" MaxLength="20" CssClass="input1"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td align="right"><%--<asp:Label ID="Label1" Text="*" runat="server" ForeColor="Red" />--%>学籍号：</td>
    <td align="left">
        <asp:TextBox ID="txtxj" runat="server" Width="200px" MaxLength="19" CssClass="input1"></asp:TextBox>
      </td>
  </tr>
     <tr>
    <td align="right"><asp:Label ID="Label2" Text="*" runat="server" ForeColor="Red" />证件号：</td>
    <td align="left"> <asp:DropDownList ID="ddsfz" runat="server" >
        <asp:ListItem Text="身份证"  Value="0"/>
          <asp:ListItem Text="其他"  Value="1"/>
        </asp:DropDownList>
      
        <asp:TextBox ID="txtsfzh" runat="server" Width="200px" MaxLength="18" CssClass="input1"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td align="right"><asp:Label ID="Label3" Text="*" runat="server" ForeColor="Red" />性别：</td>
    <td align="left">
        <asp:DropDownList ID="ddlxb" runat="server" 
          >
            
        </asp:DropDownList>
      </td>
  </tr>
    <tr>
        <td align="right">
           <asp:Label ID="Label4" Text="*" runat="server" ForeColor="Red" />毕业中学县区：
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlxqdm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlxqdm_SelectedIndexChanged">
                <asp:ListItem Value="0">请选择</asp:ListItem>
            </asp:DropDownList>
                  
       
      
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Label5" Text="*" runat="server" ForeColor="Red" />毕业中学：
        </td>
        <td>
            <asp:DropDownList ID="ddlxxdm" runat="server"  AutoPostBack="true"
                OnSelectedIndexChanged="ddlxxdm_SelectedIndexChanged">
                <asp:ListItem Value="0">请选择</asp:ListItem>
            </asp:DropDownList>
             <asp:TextBox ID="txtxxdm" runat="server" Width="200px" MaxLength="30" CssClass="input1" Visible="false"></asp:TextBox>
        </td>
    </tr>
   
    <tr>
        <td align="right">
            班级：
        </td>
        <td>
            <asp:DropDownList ID="ddlbjdm" runat="server"  >
                <asp:ListItem Value="0">请选择</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
      <tr>
        <td align="right">
           <asp:Label ID="Label8" Text="*" runat="server" ForeColor="Red" />毕业中学县区：
        </td>
        <td align="left">
            <asp:DropDownList ID="ddbmdxq" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddbmdxq_SelectedIndexChanged" >
                <asp:ListItem Value="0">请选择</asp:ListItem>
            </asp:DropDownList>
                  
       
      
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Label9" Text="*" runat="server" ForeColor="Red" />毕业中学学校：
        </td>
        <td>
            <asp:DropDownList ID="ddbmdxx" runat="server"   >
                <asp:ListItem Value="0">请选择</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
   <td align="right"><asp:Label ID="Label6" Text="*" runat="server" ForeColor="Red" />考生类别：</td>
    <td align="left">
        <asp:DropDownList ID="ddlkslb" runat="server" 
              >  </asp:DropDownList>
        </td>
  </tr>
    <tr>
    <td align="right">学生编号：</td>
    <td align="left">
        <asp:TextBox ID="txtxsbh" runat="server" Width="200px" CssClass="input1"
           MaxLength="22" ></asp:TextBox>
      </td>
  </tr>
  
  
  
  
  
    
  
  <tr>
    <td colspan="2" align="center">
      <asp:Button ID="btnSave" runat="server"  CssClass="btnStyle"  Text=" 保存 " OnClientClick="return checkInput()"  onclick="btnSave_Click" /> </td>
  </tr>
</table>
 
    </div>
    </form>
</body>
</html>
