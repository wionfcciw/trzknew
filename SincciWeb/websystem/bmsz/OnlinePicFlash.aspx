<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlinePicFlash.aspx.cs" Inherits="SincciKC.websystem.bmsz.OnlinePicFlash" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在线照相</title>
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../css/page.css" /> 
    <link href="../../prompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" /> 

</head>
<body>
    <div style="height:600px">
    <form id="form1" runat="server">
  	    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"
			    id="OnLinePhoto" width="100%" height="100%"
			    codebase="http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab">
			    <param name="movie" value="OnLinePhoto.swf" />
			    <param name="quality" value="high" />
			    <param name="bgcolor" value="#869ca7" />
			    <param name="allowScriptAccess" value="sameDomain" />
			    <embed src="OnLinePhoto.swf" quality="high" bgcolor="#869ca7"
				    width="100%" height="100%" name="OnLinePhoto" align="middle"
				    play="true"
				    loop="false"
				    quality="high"
				    allowScriptAccess="sameDomain"
				    type="application/x-shockwave-flash"
				    pluginspage="http://www.adobe.com/go/getflashplayer">
			    </embed>
	    </object>
    </form>
        </div>
</body>
</html>