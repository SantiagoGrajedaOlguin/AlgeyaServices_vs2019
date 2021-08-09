using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AppInspeccionServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Sincronizacion" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Sincronizacion.svc o Sincronizacion.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Sincronizacion : ISincronizacion
    {
        public void Sincronizar(string DataCuerpo, string DataDetalle, string DataCalidades, string DataObservaciones, string DataObservacionesDetalle)
        {
            bool resultadoSrincronizacion = true;
        }
    }
}
