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

        public string getPendientes(string usuario, string password)
        {
            BaseRespuestaMultiple baseRespuesta = new BaseRespuestaMultiple();
            try
            {
                //inicializar resultados
                baseRespuesta.DataCuerpo = "";
                baseRespuesta.DataDetalle = "";
                baseRespuesta.DataBodegas = "";
                baseRespuesta.DataBodeguero = "";
                baseRespuesta.DataInternas = "";
                baseRespuesta.DataCalidades = "";
                baseRespuesta.DataArticulos = "";
                baseRespuesta.DataObservaciones = "";
                baseRespuesta.DataObservacionesDetalle = "";
                baseRespuesta.DataResultados = "";

                //autentificar usuario
                Dictionary<string, object> parametrosLogin = new Dictionary<string, object>();
                parametrosLogin.Add("Usuario", usuario);
                parametrosLogin.Add("Password", password);
                string result = new BDmanager().getEscalarString("sp_ValidarUsuario", parametrosLogin);
                //-------------------------------------------------------------------------------------

                if (true == string.IsNullOrEmpty(result))
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
                        baseRespuesta.DataBodegas = jsonList[2];
                        baseRespuesta.DataBodeguero = jsonList[3];
                        baseRespuesta.DataInternas = jsonList[4];
                        baseRespuesta.DataCalidades = jsonList[5];
                        baseRespuesta.DataArticulos = jsonList[6];
                        baseRespuesta.DataObservaciones = jsonList[7];
                        baseRespuesta.DataObservacionesDetalle = jsonList[8];
                        baseRespuesta.DataResultados = jsonList[9];
                    }
                    else
                    {
                        baseRespuesta.Message = "Datos no encontrados";
                        baseRespuesta.Succesful = false;
                    }
                }
                else
                {
                    baseRespuesta.Message = "Autenticación no satisfactoria";
                    baseRespuesta.Succesful = false;
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

    }
}
