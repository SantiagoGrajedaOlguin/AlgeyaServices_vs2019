using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AppInspeccionServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ISeguridad" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IInformacion
    {
        [OperationContract]
        String getPendientesCuerpo(string user);
        
        [OperationContract]
        String getPendientesDetalle(int idOrigen);

        /*
        [OperationContract]
        String getArticulos(string user);
        [OperationContract]
        String getBodegas(string user);
        [OperationContract]
        String getBodegasInternas(string user);
        [OperationContract]
        String getClientes(string user);
        [OperationContract]
        String getObservaciones(string user);
        [OperationContract]
        String getObservacionesDetalle(string user);
        */
    }
}



