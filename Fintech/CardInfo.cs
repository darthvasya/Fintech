﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Fintech
{
    [DataContract]
    public class CardInfo
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string cardNumber { get; set; }

    }
}