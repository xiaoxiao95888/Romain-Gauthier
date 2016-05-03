Date.prototype.Format = function (fmt) { //author: meizz   
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
var todate = new Date();
var lastWeek = new Date(new Date().setDate(todate.getDay() - 3))
var Log = {
    viewModel: {
        FileDownLoadItems: ko.observableArray(),
        TrainLogItems: ko.observableArray(),
        LoginLogItems: ko.observableArray(),
        SearchParm: {           
            FromDate: ko.observable(lastWeek.Format('yyyy年MM月dd日')),
            ToDate: ko.observable(todate.Format('yyyy年MM月dd日')),
        },
    }
};
ko.bindingHandlers.date = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        var allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        // Date formats: http://momentjs.com/docs/#/displaying/format/
        var pattern = allBindings.format || 'YYYY/MM/DD H:m:s';

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
Log.viewModel.Load = function () {
    var model = ko.mapping.toJS(Log.viewModel.SearchParm);
    $.get("/api/FileLog", model, function (data) {
        ko.mapping.fromJS(data, {}, Log.viewModel.FileDownLoadItems);
    });
    $.get("/api/LoginLog", model, function (data) {
        ko.mapping.fromJS(data, {}, Log.viewModel.LoginLogItems);
    });
    $.get("/api/TrainLog",model, function (data) {
        ko.mapping.fromJS(data, {}, Log.viewModel.TrainLogItems);
    });
};

$(function () {
    ko.applyBindings(Log);
    Log.viewModel.Load();
    $('.input-daterange').datepicker({
        language: "zh-CN"
    });
});