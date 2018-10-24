
 
 
    //分别是奇数行默认颜色,偶数行颜色,鼠标放上时奇偶行颜色
var aBgColor = ["#FFFFFF", "#F5F5F5", "#FFFFCC", "#FFFFCC"];
    //从前面iHead行开始变色，直到倒数iEnd行结束
function addTableListener(o, iHead, iEnd) {
    try {
        o.style.cursor = "normal";
        iHead = iHead > o.rows.length ? 0 : iHead;
        iEnd = iEnd > o.rows.length ? 0 : iEnd;
        for (var i = iHead; i < o.rows.length - iEnd; i++) {
            if (i > 0) {
                o.rows[i].onmouseover = function () { setTrBgColor(this, true) }
                o.rows[i].onmouseout = function () { setTrBgColor(this, false) }
            }
        }
    } catch (e) { }
}
    function setTrBgColor(oTr,b)
    {
        oTr.rowIndex % 2 != 0 ? oTr.style.backgroundColor = b ? aBgColor[3] : aBgColor[0] : oTr.style.backgroundColor = b ? aBgColor[2] : aBgColor[1];
    }
    window.onload = function () { 
       
            addTableListener(document.getElementById("GridView1"), 0, 0); 
        
     }
 
 

 
 
function MM_showHideLayers() { //v3.0
  var i,p,v,obj,args=MM_showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v='hide')?'hidden':v; }
    obj.visibility=v; }
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}


function CheckAll(form) {
  
    for (var i = 0; i < form1.elements.length; i++) {
        
        var e = form1.elements[i];
        var obj = form1.elements[i].name;
        if (e.name != 'chkall') {
            if (obj == 'BookID') {
                if (e.disabled == false)
                    e.checked = form1.chkall.checked;
            }
        }
    }
} 