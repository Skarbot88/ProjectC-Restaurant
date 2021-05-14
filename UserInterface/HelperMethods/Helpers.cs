using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UserInterface.HelperMethods
{
    public class Helpers
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress =new Uri("http://localhost:61413/");

            return client;
         }
    }
}
   
