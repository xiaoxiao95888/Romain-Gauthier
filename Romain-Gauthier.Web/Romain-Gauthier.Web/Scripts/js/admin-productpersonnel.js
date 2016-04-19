var ProductPersonnel = {
    viewModel: {
        producttypes: ko.observableArray(),
        personnels: ko.observableArray(),
        items: ko.observableArray(),
        item: {
            ProductTypeId: ko.observable(),
            ProductTypeName: ko.observable(),
            PersonnelId: ko.observable(),
            PersonnelName: ko.observable()
        }
    }
};
//ProductPersonnel
$(function () {
    ko.applyBindings(ProductPersonnel);
    ProductPersonnel.viewModel.Load();
})