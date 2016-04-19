var Personnel = {
    viewModel: {
        items: ko.observableArray(),
        item: {
            Id: ko.observable(),
            License: ko.observable(),
            PhoneNum: ko.observable(),
            Email: ko.observable(),
            Name: ko.observable(),
            NickName: ko.observable(),
            Gender: ko.observable(),
            Language: ko.observable(),
            City: ko.observable(),
            Province: ko.observable(),
            Country: ko.observable(),
            Headimgurl: ko.observable(),
            UpdateTime: ko.observable()
        }
       
    }
};
Personnel.viewModel.Load = function () {
    $.get("/api/personnel", function (result) {
        ko.mapping.fromJS(result, {}, Personnel.viewModel.items);
    });
};
$(function() {
    ko.applyBindings(Personnel);
})