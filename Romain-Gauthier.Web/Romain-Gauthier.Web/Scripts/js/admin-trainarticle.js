var TrainArticle = {
    viewModel: {
        trainarticleitems: ko.observableArray(),
        trainarticleitem: {
            Id: ko.observable(),
            Thumbnail: ko.observable(),
            Title: ko.observable(),
            Content: ko.observable(),
            Index: ko.observable()
        },
        TrainQuestionItems: ko.observableArray(),
        TrainQuestionItem: {
            Id: ko.observable(),
            TrainArticleName: ko.observable(),
            TrainArticleId: ko.observable(),
            Question: ko.observable(),
            Score: ko.observable()
        },
        TrainQuestionSelectTrainarticle: ko.observable(),
        TrainAnswerItems: ko.observableArray(),
        TrainAnswerItem: {
            Id: ko.observable(),
            Answer: ko.observable(),
            IsCorrect: ko.observable(),
            TrainQuestionId: ko.observable()
        },
        TrainAnswerSelectTrainQuestion: ko.observable()
    }
};
TrainArticle.viewModel.LoadTrainArticle = function (callback) {
    $.get("/api/TrainArticle/", function (result) {
        ko.mapping.fromJS(result, {}, TrainArticle.viewModel.trainarticleitems);
        callback();
    });
};
TrainArticle.viewModel.LoadTrainQuestion = function () {
    var article = ko.mapping.toJS(TrainArticle.viewModel.TrainQuestionSelectTrainarticle);
    if (article == null) {
        ko.mapping.fromJS([], {}, TrainArticle.viewModel.TrainQuestionItems);
    } else {
        var model = {
            TrainArticleId: article.Id
        }
        $.get("/api/TrainQuestion/", model, function (result) {
            ko.mapping.fromJS(result, {}, TrainArticle.viewModel.TrainQuestionItems);
        });
    }

};
TrainArticle.viewModel.LoadTrainAnswer = function () {
    var question = ko.mapping.toJS(TrainArticle.viewModel.TrainAnswerSelectTrainQuestion);
    if (question == null) {
        ko.mapping.fromJS([], {}, TrainArticle.viewModel.TrainAnswerItems);
    } else {
        var model = {
            TrainQuestionId: question.Id
        }
        $.get("/api/TrainAnswer/", model, function (result) {
            ko.mapping.fromJS(result, {}, TrainArticle.viewModel.TrainAnswerItems);
        });
    }

};
//上传图片
TrainArticle.viewModel.Upload = function () {
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
        TrainArticle.viewModel.trainarticleitem.Thumbnail(fileName);
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
                    TrainArticle.viewModel.LoadTrainArticle(function () { });
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
                    TrainArticle.viewModel.LoadTrainArticle(function () { });
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
        Content: "",
        Index:""
    };
    ko.mapping.fromJS(model, {}, TrainArticle.viewModel.trainarticleitem);
    CKEDITOR.instances.content.setData("");
};
//删除培训
TrainArticle.viewModel.DeleteTrainArticle = function () {
    var model = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "删除培训主题将会一并删除主题下的题目及答案，是否确认删除?",
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
                        TrainArticle.viewModel.LoadTrainArticle(function () { });
                    }
                }
            });
        }
    });
};
//查看培训内容
TrainArticle.viewModel.TrainArticleDetail = function () {
    var model = ko.mapping.toJS(TrainArticle.viewModel.TrainQuestionItem);
    if (model.Id == null) {
        //新增的
        $.ajax({
            type: "post",
            url: "/api/TrainQuestion",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    TrainArticle.viewModel.LoadTrainArticle(function () { });
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
                    TrainArticle.viewModel.LoadTrainArticle(function () { });
                }
            }
        });
    }
}
//保存培训题目
TrainArticle.viewModel.SaveQuestion = function () {
    var model = ko.mapping.toJS(TrainArticle.viewModel.TrainQuestionItem);
    var article = ko.mapping.toJS(TrainArticle.viewModel.TrainQuestionSelectTrainarticle);
    model.TrainArticleId = article.Id;
    if (model.Id == null) {
        //新增的
        $.ajax({
            type: "post",
            url: "/api/TrainQuestion",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    TrainArticle.viewModel.LoadTrainQuestion();
                }
            }
        });
    } else {
        //更新
        $.ajax({
            type: "put",
            url: "/api/TrainQuestion",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    TrainArticle.viewModel.LoadTrainQuestion();
                }
            }
        });
    }

}
//选择培训主题
TrainArticle.viewModel.ChosenTrainarticle = function () {
    TrainArticle.viewModel.LoadTrainQuestion();
};
//编辑培训题目
TrainArticle.viewModel.EditQuestion = function () {
    var model = ko.mapping.toJS(this);
    ko.mapping.fromJS(model, {}, TrainArticle.viewModel.TrainQuestionItem);
};
//取消编辑培训题目
TrainArticle.viewModel.CannelQuestion = function () {
    var model = {
        Id: null,
        TrainArticleName: "",
        TrainArticleId: null,
        Question: "",
        Score: ""
    }
    ko.mapping.fromJS(model, {}, TrainArticle.viewModel.TrainQuestionItem);
};
//删除培训题目
TrainArticle.viewModel.DeleteQuestion = function () {
    var model = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "删除题目将会一并删除题目下的答案，是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/TrainQuestion/" + model.Id,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        TrainArticle.viewModel.LoadTrainQuestion();
                    }
                }
            });
        }
    });
};
//选择培训题目
TrainArticle.viewModel.ChosenTrainQuestion = function () {
    TrainArticle.viewModel.LoadTrainAnswer();
};
//编辑培训答案
TrainArticle.viewModel.EditAnswer = function () {
    var model = ko.mapping.toJS(this);
    ko.mapping.fromJS(model, {}, TrainArticle.viewModel.TrainAnswerItem);
};
//取消编辑培训答案
TrainArticle.viewModel.CannelAnswer = function () {
    var model = {
        Id: null,
        Answer: "",
        IsCorrect: false,
        TrainQuestionId: null
    }
    ko.mapping.fromJS(model, {}, TrainArticle.viewModel.TrainAnswerItem);
};
//保存培训答案
TrainArticle.viewModel.SaveAnswer = function () {
    var model = ko.mapping.toJS(TrainArticle.viewModel.TrainAnswerItem);
    var question = ko.mapping.toJS(TrainArticle.viewModel.TrainAnswerSelectTrainQuestion);
    model.TrainQuestionId = question.Id;
    if (model.Id == null) {
        //新增的
        $.ajax({
            type: "post",
            url: "/api/TrainAnswer",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    TrainArticle.viewModel.LoadTrainAnswer();
                }
            }
        });
    } else {
        //更新
        $.ajax({
            type: "put",
            url: "/api/TrainAnswer",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    TrainArticle.viewModel.LoadTrainAnswer();
                }
            }
        });
    }

}
//删除培训题目
TrainArticle.viewModel.DeleteAnswer = function () {
    var model = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/TrainAnswer/" + model.Id,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        TrainArticle.viewModel.LoadTrainAnswer();
                    }
                }
            });
        }
    });
};

$(function () {
    ko.applyBindings(TrainArticle);
    TrainArticle.viewModel.LoadTrainArticle(function () { });
    CKEDITOR.replace('content', {
        // Load the German interface.
        language: 'zh-cn',
    });

});