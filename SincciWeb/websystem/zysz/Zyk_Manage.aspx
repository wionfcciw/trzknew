<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zyk_Manage.aspx.cs" Inherits="SincciKC.websystem.zysz.Zyk_Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专业管理</title>

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
        ymPrompt.win({ message: 'Zyk_AddEdit.aspx?ID=' + ID, width: 440, height: 250, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }

    function opdy(ID, Title) {

        //window.parent.addTab2(Title, '/websystem/bmgl/Printxx.aspx?ksh=' + ID + '&title=' + Title);
        //  ymPrompt.win({ message: 'xxglMange.aspx.aspx?ID=' + ID, width: 340, height: 300, title: Title, iframe: true, fixPosition: true, dragOut: false })
        return false;
    }

    function addMask() {
        var str = "<div id='backgroud' class='mask-backgroud'>";
        str += "<div id='image' class='mask-image'></div>";
        str += "<div id='text' class='mask-text'>请稍等，正在导入数据。。。。。</div>"
        str += "</div>";

        $("body").append(str);
    }
</script>

<style type="text/css">
.btn
{
	 cursor: hand;
        height: 21px;
    }
  
</style>

</head>
<body>
    <form id="form1" runat="server"  >
     
    
     <div id="tb" class="datagrid-toolbar"  >
         <table>
             <tr> 
              <td>
                   选择学校：
                 </td>
                  <td>
                      <asp:DropDownList ID="ddlzsxx" runat="server"  AutoPostBack="true"
                          onselectedindexchanged="ddlzsxx_SelectedIndexChanged">
                      </asp:DropDownList>
                 </td>
                 <td>
                     专业代码/专业名称：
                 </td>
                 <td>
                     <asp:TextBox ID="txtName" CssClass="searchbox" runat="server" Width="150px"></asp:TextBox>
                     <asp:Button ID="btnSearch" runat="server" Text="  查询" CssClass="icon-search" OnClick="btnSearch_Click" />
                 </td>
                 
             </tr>
             </table>
             </div>
             <div  class="datagrid-toolbar"  >
             <table>
             <tr>
                 <td>
                     <asp:Button ID="btnDelete" runat="server" Text="  删除" CssClass="icon-remove btn"
                         OnClientClick="return MsgYes();" OnClick="btnDelete_Click" />
                 </td>
                 <td>
                     <asp:Button ID="btnNew" runat="server" Text="  新增" CssClass="icon-add btn" OnClientClick="return opdg('0&zydm=0' ,'修改数据');"  />
                 </td>
                 
             </tr>
         </table>
          
    </div> 
      <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table class="tbColor" id="GridView1" border="1" style="border-collapse:collapse;  "  cellpadding="2" cellspacing="0" >
                <tr class="datagrid-header" style="height: 26px">
                    <td>
                        <b>全选
                           <input type="checkbox" name="checkboxid" id="checkboxid"  onclick="SelectAll(this)"  /></b>
                    </td>
                    <td>
                        <b>序号</b>
                    </td>
                    <td>
                        <b>所属学校</b>
                    </td>
                    <td>
                        <b>专业代码</b>
                    </td>
                    <td>
                        <b>专业名称</b>
                    </td>
                      <td>
                        <b>修改</b>
                    </td>
                </tr>
        </HeaderTemplate>
         <ItemTemplate> 
                   <tr style="height: 24px" class="datagrid-body" <%# (Container.ItemIndex+1) % 2 ==0?"bgcolor='#F7F7F7'":"" %>>
                   <td><input type="checkbox" name="CheckBox1" id="CheckBox1" value='<%# Eval("xxdm") %>|<%# Eval("zydm") %>'> </td>
                    <td> 
                       <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.ItemIndex + 1%>
                       
                    </td>
                     <td align="left">
                        <%# Eval("dmmc")%>
                    </td>
                    <td >
                        <%# Eval("zydm")%>
                    </td>
                    <td align="left">
                        <%# Eval("zymc")%>
                    </td> 
                    <td>
                        <a href="#" onclick="return opdg('<%# Eval("xxdm") %>&zydm=<%# Eval("zydm") %>' ,'修改数据');">
                            <image src="/easyui/themes/icons/pencil.png" alt="修改" border="0"></image>
                        </a>
                    </td>
         </ItemTemplate>

         <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater> 

    <div id="tbpage" class="datagrid-toolbar" style="padding-left:10px" >
        <table width="100%">
            <tr> <td>
             <webdiyer:aspnetpager id="AspNetPager1" runat="server"   onpagechanged="AspNetPager1_PageChanged"  Width="100%"   >
               </webdiyer:aspnetpager></td>
               <td>每页：<asp:DropDownList ID="ddlPageSize" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="ddlPageSize_SelectedIndexChanged">                     
            </asp:DropDownList>条</td>
         </tr>
       </table> 
    </div> 
      <div class="datagrid-toolbar" >
    Excel数据导入<asp:FileUpload ID="fuExcelFileImport" runat="server" />
    <asp:Button ID="btnExcelFileImport" runat="server" Text="导入"
        CssClass="icon-reload btn" Width="60" onclick="btnExcelFileImport_Click" OnClientClick="addMask();"/>
        <div id="msgWindow" title="导入日志" class="easyui-window" closed="true" maximizable="false" collapsible="false" minimizable="false" style="width:385px;height:270px;" runat="server">
        </div>
         
        <a href="../../Template/zykTemplate.xls">模板下载</a>
    </div>

    <asp:HiddenField ID="hfDelIDS" runat="server" />
    </form>
</body>
</html>
