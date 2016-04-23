var Main = {
    viewModel: {
        Login: {
            AuthorizationCode: ko.observable(),
        },
        WeChartUser: ko.observable()
    }
};
function getQueryStringByName(name) {
    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
}
Main.viewModel.Login.Submit = function (data, event) {
    var user = Main.viewModel.WeChartUser();
    user.License = Main.viewModel.Login.AuthorizationCode();
    //绑定license
    $.ajax({
        type: "post",
        url: "/api/BindLicense",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(user),
        success: function (result) {
            if (result.Error) {

                $("#login").find("input").animateCss("flash");
            } else {
                $("#login").addClass('animated fadeOutRight').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                    $("#login").hide();
                    $("#loggedmenu")
              .show()
              .outerWidth(); // Reflow
                    $("#loggedmenu")
                        .addClass("animated fadeInLeft");
                });
            }
        }
    });

}

$.fn.extend({
    animateCss: function (animationName) {
        var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
        $(this).addClass('animated ' + animationName).one(animationEnd, function () {
            $(this).removeClass('animated ' + animationName);
        });
    }
});
////根据屏幕调整foot位置
//function orient() {
//    var maxheight = document.documentElement.clientHeight;
//    var padding = maxheight - ($(".fadeImg").height() + $("#logo").height());
//    var top = (padding - 50) / 2;
//    if (window.orientation === 180 || window.orientation === 0) {
//        //ipad、iphone竖屏；Andriod横屏       
//        $('#foot').css("padding-top", top);

//    }
//    else if (window.orientation === 90 || window.orientation === -90) {
//        //ipad、iphone横屏；Andriod竖屏
//        $('#foot').css("padding-top", top);      
//    }
//}
//用户变化屏幕方向时调用
//$(window).bind('orientationchange', function (e) {
//    orient();
//});

//用户第一次打开页面Login
function login() {
    //WeChatUser
    $.get("/api/WeChatUser/", function (getuser) {
        if (getuser.Error) {
            var model = {
                code: getQueryStringByName("code"),
                state: getQueryStringByName("state")
            };
            $.get('/api/HeaderSetting/', function (base64) {
                $.ajax({
                    type: "get",
                    data: model,
                    url: "http://WeChatService.mangoeasy.com:3000/api/WeChartUserInfo/",
                    beforeSend: function (xhr) { //beforeSend定义全局变量
                        xhr.setRequestHeader("Authorization", base64); //Authorization 需要授权,即身体验证
                    },
                    success: function (xmlDoc, textStatus, xhr) {
                        if (xhr.status == 200) {
                            var user = {
                                OpenId: xhr.responseJSON.openid,
                                NickName: xhr.responseJSON.nickname,
                                Gender: xhr.responseJSON.sex,
                                City: xhr.responseJSON.city,
                                Province: xhr.responseJSON.province,
                                Country: xhr.responseJSON.country,
                                Headimgurl: xhr.responseJSON.headimgurl,
                            };
                            Main.viewModel.WeChartUser(user);
                            if (user.OpenId != null) {
                                $.ajax({
                                    type: "post",
                                    url: "/api/Login",
                                    contentType: "application/json",
                                    dataType: "json",
                                    data: JSON.stringify(user),
                                    success: function (result) {
                                        if (result.Error) {

                                        } else {
                                            $("#login").addClass('animated fadeOutRight').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                                                $("#login").hide();
                                                $("#loggedmenu")
                                          .show()
                                          .outerWidth(); // Reflow
                                                $("#loggedmenu")
                                                    .addClass("animated fadeInLeft");
                                            });
                                        }
                                    }
                                });
                            }
                            
                        }
                    }
                });
            });

        }
        else {
            $("#login").addClass('animated fadeOutRight').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $("#login").hide();
                $("#loggedmenu")
          .show()
          .outerWidth(); // Reflow
                $("#loggedmenu")
                    .addClass("animated fadeInLeft");
            });
        }
    });
    
}

//页面加载时调用
$(function () {
    ko.applyBindings(Main);
    $loopimages = $('.fadeImg img');
    $next = 1;			// fixed, please do not modfy;
    $current = 0;		// fixed, please do not modfy;
    $interval = 1000;	// You can set single picture show time;
    $fadeTime = 800;	// You can set fadeing-transition time;
    $imgNum = $loopimages.length;		// How many pictures do you have    
    if ($imgNum > 1) {
        nextFadeIn();
        function nextFadeIn() {
            if ($("#foot").is(":hidden") && $('.fadeImg img').eq($current).is(":visible")) {
                $("#foot").fadeIn($fadeTime);
                //orient();
            }
            $('.fadeImg img').eq($current).fadeOut($fadeTime)
            .promise().done(function () {

                $('.fadeImg img').eq($next).fadeIn($fadeTime, nextFadeIn).delay($interval);
            });
            // if You have 5 images, then (eq) range is 0~4 
            // so we should reset to 0 when value > 4; 
            if ($next < $imgNum - 1) { $next++; } else { $next = 0; }
            if ($current < $imgNum - 1) { $current++; } else { $current = 0; }
        };
    } else {
        $loopimages.fadeIn();
        $("#foot").fadeIn($fadeTime);
    }
    
    login();
});
