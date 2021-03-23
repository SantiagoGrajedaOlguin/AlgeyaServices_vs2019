using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class Opcion
    {
        string tipo;
        string codigo;
        string padre;
        string descripcion;
        short  origen;
        short  catalogo;
        byte   esPermiso;
        int    orden;


        public string Tipo { get { return tipo; } set { tipo = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Padre { get { return padre; } set { padre = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public short Origen { get { return origen; } set { origen = value; } }
        public short Catalogo { get { return catalogo; } set { catalogo = value; } }
        public byte EsPermiso { get { return esPermiso; } set { esPermiso = value; } }
        public int Orden { get { return orden; } set { orden = value; } }
    }
}
