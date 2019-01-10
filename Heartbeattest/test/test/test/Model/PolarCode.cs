using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace test.Model
{
    public class PolarCode
    {
        public static string Grant_type { get; set; } = "authorization_code";
        public static string Code { get; set; }
        public static string Redirect_uri { get; set; } = new Uri("com.companyname.test:/oauth2redirect").ToString();
    }
}
