var TrainArticle = {
    viewModel: {
        trainarticleitems: ko.observableArray(),
        trainarticleitem: {
            Id: ko.observable(),
            Thumbnail: ko.observable(),
            Title: ko.observable(),
            Content: ko.observable()
        }
    }
};
TrainArticle.viewModel.LoadTrainArticle = function () {
    $.get("/api/TrainArticle/", function (result) {
        ko.mapping.fromJS(result, {}, TrainArticle.viewModel.trainarticleitems);
    });
};
//编辑培训
TrainArticle.viewModel.EditTrainArticle = function () {
    var model = ko.mapping.toJS(this);
    ko.mapping.fromJS(model, {}, TrainArticle.viewModel.trainarticleitem);
    CKEDITOR.instances.content.setData(model.Content);
};
//保存培训
TrainArticle.viewModel.SaveTrainArticle = function () {
    var model = ko.mapping.toJS(TrainArticle.viewModel.trainarticleitem);
    model.Content = CKEDITOR.instances.content.getData();
    if (model.Id == null) {
        //新增的
        $.ajax({
            type: "post",
            url: "/api/TrainArticle",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    TrainArticle.viewModel.LoadTrainArticle();
                }
            }
        });
    } else {
        //更新
        $.ajax({
            type: "put",
            url: "/api/TrainArticle",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    TrainArticle.viewModel.LoadTrainArticle();
                }
            }
        });
    }
};
//取消编辑
TrainArticle.viewModel.CannelTrainArticle = function () {
    var model = {
        Id: null,
        Thumbnail: "",
        Title: "",
        Content: ""
    };
    ko.mapping.fromJS(model, {}, TrainArticle.viewModel.trainarticleitem);
    CKEDITOR.instances.content.setData("");
};
//删除培训
TrainArticle.viewModel.DeleteTrainArticle = function () {
    var model = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/TrainArticle/" + model.Id,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        TrainArticle.viewModel.LoadTrainArticle();
                    }
                }
            });
        }
    });
};
//查看培训内容
TrainArticle.viewModel.TrainArticleDetail = function () {

}
$(function () {
    ko.applyBindings(TrainArticle);
    TrainArticle.viewModel.LoadTrainArticle();
    CKEDITOR.replace('content', {
        // Load the German interface.
        language: 'zh-cn',
    });

});