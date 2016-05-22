using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIProject.Models;
using ISIProject.Helpers;
using System.Net;
using System.Web.Script.Serialization;


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
                if (dotpayResponse != null)
                {
                    if (dotpayResponse.status == "OK")
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
                    else
                    {
                        ViewBag.orderId = dotpayResponse.control.Substring(32);
                        PaymentProcessHelper.payments[userToken] = null;
                        return View("Error", dotpayResponse);
                    }
                }
                
            }
            return View();
        }

        [HttpPost]
        public string urlcDotpay(string status, string control, string t_id, string description, string amount, string t_status, string email, string t_date, string channel)
        {
            
            DotpayUrlcDto value;
            DotpayUrlcDto response = new DotpayUrlcDto();
            response.control = control;
            response.status = status;
            response.t_id = t_id;
            response.description = description;
            response.amount = amount;
            response.t_status = t_status;
            response.email = email;
            response.t_date = t_date;
            response.channel = channel;
            if (PaymentProcessHelper.payments.TryGetValue(userToken, out value))
            {
                PaymentProcessHelper.payments[userToken] = response;
            }
            else
            {
                PaymentProcessHelper.payments.Add(userToken, response);
            }

            if (sendEmailWithNotification(response))
                return "OK";
            else
            return "FAILED";
        }

        private bool sendEmailWithNotification(DotpayUrlcDto payments)
        {
            OperationResult operationResult = new OperationResult();
            operationResult = transferData(operationResult, payments);
            string address = ConfigurationManager.AppSettings["esbAddressToSendEmailNotification"].ToString();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(address);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        numer = operationResult.operation_number,
                        //typ = operationResult.operation_type,
                        status = operationResult.operation_status,
                        wartosc = operationResult.operation_amount,
                        //waluta = operationResult.operation_currency,
                        data = operationResult.operation_datetime,
                        opis = operationResult.description,
                        email = operationResult.email,
                        kanal = operationResult.channel
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if ((int) httpResponse.StatusCode == 200)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        private OperationResult transferData(OperationResult operationResult, DotpayUrlcDto payments)
        {
            operationResult.operation_number = payments.t_id;
            //operationResult.operation_type = payments.t_status;
            operationResult.operation_status = payments.t_status;
            operationResult.operation_amount = payments.amount;
            //operationResult.operation_currency = payments.operation_currency;
            operationResult.operation_datetime = payments.t_date;
            operationResult.description = payments.description;
            operationResult.email = payments.email;
            operationResult.channel = payments.channel;
            return operationResult;
        }
    }
}