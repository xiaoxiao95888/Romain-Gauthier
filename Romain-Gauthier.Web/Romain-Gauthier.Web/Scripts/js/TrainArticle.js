var TrainArticle = {
    viewModel: {
        TrainArticleItems: ko.observableArray(),
        TrainArticleItem: ko.observable(),
        TrainQuestionItems: ko.observableArray(),
        TrainQuestionItem: {
            Id: ko.observable(),
            TrainArticleName: ko.observable(),
            TrainArticleId: ko.observable(),
            Question: ko.observable(),
            Score: ko.observable()
        },
        TrainAnswerItems: ko.observableArray(),
        TrainAnswerItem: {
            Id: ko.observable(),
            Answer: ko.observable(),
            IsCorrect: ko.observable(),
            TrainQuestionId: ko.observable()
        }
    }
};
TrainArticle.viewModel.LoadTrainArticle = function () {
    $.get("/api/PersonnelTrainArticle/", function (result) {
        ko.mapping.fromJS(result, {}, TrainArticle.viewModel.TrainArticleItems);
    });
};
//TrainArticle.viewModel.ShowTrainArticle = function (data, event) {
//    var dom = $(event.target);
//    $(".contentregion").fadeOut();
//    dom.parents(".crop-1").next().fadeIn();
//}
$(function () {
    ko.applyBindings(TrainArticle);
    TrainArticle.viewModel.LoadTrainArticle();

});
