var TrainArticleDetail = {
    viewModel: {
        TrainArticleItem: {
            Id: ko.observable(),
            Content: ko.observable(),
            Thumbnail: ko.observable(),
            Title: ko.observable(),
            TrainQuestionItems: ko.observableArray([])
        },
        ChosenAnswerItems: ko.observableArray()
    }
};
getUrlParam = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
TrainArticleDetail.viewModel.LoadTrainArticle = function () {
    var id = getUrlParam("id");
    $.get("/api/PersonnelTrainArticle/" + id, function (result) {
        ko.mapping.fromJS(result, {}, TrainArticleDetail.viewModel.TrainArticleItem);
        $.get("/api/TrainQuestion/", { TrainArticleId: id }, function (trainQuestions) {
            ko.mapping.fromJS(trainQuestions, {}, TrainArticleDetail.viewModel.TrainArticleItem.TrainQuestionItems);
        });
    });
};
//提交成绩
TrainArticleDetail.viewModel.Submit = function () {
    var answers = ko.mapping.toJS(TrainArticleDetail.viewModel.ChosenAnswerItems);
    if (answers != null) {
        if (confirm("是否确定提交")) {
            $.ajax({
                type: "post",
                url: "/api/SubmitAnswers",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(answers),
                success: function (result) {
                    if (result.Error) {
                        alert(result.Message);
                    } else {
                        alert(result.Message);
                    }
                }
            });
        }
    }
};
//选择答案
TrainArticleDetail.viewModel.ChosenAnswerItem = function () {
    var model = ko.mapping.toJS(this);
    var chosenAnswerItems = ko.mapping.toJS(TrainArticleDetail.viewModel.ChosenAnswerItems);
    var array = [];
    array.push(model);
    for (var i = 0; i < chosenAnswerItems.length; i++) {
        var item = chosenAnswerItems[i];
        if (item.TrainQuestionId === model.TrainQuestionId) {
        } else {
            array.push(item);
        }
    }
    ko.mapping.fromJS(array, {},TrainArticleDetail.viewModel.ChosenAnswerItems);
}
$(function () {
    ko.applyBindings(TrainArticleDetail);
    TrainArticleDetail.viewModel.LoadTrainArticle();
});
