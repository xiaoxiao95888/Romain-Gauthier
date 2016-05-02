var TrainingScoring = {
    viewModel: {
        Items: ko.observableArray(),
        Details: ko.observableArray(),
    }
};
TrainingScoring.viewModel.Load = function () {
    $.get("/api/TrainingScoring", function (result) {
        ko.mapping.fromJS(result, {}, TrainingScoring.viewModel.Items);
    });
};
TrainingScoring.viewModel.ShowDetail = function () {
    var model = ko.mapping.toJS(this);
    ko.mapping.fromJS(model.Details, {}, TrainingScoring.viewModel.Details)
    $("#Detail").modal("show");
}
$(function () {
    ko.applyBindings(TrainingScoring);
    TrainingScoring.viewModel.Load();
});