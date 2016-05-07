using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ISIProject.Models
{
    [Serializable()]
    [XmlRoot("Order")]
    public class OrderDetails
    {
        [Display(Name = "Numer zamówienia")]
        [XmlElement("Id")]
        public int orderId { get; set; }
        [Display(Name = "Kwota")]
        [XmlElement("Amount")]
        public double amount { get; set; }
        [XmlElement("Email")]
        public string email { get; set; }
        [Display(Name = "Numer faktury")]
        [XmlElement("InvoiceNo")]
        public string invoiceNo { get; set; }
        [XmlElement("StoreId")]
        public string storeId { get; set; }
        [Display(Name = "Waluta")]
        [XmlElement("Currency")]
        public string currency { get; set; }
    }
}