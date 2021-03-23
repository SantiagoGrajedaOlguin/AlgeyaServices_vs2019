using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class ExpedienteHallazgo
    {
        int    id;              // Id registro (autonumerico)
        int    idOrigen;        // Id auditoria
        int    folio;           // Folio consecutivo
        string fecha;           // Fecha de registro
        string hora;            // Hora de registro (no capturable)
        int    tipo;            // Catalogo de tipos de Hallazgo
        int    nivel;           // Nivel de riesgo: 1=Bajo, 2=Medio, 3=Alto 
        int    persona;         // 0
        string descripcion;     // Descripción del hallazgo
        int    area;            // Area responsable (Catalogo dinamico de areas)
        string fechaCompromiso; // Fecha de compromiso (No requerido)
        int    estatus;         // Catalogo dinamico de estatus de hallazgos
        string usuario;         // Id usuario sesión (no capturable)
        string fechaMod;        // Fecha registro (no capturable)
        string usuarioMod;      // Id usuario (no capturable)

        string tipoDesc;
        string nivelDesc;
        string areaDesc;
        string estatusDesc;

        public int Id { get => id; set => id = value; }
        public int IdOrigen { get => idOrigen; set => idOrigen = value; }
        public int Folio { get => folio; set => folio = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Hora { get => hora; set => hora = value; }
        public int Tipo { get => tipo; set => tipo = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Persona { get => persona; set => persona = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Area { get => area; set => area = value; }
        public string FechaCompromiso { get => fechaCompromiso; set => fechaCompromiso = value; }
        public int Estatus { get => estatus; set => estatus = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string FechaMod { get => fechaMod; set => fechaMod = value; }
        public string UsuarioMod { get => usuarioMod; set => usuarioMod = value; }
        public string TipoDesc { get => tipoDesc; set => tipoDesc = value; }
        public string NivelDesc { get => nivelDesc; set => nivelDesc = value; }
        public string AreaDesc { get => areaDesc; set => areaDesc = value; }
        public string EstatusDesc { get => estatusDesc; set => estatusDesc = value; }
    }
}
