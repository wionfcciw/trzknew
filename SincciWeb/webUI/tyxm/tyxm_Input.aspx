<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tyxm_Input.aspx.cs" Inherits="SincciKC.webUI.tyxm.tyxm_Input" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考生网上报名</title>
 <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="/style.css" />
    <script src="../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" src="../../js/checkSFZH.js" type="text/javascript"></script>  
     <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

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
                }
                else {

                    inDate = inDate.substring(2, 10).replace('-', '').replace('-', '');
                    cardDate = card.substring(6, 12);
                    if (cardDate != inDate) {
                        alert("身份证号码与出生日期不一致!");
                        return false;
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

    <style type="text/css">
       
    </style>

</head>
<body>
    <form id="form1" runat="server">
     <div id="wrap">
        <div class="header">
            <div class="logo" style="height:100px"> </div>
            <div id="menu"><uc:MenuControl ID="MenuControl1" runat="server" /> </div>
        </div>
        <div class="center_content" style=" margin-left:5px" > 
            <table cellpadding="0" border="0" cellspacing="0" style=" width:860px; margin-left:10px"  >
                <tr>
                    <td class="title">
                        <img src="/images/arrow.gif" />
                        信息采集
                    </td>
                    <td class="title" align="right">
                        <input type="button" name="btnExit" class="register" onclick="javascript:window.location.href='/webUI/Exit.aspx'"
                            value="退出系统">
                    </td>
                </tr>
                <tr><td style=" height:5px"></td></tr>
            </table>
             <div  style="border:5px solid #DFF3FE; margin-left:10px ; padding:2px; width:855px" >
                <div  class="tbltitle" >铜仁市<asp:Label runat="server" ID="SysYear"></asp:Label>中等学校体育考试考生报名信息采集表<asp:Label 
                        runat="server" ID="lblxb" Visible="False"></asp:Label></div>
                 
                 <table  width="850" border="0" style="border-collapse: collapse;" align="center" cellpadding="0" cellspacing="0">

                     
                     <tr>
                         <td>
                             <table width="100%" border="1" bordercolor="#B0DFFD" style="border-collapse: collapse;   font-size:12px"
                                 align="center" cellpadding="3" cellspacing="0">
 
                                 <tr>
                                     <td align="right">
                                        毕业中学：</td>
                                     <td>
                                     <asp:Label ID="lblbmd" runat="server" Text=""></asp:Label>
                                     </td>
                                     
                                    <td align="right">
                                        考次：</td>
                                     <td>
                                    
                                               <asp:Label ID="lblkaoci" runat="server" Text=""></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                       报名号：</td>
                                     <td>
                                        <asp:Label ID="lblksh" runat="server" Text=""></asp:Label> </td>
                                   <td align="right">
                                      姓名：  </td> 
                                     <td>
                                    
                                                 <asp:Label ID="lblxm" runat="server" Text=""></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                              <td align="right">
                                       身份证号：</td>
                                     <td>
                                       <asp:Label ID="lblsfzh" runat="server" Text=""></asp:Label>  </td>
                                   <td align="right">
                                      班级：  </td> 
                                     <td>
                                    
                                             <asp:Label ID="lblbj" runat="server" Text=""></asp:Label>   </td>
                                 </tr>

                                 <tr><td colspan="4">体育考试信息</td></tr>
                             <%--     <tr>
                                              <td align="right">
                                       <font color="red">*</font>必考项目：</td>
                                       <td colspan="3">
                                           <span id="bikao" runat="server"></span>   </td>
                                 </tr>
                                  <tr>
                                              <td align="right">
                                       <font color="red">*</font>抽定项目：</td>
                                       <td colspan="3"> <span id="chouding" runat="server"></span> 
                                           <asp:Button ID="btnchoud" runat="server" Text="进行随机抽签" 
                                               onclick="btnchoud_Click" /> <asp:Label ID="lblcdz" runat="server" Text="注:点击按钮系统将自动随机读取项目,一旦读取成功无法再选!" ForeColor="Red"></asp:Label>  </td>
                                 </tr>--%>
                                  <tr>
                                              <td align="right">
                                       <font color="red">*</font>自选项目1：</td>
                                       <td colspan="3"> <span id="zixuan" runat="server"></span>    
                                           <asp:RadioButtonList ID="rdozixuan" runat="server" RepeatDirection="Horizontal" 
                                               AutoPostBack="True" onselectedindexchanged="rdozixuan_SelectedIndexChanged" 
                                               RepeatColumns="8" RepeatLayout="Flow">
    </asp:RadioButtonList><font color="red">注:需手动选择其中的一项!</font> 
                                            </td>
                                 </tr>
                                  <tr>
                                              <td align="right">
                                        <font color="red">*</font>自选项目2：</td>
                                       <td colspan="3">  <span id="beixuan" runat="server"></span>   
                                           <asp:RadioButtonList ID="rdobeixuan" runat="server" 
                                               RepeatDirection="Horizontal" AutoPostBack="True" 
                                               onselectedindexchanged="rdobeixuan_SelectedIndexChanged" RepeatColumns="8" 
                                               RepeatLayout="Flow"> 
    </asp:RadioButtonList><font color="red">注:需手动选择其中的一项!</font> </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                          <font color="red">*</font>自选项目3：
                                     </td>
                                     <td colspan="3">
                                         <span id="Span1" runat="server"></span>
                                         <asp:RadioButtonList ID="rdozixuan3" runat="server" RepeatDirection="Horizontal"
                                             AutoPostBack="True" OnSelectedIndexChanged="rdozixuan3_SelectedIndexChanged" RepeatColumns="8"
                                             RepeatLayout="Flow">
                                         </asp:RadioButtonList><font color="red">注:需手动选择其中的一项!</font> 
                                       <%--  <font color="red">注:需手动选择其中的一项!</font>--%>
                                     </td>
                                 </tr>
                               </table>
                      
                        
                         </td>
                     </tr>
                   
                     <tr>
                         <td align="center">
                             <asp:Button ID="btnSave" runat="server" CssClass="register" Text=" 保 存 " 
                                 onclick="btnSave_Click" />
                             &nbsp;&nbsp;
                             <%--<asp:Button ID="btnBack" runat="server" CssClass="register" Text=" 返 回 " 
                                 onclick="btnBack_Click"  />--%>
                         </td>
                     </tr>
                 </table>
             </div>
            <!--end of center content-->
        </div>
        

      <uc1:FootControl ID="FootControl1" runat="server" /> 
        <!--end of footer--> 
    </div>
    </form>
</body>
</html>
