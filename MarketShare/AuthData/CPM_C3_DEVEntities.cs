using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MarketShare
{
    public partial class MarketShareEntities
    {
        public MarketShareEntities(string connectionString) : base(connectionString)
        {
            this.SetCommandTimeOut(3000);
        }
        public void SetCommandTimeOut(int timeout)
        {
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = timeout;
        }
    }
    public partial class KapowDBEntities
    {
        public KapowDBEntities(string connectionString) : base(connectionString)
        {
            this.SetCommandTimeOut(3000);
        }
        public void SetCommandTimeOut(int timeout)
        {
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = timeout;
        }
    }
}