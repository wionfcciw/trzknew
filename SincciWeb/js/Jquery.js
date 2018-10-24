
$(function () {

    var flag = true;
    $("#btnLogin").click(function () {
        var ksh = $("#ksh").val();
        var xm = $("#xm").val();
        var pwd = $("#pwd").val();
        var code = $("#code").val();


        if (ksh.length == 0) {
            alert('请输入账号');
            $("#ksh").focus();
            return;
        }
        else if (xm.length == 0) {
            alert('请输入姓名');
            $("#xm").focus();
            return;
        }
        else if (pwd.length == 0) {
            alert("请输入密码");
            $("#pwd").focus();
            return;
        }
        else if ($("#code").val().length == 0) {
            alert('请输入验证码');
            $("#code").focus();
            return;
        }

        $.ajax({
            type: "POST",
            url: "/webUI/Ks_Login.ashx",
            data: "ksh=" + escape(ksh) + "&xm=" + escape(xm) + "&pwd=" + escape(pwd) + "&code=" + escape(code),
            beforeSend: function () {

            },
            success: function (result) {
                //alert(result);
                if (result == "success") {
                    alert("登录成功！");
                    if (pwd == ksh) {
                        window.location.href = '/webUI/Ks_PwdEdit.aspx';
                    }
                    else {
                        window.location.href = 'Default.aspx';
                    }
                }
                else if (result == "success1") {
                    alert("登陆成功！");
                    window.location.href = '/webUI/Ks_PwdEdit.aspx';
                }
                else if (result == "success3") {
                    alert("您的成绩尚未达到填报志愿的资格线，无法登录！");
                
                }
                else if (result == "fail1") {
                    alert("验证码有误!");
                }
                else {
                    if (result == "0") {
                        alert("账号有误!");
                    } else if (result == "-1") {
                        alert("姓名有误!");
                    }
                    else if (result == "-2") {
                        alert(" 密码有误!");
                    }
                    else {
                        alert(result);
                        //alert("登录失败");
                    }
                }
            },
            complete: function (data) {

            },
            error: function (XMLHttpRequest, textStatus, thrownError) { }
        });
        return flag;
    });

});
 