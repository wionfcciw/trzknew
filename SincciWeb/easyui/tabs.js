/*
Author：庄燕边
Date：2012.3.22 22：31
*/
 
 
/*
* view(url) 在layout中打开页面
*/
function view(url) {
    $('#iframe').attr('src', url);
}

/*
*添加选项卡方法
*/
function addTab(title, url) {

    // 判断title长度 超过9个字的..代替。
    if (title.length > 9) {
        title = title.substring(0, 9) + "..";
    }


    //先判断是否存在标题为title的选项卡
    var tab = $('#tt').tabs('exists', title);
    if (tab) {
        //若存在，则直接打开
        $('#tt').tabs('select', title);


    } else {
        //否则创建
        $('#tt').tabs('add', {
            iconCls: "icon-tabtitle",
            title: title,
            content: createTabContent(url) , //"<iframe width='100%' height='100%'  id='iframe' frameborder='0' scrolling='auto'  src='" + url + "'></iframe>",
            closable: true

        });
    }

}


/* 生成标签内容 */
function createTabContent(url) {
    return '<iframe style="width:100%;height:100%;"   id="iframe" scrolling="auto" frameborder="0" src="' + url + '"></iframe>';
}


/*
*根据title,选中Accordion对应的面板
*/
function selectAccordion(title) {
    //   $("#accordionPanel").accordion({ animate: false });
    $('#accordionPanel').accordion('select', title);


}



//    /*
//    *检测浏览器窗口大小改变,来改变页面layout大小
//    */
//    $(function () {
//        $(window).resize(function () {
//            $('#cc').layout('resize');
//        });
//    });


$(function () {
    $("#btn").click(function (e) {
        $('#menu').menu('show', {
            left: e.pageX,
            top: e.pageY
        });
    });
});

  

/* 右下角弹出提示框   */
function show(title, msg, timeout, showType) {
    $.messager.show({
        title: title,          //提示标题
        msg: msg,              //提示内容
        timeout: timeout,      // 等于0消息窗口将不会自动关闭。5000等于5秒
        showType: showType     //null,slide,fade,show  不同弹出效果
    });
}

/* 右下角弹出提示框 结束 */

/**/

//    function TDOverOROut(iname) {
//        document.getElementById(iname).className = "table_none";
//    }
//    function TDOverORIn(iname) {
//        document.getElementById(iname).className = "table_body";
//    }

/**/




$(function () {

    //刷新
    $("#m-refresh").click(function () {
        var currTab = $('#tt').tabs('getSelected'); //获取选中的标签项
        //  alert(currTab);
        var url = $(currTab.panel('options').content).attr('src'); //获取该选项卡中内容标签（iframe）的 src 属性
        // alert(url);
        /* 重新设置该标签 */
        $('#tt').tabs('update', {
            tab: currTab,
            options: {
                content: createTabContent(url)   //createTabContent(url)
            }
        })
    });

    //关闭所有
    $("#m-closeall").click(function () {
        $(".tabs li").each(function (i, n) {
            var title = $(n).text();
            $('#tt').tabs('close', title);
        });
    });

    //除当前之外关闭所有
    $("#m-closeother").click(function () {
        var currTab = $('#tt').tabs('getSelected');
        currTitle = currTab.panel('options').title;

        $(".tabs li").each(function (i, n) {
            var title = $(n).text();

            if (currTitle != title) {
                $('#tt').tabs('close', title);
            }
        });
    });

    //关闭当前
    $("#m-close").click(function () {
        var currTab = $('#tt').tabs('getSelected');
        currTitle = currTab.panel('options').title;
        $('#tt').tabs('close', currTitle);
    });
});



$(function () {
    /*为选项卡绑定右键*/
    $(".tabs li").live('contextmenu', function (e) {

        /* 选中当前触发事件的选项卡 */
        var subtitle = $(this).text();
        $('#tt').tabs('select', subtitle);

        //显示快捷菜单
        $('#menu').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        return false;
    });
});
     