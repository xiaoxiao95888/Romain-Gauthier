var BrandInfo = {
    viewModel: {

    }
};



$.fn.extend({
    animateCss: function (animationName, callback) {
        var animationEnd = "webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend";
        $(this).addClass("animated " + animationName).one(animationEnd, function () {
            $(this).removeClass("animated " + animationName);
        });
    }
});

$(function () {
    ko.applyBindings(BrandInfo);
    $("p").fadeIn();
    //hover效果
    $("header,footer,.li-item").on("touchstart", function (e) {
        "use strict";
        var link = $(this);
        if (link.hasClass("hover")) {
            link.removeClass("hover");
            link.css("background-size", "100%");
            link.find(".caption").fadeOut();
            //link.find("p").fadeIn();
        } else {
            link.addClass("hover");
            link.css("background-size", "110%");
            link.find(".caption").fadeIn();
            //link.find("p").fadeOut();
            for (var i = 0; i < $("header,footer,.li-item").length; i++) {
                var other = $($("header,footer,.li-item")[i]);
                if (link.is(other) == false && other.hasClass("hover")) {
                    other.removeClass("hover");
                    other.css("background-size", "100%");
                    other.find(".caption").fadeOut();
                    //other.find("p").fadeIn();
                }
            }
            e.preventDefault();

        }
    });
});