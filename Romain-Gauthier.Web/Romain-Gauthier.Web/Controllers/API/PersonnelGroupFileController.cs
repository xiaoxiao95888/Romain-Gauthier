using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;
using Romain_Gauthier.Web.Models.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class PersonnelGroupFileController : BaseApiController
    {
        private readonly IFileService _fileService;
        private readonly IPersonnelGroupService _personnelGroupService;
        public PersonnelGroupFileController(IFileService fileService, IPersonnelGroupService personnelGroupService)
        {
            _fileService = fileService;
            _personnelGroupService = personnelGroupService;
        }
        public object Get()
        {
            var groupid = HttpContext.Current.Request["PersonnelGroupId"];
            var items = _fileService.GetFiles().Where(n => n.PersonnelGroup != null);
            if (!string.IsNullOrEmpty(groupid))
            {
                var id = new Guid(groupid);
                items = items.Where(n => n.PersonnelGroupId == id);
            }
            var models = items.OrderByDescending(n=>n.CreatedTime).Select(n => new FileModel
            {
                Id = n.Id,
                Name = n.Name,
                FileName = n.FileName,
                FileType = (FileType)n.FileType,
                PersonnelGroupId = n.PersonnelGroupId,
                PersonnelGroupName = n.PersonnelGroup.Name,
                UpdateTime = n.UpdateTime
            }).ToArray();
            return models;
        }
        public object Post(FileModel model)
        {
            if (model.PersonnelGroupId == null || string.IsNullOrEmpty(model.FileName) || string.IsNullOrEmpty(model.Name))
            {
                return Failed("操作失败");
            }
            var group = _personnelGroupService.GetPersonnelGroup(model.PersonnelGroupId.Value);
            if (group == null || group.IsDeleted)
            {
                return Failed("找不到分组");
            }
            _fileService.Insert(new File
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                FileName = model.FileName,
                FileType = Romain_Gauthier.Library.Models.Enum.FileType.文件,
                PersonnelGroupId = model.PersonnelGroupId,
            });
            return Success();
        }
        public object Delete(Guid id)
        {
            var item = _fileService.GetFile(id);
            if (item == null)
            {
                return Failed("找不到文件");
            }
            var filepath = ConfigurationManager.AppSettings["FilePath"] + item.FileName;
            try
            {
                System.IO.File.Delete(filepath);
            }
            catch (Exception ex)
            {

            }
            item.IsDeleted = true;
            _personnelGroupService.Update();
            return Success();
        }
    }
}
