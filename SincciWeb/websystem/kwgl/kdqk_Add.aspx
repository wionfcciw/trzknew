<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kdqk_Add.aspx.cs" Inherits="SincciKC.websystem.kwgl.kdqk_Add" %>

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
                  alert("请输入考试证号");
                  return false;
              }

//              if ($("#txtName").val() == "") {
//                  $("#txtName").focus();
//                  alert("请输入姓名");
//                  return false;
//              }
              if ($("#listkemu").val() == "") {
                  $("#listkemu").focus();
                  alert("请选择科目");
                  return false;
              }

              if ($("#listleib").val() == "") {
                  $("#listleib").focus();
                  alert("请选择考场情况类别");
                  return false;
              } else {

                  if ($("#listleib").val() == "4") {
                      if ($("#txtqk").val() == "") {
                          $("#txtqk").focus();
                          alert("请填写情况说明");

                          return false;
                      }

                  }
              }
              return true;
          }
      </script>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />


</head>
<body>
    <form id="form1" runat="server">
        <table class="windowTable">
            <tr>
                <td colspan="2" class="title">考场情况登记</td>
            </tr>

            <tr>
                <td class="labelRedTD" style=" width:160px">考试证号：</td>
                <td class="contentTD">
               <asp:TextBox ID="txtksh" runat="server"    CssClass="input1" AutoPostBack="True" 
                        ontextchanged="txtksh_TextChanged"></asp:TextBox>

                    <asp:Label Text="" runat="server" ID="lblid" Visible="false" />
                </td>
            </tr>

            <tr>
                <td class="labelRedTD">姓名：</td>
                <td class="contentTD">  <asp:Label Text="" runat="server" ID="txtName"   />
                    
                                      </td>
            </tr>

            <tr>
                <td class="labelRedTD">科目：</td>
                <td class="contentTD">
                    <asp:DropDownList ID="listkemu" runat="server" Enabled="False"  >
                    
                     </asp:DropDownList>
                
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">考场情况类别：</td>
                <td class="contentTD">
                  <asp:DropDownList ID="listleib" runat="server"  >
                     </asp:DropDownList>
                
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">情况说明：</td>
                <td class="contentTD">
    <asp:TextBox Width="300" Height="60" TextMode="MultiLine" ID="txtqk" runat="server" Text=""> </asp:TextBox> 
                </td>
            </tr>
              <tr>
                <td class="labelRedTD">备注：</td>
                <td class="contentTD">
                    <asp:TextBox Width="300" Height="60" TextMode="MultiLine" ID="txtbz" runat="server" Text=""> </asp:TextBox> 
                </td>
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
