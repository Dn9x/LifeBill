using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.Data;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace LifeBill.Models.Util
{
    public sealed class PubUtil
    {

        /// <summary>
        /// 把字符串用MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5(string str)
        {
            //把要加密的字符串转为字节
            byte[] byt = Encoding.Default.GetBytes(str);   

            //实例MD5对象
            MD5 md5 = new MD5CryptoServiceProvider();

            //得到加密后的字节
            byte[] result = md5.ComputeHash(byt);

            //返回结果
            return BitConverter.ToString(result).Replace("-", "");
        }

        /// <summary>
        /// 把list转换为json
        /// </summary>
        /// <param name="list">List里面的数据是一个类</param>
        /// <returns></returns>
        public static string ListToJson<T>(IList<T> list)
        {
            if (list.Count > 0)
            {
                //获取对象
                object obj = list[0];

                //获取json的名称
                string jsonName = obj.GetType().Name;

                //得到转换对象
                DataContractJsonSerializer json = new DataContractJsonSerializer(list.GetType());

                //通过刘转换
                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, list);

                    jsonName = Encoding.UTF8.GetString(stream.ToArray());
                }

                //返回
                return jsonName;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 检测字符串中是否有敏感字串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDangerous(string input)
        {
            string SqlStr = @"and|or|exec|execute|insert|select|delete|update|alter|create|drop|count|\*|chr|char|asc|mid|substring|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";
            try
            {
                if ((input != null) && (input != String.Empty))
                {
                    string regStr = @"\b(" + SqlStr + @")\b";

                    Regex regex = new Regex(regStr, RegexOptions.IgnoreCase);

                    if (true == regex.IsMatch(input))
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 替换输入里面的敏感词
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string ReplaceInput(string inputString)
        {
            string SqlStr = @"and|or|exec|execute|insert|select|delete|update|alter|create|drop|count|\*|chr|char|asc|mid|substring|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";
            try
            {
                if ((inputString != null) && (inputString != String.Empty))
                {
                    string str_Regex = @"\b(" + SqlStr + @")\b";

                    Regex Regex = new Regex(str_Regex, RegexOptions.IgnoreCase);

                    MatchCollection matches = Regex.Matches(inputString);
                    for (int i = 0; i < matches.Count; i++)
                    {
                        inputString = inputString.Replace(matches[i].Value, "[" + matches[i].Value + "]");
                    }
                }
            }
            catch
            {
                return "";
            }

            return inputString;
        }

        /// <summary>
        /// 替换输出里面的敏感词
        /// </summary>
        /// <param name="outputstring"></param>
        /// <returns></returns>
        public static string ReplaceOutput(string outputstring)
        {
            string SqlStr = @"and|or|exec|execute|insert|select|delete|update|alter|create|drop|count|\*|chr|char|asc|mid|substring|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";
            try
            {
                if ((outputstring != null) && (outputstring != String.Empty))
                {
                    string str_Regex = @"\[\b(" + SqlStr + @")\b\]";
                    Regex Regex = new Regex(str_Regex, RegexOptions.IgnoreCase);
                    MatchCollection matches = Regex.Matches(outputstring);
                    for (int i = 0; i < matches.Count; i++)
                    {
                        outputstring = outputstring.Replace(matches[i].Value, matches[i].Value.Substring(1, matches[i].Value.Length - 2));
                    }
                }
            }
            catch
            {
                return "";
            }

            return outputstring;
        }

        /// <summary>
        /// 根據傳入的字符截取指定的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="chr">字符,可選參數，默認是';'</param>
        /// <returns></returns>
        public static string[] SplitStr(string str, char chr = ';')
        {
            str = str.Substring(0, str.Length - 1);

            char[] separator = { chr };

            string[] res = str.Split(separator);

            return res;
        }

        /// <summary>
        /// 读取存储在cookie中的user信息
        /// </summary>
        /// <param name="userInfo">User.Identity.Name</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCookie(string userInfo, string key)
        {
            string result = null;
            string[] Info = userInfo.Split(',');
            foreach (string s in Info)
            {
                if (s.Trim().StartsWith(key))
                {
                    result = s.Substring(s.IndexOf(":", 0) + 1);
                    break;
                }
            }
            return result;
        }


        

    }
}