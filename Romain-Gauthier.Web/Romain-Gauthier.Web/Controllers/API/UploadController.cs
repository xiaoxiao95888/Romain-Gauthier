using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Romain_Gauthier.Web.Controllers.API
{
    [Authorize]
    public class UploadController : BaseApiController
    {
        public object Post()
        {
            var fileFullPath = string.Empty;
            try
            {
                var file = HttpContext.Current.Request.Files[0];
                var uploadFilePath = ConfigurationManager.AppSettings["FilePath"];
                if (!Directory.Exists(uploadFilePath))
                {
                    Directory.CreateDirectory(uploadFilePath);
                }
                var fileNamePrefix = Guid.NewGuid().ToString();
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    //扩展名
                    var fileExtension = Path.GetExtension(fileName);
                    var extension = string.IsNullOrEmpty(fileExtension) == false
                        ? fileExtension.Replace(".", string.Empty)
                        : string.Empty;

                    var newFileName = fileNamePrefix + fileExtension;
                    fileFullPath = uploadFilePath + newFileName;
                    file.SaveAs(uploadFilePath + newFileName);
                    return new
                    {
                        FileName = newFileName,
                        Extension = extension,
                        OriginalName = Path.GetFileNameWithoutExtension(file.FileName)
                    };
                }
                return Failed();
            }
            catch (Exception ex)
            {
                File.Delete(fileFullPath);
                return Failed(ex.Message);
            }
        }
    }
}
