using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class PerfilOpcion
    {
        int    id;
        int    usuario;
        int    perfil;
        string tipo;
        string codigo;
        byte   soloLectura;

        public int Id { get { return id; } set { id = value; } }
        public int Usuario { get { return usuario; } set { usuario = value; } }
        public int Perfil { get { return perfil; } set { perfil = value; } }
        public string Tipo { get { return tipo; } set { tipo = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public byte SoloLectura { get { return soloLectura; } set { soloLectura = value; } }
    }
}
