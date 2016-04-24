using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Models.Enum;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;
using Romain_Gauthier.Web.Infrastructure.Utilites;
using System.IO;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class ProductImageController : BaseApiController
    {
        private readonly IFileService _fileService;
        public ProductImageController(IFileService fileService)
        {
            _fileService = fileService;
        }

        public object Get()
        {
            var items = _fileService.GetFiles().Where(n => n.FileType == FileType.图片 && n.ProductId != null).OrderByDescending(n => n.UpdateTime).Select(n => new ProductImageModel
            {
                Id = n.Id,
                FileName = n.FileName,
                ProductId = n.ProductId.Value,
                ProductName = n.Product.Name,
                UpdateTime = n.UpdateTime.Value,
                ProductTypeName = n.Product.ProductType.Name,
                Name = n.Name
            }).ToArray();
            return items;
        }
        public object Post(ProductImageModel model)
        {
            var filepath = ConfigurationManager.AppSettings["FilePath"] + model.FileName;
            if (model.FileName == null || model.ProductId == null)
            {               
                try
                {
                    System.IO.File.Delete(filepath);
                }
                catch (Exception ex)
                {

                }
                return Failed("操作失败");
            }
           
            var fileName = Path.GetFileName(filepath);
            var fileExtension = Path.GetExtension(fileName);
            var thumbnailname = Guid.NewGuid() + fileExtension;


            MakeThumbnailHelper.MakeThumbnail(filepath, ConfigurationManager.AppSettings["FilePath"] + thumbnailname, 120, 120, "Cut");
            _fileService.Insert(new Library.Models.File
            {
                Id = Guid.NewGuid(),
                FileName = model.FileName,
                ProductId = model.ProductId,
                FileType = FileType.图片,
                Name = model.Name,
                Thumbnail = thumbnailname
            });
            return Success();
        }

        public object Delete(Guid id)
        {
            var item = _fileService.GetFile(id);
            if (item != null)
            {
                item.IsDeleted = true;
                var filepath = ConfigurationManager.AppSettings["FilePath"] + item.FileName;
                try
                {
                    System.IO.File.Delete(filepath);
                }
                catch (Exception ex)
                {

                }
                _fileService.Update();
                return Success();
            }
            return Failed("找不到图片");
        }
    }
}
