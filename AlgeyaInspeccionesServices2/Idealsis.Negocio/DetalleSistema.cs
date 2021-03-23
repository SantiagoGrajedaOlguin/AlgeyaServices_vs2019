using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class DetalleSistema
    {
        int id;
        int idOrigen;
        short posicion;
        string descripcion;
        string estatus;

        public int Id { get { return id; } set { id = value; } }
        public int IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Estatus { get { return estatus; } set { estatus = value; } }
    }
}
