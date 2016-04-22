var News = {
    viewModel: {
        ItemTypes: ko.observableArray()
    }
};
News.viewModel.Load=function() {
    $.get("/api/NewsPublish/", function(result) {
        ko.mapping.fromJS(result, {}, News.viewModel.ItemTypes);
        //var swiper = new Swiper('.swiper-container', {
        //    pagination: '.swiper-pagination',
        //    nextButton: '.swiper-button-next',
        //    prevButton: '.swiper-button-prev',
        //    slidesPerView: 1,
        //    paginationClickable: true,
        //    spaceBetween: 30,
        //    loop: true,

        //});
    });
}

$(function () {
    ko.applyBindings(News);
    News.viewModel.Load();
})