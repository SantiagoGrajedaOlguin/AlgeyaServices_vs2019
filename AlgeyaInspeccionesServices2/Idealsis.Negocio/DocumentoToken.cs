using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class DocumentoToken
    {
        string codigo;
        string descripcion;
        int    tipoDato;
        string formato;

        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public int TipoDato { get { return tipoDato; } set { tipoDato = value; } }
        public string Formato { get { return formato; } set { formato = value; } }
    }
}
