using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Fintech
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MarketService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MarketService.svc or MarketService.svc.cs at the Solution Explorer and start debugging.
    public class MarketService : IMarketService
    {
        DB_9FFBF4_FintechEntities context = new DB_9FFBF4_FintechEntities();
        public string DoWork()
        {
            List<Users> name = context.Users.Where(p => p.uid == 1).ToList<Users>();
            Users petya = new Users();
            context.Users.Remove(petya);
            context.SaveChanges();

            return "sa" + name.Where(p=> p.uid == 1).FirstOrDefault().login;
        }
    }
}
