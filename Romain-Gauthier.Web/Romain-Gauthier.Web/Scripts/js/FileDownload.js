var FileDownload = {
    viewModel: {
        Items: ko.observableArray(),
        Logined: ko.observable(),
    }
};

//获取图片列表
FileDownload.viewModel.Load = function () {
    $.get("/api/PersonnelFile/", function (data) {
        ko.mapping.fromJS(data, {}, FileDownload.viewModel.Items);
    });

};
//下载
FileDownload.viewModel.Download = function () {
    var model = ko.mapping.toJS(this);
    FileDownload.viewModel.ShowProcess();
    $.ajax({
        type: "post",
        url: "/api/PersonnelFile/" + model.Id,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(model),
        success: function (result) {
            if (result.Error) {
                alert("文件下载失败");
            } else {
                FileDownload.viewModel.HideProcess();
            }
        }
    });
};
FileDownload.viewModel.ShowProcess = function () {
    $("#ProcessDialog").find(".modal-body").empty();
    $("#ProcessDialog").find(".modal-body").html("<i class=\"fa fa-spinner fa-spin\"></i> 正在发送邮件至您邮箱");
    $("#ProcessDialog").modal({
        backdrop: "static",
        show: true
    });
}
FileDownload.viewModel.HideProcess = function () {
    $("#ProcessDialog").find(".modal-body").empty();
    $("#ProcessDialog").find(".modal-body").html("文件已发送至您邮箱，请及时查收");
    setTimeout(function () {
        $("#ProcessDialog").modal("hide");
    }, 2000);
}
function getQueryStringByName(name) {
    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
}
function login() {
    
    $.get("/api/WeChatUser/", function (getuser) {
        if (getuser.Error) {
            var model = {
                code: getQueryStringByName("code"),
                state: getQueryStringByName("state")
            };
            $.get("/api/HeaderSetting/", function (base64) {
                $.ajax({
                    type: "get",
                    data: model,
                    url: "http://WeChatService.mangoeasy.com:3000/api/WeChartUserInfo/",
                    beforeSend: function (xhr) { //beforeSend定义全局变量
                        xhr.setRequestHeader("Authorization", base64); //Authorization 需要授权,即身体验证
                    },
                    success: function (xmlDoc, textStatus, xhr) {
                        if (xhr.status == 200) {
                            var user = {
                                OpenId: xhr.responseJSON.openid,
                                NickName: xhr.responseJSON.nickname,
                                Gender: xhr.responseJSON.sex,
                                City: xhr.responseJSON.city,
                                Province: xhr.responseJSON.province,
                                Country: xhr.responseJSON.country,
                                Headimgurl: xhr.responseJSON.headimgurl
                            };
                            if (user.OpenId != null) {
                                $.ajax({
                                    type: "post",
                                    url: "/api/Login",
                                    contentType: "application/json",
                                    dataType: "json",
                                    data: JSON.stringify(user),
                                    success: function (result) {
                                        if (result.Error) {
                                            //登录失败
                                            FileDownload.viewModel.Logined(false);
                                            alert("您尚未获得授权！");

                                        } else {
                                            //登录成功
                                            FileDownload.viewModel.Logined(true);
                                        }
                                    }
                                });
                            }
                        }
                    }
                });
            });


        } else {
            FileDownload.viewModel.Logined(true);
        }

    });
}
$(function () {
    ko.applyBindings(FileDownload);
    FileDownload.viewModel.Load();
    login();
});