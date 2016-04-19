using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Library.Services
{
   public  interface IFileService : IDisposable
    {
        void Insert(File file);
        void Update();
        File GetFile(Guid id);
        IQueryable<File> GetFiles();
    }
}
