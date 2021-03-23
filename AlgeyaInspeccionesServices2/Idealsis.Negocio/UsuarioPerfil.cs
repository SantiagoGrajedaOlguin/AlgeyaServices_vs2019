using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class UsuarioPerfil
    {
        int    id;
        string codigo;
        string descripcion;
        int    nivel;
        public int Id { get { return id; } set { id = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public int Nivel { get { return nivel; } set { nivel = value; } }
    }
}
