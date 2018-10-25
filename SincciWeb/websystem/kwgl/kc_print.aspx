<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kc_print.aspx.cs" Inherits="SincciKC.websystem.kwgl.kc_print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考生考场座位查询</title>

<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />

    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function MsgYes() {
            var ids = "";
            $("[name='CheckBox1']").each(function () {
                if ($(this).attr("checked"))
                    ids +=  + $(this).val() + ",";
            });

            if (ids.length == 0) {
                alert("请选择需要删除的数据。");
                return false;
            }

            if (confirm('您确定要删除选中的信息吗？本次操作将不可恢复。')) {
                return true;
            }
            else {
                return false;
            }
        }

        function opgEdit() {
            var ids = "";

            $("[name='CheckBox1']").each(function () {
                if ($(this).attr("checked"))
                    ids = $(this).val();
            });
            if (ids.length > 0) {
                opdg(ids, "修改数据");
                return false;
            }
            else {
                ymPrompt.alert({ message: '请选择数据！', title: '提示' });
                return false;
            }
        }

        function opdg(ID, Title) {
            ymPrompt.win({ message: 'KaoDian_AddEdit.aspx?kddm=' + ID, width: 620, height: 500, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function opdg2(ID, Title) {
            ymPrompt.win({ message: 'KaoDian_Show.aspx?kddm=' + ID, width: 580, height: 480, title: Title, iframe: true, fixPosition: true, dragOut: false })
            return false;
        }
        function SelectAll() {
            var checkboxs = document.getElementsByName("CheckBox1");

            for (var i = 0; i < checkboxs.length; i++) {
                var e = checkboxs[i]; e.checked = !e.checked;
            }
        }
        function addMask() {
            var str = "<div id='backgroud' class='mask-backgroud'>";
            str += "<div id='image' class='mask-image'></div>";
            str += "<div id='text' class='mask-text'>请稍等，正在导入数据。。。。。</div>"
            str += "</div>";

            $("body").append(str);
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
    <style type="text/css">
        .btn
        {
            cursor: hand;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Div1" class="datagrid-toolbar">
   
       
        <asp:Button ID="btnPrint" runat="server" Text="   打印照片对照表" CssClass="icon-print" OnClick="btnSearch_Click" />
         <asp:Button ID="btnqdb" runat="server" Text="   打印签到表" CssClass="icon-print" 
            onclick="btnqdb_Click"   />
             <asp:Button ID="btnment" runat="server" Text="   打印门贴" 
            CssClass="icon-print" onclick="btnment_Click"  
           />
               <asp:Button ID="btnzhuot" runat="server" Text="   打印桌贴" 
            CssClass="icon-print" onclick="btnzhuot_Click"  
           />
    </div>
   
    <table class="tbColor" id="GridView1" border="1" style="border-collapse: collapse;"
        cellpadding="2" cellspacing="0">
        <asp:Repeater ID="repDisplay" runat="server"  >
            <HeaderTemplate>
                <tr class="datagrid-header" style="height: 26px">    
                  <td>
                        <b> <input type="checkbox" name="checkboxid" id="checkboxid"  onclick="SelectAll();"  />全选</b>
                    </td>
                      <td>
                        <b>考场</b>
                    </td>  
                     <td>
                        <b>县区代码</b>
                    </td>
                    <td>
                        <b>考点</b>
                    </td>
                     
                   
                    
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                     <td><input type="checkbox" name="CheckBox1" id="CheckBox1"   value='<%# Eval("kcdm") %>'> </td>
                     <td >
                         <%# Eval("kcdm").ToString().Substring(6)%></td>
                     <td>
                        <%# Eval("xqmc")%>
                    </td>
                    <td  >
                        <%# Eval("kdmc")%>
                    </td>
                    
                   
              
                    
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
  


    </form>
</body>
</html>