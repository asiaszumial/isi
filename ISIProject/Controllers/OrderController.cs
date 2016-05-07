using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIProject.Models;


namespace ISIProject.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}