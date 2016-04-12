var Technology = {
    viewModel: {
        items: ko.observableArray(),
        myswiper: ko.observable(),
        currentItem: {
            index: ko.observable(0),
            name: ko.observable()
        }
    } 
};
//显示详细
Technology.viewModel.showdetail = function (data, event) {
    var dom = $(event.target);
    var parent = dom.parents('.banner')[0];
    for (var i = 0; i < $('.banner').length; i++) {
        var item = $('.banner')[i];
        if (item !== parent) {
            $(item).children(".contentregion").fadeOut();
        } else {
            $(item).children(".contentregion").fadeIn();
        }
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
$(function () {
    var models =
    [
        {
            name: "以涡形齿轮取代宝塔锥轮",
            index: 3
        },
        {
            name: "传统芝麻链",
            index: 2
        },
        {
            name: "人体工学的上链按压",
            index: 1
        },
        {
            name: "恒定的动力",
            index: 0
        }
    ];
    ko.applyBindings(Technology);
    ko.mapping.fromJS(models, {}, Technology.viewModel.items);
    //var myswiper = new Swiper('.swiper-container', {
    //    pagination: '.swiper-pagination',
    //    paginationClickable: true,
    //    direction: 'vertical',
    //    onSlideChangeEnd: function (swiper) {
    //        pageturning(swiper.activeIndex);
    //    },
    //    onSlideChange: function () {
    //        effect(swiper.activeIndex);
    //    }
    //});
    //pageturning(0);
});