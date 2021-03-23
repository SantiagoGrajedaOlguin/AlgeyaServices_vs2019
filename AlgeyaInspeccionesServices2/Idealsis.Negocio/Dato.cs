using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class Dato
    {
        int    id;
        short  catalogo;
        int    codigo;
        string descripcion;
        byte   esEtiqueta;
        byte   tipo;
        string opciones;
        short  formato;
        string formatoDes;
        string formatoCap;

        public int Id { get { return id; } set { id = value; } }
        public short Catalogo { get { return catalogo; } set { catalogo = value; } }
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public byte EsEtiqueta { get { return esEtiqueta; } set { esEtiqueta = value; } }
        public byte Tipo { get { return tipo; } set { tipo = value; } }
        public string Opciones { get { return opciones; } set { opciones = value; } }
        public short Formato { get { return formato; } set { formato = value; } }
        public string FormatoDes { get { return formatoDes; } set { formatoDes = value; } }
        public string FormatoCap { get { return formatoCap; } set { formatoCap = value; } }

    }
}
