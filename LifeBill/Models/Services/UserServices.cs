using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeBill.Models.Util;
using LifeBill.Models.Interface;
using System.Data;
using System.Configuration;

namespace LifeBill.Models.Services
{
    public class UserServices : MySqlUtil, IUsers
    {
        private static string conn = ConfigurationManager.AppSettings["DBConn"];

        public DataTable SelectUserByNameAndPswd(string name, string pswd)
        {
            string sql = String.Format("select id, login, pswd, name, tel, email, sex, addtime from users where login='{0}' and pswd='{1}'", name, pswd);

            DataTable dt = this.GetDataSet(conn, CommandType.Text, sql).Tables[0];

            return dt;
        }
    }
}