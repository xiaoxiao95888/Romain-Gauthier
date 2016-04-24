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
            Thumbnail: ko.observable(),
            Content: ko.observable(),
            NewsTypeId: ko.observable(),
            NewsTypeName: ko.observable(),
            ExternalUrl: ko.observable(),
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
        ExternalUrl: "",
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
//上传图片
News.viewModel.Upload = function () {
    var model = ko.mapping.toJS(News.viewModel.newsitem);   
     if (model.Title == null) {
        Helper.ShowErrorDialog("必须填写新闻名称");
    } else {        
        var file = document.getElementById("file").files[0];
        if (file != null) {
            if (file.size > 5120000) {
                Helper.ShowErrorDialog("文件大小超出限制");
            } else if (file.type.indexOf("image") === -1) {
                Helper.ShowErrorDialog("文件类型超出限制");
            } else {
                var fd = new FormData();
                fd.append("file", file);
                var xhr = new XMLHttpRequest();
                xhr.upload.addEventListener("progress", uploadProgress, false);
                xhr.addEventListener("load", uploadComplete, false);
                xhr.addEventListener("error", uploadFailed, false);
                xhr.addEventListener("abort", uploadCanceled, false);
                xhr.open("POST", "/Api/Upload");
                xhr.send(fd);
            }

        }
    }
}
function uploadProgress(evt) {
    if (evt.lengthComputable) {
        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
        //document.getElementById("progressNumber").innerHTML = percentComplete.toString() + "%";
    }
    else {
        //document.getElementById("progressNumber").innerHTML = "unable to compute";
    }
}
function uploadComplete(evt) {
    /* This event is raised when the server send back a response */
    var response = JSON.parse(evt.target.response);
    if (!response.Error) {
        //上传成功
        //刷新素材列表
        var fileName = response.FileName;        
        News.viewModel.newsitem.Thumbnail(fileName);
        Helper.ShowSuccessDialog("上传成功");
    } else {
        //上传失败
        Helper.ShowErrorDialog("上传失败");
    }
}
function uploadFailed(evt) {
    //alert("There was an error attempting to upload the file.");
    Helper.ShowErrorDialog("There was an error attempting to upload the file.");
}
function uploadCanceled(evt) {
    alert("The upload has been canceled by the user or the browser dropped the connection.");
}
$(function () {
    ko.applyBindings(News);
    News.viewModel.Load();
    CKEDITOR.replace('content', {
        // Load the German interface.
        language: 'zh-cn',
    });
});