using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class Moneda
    {
        short codigo;
        string descripcion;
        string simbolo;
        string texto;
        byte esPred;
        byte esNacional;
        string claveSat;

        public short Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Simbolo { get { return simbolo; } set { simbolo = value; } }
        public string Texto { get { return texto; } set { texto = value; } }
        public byte EsPred { get { return esPred; } set { esPred = value; } }
        public byte EsNacional { get { return esNacional; } set { esNacional = value; } }
        public string ClaveSat { get { return claveSat; } set { claveSat = value; } }

    }
}
