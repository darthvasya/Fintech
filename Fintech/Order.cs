using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Fintech
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int ShopId { get; set; }
        [DataMember]
        public int CardId { get; set; }
        [DataMember]
        public List<int> GoodsId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string OrderTime { get; set; }
    }
}