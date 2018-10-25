/*谭海生 2009-05-11*/
//获取超链接参数
function getParameter(name) {
    /// <summary>
    /// 获取超链接参数,获取成功返回字符串，否则返回null
    /// </summary>
    /// <param type="string" name="name">需要获取的key名称</param>
    var paramStr = location.search;

    if (paramStr.length == 0) return null;
    if (paramStr.charAt(0) != '?') return null;

    paramStr = unescape(paramStr);
    paramStr = paramStr.substring(1);

    if (paramStr.length == 0) return null;

    var params = paramStr.split('&');

    for (var i = 0; i < params.length; i++) {
        var parts = params[i].split('=', 2);

        if (parts[0] == name) {
            if (parts.length < 2 ||
                       typeof (parts[1]) == "undefined" ||
                       parts[1] == "undefined" ||
                       parts[1] == "null")

                return "";

            return parts[1];
        }
    }
    return null;
}

//获取超链接参数
function getParameterByUrl(name, url) {
    /// <summary>
    /// 在指定的连接字符串中获取超链接参数，获取成功返回字符串，否则返回null
    /// </summary>
    /// <param type="string" name="name">需要获取的key名称</param>
    /// <param type="string" name="url">指定的超链接</param>
    var paramStr = url.search;

    if (paramStr.length == 0) return null;
    if (paramStr.charAt(0) != '?') return null;

    paramStr = unescape(paramStr);
    paramStr = paramStr.substring(1);

    if (paramStr.length == 0) return null;

    var params = paramStr.split('&');

    for (var i = 0; i < params.length; i++) {
        var parts = params[i].split('=', 2);

        if (parts[0] == name) {
            if (parts.length < 2 ||
                       typeof (parts[1]) == "undefined" ||
                       parts[1] == "undefined" ||
                       parts[1] == "null")

                return "";

            return parts[1];
        }
    }
    return null;
}


/*********************************
判断指定的URL中是否包含指定的参数值
parmKey     参数对应的Key值
parmValue   需要对比的value值
locPropoty  document对象的location属性

返回真假
*********************************/
function checkParmInURL(parmKey, parmValue, locPropoty) {
    /// <summary>
    /// 判断指定的URL中是否包含指定的参数值
    /// </summary>
    /// <param type="string" name="parmKey">参数对应的Key值</param>
    /// <param type="string" name="parmValue">需要对比的value值</param>
    /// <param type="location" name="locPropoty">document对象的location属性</param>
    var tmpValue = getParameterByUrl(parmKey, locPropoty);

    if (parmKey != null && tmpValue == parmValue)
        return true;
    else
        return false;
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