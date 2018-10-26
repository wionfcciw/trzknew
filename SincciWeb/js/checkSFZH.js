// JavaScript Document
function checkIdcard(idcard1){

var idcard=idcard1;
 
var Errors=new Array(
"验证通过!",
"身份证号码位数不对!",
"身份证号码出生日期超出范围或含有非法字符!",
"身份证号码校验错误!",
"身份证地区非法!"
);
var area={11:"北京",12:"天津",13:"河北",14:"山西",15:"内蒙古",21:"辽宁",22:"吉林",23:"黑龙江",31:"上海",32:"江苏",33:"浙江",34:"安徽",35:"福建",36:"江西",37:"山东",41:"河南",42:"湖北",43:"湖南",44:"广东",45:"广西",46:"海南",50:"重庆",51:"四川",52:"贵州",53:"云南",54:"西藏",61:"陕西",62:"甘肃",63:"青海",64:"宁夏",65:"新疆",71:"台湾",81:"香港",82:"澳门",91:"国外"} 
var idcard,Y,JYM;
var S,M;
var idcard_array = new Array();
idcard_array = idcard.split("");
/*地区检验*/
if(area[parseInt(idcard.substr(0,2))]==null) 
{
   alert(Errors[4]); 
   return false;
}
switch(idcard.length){
   case 15:
   if ( (parseInt(idcard.substr(6,2))+1900) % 4 == 0 || ((parseInt(idcard.substr(6,2))+1900) % 100 == 0 && (parseInt(idcard.substr(6,2))+1900) % 4 == 0 )){
    ereg=/^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}$/;//测试出生日期的合法性
   } else {
    ereg=/^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}$/;//测试出生日期的合法性
   }
   if(ereg.test(idcard)){
     //alert(Errors[0]+"15"); 
     return true;
    }
   else {
     alert(Errors[2]);
      return false;
     }
   break;
   case 18:
   //18位身份号码检测
   //出生日期的合法性检查 
   //闰年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))
   //平年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))
   if ( parseInt(idcard.substr(6,4)) % 4 == 0 || (parseInt(idcard.substr(6,4)) % 100 == 0 && parseInt(idcard.substr(6,4))%4 == 0 )){
   ereg=/^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$/;//闰年出生日期的合法性正则表达式
   } else {
   ereg=/^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$/;//平年出生日期的合法性正则表达式
   }
   if(ereg.test(idcard)){//测试出生日期的合法性
    //计算校验位
    S = (parseInt(idcard_array[0]) + parseInt(idcard_array[10])) * 7
    + (parseInt(idcard_array[1]) + parseInt(idcard_array[11])) * 9
    + (parseInt(idcard_array[2]) + parseInt(idcard_array[12])) * 10
    + (parseInt(idcard_array[3]) + parseInt(idcard_array[13])) * 5
    + (parseInt(idcard_array[4]) + parseInt(idcard_array[14])) * 8
    + (parseInt(idcard_array[5]) + parseInt(idcard_array[15])) * 4
    + (parseInt(idcard_array[6]) + parseInt(idcard_array[16])) * 2
    + parseInt(idcard_array[7]) * 1 
    + parseInt(idcard_array[8]) * 6
    + parseInt(idcard_array[9]) * 3 ;
	
    Y = S % 11;
    M = "F";
    JYM = "10X98765432";
    M = JYM.substr(Y,1);/*判断校验位*/
	//alert(S+"   "+idcard_array[17]+"   "+M);
    if(M.toUpperCase() == idcard_array[17].toUpperCase()){
     //alert(Errors[0]+"18"); 
     return true; /*检测ID的校验位*/
    }
    else {
     alert(Errors[3]); 
     return false;
    }
   }
   else {
    alert(Errors[2]); 
    return false;
   }
   break;
   default:
    alert(Errors[1]); 
    return false;
}
} 
 //判断日期格式
 function IsDate(DateString , Dilimeter) 
{ 
	var y='';   //年
	var m='';   //月
	var day='';   //日
	var tempArray;
	
	if (DateString==null) return false; 
	if (Dilimeter=='' || Dilimeter==null) 
		Dilimeter ='-'; 
	tempArray = DateString.split(Dilimeter);
	
	y=tempArray[0]; 
	m=tempArray[1];
	day=tempArray[2];
	
	var date=new Date();   
	var year=date.getFullYear(); 
	if (y<year-100 || y>year) return false; //判断年份在
	if(m>12 ||m<1)	return false;  //判断月份

	//判断日
	 if (m.substring(0,1)=="0")
	 {
		m=m.substring(1);
	 }
	 if(day.substring(0,1)=="0")
	 {
		day=day.substring(1);
	 }
	if(m==1 || m==3 || m==5 || m==7 || m==8 || m==10 || m==12){   
	   if(day>31 || day<1) return false;
	}   
	else if(m==4 || m==6 || m==9 || m==11){   
		 if(day>30 || day<1) return false;
	}   
	else if(m==2){   
		var flag=true;   
		flag=y%4==0&&y%100!=0||y%400==0;   
		if(flag){ 
		
				 if(day>29 || day<1) return false; 
		}   
		else{  
		
				 if(day>28 || day<1) return false;
		}   
	}  
}




function isCardID(sId){

var aCity={11:"北京",12:"天津",13:"河北",14:"山西",15:"内蒙古",21:"辽宁",22:"吉林",23:"黑龙江",31:"上海",32:"江苏",33:"浙江",34:"安徽",35:"福建",36:"江西",37:"山东",41:"河南",42:"湖北",43:"湖南",44:"广东",45:"广西",46:"海南",50:"重庆",51:"四川",52:"贵州",53:"云南",54:"西藏",61:"陕西",62:"甘肃",63:"青海",64:"宁夏",65:"新疆",71:"台湾",81:"香港",82:"澳门",91:"国外"}   
var Errors=new Array(
"你输入的身份证长度或格式错误!",
"你的身份证地区错误!",
"身份证上的出生日期错误!",
"你输入的身份证号校验位错误!"
);


var iSum=0 ;
var info="" ;

if(!/^\d{17}(\d|x)$/i.test(sId)) 
{
     alert(Errors[0]); 
     return  false ;
}

sId=sId.replace(/x$/i,"a");
if(aCity[parseInt(sId.substr(0,2))]==null) 
{
    alert(Errors[1]); 
    return  false ; 
}

sBirthday=sId.substr(6,4)+"-"+Number(sId.substr(10,2))+"-"+Number(sId.substr(12,2));
var d=new Date(sBirthday.replace(/-/g,"/")) ;
if(sBirthday!=(d.getFullYear()+"-"+ (d.getMonth()+1) + "-" + d.getDate()))  
{
    alert(Errors[2]); 
    return  false ;
}

for(var i = 17;i>=0;i --) iSum += (Math.pow(2,i) % 11) * parseInt(sId.charAt(17 - i),11) ;
if(iSum%11!=1) 
{
    alert(Errors[3]); 
    return  false ;
}

//return true;//aCity[parseInt(sId.substr(0,2))]+","+sBirthday+","+(sId.substr(16,1)%2?"男":"女") 

} 