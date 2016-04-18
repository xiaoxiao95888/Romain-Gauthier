using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romain_Gauthier.Service
{
    public class BaseService
    {
        public readonly RomainGauthierDataContext DbContext;

        public BaseService(RomainGauthierDataContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
