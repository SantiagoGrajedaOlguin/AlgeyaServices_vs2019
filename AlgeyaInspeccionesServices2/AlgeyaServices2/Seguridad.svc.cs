using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Web.Script.Serialization;

namespace AlgeyaServices2
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Seguridad" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Seguridad.svc o Seguridad.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Seguridad : ISeguridad
    {
        public string Login(string user, string pass)
        {
            model.BaseRespuesta baseRespuesta = new model.BaseRespuesta();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("Usuario", user);
                parametros.Add("Password", pass);
                
                //string result = new BDmanager().getEscalarString("sp_ValidarUsuario", parametros);
                string result = "";

                if (true == string.IsNullOrEmpty(result))
                {
                    baseRespuesta.Succesful = true;
                }
                else
                {
                    baseRespuesta.Succesful = false;
                }
                baseRespuesta.Message = result;
            }
            catch (Exception ex)
            {
                baseRespuesta.Succesful = false;
                baseRespuesta.Message = "Error al intentar autentificar: " + ex.Message;
            }
            String json = new JavaScriptSerializer().Serialize(baseRespuesta);
            return json;
        }
    }
}
