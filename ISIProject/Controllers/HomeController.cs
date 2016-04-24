using ISIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Xsl;

namespace ISIProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string xmlFile = Server.MapPath("~/Files/kursy.xml");
            string xsltFile = Server.MapPath("~/Files/kursyXSLT.xslt");

            string xmlFile_Orders = Server.MapPath("~/Files/orders.xml");
            string xsltFile_Orders = Server.MapPath("~/Files/orders_xslt.xslt");

            XsltModel model = new XsltModel();
            model.xmlFile = xmlFile;
            model.xsltFile = xsltFile;

            var myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(xsltFile_Orders);
            myXslTrans.Transform(xmlFile_Orders, Server.MapPath("~") + "/orders_email.html");

            return View(model);
        }

    }
}