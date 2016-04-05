$.fn.extend({
    animateCss: function (animationName,callback) {
        var animationEnd = "webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend";
        $(this).addClass("animated " + animationName).one(animationEnd, function () {
            $(this).removeClass("animated " + animationName);
        });
    }
});
$(function() {
    $("p").animateCss("fadeIn");
    //hover效果
    $("header,section,footer,li").on("touchstart", function (e) {
        "use strict"; 
        var link = $(this); 
        if (link.hasClass("hover")) {
            link.removeClass("hover");
            link.css("background-size", "100%");
        } else {
            link.addClass("hover");
            link.css("background-size", "110%");
            e.preventDefault();
           
        }
    });
});