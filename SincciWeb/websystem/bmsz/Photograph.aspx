<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Photograph.aspx.cs" Inherits="SincciKC.websystem.bmsz.Photograph" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>照相</title>
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" />
    
    <script src="../../prompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    
</head>
<body  >
    <form id="form1" runat="server">
    <div>
     <table align="left">
            <tr>
                <td>
                    <table width="100%" border="1"  style=" border-collapse:collapse; bordercolor:#68B4FF" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                准考证号：
                            </td>
                            <td width="200px">
                                <asp:Label ID="lblksh" runat="server" Text=""></asp:Label> </td>
                            <td rowspan="3" align="center"  width="120px">
                                <img src='<%=imgPath%>?time=<%=DateTime.Now.ToString() %>' height="133px" width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                姓名：
                            </td>
                            <td><asp:Label ID="lblxm" runat="server" Text=""></asp:Label> </td>
                        </tr>
                        <tr>
                            <td>
                                身份证号：
                            </td>
                            <td><asp:Label ID="lblsfzh" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                <table style="border:3px solid #DFF3FE; background-color:#DFF3FE;"><tr><td>
                 <script type="text/javascript">
                     var swf_width = 550
                     var swf_height = 300
                     var httpurl = '<%=url%>/tools/uploads.aspx'
                     var filename = '<%=picPath%>'
                     var markwidth = '149'
                     var markheight = '199'

                     document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="' + swf_width + '" height="' + swf_height + '">');
                     document.write('<param name="movie" value="getphoto.swf"><param name="quality" value="high">');
                     document.write('<param name="menu" value="false"><param name=wmode value="opaque">');
                     document.write('<param name="FlashVars" value="httpurl=' + httpurl + '&filename=' + filename + '&markwidth=' + markwidth + '&markheight=' + markheight + '   ">');
                     document.write('<embed src="getphoto.swf" wmode="opaque" FlashVars="httpurl=' + httpurl + '&filename=' + filename + '& menu="false" quality="high" width="' + swf_width + '" height="' + swf_height + '" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'); document.write('</object>'); 
                     </script></td></tr></table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
