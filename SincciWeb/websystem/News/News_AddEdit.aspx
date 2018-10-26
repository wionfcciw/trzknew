<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News_AddEdit.aspx.cs" Inherits="SincciWeb.websystem.News.News_AddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加新闻</title>
<link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../css/page.css"  />
<script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
<link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="../../kindeditor/themes/default/default.css" type="text/css" />
	<link rel="stylesheet" href="../../kindeditor/plugins/code/prettify.css" type="text/css"/>
	<script type="text/javascript" charset="utf-8" src="../../kindeditor/kindeditor.js"></script>
	<script type="text/javascript" charset="utf-8" src="../../kindeditor/lang/zh_CN.js"></script>
	<script type="text/javascript" charset="utf-8" src="../../kindeditor/plugins/code/prettify.js"></script>
	<script type="text/javascript">
	    KindEditor.ready(function (K) {
	        var editor1 = K.create('#content1', {
	            cssPath: '../../kindeditor/plugins/code/prettify.css',
	            uploadJson: '../../kindeditor/asp.net/upload_json.ashx',
	            fileManagerJson: '../../kindeditor/asp.net/file_manager_json.ashx',
	            allowFileManager: true,
	            afterCreate: function () {
	                var self = this;
	                K.ctrl(document, 13, function () {
	                    self.sync();
	                    K('form[name=example]')[0].submit();
	                });
	                K.ctrl(self.edit.doc, 13, function () {
	                    self.sync();
	                    K('form[name=example]')[0].submit();
	                });
	            }
	        });
	        prettyPrint();
	    });
	</script>

    
    
<script  type="text/javascript" language="javascript">
 

    function YY_checkform() {

        if (document.getElementById("txtTitle").value.length == 0) {
            alert('请输入标题！');
            return false;
        } 
        if (document.getElementById("ddlCategorys").value.length == 0) {
            alert('请选择分类！');
            return false;
        }
       
    }

    

</script>

</head>
<body>
    <form id="form1" runat="server">
    <div> 
    
    <table width="100%"  border="0" style="margin-top:1px; margin-left:1px; margin-right:1px; margin-bottom:1px"   align="center" cellpadding="3" cellspacing="1" bgcolor="#cccccc">         
		  <tr bgcolor="#f5f5f5">
            <td width="130" align="right" style="height: 30px">新闻标题：</td>
            <td style="height: 30px">&nbsp;<asp:TextBox ID="txtTitle" runat="server" Width="659px"></asp:TextBox>                 
              </td>
		  </tr>
          <tr bgcolor="#f5f5f5">
            <td align="right">链接地址：</td>
            <td>&nbsp;<asp:TextBox ID="txtSource"  runat="server" Width="659px"></asp:TextBox>                
            </td>
         </tr> 
        <tr bgcolor="#f5f5f5">
            <td align="right">新闻分类：</td>
            <td> &nbsp;<asp:DropDownList ID="ddlCategorys" runat="server" ></asp:DropDownList> 
         </td>
        </tr>
        <tr bgcolor="#f5f5f5">
            <td align="right">详细内容：</td>
            <td>
             <textarea id="content1" cols="100" rows="8" style="width:100%;height:440px;visibility:hidden;" runat="server">
             </textarea>
            </td>
        </tr> 
        <tr bgcolor="#f5f5f5">
            <td align="right">&nbsp;</td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btnStyle" OnClientClick="return YY_checkform(); " Text=" 保 存 " style="height: 21px" />
            </td>
          </tr>
</table>
    </div>
    </form>
</body>
</html>
