using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class Catalogo
    {
        int    id;
        short  tipo;
        int    idPadre;
        int    codigo;
        string descripcion;
        string corta;
        byte   activo;
        byte   bandera1;
        byte   bandera2;
        string valorStr;

        public int    Id { get { return id; } set { id = value; } }
        public short  Tipo { get { return tipo; } set { tipo = value; } }
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public int IdPadre { get { return idPadre; } set { idPadre = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Corta { get { return corta; } set { corta = value; } }
        public string ValorStr { get { return valorStr; } set { valorStr = value; } }
        public byte Activo { get { return activo; } set { activo = value; } }
        public byte Bandera1 { get { return bandera1; } set { bandera1 = value; } }
        public byte Bandera2 { get { return bandera2; } set { bandera2 = value; } }

    }
}
