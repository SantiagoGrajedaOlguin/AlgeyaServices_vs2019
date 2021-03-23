using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class CuentaBancaria
    {
        int     id;
        byte    origen;
        int     idOrigen;
        string  descripcion;
        short   moneda;
        string  monedaDesc;
        int     banco;
        string  bancoDesc;
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
        public string MonedaDesc { get => monedaDesc; set => monedaDesc = value; }
        public int Banco { get => banco; set => banco = value; }
        public string BancoDesc { get => bancoDesc; set => bancoDesc = value; }
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
    }
}
