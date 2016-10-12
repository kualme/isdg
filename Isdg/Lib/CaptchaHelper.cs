using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BotDetect.Web.Mvc;

namespace Isdg.Lib
{
    public static class CaptchaHelper
    {
        public static MvcCaptcha GetRegistrationCaptcha()
        {
            MvcCaptcha exampleCaptcha = new MvcCaptcha("RegistrationCaptcha");
                        
            exampleCaptcha.UserInputID = "CaptchaCode";

            return exampleCaptcha;
        }
    }
}