
using System;
using System.Data;

namespace Idealsis.Dal.Mapeo
{
    public class SegCatalogosDetalle
    {
        const string Tabla = "SegCatalogosDetalle";
        int    id;
        int    usuario;
        int    perfil;
        string descPerfil;
        byte   conVigencia;
        string inicioVigencia;
        string finVigencia;
        int    orden;

        public int Id { get { return id; } set { id = value; } }
        public int Usuario { get { return usuario; } set { usuario = value; } }
        public int Perfil { get { return perfil; } set { perfil = value; } }
        public string DescPerfil { get { return descPerfil; } }
        public byte ConVigencia { get { return conVigencia; } set { conVigencia = value; } }
        public string InicioVigencia { get { return inicioVigencia; } set { inicioVigencia = value; } }
        public string FinVigencia { get { return finVigencia; } set { finVigencia = value; } }
        public int Orden { get { return orden; } set { orden = value; } }

        public int Guardar(bool ObtenerId)
        {
            string c;
            string v;
            var cnn = new Conexion();
            bool EsNuevo;
            cnn.Conectar();
            if (ObtenerId) id = (int)Helper.Val(cnn.DameValor(Tabla, "Usuario=" + Usuario + " and Perfil=" + Perfil, "Id"));
            EsNuevo = (id == 0);

            c = "Id,0,9|Usuario|Perfil|ConVigencia|InicioVigencia,2|FinVigencia,2|Orden";
            v = id + "|" + usuario + "|" + perfil + "|" + conVigencia + "|" + inicioVigencia + "|" + finVigencia + "|" + orden;
            id = cnn.GuardarRegistro(Tabla, c, v);

            if (EsNuevo) TraspasarOpciones(usuario, perfil);

            /*
            if (usuario > 0)
                cnn.RegistrarBitacora("Perfil", "Se modifico usuario: " + cnn.DameValor("SegCatalogos", "Id=" + usuario,"Descripcion"));
            else
                cnn.RegistrarBitacora("Perfil", "Se modifico " + perfil);
            */
            cnn.Dispose();
            return id;
        }

