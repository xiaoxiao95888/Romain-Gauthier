var Personnel = {
    viewModel: {
        items: ko.observableArray(),
        item: {
            Id: ko.observable(),
            License: ko.observable(),
            PhoneNum: ko.observable(),
            Email: ko.observable(),
            Name: ko.observable(),
            NickName: ko.observable(),
            GenderStr: ko.observable(),
            Language: ko.observable(),
            City: ko.observable(),
            Province: ko.observable(),
            Country: ko.observable(),
            Headimgurl: ko.observable(),
            UpdateTime: ko.observable(),
            PersonnelGroupModels: ko.observableArray(),
            PersonnelGroups: ko.observable()
        },
        PersonnelGroups: ko.observableArray(),
        ChosenPersonnelGroups: ko.observableArray()
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
Personnel.viewModel.LoadPersonnelGroups = function (callback) {
    $.get("/api/PersonnelGroup/", function (result) {
        ko.mapping.fromJS(result, {}, Personnel.viewModel.PersonnelGroups);
        callback();
    });
};
Personnel.viewModel.Load = function () {
    $.get("/api/personnel", function (result) {
        ko.mapping.fromJS(result, {}, Personnel.viewModel.items);
    });
};
//编辑人员
Personnel.viewModel.Edit = function () {
    var model = ko.mapping.toJS(this);
    ko.mapping.fromJS(model, {}, Personnel.viewModel.item);
    Personnel.viewModel.LoadPersonnelGroups(function () {
        var model = ko.mapping.toJS(Personnel.viewModel.item);
        ko.mapping.fromJS([], {}, Personnel.viewModel.ChosenPersonnelGroups);
        if (model != null && model.PersonnelGroupModels != null) {
            ko.utils.arrayForEach(Personnel.viewModel.PersonnelGroups(), function (item) {
                for (var i = 0; i < model.PersonnelGroupModels.length; i++) {
                    if (item.Id() === model.PersonnelGroupModels[i].Id) {
                        Personnel.viewModel.ChosenPersonnelGroups.push(item);
                    }
                }
            });
        }
        console.log("selected group for Train");
    });
};
//取消编辑
Personnel.viewModel.Cannel = function () {
    var model = {
        Id: null,
        Name: "",
        PhoneNum: "",
        Email: "",
        PersonnelGroupModels:[]
    };
    ko.mapping.fromJS(model, {}, Personnel.viewModel.item);
    ko.mapping.fromJS([], {}, Personnel.viewModel.ChosenPersonnelGroups);
};
//保存人员
Personnel.viewModel.Save= function() {
    var model = ko.mapping.toJS(Personnel.viewModel.item);
    var groups = ko.mapping.toJS(Personnel.viewModel.ChosenPersonnelGroups);
    if (model != null) {
        model.PersonnelGroupModels = groups;
        if (model.Id == null) {
            //新增
            $.ajax({
                type: "post",
                url: "/api/personnel",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        Personnel.viewModel.Load();
                    }
                }
            });
        } else {
            //更新
            $.ajax({
                type: "put",
                url: "/api/personnel",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        Personnel.viewModel.Load();
                    }
                }
            });
        }
    }
    
}
//删除人员
Personnel.viewModel.Delete = function () {
    var model = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/Personnel/" + model.Id,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        Personnel.viewModel.Load();
                    }
                }
            });
        }
    });
};
$(function() {
    ko.applyBindings(Personnel);
    Personnel.viewModel.Load();
    Personnel.viewModel.LoadPersonnelGroups(function (){});
})