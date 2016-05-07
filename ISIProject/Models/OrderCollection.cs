using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ISIProject.Models
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("ArrayOfOrder")]
    public class OrderCollection
    {
        [XmlArray("Orders")]
        [XmlArrayItem("Order", typeof(Order))]
        public Order[] orders { get; set; }
    }
}