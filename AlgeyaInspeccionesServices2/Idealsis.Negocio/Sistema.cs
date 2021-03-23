using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class Sistema
    {
        int    id;
        int    codigo;
        string descripcion;
        string desarrollador;
        string lenguaje;
        string plataforma;
        byte   enRed;
        byte   enSucursales;

        public int Id { get { return id; } set { id = value; } }
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Desarrollador { get { return desarrollador; } set { desarrollador = value; } }
        public string Lenguaje { get { return lenguaje; } set { lenguaje = value; } }
        public string Plataforma { get { return plataforma; } set { plataforma = value; } }
        public byte EnRed { get { return enRed; } set { enRed = value; } }
        public byte EnSucursales { get { return enSucursales; } set { enSucursales = value; } }

    }
}
