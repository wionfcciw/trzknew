<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JhglList.aspx.cs" Inherits="SincciKC.websystem.zklq.JhglList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考生前台招生计划管理</title>

    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css"  />
    <script src="../../js/addTableListener.js" type="text/javascript"></script>
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <script src="../../js/Jquery183.js" type="text/javascript"></script>
    <script src="../../easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <link href="../../easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../easyui/themes/icon.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function MsgYes() {
        var ids = "";
        $("[name='CheckBox1']").each(function () {
            if ($(this).attr("checked"))
                ids += $(this).val() + ",";
        });

        if (ids.length > 0)
            $("#hfDelIDS").val(ids.substr(0, ids.length - 1));
        else {
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

    //全选、全不选
    function SelectAll(obj) {
        //获取所有的input的元素
        var inputs = document.getElementsByTagName("input");

        for (var i = 0; i < inputs.length; i++) {
            //如果是复选框
            if (inputs[i].type == "checkbox") {
                inputs[i].checked = obj.checked;
            }
        }
    }



    function opdg(ID, Title) {
        //window.parent.addTab2(Title, '/websystem/zysz/Zydz_xxglDetails.aspx?ksh=110&title=' + Title);
        //window.parent.addTab2(Title, '/websystem/bmgl/Zydz_xxglDetails.aspx?ksh=' + ID + '&title=' + Title);
        ymPrompt.win({ message: 'ZsjhEdit.aspx?ID=' + ID, width: 440, height: 370, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }
    function opdy(ID, Title) {

        //window.parent.addTab2(Title, '/websystem/bmgl/Printxx.aspx?ksh=' + ID + '&title=' + Title);
        //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
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
    <form id="form1" runat="server"  >
       <div  class="datagrid-toolbar"  >
             <table>
             <tr>
                 <td>
                     <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
                         OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
                 </td>
                 <td>
                     <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn" OnClientClick="return opdg('0' ,'新增数据');" />
                 </td>
                
             </tr>
         </table>
          
    </div> 
     
      <asp:Repeater ID="repDisplay" runat="server">
        <HeaderTemplate>
            <table class="tbColor" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >
                <tr class="datagrid-header" style="height: 26px">
                     <td>
                        <b>全选
                           <input type="checkbox" name="checkboxid" id="checkboxid"  onclick="SelectAll(this)"  /></b>
                    </td>
                    <td>
                        <b>学校代码</b>
                    </td>
                      <td>
                        <b>计划数</b>
                    </td>
                   
                     <td>
                        <b>县区代码</b>
                    </td>
                     <td>
                        <b>修改</b>
                    </td>

                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
                   <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                   <td><input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("id") %>'> </td>
                   
                     <td>
                        [<%# Eval("xxdm")%><%# Eval("zsxxmc")%>]</td>
                    
                      <td  >
                        <%# Eval("jhs")%>
                    </td>
                      <td >
                        <%# Eval("xqdm")%>
                    </td>
                
                    <td>
                        <a href="#" onclick="return opdg('<%# Eval("id") %>' ,'修改数据');">
                            <image src="/easyui/themes/icons/pencil.png" alt="修改" border="0"></image>
                        </a>
                    </td>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater> 
    <asp:HiddenField ID="hfDelIDS" runat="server" />
    </form>
</body>
</html>