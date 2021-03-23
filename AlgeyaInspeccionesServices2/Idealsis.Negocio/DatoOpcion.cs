using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class DatoOpcion
    {
        int dato;
        short posicion;
        string descripcion;

        public int Dato { get { return dato; } set { dato = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
    }
}
