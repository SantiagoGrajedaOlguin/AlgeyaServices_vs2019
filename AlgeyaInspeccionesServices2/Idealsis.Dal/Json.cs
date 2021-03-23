using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace Idealsis.Dal
{
    public class Json
    {
        private static string psErrors;

        public static string GetParserErrors()
        {
            return psErrors;
        }

        public static void ClearParserErrors()
        {
            psErrors = "";
        }

        //
        //   parse string and create JSON object
        //
        public static object parse(string str)
        {
            int Index = 0;
            psErrors = "";
            try
            {
                skipChar(ref str, ref Index);
                switch (str.Substring(Index, 1))
                {
                    case "{":
                        return parseObject(ref str, ref Index);
                    //break;
                    //Debug.Print str
                    case "[":
                        //return parseArray(str, Index);
                        return null;
                    //break;
                    default:
                        psErrors = "Invalid JSON";
                        return null;
                        //break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private static void skipChar(ref string str, ref int Index)
        {
            bool bComment = false;
            bool bStartComment = false;
            bool bLongComment = false;
            bool Salir = false;
            while (Index >= 0 && Index < str.Length && !Salir)
            {
                switch (str.Substring(Index, 1))
                {
                    case "\n":
                    case "\r":
                        if (!bLongComment)
                        {
                            bStartComment = false;
                            bComment = false;
                        }
                        break;

                    case "\t":
                    case " ":
                    case "(":
                    case ")":
                        break;

                    case "/":
                        if (!bLongComment)
                        {
                            if (bStartComment)
                            {
                                bStartComment = false;
                                bComment = true;
                            }
                            else
                            {
                                bStartComment = true;
                                bComment = false;
                                bLongComment = false;
                            }
                        }
                        else
                        {
                            if (bStartComment)
                            {
                                bLongComment = false;
                                bStartComment = false;
                                bComment = false;
                            }
                        }
                        break;

                    case "*":
                        if (bStartComment)
                        {
                            bStartComment = false;
                            bComment = true;
                            bLongComment = true;
                        }
                        else
                            bStartComment = true;
                        break;

                    default:
                        if (!bComment) Salir = true;
                        break;
                }
                Index += 1;
            }
        }

        private static Dictionary<string, object> parseObject(ref string str, ref int Index)
        {

            var Result = new Dictionary<string, object>();
            bool Salir = false;
            string sKey;

            // "{"
            skipChar(ref str, ref Index);
            if (str.Substring(Index, 1) != "{")
            {
                psErrors = psErrors + "Invalid Object at position " + Index.ToString() + " : " + str.Substring(Index) + "\n\r";
                return Result;
            }

            Index += 1;

            do
            {
                skipChar(ref str, ref Index);
                if (str.Substring(Index, 1) == "}")
                {
                    Index += 1;
                    break;
                }
                else if (str.Substring(Index, 1) == ",")
                {
                    Index += 1;
                    skipChar(ref str, ref Index);
                }
                else if (Index >= str.Length)
                {
                    psErrors = psErrors + "Missing '}': " + str.Substring(20) + "\n\r";
                    break;
                }


                // add key/value pair
                sKey = parseKey(ref str, ref Index);
                try
                {
                    Result.Add(sKey, parseValue(ref str, ref Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    psErrors = psErrors + ex.Message + ": " + sKey + "\n\r";
                    Salir = true;
                }

            } while (!Salir);
            return Result;
        }

        private static object parseValue(ref string str, ref int Index)
        {
            object Result=null;
            skipChar(ref str, ref Index);
            switch (str.Substring(Index, 1))
            {
                case "{":
                    Result = parseObject(ref str, ref Index);
                    break;
                case "[":
                    Result = parseArray(ref str, ref Index);
                    break;
                case "\"":
                case "'":
                    Result = parseString(ref str, ref Index);
                    break;
                case "t":
                case "f":
                    Result = parseBoolean(ref str, ref Index);
                    break;
                case "n":
                    Result = parseNull(ref str, ref Index);
                    break;
                default:
                    Result = parseNumber(ref str, ref Index);
                    break;
            }
            return Result;
        }

        private static string parseString(ref string str, ref int Index)
        {

            string quote;
            string Char;
            string Code;
            var SB = new System.Text.StringBuilder("");
            bool Salir = false;

            skipChar(ref str, ref Index);
            quote = str.Substring(Index, 1);
            Index += 1;

            while (Index >= 0 && Index < str.Length && !Salir)
            {
                Char = str.Substring(Index, 1);
                if (Char == "\\")
                {
                    Index += 1;
                    Char = str.Substring(Index, 1);
                    switch (Char)
                    {
                        case "\"":
                        case "\\":
                        case "/":
                        case "'":
                            SB.Append(Char);
                            Index += 1;
                            break;
                        case "b":
                            SB.Append("\b");
                            Index += 1;
                            break;
                        case "f":
                            SB.Append("\f");
                            Index += 1;
                            break;
                        case "n":
                            SB.Append("\n");
                            Index += 1;
                            break;
                        case "r":
                            SB.Append("\r");
                            Index += 1;
                            break;
                        case "t":
                            SB.Append("\t");
                            Index += 1;
                            break;

                        case "u":
                            Index += 1;
                            Code = str.Substring(Index, 4);
                            SB.Append("\\" + "u" + Code);
                            //SB.Append(ChrW(Val("+h" + Code)));
                            Index = Index + 4;
                            break;
                    }
                }
                else if (Char == quote)
                {
                    Index += 1;
                    return SB.ToString();
                    //SB = null;
                }
                else
                { 
                        SB.Append(Char);
                        Index += 1;
                        break;
                }
           }

           return SB.ToString();
            //SB = null;

        }

        //
        //   parse true / false
        //
        private static bool parseBoolean(ref string str, ref int Index)
        {
            bool Result = false;
            skipChar(ref str, ref Index);
            if (str.Substring(Index, 4) == "true")
            {
                Result = true;
                Index = Index + 4;
            }
            else if (str.Substring(Index, 5) == "false")
            {
                Result = false;
                Index = Index + 5;
            }
            else
            {
                psErrors = psErrors + "Invalid Boolean at position " + Index.ToString() + " : " + str.Substring(Index) + "\n\r";
            }
            return Result;

        }

        //
        //   parse null
        //
        private static object parseNull(ref string str, ref int Index)
        {

            skipChar(ref str, ref Index);
            if (str.Substring(Index, 4) == "null")
            {
                Index = Index + 4;
                return null;
            }
            else
            {
                psErrors = psErrors + "Invalid null value at position " + Index.ToString() + " : " + str.Substring(Index) + "\n\r";
                return "";
            }

        }

        //
        //   parse number
        //
        private static object parseNumber(ref string str, ref int Index)
        {

            string Value="";
            string Char;
            bool Salir = false;

            skipChar(ref str, ref Index);
            while (Index >= 0 && Index < str.Length && !Salir)
            {
                Char = str.Substring(Index, 1);
                if ("+-0123456789.eE".IndexOf(Char) >= 0)
                {
                    Value = Value + Char;
                    Index += 1;
                }
                else
                {
                    Salir = true;
                }
            }
            return Convert.ToDecimal(Value);
        }

        //
        //   parse list
        //
        private static List<string> parseArray(ref string str, ref int Index)
        {

            var Result = new List<string>();
            bool Salir = false;

            // "["
            skipChar(ref str, ref Index);
            if (str.Substring(Index, 1) != "[")
            {
                psErrors = psErrors + "Invalid Array at position " + Index.ToString() + " : " + str.Substring(Index) + "\n\r";
                return Result;
            }

            Index += 1;
            do
            {

                skipChar(ref str, ref Index);
                if (str.Substring(Index, 1) == "]")
                {
                    Index += 1;
                    break;
                }
                else if (str.Substring(Index, 1) == ",")
                {
                    Index += 1;
                    skipChar(ref str, ref Index);
                }
                else if (Index >= str.Length)
                {
                    psErrors = psErrors + "Missing ']': " + "\n\r";
                    break;
                }

                // add value
                try
                {
                    Result.Add((string)parseValue(ref str, ref Index));
                }
                catch (Exception ex)
                {
                    psErrors = psErrors + ex.Message + "\n\r";
                }

            } while (!Salir);
            return Result;
        }

        public static string RStoJSON(IDataReader rs, ref int lRecCnt)
        {
            string sFlds;
            var sRecs = new System.Text.StringBuilder("");
            var sNams = new System.Text.StringBuilder("");
            int lFldCnt;
            int i = 0;
            try
            {

                lRecCnt = 0;
                lFldCnt = 0;
                if (rs.IsClosed)
                    return "null";
                else
                {
                    if (rs.IsClosed)
                    {
                        return "null";
                    }
                    else
                    {
                        while (rs.Read())
                        {

                            lRecCnt += 1;
                            sFlds = "";
                            for (i = 0; i < rs.FieldCount; i++)
                            {
                                sFlds = (sFlds + (sFlds != "" ? "," : "") + "\"" + rs.GetName(i).ToString() + "\":\"" + Helper.ValidarJValor(rs[i].ToString()) + "\"");
                            }
                            sRecs.Append((sRecs.ToString().Trim() != "" ? "," + "\r\n" : "") + "{" + sFlds + "}");
                        }
                        for (i = 0; i < rs.FieldCount; i++)
                        {
                            lFldCnt += 1;
                            sNams.Append((sNams.ToString().Trim() != "" ? "," + "\r\n" : "") + "{" + "\"Name\":\"" + rs.GetName(i).ToString() + "\"}");
                        }
                        //return "( {\"Records\": [\r\n" + sRecs.ToString() + "\r\n], " + "\"RecordCount\":\"" + lRecCnt.ToString() + "\", \"Fields\": [\r\n" + sNams.ToString() + "\r\n], " + "\"FieldCount\":\"" + lFldCnt.ToString() + "\" } )";
                        return "{\"Records\": [\r\n" + sRecs.ToString() + "\r\n], " + "\"RecordCount\":\"" + lRecCnt.ToString() + "\", \"Fields\": [\r\n" + sNams.ToString() + "\r\n], " + "\"FieldCount\":\"" + lFldCnt.ToString() + "\" }";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "null";
            }

        }

        public static string ReaderToJson(IDataReader rs, ref int lRecCnt)
        {
            string sFlds;
            var sRecs = new System.Text.StringBuilder("");
            int i;
            try
            {

                lRecCnt = 0;
                if (rs.IsClosed)
                    return "null";
                else
                {
                    if (rs.IsClosed)
                    {
                        return "null";
                    }
                    else
                    {
                        while (rs.Read())
                        {
                            lRecCnt += 1;
                            sFlds = "";
                            for (i = 0; i < rs.FieldCount; i++)
                            {
                                sFlds = (sFlds + (sFlds != "" ? "," : "") + "\"" + rs.GetName(i).ToString() + "\":\"" + Helper.ValidarJValor(rs[i].ToString()) + "\"");
                            }
                            sRecs.Append((sRecs.ToString().Trim() != "" ? "," + "\r\n" : "") + "{" + sFlds + "}");
                        }
                        return "[\r\n" + sRecs.ToString() + "\r\n]";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "null";
            }

        }

        private static string parseKey(ref string str, ref int Index)
        {
            bool   dquote=false;
            bool   squote=false;
            bool   Salir = false;
            string Result = "";
            string Char;

            skipChar(ref str, ref Index);
            while (Index >= 0 && Index < str.Length && !Salir)
            {
                Char = str.Substring(Index, 1);
                switch (Char)
                {
                    case "\"":
                        dquote = !dquote;
                        Index += 1;
                        if (!dquote)
                        {
                            skipChar(ref str, ref Index);
                            if (str.Substring(Index, 1) != ":")
                            { 
                                psErrors = psErrors + "Invalid Key at position " + Index.ToString() + " : " + Result + "\n\r";
                                Salir = true;
                            }
                        }
                        break;

                    case "'":
                        squote = !squote;
                        Index += 1;
                        if (!squote)
                        {
                            skipChar(ref str, ref Index);
                            if (str.Substring(Index, 1) != ":")
                            { 
                                psErrors = psErrors + "Invalid Key at position " + Index.ToString() + " : " + Result + "\n\r";
                                Salir = true;
                            }
                        }
                        break;
                 case ":":
                        Index += 1;
                        if (!dquote && !squote)
                            Salir = true;
                        else
                            Result = Result + Char;
                        break;
                 default:
                        if (!("\n\r\t".IndexOf(Char)>=0)) Result = Result + Char;
                        Index += 1;
                        break;
                }
            }
            return Result;
        }

    }
}
