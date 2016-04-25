var TrainContent = {
    viewModel: {
        Item: {
            Id: ko.observable(),
            TrainContent: ko.observable(),
        }
    }
};
function getQueryStringByName(name) {
    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
}
TrainContent.viewModel.LoadTrainContent = function () {
    var model = {
        id: getQueryStringByName("id")
    };
    $.get("/api/PersonnelTrainArticleContent/" + model.id, function (result) {
        ko.mapping.fromJS(result, {}, TrainContent.viewModel.Item);
    });
};
//开始测验
TrainContent.viewModel.Test = function () {
    var model = ko.mapping.toJS(TrainContent.viewModel.Item);
    location.href = "/home/TrainArticleDetail?id=" + model.Id;
};
//TrainContent.viewModel.ShowTrainContent = function (data, event) {
//    var dom = $(event.target);
//    $(".contentregion").fadeOut();
//    dom.parents(".crop-1").next().fadeIn();
//}
$(function () {
    ko.applyBindings(TrainContent);
    TrainContent.viewModel.LoadTrainContent();    
});
