using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Fintech
{
    [DataContract]
    public class LogInfo
    {
        [DataMember]
        public string cardNumber { get; set; }

        [DataMember]
        public string password { get; set; }
    }
}
