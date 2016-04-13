var Polish = {
    viewModel: {
        items: ko.observableArray(),
        myswiper: ko.observable(),
        currentItem: {
            index: ko.observable(0),
            name: ko.observable()
        }
    }
};
$.fn.extend({
    animateCss: function (animationName) {
        var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
        $(this).addClass('animated ' + animationName).one(animationEnd, function () {
            $(this).removeClass('animated ' + animationName);
        });
    }
});
//切换屏幕后的动画
function effect(index) {
    switch (index) {
        case 0:
            $("#slide1 h2").fadeIn(function () {
                marquee($("#slide1 p"));
            });            
            break;
        case 1:
            $("#slide2 h2").fadeIn(function () {
                marquee($("#slide2 p"));
            });
            break;
        default:
    }
}
function marquee($element) {
    var $loopimages = $element;
    var $next = 1;			// fixed, please do not modfy;
    var $current = 0;		// fixed, please do not modfy;
    var $interval = 5000;	// You can set single picture show time;
    var $fadeTime = 800;	// You can set fadeing-transition time;
    var $imgNum = $loopimages.length;		// How many pictures do you have
    nextFadeIn();
    function nextFadeIn() {
        if ($next < $imgNum - 1) { $next++; } else { $next = 0; }
        if ($current < $imgNum - 1) { $current++; } else { $current = 0; }
        $element.eq($current).fadeOut($fadeTime)
        .promise().done(function () {
            $element.eq($next).fadeIn($fadeTime, nextFadeIn).delay($interval);
        });
    };
}
function pageturning(index) {
    var models = ko.mapping.toJS(Polish.viewModel.items);
    var model = models[index];
    ko.mapping.fromJS(model, {}, Polish.viewModel.currentItem);
    effect(index);
}
$(function () {
    var supportedFlag = $.keyframe.isSupported();
    var clientWidth = document.body.clientWidth;
    var clientHeight = document.body.clientHeight;
    var bevelingwidth = (clientHeight / 750) * -2500 + clientWidth;
    var gemassemblewidth = (clientHeight / 540) * -1700 + clientWidth;
    $.keyframe.define([
        {
            name: 'beveling',
            '100%': {
                "background-position": bevelingwidth + "px" + " 100%"
            }
            
        },
        {
            name: 'gemassemble',
            '100%': {
                "background-position": gemassemblewidth + "px" + " 100%"
            }
        }
    ]);
   
    $(".beveling").playKeyframe({
        name: 'beveling', // name of the keyframe you want to bind to the selected element
        duration: '30s', // [optional, default: 0, in ms] how long you want it to last in milliseconds
        timingFunction: 'linear', // [optional, default: ease] specifies the speed curve of the animation
        delay: '0s', //[optional, default: 0s]  how long you want to wait before the animation starts
        iterationCount: 'infinite', //[optional, default:1]  how many times you want the animation to repeat
        direction: 'alternate', //[optional, default: 'normal']  which direction you want the frames to flow
        fillMode: 'forwards', //[optional, default: 'forward']  how to apply the styles outside the animation time, default value is forwards
        complete: function(){} //[optional] Function fired after the animation is complete. If repeat is infinite, the function will be fired every time the animation is restarted.
    });
    $(".gemassemble").playKeyframe({
        name: 'gemassemble', // name of the keyframe you want to bind to the selected element
        duration: '30s', // [optional, default: 0, in ms] how long you want it to last in milliseconds
        timingFunction: 'linear', // [optional, default: ease] specifies the speed curve of the animation
        delay: '0s', //[optional, default: 0s]  how long you want to wait before the animation starts
        iterationCount: 'infinite', //[optional, default:1]  how many times you want the animation to repeat
        direction: 'alternate', //[optional, default: 'normal']  which direction you want the frames to flow
        fillMode: 'forwards', //[optional, default: 'forward']  how to apply the styles outside the animation time, default value is forwards
        complete: function () { } //[optional] Function fired after the animation is complete. If repeat is infinite, the function will be fired every time the animation is restarted.
    });
    var myswiper = new Swiper('.swiper-container', {
        pagination: '.swiper-pagination',
        paginationClickable: true,
        direction: 'vertical',
        onSlideChangeEnd: function (swiper) {
            pageturning(swiper.activeIndex);
        },
        onSlideChange: function () {
            effect(swiper.activeIndex);
        }
    });
    var models =
   [
       {
           name: "装配",
           index: 1
       },
       {
           name: "打磨",
           index: 0
       }
   ];
    ko.applyBindings(Polish);
    ko.mapping.fromJS(models, {}, Polish.viewModel.items);
    Polish.viewModel.myswiper(myswiper);
    pageturning(0);
});