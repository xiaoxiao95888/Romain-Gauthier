using Romain_Gauthier.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Service.Services
{
    public class LoginLogService : BaseService, ILoginLogService
    {
        public LoginLogService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public LoginLog GetLoginLog(Guid id)
        {
            return DbContext.LoginLogs.Where(n => n.Id == id).FirstOrDefault();
        }

        public IQueryable<LoginLog> GetLoginlogs()
        {
            return DbContext.LoginLogs.Where(n => !n.IsDeleted);
        }

        public void Insert(LoginLog loginlog)
        {
            DbContext.LoginLogs.Add(loginlog);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
