using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeBill.Models.Entity
{
    public class BillDetail
    {
        public int ID { get; set; }

        public int MasterId { get; set; }

        public int TagId { get; set; }

        public double Price { get; set; }

        public string Notes { get; set; }

        public DateTime AddTime { get; set; }

    }
}