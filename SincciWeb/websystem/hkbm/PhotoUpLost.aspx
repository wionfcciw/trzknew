<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoUpLost.aspx.cs" Inherits="SincciKC.websystem.hkbm.PhotoUpLost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批量上传照片</title>
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />

    <link href="uploadify/example/css/default.css" rel="stylesheet" type="text/css" />
    <link href="uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="uploadify/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="uploadify/swfobject.js"></script>
    <script type="text/javascript" src="uploadify/jquery.uploadify.v2.1.0.min.js"></script>

    <link href="../../prompt/skin/dmm-green/ymPrompt.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" src="../../prompt/ymPrompt.js"></script> 

 
    <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadify").uploadify({
                'uploader': 'uploadify/uploadify.swf', //触发文本对话框的flash文件
                'script': 'HandleUpPhoto.ashx',                     //后台处理程序的名称更改地方
                'cancelImg': 'uploadify/cancel.png',  //取消按钮的图片
                'folder': 'UploadFile',                             //要保存到服务器的虚拟路径   也可以在后台指定  建议在后台指定
                'queueID': 'fileQueue',                             //显示文件列表的div   <div id="fileQueue"></div>  在下面
                'auto': false,                                      //不自动上传文件
                'multi': true                                       //可以框选文件  但是上传时一个一个上传
            });
        });  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <%--<ucCurrentPosition:CurrentPosition  ID="CurrentPosition" runat="server"/>--%>
    <div>
     
        <table cellpadding="0"  cellspacing="5" border="0" runat="server" id="tbl01">
            <tr>
                <td colspan="2"  >
                  <div id="fileQueue" style="width:470px; height:390px;border:2px solid #00CCFF;"></div>
                </td>
            </tr>

            <tr> 
                <td valign="top" align="right">  <input type="file" name="uploadify" id="uploadify"/></td>
                <td valign="top">
                <asp:Button ID="btnSure" runat="server"   Text="上传相片" CssClass="icon_btn"  
                        Width="86px" Height="28"
                    OnClientClick="javascript:$('#uploadify').uploadifyUpload();return false;" 
                        />
                <asp:Button ID="btnCancel" runat="server" Text="取消上传" CssClass="icon_btn"  Width="86px" Height="28" OnClientClick="javascript:$('#uploadify').uploadifyClearQueue();return false;"/>&nbsp;
                <asp:Button ID="btnLogfile" runat="server" Text="下载日志" CssClass="icon_btn"  Width="86px" Height="28" OnClick="btnLogfile_Click" />
                  
                </td>
               
            </tr>
            <tr><td style="height:10px" colspan="2"></td></tr>
            <tr><td  align="left" colspan="2"><font color="red">操作说明：第一步点击【BROWSE】浏览要上传的相片，选择要上传的相片；<br/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;第二步点击【上传相片】上传相片；<br/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;第三步点击【下载日志】查看那些考生上传成功或失败。<br>
             相片要求：相片必须是【.jpg】格式；相片大小为：宽150px，高200px。</font></td></tr>
        </table>
        <table align="center"><tr><td runat="server" id="tbl02"></td></tr></table>
        
</div>
</form>
</body>
</html>

