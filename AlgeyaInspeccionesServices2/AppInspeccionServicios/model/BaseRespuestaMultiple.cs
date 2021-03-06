using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace AppInspeccionServicios.model
{
    [DataContract]
    public class BaseRespuestaMultiple
    {
        [DataMember]
        public bool Succesful { set; get; }
        [DataMember]
        public String Message { set; get; }
        [DataMember]
        public object DataCuerpo { set; get; }
        [DataMember]
        public object DataDetalle { set; get; }
        [DataMember]
        public object DataBodegas { set; get; }
        [DataMember]
        public object DataBodeguero { set; get; }
        [DataMember]
        public object DataInternas { set; get; }
        [DataMember]
        public object DataCalidades { set; get; }
        [DataMember]
        public object DataArticulos { set; get; }
        [DataMember]
        public object DataObservaciones { set; get; }
        [DataMember]
        public object DataObservacionesDetalle { set; get; }
        [DataMember]
        public object DataResultados { set; get; }
    }

}