        private void AsignarValores(ref Conexion cnn, IDataReader Registro)
        {
            id = Convert.ToInt32(Registro["Id"].ToString());
            usuario = Convert.ToInt32(Convert.IsDBNull(Registro["Usuario"]) ? 0 : Registro["Usuario"]);
            perfil = Convert.ToInt32(Convert.IsDBNull(Registro["Perfil"]) ? 0 : Registro["Perfil"]);
            descPerfil = ""; if (Perfil > 0) descPerfil = cnn.DameValor("SegCatalogos", "Id=" + Perfil);
            conVigencia = Convert.ToByte(Convert.IsDBNull(Registro["ConVigencia"]) ? 0 : Registro["ConVigencia"]);
            inicioVigencia = Convert.IsDBNull(Registro["InicioVigencia"]) ? "" : Convert.ToDateTime(Registro["InicioVigencia"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            finVigencia = Convert.IsDBNull(Registro["FinVigencia"]) ? "" : Convert.ToDateTime(Registro["FinVigencia"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            orden = Convert.ToInt32(Convert.IsDBNull(Registro["Orden"]) ? 0 : Registro["Orden"]);
        }

        public bool ConsultarPorId(int vId)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Id=" + vId);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(ref cnn, Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }
        public bool ConsultarPorUsuario(int vUsuario, int vPerfil)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Usuario=" + vUsuario + " and Perfil=" + vPerfil);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(ref cnn, Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public int ObtenerId(int vUsuario, int vPerfil)
        {
            var cnn = new Conexion();
            int Result;
            cnn.Conectar();
            Result = Helper.Val(cnn.DameValor(Tabla, "Usuario=" + vUsuario + " and Perfil=" + vPerfil, "Id"));
            cnn.Dispose();
            return Result;
        }

        public int ObtenerIdPerfil(int vUsuario)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            int Result = Helper.Val(cnn.DameValor(Tabla, "Usuario=" + vUsuario, "Perfil"));
            cnn.Dispose();
            return Result;
        }

        public void BorrarPorId(int vId)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec("DELETE FROM [" + Tabla + "] WHERE Id=" + vId);
            cnn.Dispose();
        }

        public int Consultar(byte TipoDeConsulta, int vUsuario, ref string strJSON)
        {
            int Count;
            var cnn = new Conexion();
            string strFiltro;
            string strConSql;

            //generar consulta SQL
            if (TipoDeConsulta == 0)
            {
                strFiltro = " WHERE d.Usuario=" + vUsuario;
                strConSql = "SELECT d.Perfil, p.Descripcion as PerfilDesc, d.ConVigencia, d.InicioVigencia, d.FinVigencia, d.Orden" +
                            " FROM  SegCatalogosDetalle as d LEFT JOIN SegCatalogos as p ON d.Perfil = p.Id" +
                            strFiltro + " ORDER BY d.Orden, d.Perfil";
            }
            else
            {
                strFiltro = " WHERE d.Usuario<>" + vUsuario;
                strConSql = "SELECT d.Usuario, u.Codigo, d.Perfil, p.Descripcion" +
                            " FROM (SegCatalogos as u INNER JOIN SegCatalogosDetalle as d ON u.Id=d.Usuario) LEFT JOIN SegCatalogos as p ON d.Perfil=p.Id" +
                            strFiltro + " ORDER BY u.Codigo, d.Usuario, d.Perfil";
            }

            //obtener registros
            strJSON = "";
            Count = 0;
            cnn.Conectar();
            cnn.SQL = strConSql;
            IDataReader Registro = cnn.AbreSQL();
            if (cnn.Hay(Registro))
            {
                strJSON = Json.ReaderToJson(Registro, ref Count);
                Registro.Dispose();
            }
            cnn.Dispose();
            return Count;
        }

        public string ObtenerPerfil(int vUsuario, string vFecha)
        {
            var cnn = new Conexion();
            IDataReader vp_rs;
            string strFiltro;
            string strConSql;

            cnn.Conectar();
            strFiltro = "";
            strConSql = "SELECT Perfil FROM [" + Tabla + "] WHERE Usuario=" + vUsuario + " and (isnull(ConVigencia,0)=0 or (" + cnn.DateSQL(vFecha) + " between InicioVigencia and FinVigencia))";
            cnn.SQL = strConSql;
            vp_rs = cnn.AbreSQL();
            if (cnn.Hay(vp_rs))
            {
                while (vp_rs.Read())
                {
                    strFiltro = strFiltro + (strFiltro.Length > 0? " or ": " and (") + " Perfil=" + vp_rs["Perfil"].ToString(); 
                }
                if (strFiltro.Length > 0) strFiltro = strFiltro + ")";
            }
            vp_rs.Dispose();
            cnn.Dispose();
            return strFiltro;
        }
        //------------------------------------------------
        //Este procedimiento habilita las nuevas opciones
        //a usuarios con perfil de administrador.
        //------------------------------------------------
        public void TraspasarOpciones(int Usuario, int Perfil)
        {
            var cnn = new Conexion();
            IDataReader Registro;
            SegCatalogosRestricciones Restriccion = new SegCatalogosRestricciones();
            SegCatalogosOpciones Opcion = new SegCatalogosOpciones();
            string strConSql;
            try
            {
                cnn.Conectar();

                //Traspasar opciones
                strConSql = "SELECT Id,Tipo,Codigo,SoloLectura FROM SegCatalogosOpciones WHERE Usuario=0 and Perfil=" + Perfil + " ORDER BY Tipo, Catalogo, Cuenta";
                cnn.SQL = strConSql;
                Registro = cnn.AbreSQL();
                if (cnn.Hay(Registro))
                {
                    while (Registro.Read())
                    {
                        Opcion.Usuario = Usuario;
                        Opcion.Perfil = Perfil;
                        Opcion.Tipo = Convert.IsDBNull(Registro["Tipo"]) ? "" : Registro["Tipo"].ToString();
                        Opcion.Codigo = Convert.IsDBNull(Registro["Codigo"]) ? "" : Registro["Codigo"].ToString();
                        Opcion.SoloLectura = Convert.ToByte(Convert.IsDBNull(Registro["SoloLectura"]) ? 0 : Registro["SoloLectura"]);
                        Opcion.Guardar(true);
                    }
                    Registro.Dispose();
                }

                //Traspasar restricciones
                strConSql = "SELECT Id,Posicion,Tipo,Codigo,Cuenta,Catalogo,Padre,Descripcion,Valor,EsPred FROM SegCatalogosRestricciones WHERE Usuario=0 and Perfil=" + Perfil + " and Tipo<100 ORDER BY Tipo, Catalogo, Cuenta, Descripcion";
                cnn.SQL = strConSql;
                Registro = cnn.AbreSQL();
                if (cnn.Hay(Registro))
                {
                    while (Registro.Read())
                    {
                        Restriccion.Usuario = Usuario;
                        Restriccion.Perfil = Perfil;
                        Restriccion.Tipo = Convert.ToInt16(Convert.IsDBNull(Registro["Tipo"]) ? 0 : Registro["Tipo"]);
                        Restriccion.Catalogo = Convert.ToInt16(Convert.IsDBNull(Registro["Catalogo"]) ? 0 : Registro["Catalogo"]);
                        Restriccion.Codigo = Convert.ToInt32(Convert.IsDBNull(Registro["Codigo"]) ? 0 : Registro["Codigo"]);
                        Restriccion.Cuenta = Convert.IsDBNull(Registro["Cuenta"]) ? "" : Registro["Cuenta"].ToString();
                        Restriccion.Descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
                        Restriccion.Padre = Convert.ToInt32(Convert.IsDBNull(Registro["Padre"]) ? 0 : Registro["Padre"]);
                        Restriccion.Valor = Convert.ToInt16(Convert.IsDBNull(Registro["Valor"]) ? 0 : Registro["Valor"]);
                        Restriccion.EsPred = Convert.ToByte(Convert.IsDBNull(Registro["EsPred"]) ? 0 : Registro["EsPred"]);
                        Restriccion.Posicion = Convert.ToInt16(Convert.IsDBNull(Registro["Posicion"]) ? 0 : Registro["Posicion"]);
                        Restriccion.Guardar(true);
                    }
                    Registro.Dispose();
                }
                cnn.Dispose();
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
            }
        }
        //------------------------------------------------
        //Este procedimiento habilita las nuevas opciones
        //a usuarios con perfil de administrador.
        //------------------------------------------------
        public void HabilitarOpciones(string vTipo, string vCodigo)
        {
            var cnn = new Conexion();
            IDataReader Registro;
            int         vPerfil;
            string      strConSql;
            int         vId;
            string      c;
            string      v;
            try
            {
                vPerfil = -1;
                cnn.Conectar();

                //agregar opción a usuarios administrador
                strConSql = "SELECT d.Perfil, d.Usuario FROM SegCatalogosDetalle as d INNER JOIN SegCatalogos as p ON d.Perfil=p.Id WHERE p.Catalogo=3 and p.Nivel=1 ORDER BY d.Perfil, d.Usuario";
                cnn.SQL = strConSql;
                Registro = cnn.AbreSQL();
                if (cnn.Hay(Registro))
                {
                    while (Registro.Read())
                    {
                        if (vPerfil != (int)Helper.Val(Registro["Perfil"].ToString()))
                        {
                            //agregar opción al perfil
                            vId = Helper.Val(cnn.DameValor("SegCatalogosOpciones", "Usuario=0 and Perfil=" + Registro["Perfil"].ToString() + " and Tipo=" + Helper.strSql(vTipo) + " and Codigo=" + Helper.strSql(vCodigo), "Id"));
                            c = "Id,0,9|Usuario|Perfil|Tipo,1|Codigo,1|SoloLectura";
                            v = vId + "|0|" + Registro["Perfil"].ToString() + "|" + vTipo + "|" + vCodigo + "|0";
                            cnn.GuardarRegistro("SegCatalogosOpciones", c, v);
                            vPerfil = (int)Helper.Val(Registro["Perfil"].ToString());
                        }

                        //agregar opción al usuario
                        vId = Helper.Val(cnn.DameValor("SegCatalogosOpciones", "Usuario=" + Registro["Usuario"].ToString() + " and Perfil=" + Registro["Perfil"].ToString() + " and Tipo=" + Helper.strSql(vTipo) + " and Codigo=" + Helper.strSql(vCodigo), "Id"));
                        c = "Id,0,9|Usuario|Perfil|Tipo,1|Codigo,1|SoloLectura";
                        v = vId + "|" + Registro["Usuario"].ToString() + "|" + Registro["Perfil"].ToString() + "|" + vTipo + "|" + vCodigo + "|0";
                        cnn.GuardarRegistro("SegCatalogosOpciones", c, v);
                    }
                    Registro.Dispose();
                }
                cnn.Dispose();
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
            }
        }

    }
}
