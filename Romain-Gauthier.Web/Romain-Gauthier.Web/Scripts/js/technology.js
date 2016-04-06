var Technology = {
    viewModel: {

    }
};

$(function () {
    //$('.modal').modal({
    //    show: true,
    //    backdrop: 'static'
    //});
    ko.applyBindings(Technology);
    var swiper = new Swiper('.swiper-container', {
        pagination: '.swiper-pagination',
        paginationClickable: true
    });
})