﻿
var Helper = {
    ShowErrorDialog: function (message) {
        var dialog = $("#Dialog");
        dialog.find(".modal-title").text("错误");
        dialog.find(".modal-body").empty();
        dialog.find(".modal-body").append(message);
        dialog.modal({
            keyboard: false,
            show: true
        });
    },
    GetQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    },
    ShowSuccessDialog: function (message) {
        var dialog = $("#Dialog");
        dialog.find(".modal-title").text("成功");
        dialog.find(".modal-body").empty();
        dialog.find(".modal-body").append(message);
        dialog.modal({
            keyboard: false,
            show: true
        });
    },
    ShowMessageDialog: function (message,title) {
        var dialog = $("#Dialog");
        dialog.find(".modal-title").text(title);
        dialog.find(".modal-body").empty();
        dialog.find(".modal-body").append(message);
        dialog.modal({
            keyboard: false,
            show: true
        });
    },
    ShowConfirmationDialog: function (parm) {
        var dialog = $("#Confirmation");
        dialog.find(".modal-title").text("提示");
        dialog.find(".modal-body").empty();
        dialog.find(".modal-body").append(parm.message);
        dialog.modal("show");
        callback = parm.confirmFunction;
    },
    getGuid:function() {
        function s4() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        }
        return (s4() + s4() + "-" + s4() + "-" + s4() + "-" + s4() + "-" + s4() + s4() + s4());
    },
    ClearObject: function (obj) {
        for (var attribute in obj) {
            if (obj.hasOwnProperty(attribute)) {
                if (typeof (obj[attribute]) == "object") {
                    this.ClearObject(obj[attribute]); //递归遍历
                } else {
                    obj[attribute] = null;
                }
            }
        }
    }
};



var callback;
var Messages = {
    UploadFileError: "文件类型错误，仅支持xls、xlsx",
    Success: "操作成功"
};
$(function () {
    var dialog = $("#Confirmation");
    var confirmbtn = dialog.find(".btn-primary");
    confirmbtn.click(function () {
        dialog.modal("hide");
        if (callback != null) {
            callback();
        }
    });
});
