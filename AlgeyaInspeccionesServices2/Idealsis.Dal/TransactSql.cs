using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Dal
{


    class TransactSql
    {

        public string Validar(string SQL, int vp_prv)
        {

            string tmp;
            int p=0;       //posicion inicial
            int f=0;       //posicion final
            int i=0;       //posicion actual
            int l=0;       //longitud de cadena
            int t=0;       //temporal
            string parte;

            tmp = SQL;
            if (vp_prv == 0)  //Base de Datos de Access
            {

                tmp = tmp.Replace(" CHAR(", " CHR(");
                tmp = tmp.Replace("DBO.", "");
                tmp = tmp.Replace("[DBO].", "");
                tmp = tmp.Replace("ON [PRIMARY]", "");
                tmp = tmp.Replace("COLLATE SQL_LATIN1_GENERAL_CP1_CI_AS", "");
                tmp = tmp.Replace("NONCLUSTERED", "");
                tmp = tmp.Replace("\t", "");
                
                //validar sintaxis "DATEPART('w'
                f = 1;
                do
                {
                    p = tmp.IndexOf("DATEPART('w'", f);
                    if (p >= 0)
                    {
                        f = ParFin(p, tmp) + 1;
                        l = tmp.Length;
                        tmp = tmp.Substring(0, f - 2) + ",2" + tmp.Substring(f - 1, l - (f - 2));
                    }
                } while (p >= 0);



                //validar sintaxis "CASE WHEN"
                do
                {
                    p = tmp.IndexOf("CASE WHEN ", 0);
                    if (p >= 0)
                    {
                        f = tmp.IndexOf(" END", p) + 4;
                        p -= 1;
                        for (i = p; i >= 0; i--)
                        {
                            if (tmp.Substring(i, 1) != " ") { p = i; break; }
                        }
                        if (tmp.Substring(p, 1) == "=")
                        {
                            p -= 1;
                            for (i = p; i <= 0; i--)
                            {
                                if (tmp.Substring(i, 1) != " ") { p = i;  break; }
                            }
                            for (i = p; i <= 0; i--)
                            {
                                if (tmp.Substring(i, 1) == " ") { p = i;  break; }
                            }
                        }
                        else
                        {
                            p += 1;
                        }
                        if (f > p)
                        {
                            parte = tmp.Substring(p, f - p);
                            tmp = tmp.Replace(parte, CamExp(parte, 2));
                        }
                    }
                } while (p >= 0);
            }


            if (vp_prv == 1)  //Base de datos: SQL SERVER
            {

                //SELECT LEFT('00000' + CAST(factura AS VARCHAR(5)), 5) formateado FROM tu_tabla
                tmp = tmp.Replace("CHR(", "CHAR(");
                tmp = tmp.Replace("MID(", "SUBSTRING(");
                tmp = tmp.Replace("DELETE *", "DELETE");
                tmp = tmp.Replace("CSTR(", "STR(");
                tmp = tmp.Replace("DATEADD('D'", "DATEADD(DAY");
                tmp = tmp.Replace("DATEDIFF('D'", "DATEDIFF(DAY");
                tmp = tmp.Replace("CCUR(", "CONVERT(MONEY, ");
                tmp = tmp.Replace("LAST(", "MAX(");
                tmp = tmp.Replace("FIRST(", "MIN(");
                tmp = tmp.Replace("DATE()", "GETDATE()");
                tmp = tmp.Replace("DATEPART('w'", "DATEPART(dw");
                tmp = tmp.Replace("DATEPART('mm'", "DATEPART(mm");
                tmp = tmp.Replace("DATEPART('yyyy'", "DATEPART(yyyy");
                tmp = tmp.Replace("TRIM(", "RTRIM(");
                tmp = tmp.Replace("LRTRIM(", "LTRIM(");
                tmp = tmp.Replace("RRTRIM(", "RTRIM(");
                //validar sintaxis "IIF"

                do
                { 
                    p = tmp.IndexOf("IIF(", 0);
                    if (p >= 0)
                    {
                        f = ParFin(p, tmp) + 1;
                        l = tmp.Length;
                        t = AsIni(f, tmp, l);
                        if (t >= 0)
                        {
                            f = t;
                            for (i = f; i < l; i++)
                            {
                                if (tmp.Substring(i, 1) != " ")
                                { 
                                    f = i; break;
                                }
                            }
                            for (i = f; i < l; i++)
                            {
                                if (tmp.Substring(i, 1) == " " || tmp.Substring(i, 1) == ",")
                                {
                                    f = i; break;
                                }
                            }
                        }
                        if (f > p)
                        {
                            parte = tmp.Substring(p, f - p);
                            tmp = tmp.Replace(parte, CamExp(parte, 1)+1);
                        }
                    }
                } while (p >= 0) ;


                //validar sintaxis "FORMAT"
                do
                {
                    p = tmp.IndexOf("FORMAT(", 0);
                    if (p >= 0)
                    {
                        f = ParFin(p, tmp) + 1;
                        l = tmp.Length;
                        t = AsIni(f, tmp, l);
                        if (t >= 0)
                        {
                            f = t;
                            for (i = f; i < l; i++)
                            {
                                if (tmp.Substring(i, 1) != " ") { f = i; break; }
                            }
                            for (i = f; i < l; i++)
                            {
                                if (tmp.Substring(i, 1) == " " || tmp.Substring(i, 1) == ",") { f = i; break; }
                            }
                        }
                        if (f > p)
                        {
                            parte = tmp.Substring(p, f - p);
                            tmp = tmp.Replace(parte, CamExp(parte, 3));
                        }
                    }
                } while (p >= 0);


            }
            return tmp;

        }

        private string CamExp(string exp, int tpo_exp)
        {

            string cpo;   //campo o expresion
            string fto;   //formato
            int p;
            int pi;
            string tmp = "";

            //Debug.Print exp
            tmp = "";
            switch (tpo_exp)
            {
                case 1: //IIF por CASE WHEN

                    pi = ComIni(1, exp); //primera separacion
                    tmp = exp.Substring(0, pi - 1) + Helper.ReplaceVb6(exp, ",", " THEN ", pi, 1);

                    pi = ComIni(1, tmp); //segunda separación
                    tmp = tmp.Substring(0, pi - 1) + Helper.ReplaceVb6(tmp, ",", " ELSE ", pi, 1);
                    tmp = Helper.ReplaceVb6(tmp, "IIF(", "CASE WHEN ", 0, 1);
                    tmp = (string)Helper.ReplaceVb6((string)tmp.Reverse(), ")", " ", 0, 1).Reverse();
                    p = tmp.IndexOf(" AS ");
                    if (p >= 0) tmp = tmp.Substring(p + 3, tmp.Length - p + 3).Trim() + " = " + tmp.Substring(0, p - 1).Trim();
                    tmp = tmp + " END";
                    break;


                case 2: //CASE WHEN por IIF


                    tmp = Helper.ReplaceVb6(exp, "CASE WHEN ", "IIF( ", 0, 1);
                    tmp = Helper.ReplaceVb6(tmp, " THEN ", " , ", 0, 1);
                    tmp = Helper.ReplaceVb6(tmp, " ELSE ", " , ", 0, 1);
                    tmp = Helper.ReplaceVb6(tmp, " END", " )", 0, 1);

                    p = tmp.IndexOf("=");
                    if (p >= 0)
                    {
                        if (p < tmp.IndexOf("IIF( ")) tmp = tmp.Substring(p + 1, tmp.Length - p + 1).Trim() + " AS " + tmp.Substring(1, p - 1).Trim();
                    }
                    break;


                case 3: //FORMAT por CAST


                    fto = CadFor(exp);
                    cpo = CadCpo(exp);
                    if (fto == "mm/yyyy")
                    {
                        tmp = "CAST(DATEPART(mm," + cpo + ") AS VARCHAR) + '/' + CAST(DATEPART(yyyy," + cpo + ") AS VARCHAR)";
                    }
                    else if (Helper.isNumeric(fto) && Convert.ToInt16(fto) == 0)
                    {
                        tmp = "RIGHT('" + fto + "' + CAST(" + cpo + " AS VARCHAR), " + fto.Length + ")";
                    }

                    p = exp.IndexOf(" AS ");
                    if (p >= 0) tmp = tmp + " AS " + exp.Substring(p + 3, exp.Length - p + 3).Trim();
                    break;

            }
            return tmp;

            //usar format en SQL server
            //SELECT LEFT('00000' + CAST(factura AS VARCHAR(5)), 5) formateado FROM tu_tabla

        }

        private int ParFin(int p, string cad)
        {
            int i;
            int pA = 0;
            int Pc = 0;
            int tam;
            int Result = 0;

            tam = cad.Length;
            for (i = p; i < tam; i++)
            {
                if (cad.Substring(i, 1) == "(") pA += 1;
                if (cad.Substring(i, 1) == ")")
                {
                    Pc += 1;
                    if (Pc == pA)
                    {
                        Result = i;
                        break;
                    }
                }
            }
            return Result;
        }

        private string CadFor(string cad)
        {
            int i;
            int f;
            string Result = "";

            i = cad.IndexOf("'", 0);
            if (i >= 0)
            {
                f = cad.IndexOf("'", i + 1);
                if (f >= 0) Result = cad.Substring(i + 1, (f - i)).Trim();
            }
            return Result;
        }
        private string CadCpo(string cad)
        {
            int i;
            int f;
            string Result = "";

            i = cad.IndexOf("(", 0);
            if (i >= 0)
            {
                f = cad.IndexOf(",", i + 1);
                if (f >= 0) Result = cad.Substring(i + 1, (f - i)).Trim();
            }
            return Result;
        }

        //obtener posicion de coma "," inicial
        private int ComIni(int p, string cad)
        {

            int i;
            int tam;
            int pA = 0;
            int Pc = 0;
            int ps = 0;
            int Result = 0;

            tam = cad.Length;
            for (i = p; i < tam; i++)
            {

                if (cad.Substring(i, 1) == "(") pA += 1;
                if (cad.Substring(i, 1) == ")") Pc += 1;
                if (cad.Substring(i, 1) == "'") ps += 1;
                if (cad.Substring(i, 1) == ",")
                {
                    if ((pA - Pc) == 1 && (ps % 2) == 0)
                    {
                        Result = i;
                        break;
                    }
                }
            }
            return Result;
        }

        //obtener posicion de coma "," inicial
        private int AsIni(int p, string cad, int tam)
        {

            int i;
            byte E = 0;
            byte a = 0;
            byte s = 0;
            int Result = 0;

            for (i = p; i < tam; i++)
            {
                switch (cad.Substring(i, 1))
                {
                    case " ":
                        if (E == 0) E = 1;
                        break;
                    case "A":
                        if (E > 0) a = 1; else return Result;
                        break;
                    case "S":
                        if (a > 0) s = 1; else return Result;
                        break;
                    default: return Result;
                }
                if (s > 0)
                {
                    Result = i + 1;
                    return Result;
                }
            }
            return Result;
        }

    }
}
