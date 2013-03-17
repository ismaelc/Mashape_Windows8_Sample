using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App5.Mashape
{
    class MashapeGenericClass
    {
        static private string PUBLIC_DNS = "yoda.p.mashape.com";
        static private string MASHAPE_KEY = null;

        public MashapeGenericClass (string mashapeKey) 
        {
            //TODO: Break out into Auth class
            MASHAPE_KEY = mashapeKey;
        }

        //TODO: Break out into MashapeResponse class
        public async Task<HttpResponseMessage> yodaSpeak(string sentence)
        {
            var client = new System.Net.Http.HttpClient();
         
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("sentence", sentence)
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Headers.Add("X-Mashape-Authorization", MASHAPE_KEY);

            //TODO: Fix GET POST guesswork
            return await client.PostAsync("https://" + PUBLIC_DNS + "/yoda", content);
        }
    
    }
}
