var ImageDownload = {
    viewModel: {
        Items: ko.observableArray()        
    }
};

//获取图片列表
ImageDownload.viewModel.Load = function () {
    $.get("/api/PersonnelImage/", function (data) {
        ko.mapping.fromJS(data, {}, ImageDownload.viewModel.Items);
        //jQuery('.nailthumb-container').nailthumb({ width: 120, height: 120 });
    });

};

//下载
ImageDownload.viewModel.Download = function() {
    var model = ko.mapping.toJS(this);
    $.ajax({
        type: "post",
        url: "/api/PersonnelImage/",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(model),
        success: function (result) {
            if (result.Error) {
                alert("文件下载失败");               
            } else {
               alert("文件已发送至您邮箱，请及时查收");  
            }
        }
    });
};
$(function () {
    ko.applyBindings(ImageDownload);
    ImageDownload.viewModel.Load();   
});