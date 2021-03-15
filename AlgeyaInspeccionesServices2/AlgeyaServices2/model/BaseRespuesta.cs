using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace AlgeyaServices2.model
{
    [DataContract]
    public class BaseRespuesta
    {
        [DataMember]
        public bool Succesful { set; get; }
        [DataMember]
        public String Message { set; get; }
    }
}