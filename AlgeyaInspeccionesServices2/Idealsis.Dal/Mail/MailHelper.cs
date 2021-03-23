using CDO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Idealsis.Dal.Mail
{
    public class MailHelper
    {
        String MailAddressPattern = @"[\w!#$%+'*+/=?^_`{|}~-]+(?:\.[\w!#$%+'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?";//邮件地址正则
        String IpPattern = @"(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)";//Ip地址正则
        String PhonePattern = @"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)";//电话号码正则
        String MailNumPattern = @"[1-9]\d{5}(?!\d)";
        String IDCardNumPattern = @"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$";
        String QQNumPattern = @"[1-9][0-9]{4,}";
        String ContainerNumPattern = @"[A-Z]{3}U\s{0,1}\d{7}";

        /// <summary>
        ///
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public Message ResolveMailByLSoft(String FilePath)
        {
            Message oMsg = new Message();
            ADODB.Stream stm = null;
            try
            {
                stm = new ADODB.Stream();
                stm.Open(System.Reflection.Missing.Value,
                ADODB.ConnectModeEnum.adModeUnknown,
                ADODB.StreamOpenOptionsEnum.adOpenStreamUnspecified, "", "");
                stm.Type = ADODB.StreamTypeEnum.adTypeBinary;//二进制方式读入

                stm.LoadFromFile(FilePath); //将EML读入数据流

                oMsg.DataSource.OpenObject(stm, "_stream"); //将EML数据流载入到CDO.Message，要做解析的话，后面就可以了。

            }
            catch (IOException)
            {

            }
            finally
            {
                stm.Close();
            }
            return oMsg;
        }

        /// <summary>
        /// 提取字符串中的邮件地址
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返回匹配到的结果</returns>
        public String MatcheMailPattern(String Input, String Type)
        {
            Regex r = new Regex(GetPattern(Type), RegexOptions.ECMAScript);
            Match matchs = r.Match(Input);
            if (matchs.Success)//判断是否包含邮件地址
            {
                return matchs.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 提取字符串的指定类型数据
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <param name="Type">指定类型</param>
        /// <returns>返回提取结果</returns>
        public List<String> MatchesMailPattern(String Input, String Type)
        {
            List<String> AddressList = new List<String>();
            String Pattern = GetPattern(Type);
            if (!String.IsNullOrWhiteSpace(Pattern))
            {
                Regex r = new Regex(GetPattern(Type), RegexOptions.ECMAScript);
                MatchCollection matchs = r.Matches(Input);
                foreach (Match Address in matchs)
                {
                    AddressList.Add(Address.Value);
                }
            }
            return AddressList;
        }

        private String GetPattern(String Type)
        {
            switch (Type)
            {
                case "MailAddress":
                    return this.MailAddressPattern;
                case "Ip":
                    return this.IpPattern;
                case "Phone":
                    return this.PhonePattern;
                case "MailNum":
                    return this.MailNumPattern;
                case "IDCardNum":
                    return this.IDCardNumPattern;
                case "QQNum":
                    return this.QQNumPattern;
                case "ContainerNum":
                    return this.ContainerNumPattern;
                default:
                    return "";
            }
        }
    }
}