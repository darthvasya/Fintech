using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Fintech
{
    [DataContract]
    public class Good
    {
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ShopId { get; set; }
        [DataMember]
        public int CatId { get; set; }
    }
}