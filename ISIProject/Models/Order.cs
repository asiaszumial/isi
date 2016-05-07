using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ISIProject.Models
{
    [Serializable()]
    [XmlRoot("Order")]
    public class Order
    {
        [XmlElement("Id")]
        public int orderId { get; set; }
        [XmlElement("Amount")]
        public double amount { get; set; }
        [XmlElement("Currency")]
        public string currency { get; set; }
    }
}