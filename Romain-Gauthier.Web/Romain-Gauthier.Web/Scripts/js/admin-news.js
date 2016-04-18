var News = {
    viewModel: {
        news: ko.observableArray(),
        newstypes: ko.observableArray(),
        trains: ko.observableArray(),
        personnels: ko.observableArray(),
        newsitem: {
            Id: ko.observable(),
            Title: ko.observable(),
            Description: ko.observable(),
            ThumbnailUrl: ko.observable(),
            Content: ko.observable(),
            NewsTypeId: ko.observable(),
            NewsTypeName: ko.observable(),
            IsPublish: ko.observable(),
        },
        newstypeitem: ko.observable(),
    }
};
ko.bindingHandlers.date = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        var allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        // Date formats: http://momentjs.com/docs/#/displaying/format/
        var pattern = allBindings.format || "YYYY/MM/DD";

        var output = "-";
        if (valueUnwrapped !== null && valueUnwrapped !== undefined && valueUnwrapped.length > 0) {
            output = moment(valueUnwrapped).format(pattern);
        }

        if ($(element).is("input") === true) {
            $(element).val(output);
        } else {
            $(element).text(output);
        }
    }
};
//获取新闻数据
News.viewModel.Load = function () {
    $.get("/api/newstype/", function (types) {
        ko.mapping.fromJS(types, {}, News.viewModel.newstypes);
        $.get("/api/news/", function (result) {
            ko.mapping.fromJS(result, {}, News.viewModel.news);
        });
    });

};
//新增新闻
News.viewModel.AddNews = function () {
    var model = {
        Id: null,
        Title: "",
        Description: "",
        ThumbnailUrl: "",
        Content: "",
        NewsTypeId: "",
        NewsTypeName: "",
        IsPublish: false
    };
    ko.mapping.fromJS(model, {}, News.viewModel.newsitem);
    CKEDITOR.instances.content.setData("");
};
//编辑新闻
News.viewModel.EditNews = function () {
    var model = ko.mapping.toJS(this);
    ko.mapping.fromJS(model, {}, News.viewModel.newsitem);
    var newstypeid = model.NewsTypeId;
    ko.utils.arrayForEach(News.viewModel.newstypes(), function (item) {
        if (item.Id() === newstypeid) {
            News.viewModel.newstypeitem(item);
        }
    });
    CKEDITOR.instances.content.setData(model.Content);
    $("#newsmodal").modal({
        show: true,
        backdrop: "static"
    });
};
//保存新闻
News.viewModel.NewsSave = function () {
    var model = ko.mapping.toJS(News.viewModel.newsitem);
    var typemodel = ko.mapping.toJS(News.viewModel.newstypeitem);
    if (typemodel != null) {
        model.NewsTypeId = typemodel.Id;
        model.Content = CKEDITOR.instances.content.getData();
        if (model.Id == null) { //新增保存
            $.ajax({
                type: "post",
                url: "/api/news",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function(result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        News.viewModel.Load();
                    }
                }
            });
        } else {
            //更新保存
            $.ajax({
                type: "put",
                url: "/api/news",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function(result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        News.viewModel.Load();
                    }
                }
            });
        }
    } else {
        Helper.ShowErrorDialog("未选择分类");
    }
    
};
//删除新闻
News.viewModel.NewsDelete = function() {
    var model = ko.mapping.toJS(News.viewModel.newsitem);
    Helper.ShowConfirmationDialog({
        message: "是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/news/" + model.Id,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        News.viewModel.Load();
                        model = {
                            Id: null,
                            Title: "",
                            Description: "",
                            ThumbnailUrl: "",
                            Content: "",
                            NewsTypeId: "",
                            NewsTypeName: "",
                            IsPublish: false
                        };
                        ko.mapping.fromJS(model, {}, News.viewModel.newsitem);
                    }
                }
            });
        }
    });
   
};
$(function () {
    ko.applyBindings(News);
    News.viewModel.Load();
    CKEDITOR.replace('content', {
        // Load the German interface.
        language: 'zh-cn',
    });
});