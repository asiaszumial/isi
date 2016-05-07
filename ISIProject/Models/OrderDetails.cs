using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISIProject.Models
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("Order")]
    public class OrderDetails
    {
        [Display(Name = "Numer zamówienia")]
        [System.Xml.Serialization.XmlElement("id")]
        public int orderId { get; set; }
        [Display(Name = "Kwota")]
        [System.Xml.Serialization.XmlElement("amount")]
        public double amount { get; set; }
        [System.Xml.Serialization.XmlElement("email")]
        public string email { get; set; }
        [Display(Name = "Numer faktury")]
        [System.Xml.Serialization.XmlElement("invoiceNo")]
        public string invoiceNo { get; set; }
        [System.Xml.Serialization.XmlElement("storeId")]
        public string storeId { get; set; }
        [Display(Name = "Waluta")]
        [System.Xml.Serialization.XmlElement("currency")]
        public string currency { get; set; }
    }
}