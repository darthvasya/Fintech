using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Fintech
{
    [DataContract]
    public class Category
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}