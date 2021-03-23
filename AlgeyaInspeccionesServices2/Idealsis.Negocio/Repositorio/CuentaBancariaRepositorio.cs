using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Idealsis.Dal;
using Idealsis.Dal.Mapeo;
using Newtonsoft.Json.Linq;

namespace Idealsis.Negocio.Repositorio
{
    public class CuentaBancariaRepositorio
    {
        CatCuentasBancarias Catalogo;

        public CuentaBancariaRepositorio()
        {
            Catalogo = new CatCuentasBancarias();
        }

        public int Guardar(CuentaBancaria entidad)
        {
            int Id = entidad.Id;
            Catalogo.Id = Id;
            Catalogo.Origen = entidad.Origen;
            Catalogo.IdOrigen = entidad.IdOrigen;
            Catalogo.Descripcion = entidad.Descripcion;
            Catalogo.Moneda = entidad.Moneda;
            Catalogo.Banco = entidad.Banco;
            Catalogo.NoCuenta = entidad.NoCuenta;
            Catalogo.NoSuc = entidad.NoSuc;
            Catalogo.Clabe = entidad.Clabe;
            Catalogo.NoTarjeta = entidad.NoTarjeta;
            Catalogo.Sucursal = entidad.Sucursal;
            Catalogo.FechaAlta = entidad.FechaAlta;
            Catalogo.Activa = entidad.Activa;
            Catalogo.ManejaChequera = entidad.ManejaChequera;
            Catalogo.EsCaja = entidad.EsCaja;
            Catalogo.EsConcentradora = entidad.EsConcentradora;
            Catalogo.FolioCheque = entidad.FolioCheque;
            Catalogo.FechaDeCorte = entidad.FechaDeCorte;
            Catalogo.Depositos = entidad.Depositos;
            Catalogo.Retiros = entidad.Retiros;
            Catalogo.Saldo = entidad.Saldo;
            Catalogo.DepositosEnTransito = entidad.DepositosEnTransito;
            Catalogo.RetirosEnTransito = entidad.RetirosEnTransito;
            Catalogo.SaldoFinal = entidad.SaldoFinal;
            Catalogo.EsPred = entidad.EsPred;
            Id = Catalogo.Guardar(false);
            return Id;
        }
        public bool Remove(int Id)
        {
            return Catalogo.BorrarPorId(Id);
        }
        public List<CuentaBancaria> GetAll(byte Origen, int IdOrigen)
        {
            List<CuentaBancaria> Lista = new List<CuentaBancaria>();
            DataTable Datos = Catalogo.ListarTabla(Origen, IdOrigen);
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new CuentaBancaria()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Origen = Convert.ToByte(Convert.IsDBNull(row["Origen"]) ? 0 : row["Origen"]),
                        IdOrigen = Convert.ToInt16(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Moneda = Convert.ToInt16(Convert.IsDBNull(row["Moneda"]) ? 0 : row["Moneda"]),
                        MonedaDesc = Convert.IsDBNull(row["MonedaDesc"]) ? "" : row["MonedaDesc"].ToString(),
                        Banco = Convert.ToInt32(Convert.IsDBNull(row["Banco"]) ? 0 : row["Banco"]),
                        BancoDesc = Convert.IsDBNull(row["BancoDesc"]) ? "" : row["BancoDesc"].ToString(),
                        NoCuenta = Convert.IsDBNull(row["NoCuenta"]) ? "" : row["NoCuenta"].ToString(),
                        NoSuc = Convert.IsDBNull(row["NoSuc"]) ? "" : row["NoSuc"].ToString(),
                        Clabe = Convert.IsDBNull(row["Clabe"]) ? "" : row["Clabe"].ToString(),
                        NoTarjeta = Convert.IsDBNull(row["NoTarjeta"]) ? "" : row["NoTarjeta"].ToString(),
                        Sucursal = Convert.ToInt16(Convert.IsDBNull(row["Sucursal"]) ? 0 : row["Sucursal"]),
                        FechaAlta = Convert.IsDBNull(row["FechaAlta"]) ? "" : Convert.ToDateTime(row["FechaAlta"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Activa = Convert.ToByte(Convert.IsDBNull(row["Activa"]) ? 0 : row["Activa"]),
                        ManejaChequera = Convert.ToByte(Convert.IsDBNull(row["ManejaChequera"]) ? 0 : row["ManejaChequera"]),
                        EsCaja = Convert.ToByte(Convert.IsDBNull(row["EsCaja"]) ? 0 : row["EsCaja"]),
                        EsConcentradora = Convert.ToByte(Convert.IsDBNull(row["EsConcentradora"]) ? 0 : row["EsConcentradora"]),
                        FolioCheque = Convert.ToInt32(Convert.IsDBNull(row["FolioCheque"]) ? 0 : row["FolioCheque"]),
                        FechaDeCorte = Convert.IsDBNull(row["FechaDeCorte"]) ? "" : Convert.ToDateTime(row["FechaDeCorte"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Depositos = Convert.ToDecimal(Convert.IsDBNull(row["Depositos"]) ? 0 : row["Depositos"]),
                        Retiros = Convert.ToDecimal(Convert.IsDBNull(row["Retiros"]) ? 0 : row["Retiros"]),
                        Saldo = Convert.ToDecimal(Convert.IsDBNull(row["Saldo"]) ? 0 : row["Saldo"]),
                        DepositosEnTransito = Convert.ToDecimal(Convert.IsDBNull(row["DepositosEnTransito"]) ? 0 : row["DepositosEnTransito"]),
                        RetirosEnTransito = Convert.ToDecimal(Convert.IsDBNull(row["RetirosEnTransito"]) ? 0 : row["RetirosEnTransito"]),
                        SaldoFinal = Convert.ToDecimal(Convert.IsDBNull(row["SaldoFinal"]) ? 0 : row["SaldoFinal"]),
                        EsPred = Convert.ToByte(Convert.IsDBNull(row["EsPred"]) ? 0 : row["EsPred"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
