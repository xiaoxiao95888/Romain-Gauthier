using Romain_Gauthier.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romain_Gauthier.Library.Services
{
    public interface ILoginLogService : IDisposable
    {
        void Insert(LoginLog loginlog);
        void Update();
        LoginLog GetLoginLog(Guid id);
        IQueryable<LoginLog> GetLoginlogs();
    }
}
