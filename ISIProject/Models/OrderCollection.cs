using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ISIProject.Models
{
    [Serializable()]
    [XmlRoot("ArrayOfOrder")]
    public class OrderCollection
    {
        [XmlElement("Order")]
        public Order[] orders { get; set; }
    }
}