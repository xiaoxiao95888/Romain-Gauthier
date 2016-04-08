var ProductInfo = {
    viewModel: {
        product: {
            name: ko.observable(),
            index: ko.observable(),
            items:ko.observableArray()
        },
        myswiper: ko.observable(),
        currentItem: {
            index: ko.observable(0),
            name: ko.observable()
        }
    }
};
//技术参数
ProductInfo.viewModel.technical = function (data, event) {
    var dom = $(event.target);
    //console.log("a");
    //dom.parents().find(".modal").modal({
    //    show: true
    //});
}
ProductInfo.viewModel.shownavigation = function () {
    $(".navlist").animate({ left: 0 });
};
ProductInfo.viewModel.navigationClose = function () {
    $(".navlist").animate({ left: "-180px" });
};
ProductInfo.viewModel.navigation = function () {
    ProductInfo.viewModel.navigationClose();
    var model = ko.mapping.toJS(this);
    var index = model.index;
    ProductInfo.viewModel.myswiper().slideTo(index, 1000, true);
    ko.mapping.fromJS(model, {}, ProductInfo.viewModel.currentItem);
};
//切换屏幕后的动画
function effect(index) {
    switch (index) {
        case 0:
            $("#slide1 .contentregion").animateCss("fadeInUp");
            marquee($("#slide1 .bg img"));
            break;
        case 1:
            $("#slide2 .bg img").animateCss("pulse");
            $("#slide2 .contentregion").animateCss("fadeInUp");
            break;
        case 2:
            $("#slide3 .bg img").animateCss("fadeInUp");
            $("#slide3 .contentregion").animateCss("fadeInRight");
            break;
        case 3:
            $("#slide4 .bg img").animateCss("pulse");
            $("#slide4 .contentregion").animateCss("fadeIn");
            break;
        default:

    }
}
function marquee($element) {
    var $loopimages = $element;
    var $next = 1;			// fixed, please do not modfy;
    var $current = 0;		// fixed, please do not modfy;
    var $interval = 1000;	// You can set single picture show time;
    var $fadeTime = 800;	// You can set fadeing-transition time;
    var $imgNum = $loopimages.length;		// How many pictures do you have
    nextFadeIn();
    function nextFadeIn() {
        $element.eq($current).fadeOut($fadeTime)
        .promise().done(function () {
            $element.eq($next).fadeIn($fadeTime, nextFadeIn).delay($interval);
        });
        if ($next < $imgNum - 1) { $next++; } else { $next = 0; }
        if ($current < $imgNum - 1) { $current++; } else { $current = 0; }
    };
}
function pageturning(index) {
    //console.log(swiper);
    //console.log(swiper.activeIndex);
    var models = ko.mapping.toJS(ProductInfo.viewModel.product.items);
    var model = models[index];
    ko.mapping.fromJS(model, {}, ProductInfo.viewModel.currentItem);
    effect(index);
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
            name: "Logical one",
            index: 4,
            items: [{ name: "Black titanium" }, { name: "Natural titanium" }, { name: "Platinum" }, { name: "Red Gold" }, { name: "White Gold" }]
        },
        {
            name: "Logical one secret",
            index: 3,
            items: []
        },
        {
            name: "Empire's secret",
            index: 2,
            items: []
        },
        {
            name: "Presitge HM",
            index: 1,
            items: [{ name: "Diamond-set" }, { name: "Platinum" }, { name: "Red Gold" }, { name: "White Gold" }]
        },
        {
            name: "Presitge HMS",
            index:0,
            items: [{ name: "HMS TEN", index: 0 }, { name: "Platinum", index: 1 }, { name: "Red Gold", index: 2 }, { name: "White Gold", index: 3 }]
        }
    ];
    ko.applyBindings(ProductInfo);
    ko.mapping.fromJS(models[4], {}, ProductInfo.viewModel.product);
    var myswiper = new Swiper('.swiper-container', {
        pagination: '.swiper-pagination',
        paginationClickable: true,
        direction: 'vertical',
        onSlideChangeEnd: function (swiper) {
            pageturning(swiper.activeIndex);
        },
        onSlideChange: function() {
            effect(swiper.activeIndex);
        }
    });
    ProductInfo.viewModel.myswiper(myswiper);
    pageturning(0);
});