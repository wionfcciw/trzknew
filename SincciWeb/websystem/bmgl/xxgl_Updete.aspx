<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xxgl_Updete.aspx.cs" Inherits="SincciKC.websystem.bmgl.xxgl_Updete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考生个人信息修改</title>
    
    <script language="javascript" src="../../js/checkSFZH.js" type="text/javascript"></script>  
        <script src="../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
</head>
<script  type="text/javascript" language="javascript">
    function csrq(minDate1, maxDate1) {
        WdatePicker({ skin: 'whyGreen', minDate: minDate1, maxDate: maxDate1 });
    }
    function change() {
        var img = document.getElementById("checkImg");
        img.src = img.src + '?';
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

    function YY_checkform() {

        if (document.getElementById("txtxm").value.length == 0) {
            alert('请输入姓名！');
            return false;
        }

        if (document.getElementById("ddlxb").value.length == 0) {
            alert('请选择性别！');
            return false;
        }
        if (document.getElementById("ddlmzdm").value.length == 0) {
            alert('请选择民族！');
            return false;
        }
        if (document.getElementById("ddlzzmmdm").value.length == 0) {
            alert('请选择政治面貌！');
            return false;
        }
        if (document.getElementById("ddlkslbdm").value.length == 0) {
            alert('请选择考生类别！');
            return false;
        }


        if (document.getElementById("txtsfzh").value.length == 0) {
            alert('请输入证件号码！');
            return false;
        }
        //检验大陆身份证号 证件类别1 代表身份证号
        if (document.getElementById("ddlzjlb").value == 1) {
            if (document.getElementById("txtsfzh").value.length > 0) {

                if (document.getElementById("txtsfzh").value.length != 18) {
                    alert('请输入18位身份证号！');
                    return false;
                }

                if (isCardID(document.getElementById("txtsfzh").value) == false) {
                    document.getElementById("txtsfzh").focus();
                    return false;
                }

                var card = document.getElementById("txtsfzh").value;
                var cardLen = document.getElementById("txtsfzh").value.length;
                var inDate = document.getElementById("txtcsrq").value;
                if (18 == cardLen) {

                    inDate = inDate.replace('-', '').replace('-', '');
                    cardDate = card.substring(6, 14);
                    if (cardDate != inDate) {
                        alert("身份证号码与出生日期不一致!");
                        return false;
                    }
                    if (card.substring(14, 17) % 2 == 0) {
                        if (document.getElementById("ddlxb").value != "2") {
                            alert("身份证号码与性别不一致"); //女
                            return false;
                        }

                    } else {
                        if (document.getElementById("ddlxb").value != "1") {
                            alert("身份证号码与性别不一致"); //男
                            return false;
                        }
                    }
                }
                else {

                    inDate = inDate.substring(2, 10).replace('-', '').replace('-', '');
                    cardDate = card.substring(6, 12);
                    if (cardDate != inDate) {
                        alert("身份证号码与出生日期不一致!");
                        return false;
                    }
                    if (card.substring(14, 15) % 2 == 0) {
                        if (document.getElementById("ddlxb").value != "2") {
                            alert("身份证号码与性别不一致"); //女
                            return false;
                        }

                    } else {
                        if (document.getElementById("ddlxb").value != "1") {
                            alert("身份证号码与性别不一致"); //男
                            return false;
                        }
                    }
                }

            }
        }
        //结束 

        //        if (document.getElementById("txtlxdh").value.length == 0&&) {
        //            alert('请输入家庭固定电话！');
        //            return false;
        //        }

        //        if (document.getElementById("txtyddh").value.length == 0) {
        //            alert('请输入家长移动电话！');
        //            return false;
        //        } 

        if (document.getElementById("txtyddh").value.length == 0) {
            alert('家长移动电话不能为空！');
            return false;
        }
        if (document.getElementById("txtlxdh").value == document.getElementById("txtyddh").value) {
            alert('请输入家庭固定电话和家长移动电话不能相同！');
            return false;
        }

        if (document.getElementById("txtyddh").value.length > 0) {
            if (document.getElementById("txtyddh").value.length != 11) {
                alert('家长移动电话不是11位！');
                return false;
            }
        }

        //        if (document.getElementById("txtxjh").value.length == 0) {
        //            alert('请输入学籍号！');
        //            return false;
        //        } 

        //毕业学校
        if (document.getElementById("ddlxqdm").value.length == 0) {
            alert('请输入毕业学校县区！');
            return false;
        }
        var obj = document.getElementById("ddlbyzxdm");
        if (obj != null) {
            if (document.getElementById("ddlbyzxdm").value.length == 0) {
                alert('请选择毕业学校！');
                return false;
            }

            if (document.getElementById("ddlbjdm").value.length == 0) {
                alert('请选择班级！');
                return false;
            }

        }
        else {
            if (document.getElementById("txtbyzxdm").value.length == 0) {
                alert('请输入毕业学校！');
                return false;
            }
        }
        //结束


        if (document.getElementById("ddlhjdq").value.length == 0) {
            alert('请选择户籍所在地！');
            return false;
        }
        var HDhjdq = document.getElementById("HDhjdq").value;
        var txthjdz = document.getElementById("txthjdz").value;

        if (txthjdz.length == 0 | HDhjdq == txthjdz) {
            alert('请输入户籍所在地详细地址！');
            return false;
        }

        if (txthjdz.substring(0, HDhjdq.length) != HDhjdq) {
            alert('户籍所在地县区与选择不一致！');
            return false;
        }

        if (document.getElementById("ddljtdq").value.length == 0) {
            alert('请选择通讯地址！');
            return false;
        }

        var HDjtdq = document.getElementById("HDjtdq").value;
        var txtjtdz = document.getElementById("txtjtdz").value;
        if (txtjtdz.length == 0 | HDjtdq == txtjtdz) {
            alert('请输入通讯地址详细地址！');
            return false;
        }
        if (txtjtdz.substring(0, HDjtdq.length) != HDjtdq) {
            alert('通讯地址所在县区与选择不一致！');
            return false;
        }


        if (document.getElementById("txtyzbm").value.length == 0) {
            alert('请输入邮政编码！');
            return false;
        }

        if (document.getElementById("txtsjr").value.length == 0) {
            alert('请输入收件人！');
            return false;
        }
    }
</script>
<body>
    <form id="form1" runat="server">
       <table  width="850" border="0" style="border-collapse: collapse;" align="center" cellpadding="0" cellspacing="0">

                     
                     <tr>
                         <td>
                             <table width="100%" border="1" bordercolor="#B0DFFD" style="border-collapse: collapse;   font-size:12px"
                                 align="center" cellpadding="3" cellspacing="0">
                                 <tr>
                                     <td align="right">
                                         报名号：
                                     </td>
                                     <td>
                                         <asp:Label ID="lblksh" runat="server" Text=" "></asp:Label>
                                          
                                     </td>
                                     <td align="right" colspan="2">
                                         <font color="red">*</font>姓名：
                                     </td>
                                     <td>
                                         <asp:TextBox ID="txtxm" CssClass="input1" runat="server" Width="100px" 
                                             MaxLength="20"></asp:TextBox>
                                     </td>
                                     <td align="right">
                                         <font color="red">*</font>性别：</td>
                                     <td>
                                         <asp:DropDownList ID="ddlxb" runat="server">
                                         </asp:DropDownList>
                                     </td>
                                     <td rowspan="5" align="center">
                                         <img src='ShowKSPic.aspx?ksh=<%=ksh %>' width="120px" height="160px" alt="考生相片" />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         &nbsp;<font color="red">*</font>民族：
                                     </td>
                                     <td>
                                         <asp:DropDownList ID="ddlmzdm" runat="server">
                                         </asp:DropDownList>
                                     </td>
                                     <td align="right" colspan="2">
                                         &nbsp;<font color="red">*</font>政治面貌：</td>
                                     <td>
                                         <asp:DropDownList ID="ddlzzmmdm" runat="server">
                                         </asp:DropDownList>
                                     </td>
                                     <td align="right"><font color="red">*</font>考生类别：</td>
                                     <td>
                                         <asp:DropDownList ID="ddlkslbdm" runat="server"   >
                                             </asp:DropDownList>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">考次：</td>
                                     <td><asp:Label ID="lblkaoci" runat="server" Text=""></asp:Label>
                                          
                                     </td>
                                     <td align="right">
                                         <font color="red">*</font>证件号码：</td>
                                     <td colspan="4">
                                         <asp:DropDownList ID="ddlzjlb" runat="server">
                                         </asp:DropDownList>
                                         <asp:TextBox ID="txtsfzh" CssClass="input1" MaxLength="18" runat="server" Width="160px"></asp:TextBox>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <font color="red">*</font>出生日期：</td>
                                     <td>

                                         <asp:TextBox ID="txtcsrq"  onFocus="WdatePicker({startDate:'%y-%M-01',dateFmt:'yyyy-MM-dd',alwaysUseStartDate:true})"   ReadOnly="true"
                                             CssClass="Wdate" runat="server" ></asp:TextBox>
                                     </td>
                                     <td align="right" colspan="2">
                                         </font>家庭固定电话：</td>
                                     <td>
                                         <asp:TextBox ID="txtlxdh" CssClass="input1" onKeyUp="checknum(txtlxdh);" Width="90px" runat="server" MaxLength="12"></asp:TextBox>
                                     </td>
                                     <td align="right">
                                         <font color="red">*</font>家长移动电话：</td>
                                     <td>
                                         <asp:TextBox ID="txtyddh" CssClass="input1" onKeyUp="checknum(txtyddh);" runat="server" MaxLength="11" 
                                             Width="90px"></asp:TextBox>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <font color="red">*</font>学籍号：</td>
                                     <td>
                                         <asp:TextBox ID="txtxjh" CssClass="input1"  Enabled="false" runat="server"  Width="145px"></asp:TextBox>
                                     </td>
                                     <td colspan="2" align="right">
                                         学生编码：</td>
                                     <td colspan="3">
                                         <asp:Label ID="lblxsbh" runat="server" Text=""></asp:Label>
                                         
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                          毕业中学：</td>
                                     <td colspan="4">
                                         <asp:Label ID="lblbmddm" runat="server" Text=" "></asp:Label></td>
                                     <td align="right">
                                          初二会考号：</td>
                                     <td colspan="2">
                                     <asp:TextBox ID="txtcrhkh" CssClass="input1"  Enabled="false" runat="server"  Width="145px"></asp:TextBox>
                                      </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <font color="red">*</font>毕业中学：</td>
                                     <td colspan="4">
                                         <asp:DropDownList ID="ddlxqdm" runat="server" AutoPostBack="true" 
                                             onselectedindexchanged="ddlxqdm_SelectedIndexChanged">
                                         </asp:DropDownList>
                                          <asp:DropDownList ID="ddlbyzxdm" runat="server"  
                                               >
                                         </asp:DropDownList>
                                        <asp:TextBox ID="txtbyzxdm" CssClass="input1" runat="server" Visible="false" 
                                             Width="221px" MaxLength="25"></asp:TextBox> 
                                         
                                     </td>
                                     <td align="right">
                                          <font color="red">*</font>班级：</td>
                                     <td colspan="2">
                                         <asp:DropDownList ID="ddlbjdm" runat="server">
                                         </asp:DropDownList>
                                         <asp:Label ID="lblbjdm" runat="server" Visible="false"  Text=""></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <font color="red">*</font>户籍所在地：
                                     </td>
                                     <td colspan="7">
                                         <asp:DropDownList ID="ddlhjdq" runat="server"   AutoPostBack="true"
                                             onselectedindexchanged="ddlhjdq_SelectedIndexChanged">
                                         </asp:DropDownList> 
                                         <asp:TextBox ID="txthjdz" CssClass="input1" runat="server" Width="600px" 
                                             MaxLength="100"  ></asp:TextBox>
                                             <asp:HiddenField Value="" ID="HDhjdq" runat="server" />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <font color="red">*</font>通讯地址：
                                     </td>
                                     <td colspan="7"><asp:DropDownList ID="ddljtdq" runat="server"   AutoPostBack="true"
                                             onselectedindexchanged="ddljtdq_SelectedIndexChanged">
                                         </asp:DropDownList> 
                                         <asp:TextBox ID="txtjtdz" CssClass="input1" runat="server" Width="600px" 
                                             MaxLength="100"></asp:TextBox>
                                             <asp:HiddenField Value="" ID="HDjtdq" runat="server" />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <font color="red">*</font>邮政编码：
                                     </td>
                                     <td>
                                         <asp:TextBox ID="txtyzbm" CssClass="input1" onKeyUp="checknum(txtyzbm);"  MaxLength="6" runat="server" Width="90px"></asp:TextBox>
                                     </td>
                                     <td align="right" colspan="2">
                                         &nbsp;<font color="red">*</font>收件人：</td>
                                     <td colspan="4">
                                         <asp:TextBox ID="txtsjr" CssClass="input1" runat="server" Width="104px" 
                                             MaxLength="20"></asp:TextBox>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <asp:Label ID="lblbzTitle" runat="server" Text=""></asp:Label> ：
                                     </td>
                                     <td colspan="7">
                                         <asp:TextBox ID="txtbz" runat="server" MaxLength="100" CssClass="input1" 
                                             Width="666px" Height="83px"  ></asp:TextBox>
                                     </td>
                                 </tr> 
                             </table>
                         </td>
                     </tr>
                     <tr><td>
                              <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0"  style=" border:1px solid #95DDFF ;border-collapse:collapse" >
                                <tr> 
                                    <td align="right" style="width:170px" valign="top"   style="color:red"> 提示：</td>
                                  <td height="55" align="left" > 
                                  <span style="color:red">1、带 * 号是必填。 
                                  <br/>2、请仔细检查你填写的资料，如确认无误请按下面的“保存”按钮。 
                                  </span>&nbsp;</td>
                                </tr>
                            </table>
                     </td></tr>
                     <tr>
                         <td align="center">
                             <asp:Button ID="btnSave" runat="server" CssClass="btnStyle" Text=" 保 存 "   OnClientClick="return YY_checkform()"
                                 onclick="btnSave_Click" />
                           
                         </td>
                     </tr>
                 </table>
    </form>
</body>
</html>
