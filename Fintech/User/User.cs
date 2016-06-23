using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Fintech.User
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int uid { get; set; }

        [DataMember]
        public string login { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public int cardNumber { get; set; }

    }
}
