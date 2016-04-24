var File = {
    viewModel: {
        PersonnelGroups: ko.observableArray(),
        Files: ko.observableArray(),       
        File: {
            Id: ko.observable(),
            Name: ko.observable(),
            FileName: ko.observable(),
            FileType: ko.observable(),           
            PersonnelGroupId: ko.observable(),
            PersonnelGroupName: ko.observable()
        },       
        //上传文件时选择的分组
        PersonnelGroup: ko.observable()
    }
};

File.viewModel.LoadFiles = function () {
    var group = ko.mapping.toJS(File.viewModel.PersonnelGroup);
    var model = {
        PersonnelGroupId: ''
    };
    if (group != null) {
        model.PersonnelGroupId = group.Id
    };
    $.get("/api/PersonnelGroupFile",model, function (result) {
        ko.mapping.fromJS(result, {}, File.viewModel.Files);
    });
};
File.viewModel.LoadPersonnelGroups = function () {
    $.get("/api/PersonnelGroup", function (result) {
        ko.mapping.fromJS(result, {}, File.viewModel.PersonnelGroups);
    });
};
//选择分组时刷新文件
File.viewModel.ChosenGroupForFile = function () {
    File.viewModel.LoadFiles();
}
//删除文件
File.viewModel.Delete = function () {
    var model = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/PersonnelGroupFile/" + model.Id,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        File.viewModel.LoadFiles();
                        model = {
                            Id: null,
                            Name: "",
                            FileName: "",
                            FileType: "",
                            ProductId: "",
                            PersonnelGroupId: "",
                            PersonnelGroupName: "",
                        };
                        ko.mapping.fromJS(model, {}, File.viewModel.Files);
                    }
                }
            });
        }
    });
};

//上传文件
File.viewModel.Upload = function () {
    var model = ko.mapping.toJS(File.viewModel.File);
    var Group = ko.mapping.toJS(File.viewModel.PersonnelGroup);
    if (Group == null) {
        Helper.ShowErrorDialog("必须选择分组");
    } else if (model.Name == null) {
        Helper.ShowErrorDialog("必须填写文件名称");
    } else {
        File.viewModel.File.PersonnelGroupId(Group.Id);
        var file = document.getElementById("file").files[0];
        if (file != null) {
            if (file.size > 5120000) {
                Helper.ShowErrorDialog("文件大小超出限制");
            } 
            else {
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
        File.viewModel.File.FileName(fileName);      
        var model = ko.mapping.toJS(File.viewModel.File);        
        $.ajax({
            type: "post",
            url: "/api/PersonnelGroupFile",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    File.viewModel.LoadFiles();
                }
            }
        });
    } else {
        //上传失败
        Helper.ShowErrorDialog("上传失败");
    }
    var obj = document.getElementById('file');
    obj.outerHTML = obj.outerHTML;
}
function uploadFailed(evt) {
    //alert("There was an error attempting to upload the file.");
    Helper.ShowErrorDialog("There was an error attempting to upload the file.");
}
function uploadCanceled(evt) {
    alert("The upload has been canceled by the user or the browser dropped the connection.");
}
$(function () {
    ko.applyBindings(File);
    File.viewModel.LoadFiles();
    File.viewModel.LoadPersonnelGroups();
});