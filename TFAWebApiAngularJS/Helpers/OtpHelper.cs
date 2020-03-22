using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using TFAWebApiAngularJS.Services;

namespace TFAWebApiAngularJS.Helpers
{
    public static class OtpHelper
    {
        private const string OTP_HEADER = "X-OTP";

        public static bool HasValidTotp(this HttpRequestMessage request, string key)
        {
            //guarda se l'header contiene l'X-OTP ossia il codice di google a 6 cifre
            if (request.Headers.Contains(OTP_HEADER))
            {
                string otp = request.Headers.GetValues(OTP_HEADER).First();

                // We need to check the passcode against the past, current, and future passcodes

                if (!string.IsNullOrWhiteSpace(otp))
                {
                    //qui verifica se il codice inserito è uno dei tre generati in base alla chiave applicativa univoca per l'utente corrente
                    if (TimeSensitivePassCode.GetListOfOTPs(key).Any(t => t.Equals(otp)))
                    {
                        return true;
                    }
                }

            }
            return false;
        }
    }
}