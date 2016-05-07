using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISIProject.Models
{
    [Serializable()]
    public class Order
    {
        [System.Xml.Serialization.XmlElement("Id")]
        public int orderId { get; set; }
        [System.Xml.Serialization.XmlElement("Amount")]
        public double amount { get; set; }
        [System.Xml.Serialization.XmlElement("Currency")]
        public string currency { get; set; }
    }
}