using ISIProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace ISIProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            OrderCollection orders = null;
            XmlSerializer serializer = new XmlSerializer(typeof(OrderCollection));
            StreamReader reader = new StreamReader(Server.MapPath("~/Files/orders.xml"));
            orders = (OrderCollection)serializer.Deserialize(reader);
            reader.Close();
            return View(orders);
        }

        public ActionResult Payment(int orderId)
        {
            ViewBag.XsltModel = getFiles();

            XmlSerializer serializer = new XmlSerializer(typeof(OrderDetails));
            StreamReader reader = new StreamReader(Server.MapPath("~/Files/order" + orderId + ".xml"));
            OrderDetails viewModel = null;
            viewModel = (OrderDetails)serializer.Deserialize(reader);
            reader.Close();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Payment(OrderDetails model)
        {
            return Redirect("https://ssl.dotpay.pl/test_payment/?id=" + model.storeId
                + "&kwota=" + model.amount
                + "&waluta=" + model.currency
                + "&opis=" + "Zapłata za fakturę " + model.invoiceNo);
        }

        private XsltModel getFiles()
        {
            string xmlFile = Server.MapPath("~/Files/kursy.xml");
            string xsltFile = Server.MapPath("~/Files/kursyXSLT.xslt");
            if (!System.IO.File.Exists(xmlFile) || System.IO.File.GetLastWriteTime(xmlFile).Date != DateTime.Today)
            {
                String URLString = "https://www.nbp.pl/kursy/xml/LastA.xml";
                XmlTextReader xmlReader = new XmlTextReader(URLString);
                XmlWriterSettings settings = new XmlWriterSettings { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Fragment };
                XmlWriter writer = XmlWriter.Create(xmlFile, settings);
                writer.WriteNode(xmlReader, true);
                writer.Close();
                xmlReader.Close();
            }
               
            XsltModel model = null;
            if (System.IO.File.Exists(xmlFile))
            {
                model = new XsltModel();
                model.xmlFile = xmlFile;
                model.xsltFile = xsltFile;
            }

            return model;
        }

    }
}