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
            MASHAPE_KEY = mashapeKey;
        }

        public async Task<HttpResponseMessage> yodaSpeak(string sentence)
        {
            var client = new System.Net.Http.HttpClient();

            // Get your own Bitly login and apikey at http://dev.bitly.com
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("sentence", sentence)
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // Get your Mashape header value at https://www.mashape.com/docs/consume/rest
            // To see how this was computed, head over to https://www.mashape.com/docs/consume/csharp
            content.Headers.Add("X-Mashape-Authorization", MASHAPE_KEY);

            // You can (should) do this with GetAsync.  I'll let you figure that out :)
            return await client.PostAsync("https://" + PUBLIC_DNS + "/yoda", content);
        }
    
    }
}
