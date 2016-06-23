using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
 

namespace Fintech
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CustomerService.svc or CustomerService.svc.cs at the Solution Explorer and start debugging.
    public class CustomerService : ICustomerService
    {
        DB_9FFBF4_FintechEntities context = new DB_9FFBF4_FintechEntities();
        public string AddNewUser(Users newUser)
        {
            try
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            
            
        }

        public string DoWork()
        {
            DB_9FFBF4_FintechEntities context = new DB_9FFBF4_FintechEntities();
            string da = context.Users.Where(p => p.uid == 1).FirstOrDefault().login.ToString();
            return "da"+ da;
        }
    }
}
