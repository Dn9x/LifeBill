using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace LifeBill.Models.Interface
{
    public interface IUsers
    {

        /// <summary>
        /// 根据用户名和密码查询用户信息
        /// </summary>
        /// <param name="name">用户名称</param>
        /// <param name="pswd">用户密码</param>
        /// <returns></returns>
        DataTable SelectUserByNameAndPswd(string name, string pswd);

    }
}