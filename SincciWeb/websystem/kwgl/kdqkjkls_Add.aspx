<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kdqkjkls_Add.aspx.cs" Inherits="SincciKC.websystem.kwgl.kdqkjkls_Add" %>

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
                  alert("请输入身份证号");
                  return false;
              }

              if ($("#txtName").val() == "") {
                  $("#txtName").focus();
                  alert("请输入姓名");
                  return false;
              }
              if ($("#txtdw").val() == "") {
                  $("#txtdw").focus();
                  alert("请输入工作单位");
                  return false;
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
                <td colspan="2" class="title">考点监考老师登记</td>
            </tr>

            <tr>
                <td class="labelRedTD" style=" width:160px">身份证号：</td>
                <td class="contentTD">
               <asp:TextBox ID="txtksh" runat="server"    CssClass="input1"></asp:TextBox>

                    <asp:Label Text="" runat="server" ID="lblid" Visible="false" />
                </td>
            </tr>

            <tr>
                <td class="labelRedTD">姓名：</td>
                <td class="contentTD">
                    <asp:TextBox ID="txtName" runat="server"    CssClass="input1"></asp:TextBox>
                                      </td>
            </tr>
             <tr>
                <td class="labelRedTD">工作单位：</td>
                <td class="contentTD">
                    <asp:TextBox ID="txtdw" runat="server"    CssClass="input1"></asp:TextBox>
                                      </td>
            </tr>
            <tr>
                <td class="labelRedTD">评价1：</td>
                <td class="contentTD">
                    <asp:TextBox ID="pj1" runat="server"    CssClass="input1"></asp:TextBox>
                </td>
            </tr>
   <tr>
                <td class="labelRedTD">评价2：</td>
                <td class="contentTD">
                    <asp:TextBox ID="pj2" runat="server"    CssClass="input1"></asp:TextBox>
                </td>
            </tr>
               <tr>
                <td class="labelRedTD">评价3：</td>
                <td class="contentTD">
                    <asp:TextBox ID="pj3" runat="server"    CssClass="input1"></asp:TextBox>
                </td>
            </tr>
               <tr>
                <td class="labelRedTD">评价4：</td>
                <td class="contentTD">
                    <asp:TextBox ID="pj4" runat="server"    CssClass="input1"></asp:TextBox>
                </td>
            </tr>
               <tr>
                <td class="labelRedTD">评价5：</td>
                <td class="contentTD">
                    <asp:TextBox ID="pj5" runat="server"    CssClass="input1"></asp:TextBox>
                </td>
            </tr>
               <tr>
                <td class="labelRedTD">评价6：</td>
                <td class="contentTD">
                    <asp:TextBox ID="pj6" runat="server"    CssClass="input1"></asp:TextBox>
                </td>
            </tr>
               <tr>
                <td class="labelRedTD">评价7：</td>
                <td class="contentTD">
                    <asp:TextBox ID="pj7" runat="server"    CssClass="input1"></asp:TextBox>
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
