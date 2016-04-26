var ProductInfo = {
    viewModel: {
        product: {
            name: ko.observable(),
            index: ko.observable(),
            items:ko.observableArray()
        },
        Items:ko.observableArray(),
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
    var item = dom.parents(".swiper-slide");
    var topview = item.find(".topview");
    var center = item.find(".center");
    center.fadeOut( function () {
        topview.fadeIn();
    }); 
    //center.hide( function () {
    //    topview.show();
    //});  
    //topview.show(function () {
    //    center.hide();
    //});
}
ProductInfo.viewModel.technicalclose = function (data, event) {
    var dom = $(event.target);
    var item = dom.parents(".swiper-slide");
    var topview = item.find(".topview");
    var center = item.find(".center");
    //center.show(function () {
    //    topview.hide();
    //});
    topview.fadeOut(function () {
        center.fadeIn();
    });
}
ProductInfo.viewModel.shownavigation = function () {
    $(".navlist").animate({ left: 0 });
};
ProductInfo.viewModel.navigationClose = function () {
    $(".navlist").animate({ left: "-200px" });
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
            //$("#slide1 .contentregion").animateCss("fadeInUp");
            //marquee($("#slide1 .center .bg img"));
            $("#slide1 .bg img").animateCss("pulse");
            $("#slide1 .contentregion").animateCss("fadeInUp");
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
            $("#slide4 .contentregion").animateCss("fadeInUp");
            break;
        case 4:
            $("#slide5 .bg img").animateCss("pulse");
            $("#slide5 .contentregion").animateCss("fadeInUp");
            break;
        case 5:
            $("#slide6 .bg img").animateCss("pulse");
            $("#slide6 .contentregion").animateCss("fadeInUp");
            break;
        case 6:
            $("#slide7 .bg img").animateCss("pulse");
            $("#slide7 .contentregion").animateCss("fadeInUp");
            break;
        case 7:
            $("#slide8 .bg img").animateCss("pulse");
            $("#slide8 .contentregion").animateCss("fadeInUp");
            break;
        case 8:
            $("#slide9 .bg img").animateCss("pulse");
            $("#slide9 .contentregion").animateCss("fadeIn");
            break;
        case 9:
            $("#slide10 .bg img").animateCss("pulse");
            $("#slide10 .contentregion").animateCss("fadeIn");
            break;
        case 10:
            $("#slide11 .bg img").animateCss("pulse");
            $("#slide11 .contentregion").animateCss("fadeIn");
            break;
        case 11:
            $("#slide12 .contentregion").animateCss("fadeInUp");
            marquee($("#slide12 .center .bg img"));
            break;
        case 12:
            $("#slide13 .bg img").animateCss("pulse");
            $("#slide13 .contentregion").animateCss("fadeIn");
            break;
        case 13:
            $("#slide14 .bg img").animateCss("pulse");
            $("#slide14 .contentregion").animateCss("fadeIn");
            break;
        case 14:
            $("#slide15 .bg img").animateCss("pulse");
            $("#slide15 .contentregion").animateCss("fadeIn");
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
    var models = ko.mapping.toJS(ProductInfo.viewModel.Items);
    for (var i = 0; i < models.length; i++) {
        var xilie = models[i];
        for (var j = 0; j < xilie.items.length; j++) {
            var p = xilie.items[j];
            if (p.index === index) {
                ko.mapping.fromJS(p, {}, ProductInfo.viewModel.currentItem);
                effect(index);
            }
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
            name: "Logical One",
            index: 3,
            items: [{ name: "Black Titanium", index: 0 }, { name: "Natural Titanium", index: 1 }, { name: "Platinum", index: 2 }, { name: "Red Gold", index: 3 }, { name: "White Gold", index: 4 }]
        },
        {
            name: "Logical One Secret",
            index: 2,
            items: [{ name: "Empire's Secret", index: 5 }]
        },
        {
            name: "Prestige HM",
            index: 1,
            items: [{ name: "Diamond-Set", index: 6 }, { name: "Platinum", index: 7 }, { name: "Red Gold Champagne Dial", index: 8 }, { name: "Red Gold Black Dial", index: 9 }, { name: "White Gold", index: 10 }]
        },
        {
            name: "Prestige HMS",
            index:0,
            items: [{ name: "HMS TEN", index: 11 }, { name: "Platinum", index: 12 }, { name: "Red Gold", index: 13 }, { name: "White Gold", index: 14 }]
        }
    ];
    ko.applyBindings(ProductInfo);
    ko.mapping.fromJS(models, {}, ProductInfo.viewModel.Items);
    //ko.mapping.fromJS(models[4], {}, ProductInfo.viewModel.product);
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