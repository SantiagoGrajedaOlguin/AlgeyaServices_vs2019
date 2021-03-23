using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class ConfigEmail
    {
        private int    id;
        private byte   origen;
        private int    idOrigen;
        private string descripcion;
        private string email;
        private string servidorSmtp;
        private string puerto;
        private byte   autentificacion;
        private string usuario;
        private string password;
        private byte   conSsl;
        private byte   libreria;

        // Id           = Autonumerico
        // Origen       = Fijo 1
        // IdOrigen     = Id de usuario
        // Descripcion  = ""
        // Email        = Email
        // ServidorSmtp = ""

        public int Id { get => id; set => id = value; }
        public byte Origen { get => origen; set => origen = value; }
        public int IdOrigen { get => idOrigen; set => idOrigen = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Email { get => email; set => email = value; }
        public string ServidorSmtp { get => servidorSmtp; set => servidorSmtp = value; }
        public string Puerto { get => puerto; set => puerto = value; }
        public byte Autentificacion { get => autentificacion; set => autentificacion = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Password { get => password; set => password = value; }
        public byte ConSsl { get => conSsl; set => conSsl = value; }
        public byte Libreria { get => libreria; set => libreria = value; }

    }
}
