var Product = {
    viewModel: {
        types: ko.observableArray(),
        products: ko.observableArray(),
        productimages: ko.observableArray(),
        product: {
            Id: ko.observable(),
            Name: ko.observable(),
            TypeId: ko.observable(),
            TypeName: ko.observable()
        },
        type: {
            Id: ko.observable(),
            Name: ko.observable(),
            ParentId: ko.observable(),
            ParentName: ko.observable()
        },
        productimage: {
            Id: ko.observable(),
            Name: ko.observable(),
            FileName: ko.observable(),
            Url: ko.observable(),
            ProductId: ko.observable(),
            ProductName: ko.observable(),
            ProductTypeName: ko.observable()
        },
        parentType: ko.observable(),
        producttype: ko.observable(),
        //上传图片时选择的产品
        imageproduct: ko.observable()
    }
};
Product.viewModel.LoadTypes = function () {
    $.get("/api/producttype", function (result) {
        ko.mapping.fromJS(result, {}, Product.viewModel.types);
    });
};
Product.viewModel.LoadProducts = function () {
    $.get("/api/product", function (result) {
        ko.mapping.fromJS(result, {}, Product.viewModel.products);
    });
};
Product.viewModel.LoadProductImages = function () {
    $.get("/api/productImage", function (result) {
        ko.mapping.fromJS(result, {}, Product.viewModel.productimages);
    });
};

//编辑类型
Product.viewModel.EditType = function () {
    var model = ko.mapping.toJS(this);
    var parentId = model.ParentId;
    Product.viewModel.parentType(null);
    ko.utils.arrayForEach(Product.viewModel.types(), function (item) {
        if (item.Id() === parentId) {
            Product.viewModel.parentType(item);
        }
    });
    ko.mapping.fromJS(model, {}, Product.viewModel.type);
};
//保存类型
Product.viewModel.SaveType = function () {
    var model = ko.mapping.toJS(Product.viewModel.type);
    var parent = ko.mapping.toJS(Product.viewModel.parentType);
    if (parent != null) {
        model.ParentId = parent.Id;
    }
    if (model.Id == null) {
        //新增的
        $.ajax({
            type: "post",
            url: "/api/producttype",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    Product.viewModel.LoadTypes();
                }
            }
        });
    } else {
        //更新
        $.ajax({
            type: "put",
            url: "/api/producttype",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    Product.viewModel.LoadTypes();
                }
            }
        });
    }
};
//取消编辑
Product.viewModel.TypeCannel = function () {
    Product.viewModel.parentType(null);
    var model = {
        Id: null,
        Name: "",
        ParentId: "",
        ParentName: ""
    };
    ko.mapping.fromJS(model, {}, Product.viewModel.type);
};
//删除类型
Product.viewModel.TypeDelete = function () {
    var model = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/producttype/" + model.Id,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        Product.viewModel.LoadTypes();
                        model = {
                            Id: null,
                            Name: "",
                            ParentId: "",
                            ParentName: ""
                        };
                        ko.mapping.fromJS(model, {}, Product.viewModel.type);
                    }
                }
            });
        }
    });
};


//编辑产品
Product.viewModel.EditProduct = function () {
    var model = ko.mapping.toJS(this);
    var typeId = model.TypeId;
    Product.viewModel.producttype(null);
    ko.utils.arrayForEach(Product.viewModel.types(), function (item) {
        if (item.Id() === typeId) {
            Product.viewModel.producttype(item);
        }
    });
    ko.mapping.fromJS(model, {}, Product.viewModel.product);
};
//保存类型
Product.viewModel.SaveProduct = function () {
    var model = ko.mapping.toJS(Product.viewModel.product);
    var type = ko.mapping.toJS(Product.viewModel.producttype);
    if (type != null) {
        model.TypeId = type.Id;
    }
    if (model.Id == null) {
        //新增的
        $.ajax({
            type: "post",
            url: "/api/product",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    Product.viewModel.LoadProducts();
                }
            }
        });
    } else {
        //更新
        $.ajax({
            type: "put",
            url: "/api/product",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    Product.viewModel.LoadProducts();
                }
            }
        });
    }
};
//取消编辑
Product.viewModel.ProductCannel = function () {
    Product.viewModel.producttype(null);
    var model = {
        Id: null,
        Name: "",
        TypeId: null,
        TypeName: ""
    };
    ko.mapping.fromJS(model, {}, Product.viewModel.product);
};
//删除类型
Product.viewModel.DeleteProduct = function () {
    var model = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/product/" + model.Id,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        Product.viewModel.LoadProducts();
                        model = {
                            Id: null,
                            Name: "",
                            ParentId: "",
                            ParentName: ""
                        };
                        ko.mapping.fromJS(model, {}, Product.viewModel.product);
                    }
                }
            });
        }
    });
};
//上传图片
Product.viewModel.Upload = function () {
    var model = ko.mapping.toJS(Product.viewModel.productimage);
    var product = ko.mapping.toJS(Product.viewModel.imageproduct);
    if (product == null) {
        Helper.ShowErrorDialog("必须选择产品");
    } else if (model.Name == null) {
        Helper.ShowErrorDialog("必须填写名称");
    } else {
        Product.viewModel.productimage.ProductId(product.Id);
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
        Product.viewModel.productimage.FileName(fileName);
        var model = ko.mapping.toJS(Product.viewModel.productimage);
        var product = ko.mapping.toJS(Product.viewModel.imageproduct);
        model.ProductId = product.Id;
        $.ajax({
            type: "post",
            url: "/api/ProductImage",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    Helper.ShowSuccessDialog(Messages.Success);
                    Product.viewModel.LoadProductImages();
                }
            }
        });
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
//删除图片
Product.viewModel.ImageDelete = function() {
    var model = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/ProductImage/" + model.Id,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        Helper.ShowSuccessDialog(Messages.Success);
                        Product.viewModel.LoadProductImages();
                    }
                }
            });
        }
    });
};
$(function () {
    ko.applyBindings(Product);
    Product.viewModel.LoadTypes();
    Product.viewModel.LoadProducts();
    Product.viewModel.LoadProductImages();
});