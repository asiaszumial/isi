using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISIProject.Models
{
    public class DotpayUrlcDto
    {
        public int id { get; set; }
        public string operation_number { get; set; }
        public string operation_type { get; set; }
        public string operation_status { get; set; } //completed, rejected
        public string operation_amount { get; set; }
        public string operation_currency { get; set; }
        public string operation_datetime { get; set; } //Format: YYYY-MM-DD hh:mm:ss
        public string control { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string p_info { get; set; }
    }
}