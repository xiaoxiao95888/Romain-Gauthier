using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;

namespace Romain_Gauthier.Service.Services
{
    public class FileService : BaseService, IFileService
    {
        public FileService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public File GetFile(Guid id)
        {
            return DbContext.Files.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<File> GetFiles()
        {
            return DbContext.Files.Where(n => !n.IsDeleted);
        }

        public void Insert(File file)
        {
            DbContext.Files.Add(file);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
