using System;
using System.Collections.Generic;
using System.Data;
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
        public string getPendientesCuerpo(string usuario)
        {
            BaseRespuesta baseRespuesta = new BaseRespuesta();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("Usuario", usuario);
                String jsonList = new BDmanager().getJsonList("sp_getPendientesCuerpo", parametros);
                if (jsonList.Length>2)
                {
                    baseRespuesta.Succesful = true;
                    baseRespuesta.Message = "Datos encontrados";
                    baseRespuesta.Data = jsonList;
                }
                else
                {
                    baseRespuesta.Message = "Datos no encontrados";
                    baseRespuesta.Succesful = false;
                    baseRespuesta.Data = "";
                }
            }
            catch (Exception ex)
            {
                baseRespuesta.Succesful = false;
                baseRespuesta.Message = "Error al intentar obtener datos: " + ex.Message;
            }
            String json = new JavaScriptSerializer().Serialize(baseRespuesta);
            return json;
        }

        public string getPendientesDetalle(int idOrigen)
        {
            BaseRespuesta baseRespuesta = new BaseRespuesta();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("IdOrigen", idOrigen);
                String jsonList = new BDmanager().getJsonList("sp_getPendientesDetalle", parametros);
                if (jsonList.Length > 2)
                {
                    baseRespuesta.Succesful = true;
                    baseRespuesta.Message = "Datos encontrados";
                    baseRespuesta.Data = jsonList;
                }
                else
                {
                    baseRespuesta.Message = "Datos no encontrados";
                    baseRespuesta.Succesful = false;
                    baseRespuesta.Data = "";
                }
            }
            catch (Exception ex)
            {
                baseRespuesta.Succesful = false;
                baseRespuesta.Message = "Error al intentar obtener datos: " + ex.Message;
            }
            String json = new JavaScriptSerializer().Serialize(baseRespuesta);
            return json;
        }

        public string getPendientes(string usuario)
        {
            BaseRespuestaMultiple baseRespuesta = new BaseRespuestaMultiple();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("Usuario", usuario);
                String[] jsonList = new BDmanager().getMultipleJsonList("sp_getDatosPendientesInspeccion", parametros);
                if (jsonList.Length > 2)
                {
                    baseRespuesta.Succesful = true;
                    baseRespuesta.Message = "Datos encontrados";
                    baseRespuesta.DataCuerpo = jsonList[0];
                    baseRespuesta.DataDetalle = jsonList[1];
                    baseRespuesta.DataBodegas  = jsonList[2];
                    baseRespuesta.DataBodeguero = jsonList[3];
                    baseRespuesta.DataInternas  = jsonList[4];
                }
                else
                {
                    baseRespuesta.Message = "Datos no encontrados";
                    baseRespuesta.Succesful = false;
                    baseRespuesta.DataCuerpo = "";
                    baseRespuesta.DataDetalle = "";
                    baseRespuesta.DataBodegas = "";
                    baseRespuesta.DataBodeguero = "";
                    baseRespuesta.DataInternas = "";
                }
            }
            catch (Exception ex)
            {
                baseRespuesta.Succesful = false;
                baseRespuesta.Message = "Error al intentar obtener datos: " + ex.Message;
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
