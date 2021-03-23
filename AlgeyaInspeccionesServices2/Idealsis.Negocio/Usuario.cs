using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class Usuario
    {
        int         id;
        string      codigo;
        string      descripcion;
        int         idGrupo;
        string      password;
        int         nivel;
        int         activo;
        int         idPerfil;
        ConfigEmail correo;

        public int Id { get { return id; } set { id = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Password { get { return password; } set { password = value; } }
        public int IdGrupo { get { return idGrupo; } set { idGrupo = value; } }
        public int Nivel { get { return nivel; } set { nivel = value; } }
        public int Activo { get { return activo; } set { activo = value; } }
        public int IdPerfil { get { return idPerfil; } set { idPerfil = value; } }
        public ConfigEmail Correo { get { return correo; } set { correo = value; } }
    }
}
