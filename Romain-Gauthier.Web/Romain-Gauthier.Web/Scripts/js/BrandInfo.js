﻿var BrandInfo = {
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
    //hover效果
    $(".item,.header,.sectionbackimg").on("touchstart", function (e) {
        "use strict";
        var link = $(this);
        if (link.hasClass("hover")) {
            if (e.target.nodeName === "A") {
                e.target.click();
                e.preventDefault();
            } else {
                link.find("p").fadeIn();
                link.removeClass("hover");
                link.css("background-size", "100%");
                link.find('.item-container').css('z-index', -1);
            }
        } else {
            e.preventDefault();
            link.addClass("hover");
            link.find("p").fadeOut();
            link.css("background-size", "110%");
            link.find('.item-container').css('z-index', 1);
            link.find('a').animateCss("fadeIn");
            for (var i = 0; i < $(".item,.header,.sectionbackimg").length; i++) {
                var other = $($(".item,.header,.sectionbackimg")[i]);
                if (link.is(other) === false && other.hasClass("hover")) {
                    other.removeClass("hover");
                    other.find("p").fadeIn();
                    other.css("background-size", "100%");
                    other.find('.item-container').css('z-index', -1);
                }
            }
        }
        //e.preventDefault();
    });
});