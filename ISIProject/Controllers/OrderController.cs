﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIProject.Models;
using ISIProject.Helpers;
using System.Net;


namespace ISIProject.Controllers
{
    public class OrderController : Controller
    {
        String userToken = "45f0f92c2966491f8a3454e84a79d23a";

        public static Boolean PUTReqest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                request.Method = "PUT";
                request.ContentLength = 0;

                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if ((int)response.StatusCode == 200)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                return false;
            }
        }

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

                    String URLString = "https://jetdevserver2.cloudapp.net/StoreISI/sklepAPI/Orders?token=" + userToken + "&order_id=" + ViewBag.orderId;

                    if (PUTReqest(URLString) == true)
                    {
                        return View("Success", dotpayResponse);
                    }

                    return View("Error", dotpayResponse); 
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