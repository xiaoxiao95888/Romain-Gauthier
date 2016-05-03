using Romain_Gauthier.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Service.Services
{
    public class ViewRecordService : BaseService, IViewRecordService
    {
        public ViewRecordService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public IQueryable<ViewRecord> GetViewRecords()
        {
            return DbContext.ViewRecords.Where(n => !n.IsDeleted);
        }
    }
}
