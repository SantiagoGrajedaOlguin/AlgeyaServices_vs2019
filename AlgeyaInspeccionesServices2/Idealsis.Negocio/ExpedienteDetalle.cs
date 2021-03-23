using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class ExpedienteDetalle
    {
        int     id;           //Id registro (autonumerico)
        int     idOrigen;     //Id de auditoria
        byte    tipo;         //Tipo de detalle: 0=Disposiciones de caracter general, 1=Lineamientos, 2 = Requerimientos
        int     posicion;     //Id detalle catalago
        int     codigo;       //0
        byte    esRequerido;  //0
        int     dato;         //0
        int     persona;      //0
        int     plantilla;    //0
        decimal valor;        //0
        string  comentarios;  //Comentarios
        string  notas;        //Notas (Common Gaps)
        int     estatus;      //Id estatus
        string  fechaEstatus; //Fecha (captura)
        int     idArchivo;    //0
        string  fecha;        //Fecha registro
        string  usuario;      //Usuario
        string  fechaMod;     //Fecha modificacion
        string  usuarioMod;   //Usuario modificacion
        int     numReq;       //0
        int     numAdd;       //0

        /*
        -----------------------------
        Operaciones relevantes cuerpo
        -----------------------------
        Id           -->  Id registro (autonumerico)
        IdOrigen     -->  Id de auditoria
        Tipo         -->  3
        Posicion     -->  Id cuenta bancaria del cliente
        Codigo       -->  0
        EsRequerido  -->  0
        Dato         -->  0
        Persona      -->  Persona que elaboro (Catalogo de auditores)
        Plantilla    -->  0
        Valor        -->  0
        Comentarios  -->  ""
        Notas        -->  Notas
        Estatus      -->  Id estatus de lineamiento
        FechaEstatus -->  Fecha (captura)
        IdArchivo    -->  0
        Fecha        -->  Fecha registro
        Usuario      -->  Usuario
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        

        ------------------------------
        Operaciones relevantes detalle
        ------------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id cuerpo
        Tipo         -->  4
        Posicion     -->  Numero de mes (1-12)
        Codigo       -->  0
        EsRequerido  -->  0
        Dato         -->  0
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  Saldo fin de mes $
        Comentarios  -->  ""
        Notas        -->  Notas
        Estatus      -->  Combo tuvo operaciones relevantes (Si=1 o No=0)
        FechaEstatus -->  null
        IdArchivo    -->  0
        Fecha        -->  Fecha registro
        Usuario      -->  Usuario
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0


        ------------------------------------------
        Operaciones relevantes detalle movimientos
        ------------------------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id detalle
        Tipo         -->  5
        Posicion     -->  Numero de renglon
        Codigo       -->  0
        EsRequerido  -->  0
        Dato         -->  0
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  Monto de operacion $
        Comentarios  -->  ""
        Notas        -->  Notas (Opcional)
        Estatus      -->  ""
        FechaEstatus -->  Fecha de operación
        IdArchivo    -->  0
        Fecha        -->  Fecha registro
        Usuario      -->  Usuario
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0

        -----------------------------
        Minutas cuerpo
        -----------------------------
        Id           -->  Id registro (autonumerico)
        IdOrigen     -->  Id de auditoria
        Tipo         -->  6
        Posicion     -->  0
        Codigo       -->  0
        EsRequerido  -->  0
        Dato         -->  Tipo de sesion (1=Ordinaria, 2=Extraordinaria)
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  0
        Comentarios  -->  Comentarios
        Notas        -->  Acta / Temas
        Estatus      -->  Quorum (1=Si, 2=No)
        FechaEstatus -->  Fecha (Captura)
        IdArchivo    -->  0
        Fecha        -->  Fecha registro
        Usuario      -->  Usuario
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        */

        /*
        -----------------------------
        Minutas detalle (temas)
        -----------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id de minuta
        Tipo         -->  7
        Posicion     -->  Id tema
        Codigo       -->  0
        EsRequerido  -->  0
        Dato         -->  0
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  0
        Comentarios  -->  0
        Notas        -->  Acta / Temas
        Estatus      -->  0
        FechaEstatus -->  null
        IdArchivo    -->  0
        Fecha        -->  Fecha registro
        Usuario      -->  Usuario
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        */

        /*
        ---------------------------
        Oficios
        ---------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id de auditoria
        Tipo         -->  8
        Posicion     -->  0
        Codigo       -->  Id motivo de oficio (Catalogo 100)
        EsRequerido  -->  0
        Dato         -->  Id autoridad (Catalogo 86)
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  Contestado en tiempo y forma (1=Si, 0=No)
        Comentarios  -->  Emitido por
        Notas        -->  No. de oficio
        Estatus      -->  Id estatus oficios (Catalogo 101)
        FechaEstatus -->  NULL 
        IdArchivo    -->  0
        Fecha        -->  Fecha oficio
        Usuario      -->  Usuario
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        */

        /*
        ------------------------------
        Test de listas negras (Cuerpo)
        ------------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id de auditoria
        Tipo         -->  9
        Posicion     -->  0
        Codigo       -->  0
        EsRequerido  -->  0
        Dato         -->  0
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  Numero de coincidencias 
        Comentarios  -->  ""
        Notas        -->  Nombre de persona buscada
        Estatus      -->  0
        FechaEstatus -->  Fecha de captura
        IdArchivo    -->  0
        Fecha        -->  Fecha alta
        Usuario      -->  Usuario alta
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        */

        /*
        -------------------------------
        Test de listas negras (Detalle)
        -------------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id de cuerpo
        Tipo         -->  10
        Posicion     -->  Id tipo de lista (Catalogo 82)
        Codigo       -->  0
        EsRequerido  -->  0
        Dato         -->  0
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  1=Conicidencia, 0=Sin coincidencia
        Comentarios  -->  0
        Notas        -->  0
        Estatus      -->  0
        FechaEstatus -->  NULL 
        IdArchivo    -->  0
        Fecha        -->  Fecha alta
        Usuario      -->  Usuario alta
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        */

        /*
        ------------------------------
        Reportes enviados
        ------------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id de auditoria
        Tipo         -->  11
        Posicion     -->  Folio
        Codigo       -->  Enviado (1=Si, 0=No)
        EsRequerido  -->  Enviado en tiempo (1=Si, 0=No)
        Dato         -->  Tipo de reporte (Catalogo 85)
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  Registros reportados
        Comentarios  -->  Comentarios
        Notas        -->  Periodo
        Estatus      -->  0
        FechaEstatus -->  Fecha presentación CCC
        IdArchivo    -->  0
        Fecha        -->  Fecha de captura (Editable)
        Usuario      -->  Usuario alta
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        */

        /*
        ------------------------------
        Acuses
        ------------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id de auditoria
        Tipo         -->  12
        Posicion     -->  0
        Codigo       -->  0
        EsRequerido  -->  0
        Dato         -->  Id. tipo de acuse
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  0
        Comentarios  -->  Folio acuse
        Notas        -->  Descripción de acuse
        Estatus      -->  0
        FechaEstatus -->  null
        IdArchivo    -->  0
        Fecha        -->  Fecha alta
        Usuario      -->  Usuario alta
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        */

        /*
        ------------------------------------
        Papel de cliente por tipo de persona (cuerpo)
        ------------------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id de auditoria
        Tipo         -->  13
        Posicion     -->  Id tipo de persona
        Codigo       -->  Consecutivo
        EsRequerido  -->  0
        Dato         -->  0
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  0
        Comentarios  -->  ""
        Notas        -->  ""
        Estatus      -->  0
        FechaEstatus -->  Fecha
        IdArchivo    -->  0
        Fecha        -->  Fecha alta
        Usuario      -->  Usuario alta
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        */

        /*
        ----------------------------------------------
        Papel de cliente por tipo de persona (detalle)
        ----------------------------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id cuerpo
        Tipo         -->  14
        Posicion     -->  IdDetalle
        Codigo       -->  0
        EsRequerido  -->  0
        Dato         -->  Id. dato
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  Valor si es numerico
        Comentarios  -->  ""
        Notas        -->  Valor si es cadena
        Estatus      -->  0
        FechaEstatus -->  Valor si es fecha
        IdArchivo    -->  0
        Fecha        -->  Fecha alta
        Usuario      -->  Usuario alta
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        */

        /*
        ----------------------------------------------
        Detalle de documentos
        ----------------------------------------------
        Id           -->  Id registro(autonumerico)
        IdOrigen     -->  Id de auditoria
        Tipo         -->  15
        Posicion     -->  0
        Codigo       -->  0
        EsRequerido  -->  0
        Dato         -->  Id. documento modelo
        Persona      -->  0
        Plantilla    -->  0
        Valor        -->  0
        Comentarios  -->  Descripcion de documento
        Notas        -->  Desc de hallazgo
        Estatus      -->  0
        FechaEstatus -->  ""
        IdArchivo    -->  Id de hallazgo
        Fecha        -->  Fecha alta
        Usuario      -->  Usuario alta
        FechaMod     -->  Fecha modificacion
        UsuarioMod   -->  Usuario modificacion
        NumReq       -->  0
        NumAdd       -->  0
        */

        public int Id { get => id; set => id = value; }
        public int IdOrigen { get => idOrigen; set => idOrigen = value; }
        public byte Tipo { get => tipo; set => tipo = value; }
        public int Posicion { get => posicion; set => posicion = value; }
        public int Codigo { get => codigo; set => codigo = value; }
        public byte EsRequerido { get => esRequerido; set => esRequerido = value; }
        public int Dato { get => dato; set => dato = value; }
        public int Persona { get => persona; set => persona = value; }
        public int Plantilla { get => plantilla; set => plantilla = value; }
        public decimal Valor { get => valor; set => valor = value; }
        public string Comentarios { get => comentarios; set => comentarios = value; }
        public string Notas { get => notas; set => notas = value; }
        public int Estatus { get => estatus; set => estatus = value; }
        public string FechaEstatus { get => fechaEstatus; set => fechaEstatus = value; }
        public int IdArchivo { get => idArchivo; set => idArchivo = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string FechaMod { get => fechaMod; set => fechaMod = value; }
        public string UsuarioMod { get => usuarioMod; set => usuarioMod = value; }
        public int NumReq { get => numReq; set => numReq = value; }
        public int NumAdd { get => numAdd; set => numAdd = value; }


        public ImgPreviewConfigDTO filePreview { set; get; }
    }
}
