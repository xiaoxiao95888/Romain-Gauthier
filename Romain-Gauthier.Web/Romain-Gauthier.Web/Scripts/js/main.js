
function orient() {
    if (window.orientation === 180 || window.orientation === 0) {
        //ipad、iphone竖屏；Andriod横屏
        var maxheight = document.documentElement.clientHeight;
        var padding = maxheight - ($("#roll").height() + $("#logo").height());
        var top = (padding - 50) / 2;
        $('#foot').css("padding-top", top);
        $('#foot').removeClass("hide");
        //alert(top)
        return false;
    }
    else if (window.orientation === 90 || window.orientation === -90) {
        //ipad、iphone横屏；Andriod竖屏
        $('#foot').css("padding-top", 0);
        $('#foot').removeClass("hide");
        return false;
    }
}
//页面加载时调用
$(function () {
    orient();
});
//用户变化屏幕方向时调用
$(window).bind('orientationchange', function (e) {
    orient();
});