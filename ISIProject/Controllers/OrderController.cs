using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIProject.Models;
using ISIProject.Helpers;


namespace ISIProject.Controllers
{
    public class OrderController : Controller
    {
        String userToken = "45f0f92c2966491f8a3454e84a79d23a";

        public ActionResult Index()
        {
            if (PaymentProcessHelper.payments.ContainsKey(userToken))
            {
                DotpayUrlcDto dotpayResponse = null;
                PaymentProcessHelper.payments.TryGetValue(userToken, out dotpayResponse);
                if (dotpayResponse != null && dotpayResponse.operation_status == "completed")
                {
                    ViewBag.orderId = dotpayResponse.control.Substring(32);
                    PaymentProcessHelper.payments[userToken] = null;
                    return View("Success", dotpayResponse);
                }
                else if (dotpayResponse != null && dotpayResponse.operation_status == "rejected")
                {
                    ViewBag.orderId = dotpayResponse.control.Substring(32);
                    PaymentProcessHelper.payments[userToken] = null;
                    return View("Error", dotpayResponse);
                }
            }
            return View();
        }

        [HttpPost]
        public string urlcDotpay(DotpayUrlcDto response)
        {
            DotpayUrlcDto value;
            if (PaymentProcessHelper.payments.TryGetValue(userToken, out value))
            {
                PaymentProcessHelper.payments[userToken] = response;
            }
            else
            {
                PaymentProcessHelper.payments.Add(userToken, response);
            }
            
            return "OK";
        }
    }
}