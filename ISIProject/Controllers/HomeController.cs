using ISIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISIProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string xmlFile = Server.MapPath("~/Files/kursy.xml");
            string xsltFile = Server.MapPath("~/Files/kursyXSLT.xslt");

            XsltModel model = new XsltModel();
            model.xmlFile = xmlFile;
            model.xsltFile = xsltFile;

            return View(model);
        }

    }
}