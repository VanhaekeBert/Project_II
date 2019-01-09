using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace test.Model
{
    public class PolarToken
    {
        public static string grant_type { get; set; } = "authorization_code";
        public static string code { get; set; }
        public static string redirect_uri { get; set; } = new Uri("com.companyname.test:/oauth2redirect").ToString();


    }
}
