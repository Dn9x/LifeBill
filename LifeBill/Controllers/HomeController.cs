using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Data;
using LifeBill.Models;
using LifeBill.Models.Interface;
using LifeBill.Models.Services;
using System.Web.Security;
using LifeBill.Models.Util;

namespace LifeBill.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            if (Request["y"] != null&& Request["m"] != null)
            {
                try
                {
                    year = Convert.ToInt32(Request["y"]);
                    month = Convert.ToInt32(Request["m"]);
                }
                catch (Exception ex)
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                }
            }

            var str = GetCalendar(year, month, SetMap(year, month));

            ViewBag.Message = str;

            string u = "";
            string d = "";

            GetYaM(year, month, ref u, ref d);

            ViewBag.Date = year + "/" + month;
            ViewBag.Up = u;
            ViewBag.Down = d;

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string pswd)
        {
            if (!PubUtil.IsDangerous(name)) {
                return View();
            }

            IUsers iUser = new UserServices();

            DataTable dt = iUser.SelectUserByNameAndPswd(HttpUtility.HtmlEncode(name), PubUtil.GetMd5(HttpUtility.HtmlEncode(pswd)));

            if (dt.Rows.Count > 0)
            {
                FormsAuthentication.SetAuthCookie(dt.Rows[0]["name"].ToString(), true);
                
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            IBill iBill = new BillServices();

            DataTable dt = iBill.SelectTagListByIsShow("Y");

            ViewBag.Tags = dt;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            string[] type = form["type"].Split(',');
            string[] price = form["price"].Split(',');
            string[] notes = form["notes"].Split(',');

            int year = Convert.ToInt32(form["year"]);
            int month = Convert.ToInt32(form["month"]);
            int day = Convert.ToInt32(form["day"]);

            List<string> list = new List<string>();

            IBill iBill = new BillServices();

            //获取入账的类型
            DataTable tags = iBill.SelectTagByType("I");

            int revenue = 0;        //入账
            int outlay = 0;         //出账

            for (int i = 0; i < type.Length; i++)
            {
                int temp = 0;

                for (int j = 0; j < tags.Rows.Count; j++)
                {
                    if (type[i] == tags.Rows[j]["id"].ToString())
                    {
                        revenue += Convert.ToInt32(price[i]);
                        temp = 0;
                        break;
                    }
                    else
                    {
                        temp = Convert.ToInt32(price[i]);
                    }
                }

                outlay += temp;
            }

            //拼接主档SQL
            string sql = String.Format("insert into billmaster(years, months, days, revenue, outlay, addtime, userid) values({0}, {1}, {2}, {3}, {4}, now(), 1)", year, month, day, revenue, outlay);

            list.Add(sql);

            //拼接明细档
            for (int i = 0; i < type.Length; i++)
            {
                sql = String.Format("insert into billdetail(masterid, tagid, price, notes, addtime) " +
                                "values((select id from billmaster where years={0} and months={1} and days={2}), {3}, {4}, '{5}', now())", year, month, day, type[i], price[i], notes[i]);

                list.Add(sql);
            }

            iBill.InsertBill(list);

            return RedirectToAction("About");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Upd()
        {
            int mid = Convert.ToInt32(Request["i"]);

            IBill iBill = new BillServices();

            DataTable tags = iBill.SelectTagListByIsShow("Y");

            DataTable dets = iBill.SelectDetailByMasterId(mid);

            ViewBag.Tags = tags;

            ViewBag.Details = dets;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upd(FormCollection form)
        {
            int mid = Convert.ToInt32(Convert.ToInt32(form["id"])); 
            
            string[] type = form["type"].Split(',');
            string[] price = form["price"].Split(',');
            string[] notes = form["notes"].Split(',');

            List<string> list = new List<string>();

            IBill iBill = new BillServices();

            //获取入账的类型
            DataTable tags = iBill.SelectTagByType("I");

            int revenue = 0;        //入账
            int outlay = 0;         //出账

            for (int i = 0; i < type.Length; i++)
            {
                int temp = 0;

                for (int j = 0; j < tags.Rows.Count; j++)
                {
                    if (type[i] == tags.Rows[j]["id"].ToString())
                    {
                        revenue += Convert.ToInt32(price[i]);
                        temp = 0;
                        break;
                    }
                    else
                    {
                        temp = Convert.ToInt32(price[i]);
                    }
                }

                outlay += temp;
            }

            //拼接主档SQL
            string sql = String.Format("update billmaster set revenue={0}, outlay={1}, addtime=now() where id={2}", revenue, outlay, mid);

            list.Add(sql);

            //删除明细档
            sql = String.Format("delete from billdetail where masterid={0} ", mid);

            list.Add(sql);

            //拼接明细档
            for (int i = 0; i < type.Length; i++)
            {
                sql = String.Format("insert into billdetail(masterid, tagid, price, notes, addtime) " +
                                "values({0}, {1}, {2}, '{3}', now())", mid, type[i], price[i], notes[i]);

                list.Add(sql);
            }

            iBill.InsertBill(list);

            return RedirectToAction("About");
        }

        public ActionResult About()
        {
            return View();
        }

        public ContentResult GetJson()
        { 
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            if (Request["y"] != null&& Request["m"] != null)
            {
                try
                {
                    year = Convert.ToInt32(Request["y"]);
                    month = Convert.ToInt32(Request["m"]);
                }
                catch (Exception ex)
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                }
            }
            
            var str = SetJson(year, month);

            return Content(str);
        }

        private string SetJson(int year, int month)
        {
            IBill iBill = new BillServices();

            DataTable dt = iBill.SelectMasterByDate(year, month);

            string json = "{";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    json += "\"_" + dt.Rows[i]["date"] + "\":[{\"total\":\"" + dt.Rows[i]["total"] + "\", \"id\":\""+dt.Rows[i]["ID"]+"\"}], ";
                }
            }

            return json + "}";
        }

        private Hashtable SetMap(int year, int month)
        {
            Hashtable ht = new Hashtable();

            IBill iBill = new BillServices();

            DataTable dt = iBill.SelectMasterByDate(year, month);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ht.Add(dt.Rows[i]["date"], dt.Rows[i]["total"]);
                }
            }

            return ht;
        }

        private void GetYaM(int year, int month, ref string u, ref string d)
        { 
            if(month == 1)
            {
                u = "y=" + (year-1) + "&m=" + 12;
                d = "y=" + year + "&m=" + (month+1);
            }
            else if (month == 12)
            {
                u = "y=" + year + "&m=" + (month - 1);
                d = "y=" + (year + 1) + "&m=" + 1;
            }
            else
            {
                u = "y=" + year + "&m=" + (month - 1);
                d = "y=" + year + "&m=" + (month + 1);
            }
        }

        private string GetCalendar(int year, int month, Hashtable ht)
        {
            //获取当前天的时间对象
            DateTime dtw = new DateTime(year, month, 1);

            //获取当前月的第一条是礼拜几以数字表示
            var week = dtw.DayOfWeek.GetHashCode() == 0 ? 7 : dtw.DayOfWeek.GetHashCode();

            var cal = "<tr>";       //拼接日历

            //1.1.判断前面空几个
            int qg = week-1;

            //1.2.获取前面空格上面的日期从那天开始：上个月天数-空格数+1
            int qm = 0;
            if (month == 1)
            {
                qm = DateTime.DaysInMonth(year-1, 12) - qg + 1;
            }
            else
            {
                qm = DateTime.DaysInMonth(year, month - 1) - qg + 1;
            }

            //1.3.循环添加前面的空格日期
            for (int i = 0; i < qg; i++)
            {
                cal += "<td>" + 
                        "<div class='td_div_main'>" +
                        "<div class='td_div_topg'>" + (qm + i) + "</div>" + 
                        "<div class='td_div_cont'>&nbsp;</div>" + 
                        "</div>" + 
                        "</td>";
            }

            //1.4.循环这一行里面剩下的几天
            for (int i = 0; i < (7 - week + 1); i++)
            {
                cal += "<td>" +
                        "<div class='td_div_main'>" +
                        "<div class='td_div_top'>" + (1 + i) + "</div>" +
                        "<div class='td_div_cont'>"+ht[year+""+month+""+(i+1)]+"</div>" +
                        "</div>" +
                        "</td>";
            }

            //1.5.第一行数据拼接完成
            cal += "</tr>";

            //2.1.计算本月有多少天
            int dd = DateTime.DaysInMonth(year, month);

            //2.2.计算还有多少天要循环
            int sd = dd - (7 - week + 1);

            //2.3.计算整行的循环还有多少行
            int hd = sd / 7;

            //累加余下的日期开始数
            int lc = 7 - week + 1;

            //2.5.开始循环整行的
            for (int i = 0; i < hd; i++)
            {
                cal += "<tr>";

                //2.5.1.开始循环整行里面的td
                for (int j = 0; j < 7; j++)
                {
                    lc++;
                    cal += "<td>" +
                           "<div class='td_div_main'>" +
                           "<div class='td_div_top'>" + lc + "</div>" +
                           "<div class='td_div_cont'>" + ht[year + "" + month + "" + lc] + "</div>" +
                           "</div>" +
                           "</td>";
                }

                cal += "</tr>";
            }

            //3.1.计算整行之后还有多少天剩余要循环
            int gd = sd % 7;

            //3.2.循环剩余没有循环的td
            cal += "<tr>";
            for (int i = 0; i < gd; i++)
            {
                lc++;
                cal += "<td>" +
                       "<div class='td_div_main'>" +
                       "<div class='td_div_top'>" + lc + "</div>" +
                       "<div class='td_div_cont'>" + ht[year + "" + month + "" + lc] + "</div>" +
                       "</div>" +
                       "</td>";
            }

            //3.3.循环最后一行不够循环剩余的td个数
            for (int i = 0; i < 7-gd; i++)
            {
                cal += "<td>" +
                       "<div class='td_div_main'>" +
                       "<div class='td_div_topg'>" + (i + 1) + "</div>" +
                       "<div class='td_div_cont'>&nbsp;</div>" +
                       "</div>" +
                       "</td>";
            }
            cal += "</tr>";

               // DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            return cal;
        }

    }
}
