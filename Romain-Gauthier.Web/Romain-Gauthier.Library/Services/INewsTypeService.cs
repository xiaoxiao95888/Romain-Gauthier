using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Library.Services
{
    public interface INewsTypeService : IDisposable
    {
        void Insert(NewsType newsType);
        void Update();
        NewsType GetNewsType(Guid id);
        IQueryable<NewsType> GetNewsTypes();
    }
}
