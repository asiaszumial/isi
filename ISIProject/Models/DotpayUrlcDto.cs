using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISIProject.Models
{
    public class DotpayUrlcDto
    {
        public int id { get; set; }
        public string t_id { get; set; }
        public string status { get; set; } //OK
        public string t_status { get; set; }
        public string orginal_amount { get; set; }
        public string md5 { get; set; }
        public string t_date { get; set; } //Format: YYYY-MM-DD hh:mm:ss
        public string control { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string p_info { get; set; }
        public string p_email { get; set; }
        public string amount { get; set; }
        public string channel { get; set; }
    }
}