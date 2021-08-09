using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AppInspeccionServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ISincronizacion" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ISincronizacion
    {
        [OperationContract]
        void Sincronizar(string DataCuerpo, string DataDetalle, string DataCalidades, string DataObservaciones, string DataObservacionesDetalle);
    }
}
