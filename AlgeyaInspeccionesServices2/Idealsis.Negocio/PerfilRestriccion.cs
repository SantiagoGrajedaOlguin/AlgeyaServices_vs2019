using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class PerfilRestriccion
    {
        int    id;
        int    usuario;
        int    perfil;
        short  tipo;
        short  catalogo;
        int    codigo;
        string cuenta;
        string descripcion;
        int    padre;
        short  valor;
        byte   esPred;
        short  posicion;

        public int Id { get { return id; } set { id = value; } }
        public int Usuario { get { return usuario; } set { usuario = value; } }
        public int Perfil { get { return perfil; } set { perfil = value; } }
        public short Tipo { get { return tipo; } set { tipo = value; } }
        public short Catalogo { get { return catalogo; } set { catalogo = value; } }
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Cuenta { get { return cuenta; } set { cuenta = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public int Padre { get { return padre; } set { padre = value; } }
        public short Valor { get { return valor; } set { valor = value; } }
        public byte EsPred { get { return esPred; } set { esPred = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }
    }
}
