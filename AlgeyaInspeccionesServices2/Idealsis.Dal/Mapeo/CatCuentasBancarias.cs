
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Windows.Forms;

namespace Idealsis.Dal.Mapeo
{
    public class CatCuentasBancarias
    {
        const string Tabla = "CatCuentasBancarias";

        int     id;
        byte    origen;
        int     idOrigen;
        string  descripcion;
        short   moneda;
        int     banco;
        string  noCuenta;
        string  noSuc;
        string  clabe;
        string  noTarjeta;
        short   sucursal;
        string  fechaAlta;
        byte    activa;
        byte    manejaChequera;
        byte    esCaja;
        byte    esConcentradora;
        int     folioCheque;
        string  fechaDeCorte;
        decimal depositos;
        decimal retiros;
        decimal saldo;
        decimal depositosEnTransito;
        decimal retirosEnTransito;
        decimal saldoFinal;
        byte    esPred;

        public int Id { get => id; set => id = value; }
        public byte Origen { get => origen; set => origen = value; }
        public int IdOrigen { get => idOrigen; set => idOrigen = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public short Moneda { get => moneda; set => moneda = value; }
        public int Banco { get => banco; set => banco = value; }
        public string NoCuenta { get => noCuenta; set => noCuenta = value; }
        public string NoSuc { get => noSuc; set => noSuc = value; }
        public string Clabe { get => clabe; set => clabe = value; }
        public string NoTarjeta { get => noTarjeta; set => noTarjeta = value; }
        public short Sucursal { get => sucursal; set => sucursal = value; }
        public string FechaAlta { get => fechaAlta; set => fechaAlta = value; }
        public byte Activa { get => activa; set => activa = value; }
        public byte ManejaChequera { get => manejaChequera; set => manejaChequera = value; }
        public byte EsCaja { get => esCaja; set => esCaja = value; }
        public byte EsConcentradora { get => esConcentradora; set => esConcentradora = value; }
        public int FolioCheque { get => folioCheque; set => folioCheque = value; }
        public string FechaDeCorte { get => fechaDeCorte; set => fechaDeCorte = value; }
        public decimal Depositos { get => depositos; set => depositos = value; }
        public decimal Retiros { get => retiros; set => retiros = value; }
        public decimal Saldo { get => saldo; set => saldo = value; }
        public decimal DepositosEnTransito { get => depositosEnTransito; set => depositosEnTransito = value; }
        public decimal RetirosEnTransito { get => retirosEnTransito; set => retirosEnTransito = value; }
        public decimal SaldoFinal { get => saldoFinal; set => saldoFinal = value; }
        public byte EsPred { get => esPred; set => esPred = value; }

        public int Guardar(bool Buscar)
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();

            if (Buscar) id = Helper.Val(cnn.DameValor(Tabla, "Origen=" + Origen + " and IdOrigen=" + IdOrigen, "Id"));

            c = "Id,0,9|Origen|IdOrigen|Descripcion,1|Moneda|Banco|NoCuenta,1|NoSuc,1|Clabe,1|NoTarjeta,1|Sucursal|FechaAlta,2|Activa|";
            c = c + "ManejaChequera|EsCaja|EsConcentradora|FolioCheque|FechaDeCorte,2|";
            c = c + "Depositos|Retiros|Saldo|DepositosEnTransito|RetirosEnTransito|SaldoFinal|EsPred";

            v = id + "|" + origen + "|" + idOrigen  + "|" + descripcion + "|" + moneda + "|" + banco + "|" + noCuenta + "|" + noSuc + "|" + clabe + "|" + noTarjeta + "|" + sucursal + "|" + fechaAlta + "|" + activa + "|";
            v = v + manejaChequera + "|" + esCaja + "|" + esConcentradora + "|" + folioCheque + "|" + fechaDeCorte + "|";
            v = v + depositos + "|" + retiros + "|" + saldo + "|" + depositosEnTransito + "|" + retirosEnTransito + "|" + saldoFinal + "|" + esPred;

            id = cnn.GuardarRegistro(Tabla, c, v);

            cnn.Dispose();
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Convert.IsDBNull(Registro["Id"]) ? 0 : Registro["Id"]);
            origen = Convert.ToByte(Convert.IsDBNull(Registro["Origen"]) ? 0 : Registro["Origen"]);
            idOrigen = Convert.ToInt32(Convert.IsDBNull(Registro["IdOrigen"]) ? 0 : Registro["IdOrigen"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            moneda = Convert.ToInt16(Convert.IsDBNull(Registro["Moneda"]) ? 0 : Registro["Moneda"]);
            banco = Convert.ToInt32(Convert.IsDBNull(Registro["Banco"]) ? 0 : Registro["Banco"]);
            noCuenta = Convert.IsDBNull(Registro["NoCuenta"]) ? "" : Registro["NoCuenta"].ToString();
            noSuc = Convert.IsDBNull(Registro["NoSuc"]) ? "" : Registro["NoSuc"].ToString();
            clabe = Convert.IsDBNull(Registro["Clabe"]) ? "" : Registro["Clabe"].ToString();
            noTarjeta = Convert.IsDBNull(Registro["NoTarjeta"]) ? "" : Registro["NoTarjeta"].ToString();
            sucursal = Convert.ToInt16(Convert.IsDBNull(Registro["Sucursal"]) ? 0 : Registro["Sucursal"]);
            fechaAlta = Convert.IsDBNull(Registro["FechaAlta"]) ? "" : Convert.ToDateTime(Registro["FechaAlta"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            activa = Convert.ToByte(Convert.IsDBNull(Registro["Activa"]) ? 0 : Registro["Activa"]);
            manejaChequera = Convert.ToByte(Convert.IsDBNull(Registro["ManejaChequera"]) ? 0 : Registro["ManejaChequera"]);
            esCaja = Convert.ToByte(Convert.IsDBNull(Registro["EsCaja"]) ? 0 : Registro["EsCaja"]);
            esConcentradora = Convert.ToByte(Convert.IsDBNull(Registro["EsConcentradora"]) ? 0 : Registro["EsConcentradora"]);
            folioCheque = Convert.ToInt32(Convert.IsDBNull(Registro["FolioCheque"]) ? 0 : Registro["FolioCheque"]);
            fechaDeCorte = Convert.IsDBNull(Registro["FechaDeCorte"]) ? "" : Convert.ToDateTime(Registro["FechaDeCorte"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            depositos = Convert.ToDecimal(Convert.IsDBNull(Registro["Depositos"]) ? 0 : Registro["Depositos"]);
            Retiros = Convert.ToDecimal(Convert.IsDBNull(Registro["Retiros"]) ? 0 : Registro["Retiros"]);
            saldo = Convert.ToDecimal(Convert.IsDBNull(Registro["Saldo"]) ? 0 : Registro["Saldo"]);
            depositosEnTransito = Convert.ToDecimal(Convert.IsDBNull(Registro["DepositosEnTransito"]) ? 0 : Registro["DepositosEnTransito"]);
            retirosEnTransito = Convert.ToDecimal(Convert.IsDBNull(Registro["RetirosEnTransito"]) ? 0 : Registro["RetirosEnTransito"]);
            saldoFinal = Convert.ToDecimal(Convert.IsDBNull(Registro["SaldoFinal"]) ? 0 : Registro["SaldoFinal"]);
            esPred = Convert.ToByte(Convert.IsDBNull(Registro["EsPred"]) ? 0 : Registro["EsPred"]);
            Registro.Dispose();
        }

        public bool BuscarPorId(int vId)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Id=" + vId);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool BuscarPorOrigen(byte Origen, int IdOrigen)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Origen=" + Origen + " and IdOrigen=" + IdOrigen);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool BorrarPorId(int vId)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Id=" + vId, false);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable ListarTabla(byte Origen, int IdOrigen)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE d.Origen=" + Origen + " and d.IdOrigen=" + IdOrigen;
            strConsulta = "SELECT d.*,Bancos.Descripcion as BancoDesc,CatMonedas.Descripcion as MonedaDesc FROM ([" + Tabla + "] as d LEFT JOIN SysCatalogos as Bancos ON d.Banco=Bancos.Id) LEFT JOIN CatMonedas ON d.Moneda=CatMonedas.Codigo " + strFiltro + " ORDER BY d.Id";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

    }
}
