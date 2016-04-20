var PersonnelGroup = {
    viewModel: {
        Personnels: ko.observableArray(),
        TrainArticles: ko.observableArray(),
        ProductTypes: ko.observableArray(),
        PersonnelGroups: ko.observableArray(),
        Item: {
            Id: ko.observable(),
            Name: ko.observable(),
            Description: ko.observable(),
            ProductTypeModels: ko.observableArray(),
            TrainArticleModels: ko.observableArray()
        },
        //ProductType权限分配中选中的Group
        GroupForProductType: ko.observable(),
        ChosenProductTypes: ko.observableArray(),
        //Train权限分配中选中的Group
        GroupForTrain: ko.observable(),
        ChosenTrains: ko.observableArray(),
    }
};
PersonnelGroup.viewModel.LoadPersonnelGroups = function() {
    $.get("/api/PersonnelGroup/", function(result) {
        ko.mapping.fromJS(result, {}, PersonnelGroup.viewModel.PersonnelGroups);
    });
};
//加载ProductTypes
PersonnelGroup.viewModel.LoadProductTypes = function (callback) {
    $.get("/api/ProductType/", function (result) {
        ko.mapping.fromJS(result, {}, PersonnelGroup.viewModel.ProductTypes);
        callback();
    });
};
//加载Trains
PersonnelGroup.viewModel.LoadTrains = function (callback) {
    $.get("/api/TrainArticle/", function (result) {
        ko.mapping.fromJS(result, {}, PersonnelGroup.viewModel.TrainArticles);
        callback();
    });
};
//新增、更新group保存
PersonnelGroup.viewModel.CreateSave = function () {
    var model = ko.mapping.toJS(PersonnelGroup.viewModel.Item);
    $.ajax({
        type: "post",
        url: "/api/PersonnelGroup/",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(model),
        success: function (result) {
            if (result.Error) {
                Helper.ShowErrorDialog(result.Message);
            } else {
                Helper.ShowSuccessDialog(Messages.Success);
                PersonnelGroup.viewModel.LoadPersonnelGroups();
            }
        }
    });
};
//取消
PersonnelGroup.viewModel.Cannel=function() {
    var model = {
        Id: null,
        Name: "",
        Description: "",
        ProductTypeModels:[],
        TrainArticleModels: []
    };
    ko.mapping.fromJS(model, {}, PersonnelGroup.viewModel.Item);
}
//编辑group
PersonnelGroup.viewModel.Edit= function() {
    var model = ko.mapping.toJS(this);
    ko.mapping.fromJS(model, {}, PersonnelGroup.viewModel.Item);
}
//删除group
PersonnelGroup.viewModel.Delete = function () {
    var model = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "删除分组后，该分组的相关权限将一并删除，是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/PersonnelGroup/" + model.Id,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        PersonnelGroup.viewModel.LoadPersonnelGroups();
                    }
                }
            });
        }
    });
};
//选中group时（分配ProductType权限）
PersonnelGroup.viewModel.ChosenGroupForProductType = function() {
    PersonnelGroup.viewModel.LoadProductTypes(function() {
        var model = ko.mapping.toJS(PersonnelGroup.viewModel.GroupForProductType);
        ko.mapping.fromJS([], {}, PersonnelGroup.viewModel.ChosenProductTypes);
        if (model != null && model.ProductTypeModels != null) {
            ko.utils.arrayForEach(PersonnelGroup.viewModel.ProductTypes(), function (item) {
                for (var i = 0; i < model.ProductTypeModels.length; i++) {
                    if (item.Id() === model.ProductTypeModels[i].Id) {
                        PersonnelGroup.viewModel.ChosenProductTypes.push(item);
                    }
                }
            });
        }
        console.log("selected group for ProductType");
    });
    
};
//保存group相关的producttypes权限
PersonnelGroup.viewModel.GroupPrductTypesSave = function() {
    var model = ko.mapping.toJS(PersonnelGroup.viewModel.GroupForProductType);
    var producttypes = ko.mapping.toJS(PersonnelGroup.viewModel.ChosenProductTypes);
    if (model != null) {
        model.ProductTypeModels = producttypes;
        $.ajax({
            type: "put",
            url: "/api/PersonnelGroup",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    PersonnelGroup.viewModel.LoadPersonnelGroups();
                }
            }
        });
    }
};
//选中group时（分配Train权限）
PersonnelGroup.viewModel.ChosenGroupForTrain = function () {
    PersonnelGroup.viewModel.LoadTrains(function () {
        var model = ko.mapping.toJS(PersonnelGroup.viewModel.GroupForTrain);
        ko.mapping.fromJS([], {}, PersonnelGroup.viewModel.ChosenTrains);
        if (model != null && model.TrainArticleModels != null) {
            ko.utils.arrayForEach(PersonnelGroup.viewModel.TrainArticles(), function (item) {
                for (var i = 0; i < model.TrainArticleModels.length; i++) {
                    if (item.Id() === model.TrainArticleModels[i].Id) {
                        PersonnelGroup.viewModel.ChosenTrains.push(item);
                    }
                }
            });
        }
        console.log("selected group for Train");
    });

};
//保存group相关的Train权限
PersonnelGroup.viewModel.GroupTrainsSave = function () {
    var model = ko.mapping.toJS(PersonnelGroup.viewModel.GroupForTrain);
    var trains = ko.mapping.toJS(PersonnelGroup.viewModel.ChosenTrains);
    if (model != null) {
        model.TrainArticleModels = trains;
        $.ajax({
            type: "put",
            url: "/api/PersonnelGroup",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    PersonnelGroup.viewModel.LoadPersonnelGroups();
                }
            }
        });
    }
};
$(function () {
    ko.applyBindings(PersonnelGroup);
    PersonnelGroup.viewModel.LoadPersonnelGroups();
    //load producttypes
    PersonnelGroup.viewModel.LoadProductTypes(function () { });
    PersonnelGroup.viewModel.LoadTrains(function () { });
});