using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class CatalogoConfig
    {
        short  codigo;
        string descripcion;
        string etiqueta;
        byte   conCodigo;
        byte   conCorta;
        byte   conEstatus;
        short  catPadre;
        short  catHijo;
        byte   bandera;

        public short  Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Etiqueta { get { return etiqueta; } set { etiqueta = value; } }
        public byte   ConCodigo { get { return conCodigo; } set { conCodigo = value; } }
        public byte   ConCorta { get { return conCorta; } set { conCorta = value; } }
        public byte   ConEstatus { get { return conEstatus; } set { conEstatus = value; } }
        public short  CatPadre { get { return catPadre; } set { catPadre = value; } }
        public short  CatHijo { get { return catHijo; } set { catHijo = value; } }
        public byte   Bandera { get { return bandera; } set { bandera = value; } }

    }
}
