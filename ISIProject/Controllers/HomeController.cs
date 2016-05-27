using ISIProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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

        String userToken = "45f0f92c2966491f8a3454e84a79d23a";

        public static XmlDocument GETRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                request.Method = "GET";
                request.Accept = "application/xml";

                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if ((int)response.StatusCode == 200)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(response.GetResponseStream());
                    return (xmlDoc);                
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ActionResult Index()
        {
            OrderCollection orders = null;
            XmlReader xmlReader;
            XmlSerializer serializer = new XmlSerializer(typeof(OrderCollection));

            xmlReader = new XmlTextReader(Server.MapPath("~/Files/orders.xml"));

            String URLString = "https://jetdevserver2.cloudapp.net/StoreISI/sklepAPI/Orders?token=" + userToken + "&unpaid=true";

            if (GETRequest(URLString) != null)
            {
                xmlReader = new XmlNodeReader(GETRequest(URLString));
            }
            
            orders = (OrderCollection)serializer.Deserialize(xmlReader);
            
            xmlReader.Close();
            return View(orders);
        }

        public ActionResult Payment(int orderId)
        {
            ViewBag.XsltModel = getFiles();

            OrderDetails viewModel = null;
            XmlReader xmlReader;
            XmlSerializer serializer = new XmlSerializer(typeof(OrderDetails));
            xmlReader = new XmlTextReader(Server.MapPath("~/Files/order" + orderId + ".xml"));

            String URLString = "https://jetdevserver2.cloudapp.net/StoreISI/sklepAPI/Orders?token=" + userToken + "&order_id=" + orderId;

            if (GETRequest(URLString) != null)
            {
                xmlReader = new XmlNodeReader(GETRequest(URLString));
            }
               
            viewModel = (OrderDetails)serializer.Deserialize(xmlReader);
            xmlReader.Close();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Payment(OrderDetails model)
        {
            return Redirect("https://ssl.dotpay.pl/test_payment/?id=" + model.storeId
                + "&kwota=" + model.amount
                + "&waluta=" + model.currency
                + "&opis=" + "Zapłata za fakturę " + model.invoiceNo
                + "&control=" + userToken + model.orderId
                + "&URL=http://isiproject.azurewebsites.net/Order"
                + "&type=0"
                + "&URLC=http://isiproject.azurewebsites.net/Order/urlcDotpay");
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

        public ActionResult History(int days)
        {
            string end = String.Format("{0:ddMMyyyy}", DateTime.Today);
            string start = String.Format("{0:ddMMyyyy}", DateTime.Today.AddDays(-days));
            
            OrderCollection orders = null;
            XmlReader xmlReader;
            XmlSerializer serializer = new XmlSerializer(typeof(OrderCollection));

            xmlReader = new XmlTextReader(Server.MapPath("~/Files/orders.xml"));

            String URLString = "https://jetdevserver2.cloudapp.net/StoreISI/sklepAPI/Orders?token=" + userToken + "&unpaid=false&startDate=" + start + "&endDate=" + end;

            if (GETRequest(URLString) != null)
            {
                //System.Diagnostics.Debug.WriteLine("OK");
                xmlReader = new XmlNodeReader(GETRequest(URLString));
            }

            orders = (OrderCollection)serializer.Deserialize(xmlReader);

            xmlReader.Close();
            return View(orders);
        }       
    }
}