using System;
using System.Collections.Generic;
using System.Text;

namespace test.Model
{
    public class PolarToken
    {

        public string grant_type { get; set; } = "authorization_code";
        public string code { get; set; }
        public string redirect_uri { get; set; } = new Uri("com.companyname.test:/oauth2redirect").ToString();


    }
}
