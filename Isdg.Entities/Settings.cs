using System;
using System.Configuration;
using Isdg.Core.Data;

namespace Isdg.Core
{
    public static class Settings
    {
        public static EmailAccount EmailAccount 
        { 
            get 
            {
                return new EmailAccount()
                {
                    Email = System.Configuration.ConfigurationManager.AppSettings["Email"],
                    DisplayName = System.Configuration.ConfigurationManager.AppSettings["DisplayName"],
                    Host = System.Configuration.ConfigurationManager.AppSettings["Host"],
                    Port = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Port"]),
                    Username = System.Configuration.ConfigurationManager.AppSettings["Username"],
                    Password = System.Configuration.ConfigurationManager.AppSettings["Password"],
                    EnableSsl = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["EnableSsl"]),
                    UseDefaultCredentials = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["UseDefaultCredentials"])
                };
            } 
        }

        public static FacebookAuth FacebookAuth 
        { 
            get 
            {
                return new FacebookAuth() 
                {
                    AppId = System.Configuration.ConfigurationManager.AppSettings["FacebookAppId"],
                    AppSecret = System.Configuration.ConfigurationManager.AppSettings["FacebookAppSecret"],
                };
            } 
        }

        public static GoogleAuth GoogleAuth
        {
            get
            {
                return new GoogleAuth()
                {
                    ClientId = System.Configuration.ConfigurationManager.AppSettings["GoogleClientId"],
                    ClientSecret = System.Configuration.ConfigurationManager.AppSettings["GoogleClientSecret"],
                };
            }
        }
    }
}
