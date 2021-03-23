using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Web.Script.Serialization;
using System.Xml;
using AppInspeccionServicios.model;

namespace AppInspeccionServicios
{
    public class Informacion : IInformacion
    {
        // Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
        // Para crear una operación que devuelva XML,
        //     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
        //     e incluya la siguiente línea en el cuerpo de la operación:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        public string getPendientesCuerpo(string user)
        {
            model.BaseRespuesta baseRespuesta = new model.BaseRespuesta();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("Usuario", user);
                SqlDataReader result = new BDmanager().getReader("sp_getPendientesCuerpo", parametros);
                var r = HelperJson.Serialize(result);
                String jsonData = new JavaScriptSerializer().Serialize(r);
                if (jsonData.Length>2)
                {
                    baseRespuesta.Succesful = true;
                    baseRespuesta.Message = "Autenticación satisfactoria";
                    baseRespuesta.Data = jsonData;
                }
                else
                {
                    baseRespuesta.Message = "Autenticación no satisfactoria";
                    baseRespuesta.Succesful = false;
                    baseRespuesta.Data = "";
                }
            }
            catch (Exception ex)
            {
                baseRespuesta.Succesful = false;
                baseRespuesta.Message = "Error al intentar autentificar: " + ex.Message;
            }
            String json = new JavaScriptSerializer().Serialize(baseRespuesta);
            return json;
        }

        public string getPendientesDetalle(int idOrigen)
        {
            model.BaseRespuesta baseRespuesta = new model.BaseRespuesta();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("IdOrigen", idOrigen);
                SqlDataReader result = new BDmanager().getReader("sp_getPendientesDetalle", parametros);
                var r = HelperJson.Serialize(result);
                String jsonData = new JavaScriptSerializer().Serialize(r);
                if (jsonData.Length > 2)
                {
                    baseRespuesta.Succesful = true;
                    baseRespuesta.Message = "Autenticación satisfactoria";
                    baseRespuesta.Data = jsonData;
                }
                else
                {
                    baseRespuesta.Message = "Autenticación no satisfactoria";
                    baseRespuesta.Succesful = false;
                    baseRespuesta.Data = "";
                }
            }
            catch (Exception ex)
            {
                baseRespuesta.Succesful = false;
                baseRespuesta.Message = "Error al intentar autentificar: " + ex.Message;
            }
            String json = new JavaScriptSerializer().Serialize(baseRespuesta);
            return json;
        }
        /*
public string getArticulos(string user)
{
   string result = "";
   return result;
}
public string getBodegas(string user)
{
   string result = "";
   return result;
}
public string getBodegasInternas(string user)
{
   string result = "";
   return result;
}
public string getClientes(string user)
{
   string result = "";
   return result;
}
public string getObservaciones(string user)
{
   string result = "";
   return result;
}
public string getObservacionesDetalle(string user)
{
   string result = "";
   return result;
}
*/

    }
}
