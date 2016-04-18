using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Library.Services
{
    public interface INewsService : IDisposable
    {
        void Insert(News news);
        void Update();
        News GetNews(Guid id);
        IQueryable<News> GetNewses();
    }
}
