var ProductInfo = {
    viewModel: {
       
    }
};
$(function () {
    $('.modal').modal({
        show: true,
        backdrop: 'static'
    });
    ko.applyBindings(ProductInfo);
})