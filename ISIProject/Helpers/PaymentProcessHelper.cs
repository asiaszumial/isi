using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISIProject.Models;

namespace ISIProject.Helpers
{
    public static class PaymentProcessHelper
    {
        public static Dictionary<string, DotpayUrlcDto> payments;

        static PaymentProcessHelper()
        {
            payments = new Dictionary<string, DotpayUrlcDto>();
        }
    }
}