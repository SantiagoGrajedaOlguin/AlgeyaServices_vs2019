using System;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal
{
    public class AccesoDirecto
    {
        int    _Id;
        string _Descripcion;
        byte   _Estatus;
        byte   _Proveedor;
        string _Servidor;
        byte   _Autentificacion;
        string _Usuario;
        string _Password;
        string _BaseDeDatos;
        int    _TiempoDeEspera;
        int    _Cursor;
        string _CadenaDeConexion;
        string _RutaUltimaVersion;
        bool   _EsTactil;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public byte Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        public byte Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }

        public string Servidor
        {
            get { return _Servidor; }
            set { _Servidor = value; }
        }

        public byte Autentificacion
        {
            get { return _Autentificacion; }
            set { _Autentificacion = value; }
        }

        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string BaseDeDatos
        {
            get { return _BaseDeDatos; }
            set { _BaseDeDatos = value; }
        }

        public int TiempoDeEspera
        {
            get { return _TiempoDeEspera; }
            set { _TiempoDeEspera = value; }
        }

        public int Cursor
        {
            get { return _Cursor; }
            set { _Cursor = value; }
        }

        public string CadenaDeConexion
        {
            get { return _CadenaDeConexion; }
            set { _CadenaDeConexion = value; }
        }

        public string RutaUltimaVersion
        {
            get { return _RutaUltimaVersion; }
            set { _RutaUltimaVersion = value; }
        }

        public bool EsTactil
        {
            get { return _EsTactil; }
            set { _EsTactil = value; }
        }

        public string GetCadenaDeConexion()
        {
            string Result;
            if (_Proveedor == 1)
            {
                if (_Usuario.Length > 0)
                    Result = "provider=SQLOLEDB;Persist Security Info=false;Data Source=" + _Servidor + ";Initial Catalog=" + _BaseDeDatos + ";uid=" + _Usuario + ";pwd=" + _Password;
                else
                    Result = "provider=SQLOLEDB;Persist Security Info=false;Integrated Security=SSPI;Data Source=" + _Servidor + ";Initial Catalog=" + _BaseDeDatos + ";";
            }
            else
            {
                Result = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _BaseDeDatos + ";";
            }
            return Result;
        }
    }
}
