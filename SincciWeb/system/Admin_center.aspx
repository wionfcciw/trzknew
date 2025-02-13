<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeBehind="Admin_center.aspx.cs" Inherits="SincciWeb.system.Admin_center" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html >
<head id="Head1" runat="server">
<title>铜仁市高中阶段学校招生考试管理系统</title>

<link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../css/main.css" />

<script type="text/javascript" charset="utf-8" src="../easyui/jquery-1.7.1.min.js" ></script>
<script type="text/javascript" charset="utf-8" src="../easyui/jquery.easyui.min.js" ></script>
<script src="../js/noContextMenu.js" type="text/javascript"></script>
 


<script type="text/javascript">

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
            title = title.substring(0,9) + "..";
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
                content: createTabContent(url), //"<iframe width='100%' height='100%'  id='iframe' frameborder='0' scrolling='auto'  src='" + url + "'></iframe>",
                closable: true

            });
        }

    }
    //如果存在先关闭，再打开
    function addTab2(title, url) {

        // 判断title长度 超过9个字的..代替。
        if (title.length > 9) {
            title = title.substring(0, 9) + "..";
        }


        //先判断是否存在标题为title的选项卡
        var tab = $('#tt').tabs('exists', title);
        if (tab) {
            //若存在，则先关闭
            $('#tt').tabs('close', title); 
        }  

        //否则创建
        $('#tt').tabs('add', {
            iconCls: "icon-tabtitle",
            title: title,
            content: createTabContent(url), //"<iframe width='100%' height='100%'  id='iframe' frameborder='0' scrolling='auto'  src='" + url + "'></iframe>",
            closable: true

        });
       


    }



    //关闭选项卡方法
    function closeTab(title) {
        $('#tt').tabs('close', title);
    }

    //刷新选项卡方法
    function refreshTab(href) {
      //  $('#tt').tabs('close', title);

    //  $('#tt').tabs('getSelected').panel("refresh", href)


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


//        var currTab = $('#tt').tabs('exists', title); // $('#tt').tabs('getSelected'); //获取选中的标签项
//          alert(currTab);
//       var url = $(currTab.panel('options').content).attr('src'); //获取该选项卡中内容标签（iframe）的 src 属性
//          alert(url);
//        /* 重新设置该标签 */
//        $('#tt').tabs('update', {
//            tab: currTab,
//            options: {
//                content: createTabContent(url)   //createTabContent(url)
//            }
//        })

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


    /*
    *刷新时间
    */
    function showTime() {
        var date = new Date();
        $('#timeInfo').html();
        $('#timeInfo').html('欢迎你，<%=UserName%>&nbsp;&nbsp;&nbsp;&nbsp;' + date.toLocaleString() + "&nbsp;&nbsp;");
    }
    setInterval(showTime, 1000);

    //    /*
    //    *检测浏览器窗口大小改变,来改变页面layout大小
    //    */
    //    $(function () {
    //        $(window).resize(function () {
    //            $('#cc').layout('resize');
    //        });
    //    });


 


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

    
 
 

 /*  tabs右键菜单  */

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
                if (title != "系统首页") {
                    $('#tt').tabs('close', title);
                }
            });
        });

        //除当前之外关闭所有
        $("#m-closeother").click(function () {
            var currTab = $('#tt').tabs('getSelected');
            currTitle = currTab.panel('options').title;

            $(".tabs li").each(function (i, n) {
                var title = $(n).text();

                if (currTitle != title) {
                    if (title != "系统首页") {
                        $('#tt').tabs('close', title);
                    }
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


    /*为选项卡绑定右键*/
    $(function () {
        /*为选项卡绑定右键*/
        $(".tabs li").live('contextmenu', function (e) {

            /* 选中当前触发事件的选项卡 */
            var subtitle = $(this).text();
            if (subtitle != "系统首页") {
                $('#tt').tabs('select', subtitle);

                //显示快捷菜单
                $('#menu').menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            }
            return false;
        });
    });

    /*  tabs右键菜单结束  */


    /*鼠标停留时间*/
//    var handle = null;
//    $("#easyui-layout").mouseover(function () {
//        alert("tdst");
//        handle = setTimeout(popdiv2, 500);
//    }).mouseout(function () {
//        clearTimeout(handle);
//    });

</script>


</head>

<body class="easyui-layout"  onload="showTime();" >
      
      

		<!-- 页面顶部top及菜单栏 -->
    <div region="north" style="height: 54px; width: 100%;">
        <div class="header2">
            <table height="50px" width="100%" cellpadding="0" cellspacing="0">
                <tr valign="middle">
                    <td style="filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr='#0A6CCE', endColorStr='#FFFFFF', gradientType='1')">
                        <div class="toptitle">
                            高中阶段学校招生考试管理系统</div>
                        <div style="text-align: right; padding-right: 20px; padding-top: 22px;">
                            <span style="color: #000000" id="timeInfo"></span><a href="/ht/Exit.aspx" style="color: #FF0000;
                                text-decoration: none;">
                                <img src="../images/exit.gif" border="0" alt="退出" />退出</a>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- 页面顶部top及菜单栏  结束 -->  

<%--	    <!-- 页面底部信息 -->
	    <div region="south" style="height: 25px;padding-top:6px; background-color:#F5F5F5;" > 
	    	<%--<iframe style="width:100%;height:20px;"  scrolling="no" frameborder="0" src="Foot.aspx"></iframe>--%>
        <%--     <center>	    		  
                <span>  版权所有　&copy;广州市信驰智能科技有限责任公司 Copyright 2012 All Rights Reserved  </span>
             </center>
	    </div>  --%> 
         <!-- 页面底部信息 结束 -->

		<!-- 左侧导航菜单 -->	    
	    <div region="west" title="菜单栏 "   split="true"  icon="icon-menu" style="width:180px;">
			<div class="easyui-accordion"   fit="true" border="false" style="text-align:left;overflow-y:scroll" >           
                <%=sb_Menu.ToString()%>
			</div>	   
	    </div>  
	    <!-- 左侧导航菜单 结束 -->	


	    <!-- 主显示区域选项卡界面 title="主显示区域"-->
	    <div region="center">
	    	<div class="easyui-tabs" fit="true" id="tt"> 
	    		<div title="系统首页" icon="icon-tabtitle"  style="OVERFLOW-y:hidden" >
	    			<iframe width='100%' height='100%'  id='iframe' frameborder='0' scrolling='auto'  src='main.aspx'></iframe>
	    		</div>
	    	</div>
	    </div>  
        <!---- 主显示区结束 --->
	
  <!---右键菜单 --->   
 <div id="menu" class="easyui-menu" style="width:150px;">
    <div id="m-refresh">刷新</div>
    <div class="menu-sep"></div>
    <div id="m-closeall">全部关闭</div>
    <div class="menu-sep"></div>
    <div id="m-closeother">除此之外全部关闭</div>
    <div class="menu-sep"></div>
    <div id="m-close">关闭</div>
</div>
  <!---右键菜单 结束 --->    
   
 
</body>
</html>
