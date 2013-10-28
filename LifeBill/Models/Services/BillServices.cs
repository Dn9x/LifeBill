using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeBill.Models.Interface;
using LifeBill.Models.Util;
using System.Configuration;
using System.Data;
using LifeBill.Models.Entity;

namespace LifeBill.Models.Services
{
    public class BillServices : MySqlUtil, IBill
    {
        private static string conn = ConfigurationManager.AppSettings["DBConn"];

        #region IBill 成员

        public DataTable SelectMasterByDate(int year, int month)
        {
            string sql = String.Format("select Concat(years, months, days) as date, CONCAT(outlay, '/', revenue) as total, id from billmaster where years={0} and months={1}", year, month);

            DataTable dt = this.GetDataSet(conn, CommandType.Text, sql).Tables[0];

            return dt;
        }

        public DataTable SelectMasterById(int id)
        {
            throw new NotImplementedException();
        }

        public DataTable SelectMasterByYear(int year)
        {
            throw new NotImplementedException();
        }

        public DataTable SelectTagList()
        {
            throw new NotImplementedException();
        }

        public DataTable SelectTagListByIsShow(string isShow)
        {
            string sql = String.Format("select id, tagname, tagtype, isshow, pid, addtime from billtags where isshow='{0}'", isShow);

            DataTable dt = this.GetDataSet(conn, CommandType.Text, sql).Tables[0];

            return dt;
        }

        public DataTable SelectTagByType(string type)
        {
            string sql = String.Format("select id, tagname, tagtype, isshow, pid, addtime from billtags where tagtype='{0}'", type);

            DataTable dt = this.GetDataSet(conn, CommandType.Text, sql).Tables[0];

            return dt;
        }

        public DataTable SelectTagById(int id)
        {
            throw new NotImplementedException();
        }

        public DataTable SelectTagByPid(int pid)
        {
            throw new NotImplementedException();
        }

        public DataTable SelectDetailByMasterId(int mid)
        {
            string sql = String.Format("select id, masterid, tagid, price, notes, addtime from billdetail where masterid={0}", mid);

            DataTable dt = this.GetDataSet(conn, CommandType.Text, sql).Tables[0];

            return dt;
        }

        public DataTable SelectDetailByTagId(int tid)
        {
            throw new NotImplementedException();
        }

        public string InsertBill(List<string> sqls)
        {
            return ExecuteSql(conn, sqls);
        }

        #endregion

    }
}