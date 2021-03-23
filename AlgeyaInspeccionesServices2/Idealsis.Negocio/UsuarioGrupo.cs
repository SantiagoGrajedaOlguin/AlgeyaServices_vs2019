using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class UsuarioGrupo
    {
        int    id;
        string codigo;
        string descripcion;


        public int Id { get { return id; } set { id = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }

    }
}
