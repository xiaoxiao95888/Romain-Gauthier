var Main = {
    viewModel: {
        Login: {
            AuthorizationCode: ko.observable(),
        }
    }
};
Main.viewModel.Login.Submit = function (data, event) {
    //test绑定授权码
    var dom = $(event.target);
    if (Main.viewModel.Login.AuthorizationCode() != "123456") {
        dom.parent().parent().find("input").animateCss("flash");
    } else {
        dom.parents("#login").addClass('animated fadeOutRight').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $("#login").hide();
            $("#loggedmenu")
      .show()
      .outerWidth(); // Reflow
            $("#loggedmenu")
                .addClass("animated fadeInLeft");
        });
    }
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

//页面加载时调用
$(function () {
    ko.applyBindings(Main);
    $loopimages = $('.fadeImg img');
    $next = 1;			// fixed, please do not modfy;
    $current = 0;		// fixed, please do not modfy;
    $interval = 1000;	// You can set single picture show time;
    $fadeTime = 800;	// You can set fadeing-transition time;
    $imgNum = $loopimages.length;		// How many pictures do you have
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
});
