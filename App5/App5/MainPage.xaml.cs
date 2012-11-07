using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace App5
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : App5.Common.LayoutAwarePage
    {
        //Mashape - Get your Mashape header value at https://www.mashape.com/docs/consume/rest
        const string MashapeHeader = "";

        //Bitly - Get your own Bitly login and apikey at http://dev.bitly.com
        const string BitlyLogin = "";
        const string BitlyApiKey = "";

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private async void btnCallMashape_Click(object sender, RoutedEventArgs e)
        {
            // URL to shorten
            await MashapeBitlyCall("http://chrispogeek.wordpress.com"); 
        }

        private async void btnCallWordCloud_Click(object sender, RoutedEventArgs e)
        {
            // Text to show world cloud for
            await MashapeWordCloudCall("Mashape Mashape makes the world world go round round Mashape");
        }

        public async Task MashapeBitlyCall(string url)
        {
            // See https://ismaelc-bitly.p.mashape.com for Mashape documentation
            var targeturi = "https://ismaelc-bitly.p.mashape.com/v3/shorten?";
 
            var client = new System.Net.Http.HttpClient();

            // Get your own Bitly login and apikey at http://dev.bitly.com
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("longUrl", url),
                new KeyValuePair<string, string>("login", BitlyLogin),
                new KeyValuePair<string, string>("apikey", BitlyApiKey)
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // Get your Mashape header value at https://www.mashape.com/docs/consume/rest
            // To see how this was computed, head over to https://www.mashape.com/docs/consume/csharp
            content.Headers.Add("X-Mashape-Authorization", MashapeHeader);
            
            // You can (should) do this with GetAsync.  I'll let you figure that out :)
            var response = await client.PostAsync(targeturi, content);

            if (response.IsSuccessStatusCode)
            {
                //The POST succeeded, so update the UI accordingly
                string respContent = await response.Content.ReadAsStringAsync();
                //txtBlockResult.Text = await Task.Run(() => JsonObject.Parse(respContent).GetNamedObject("data").GetNamedString("url").ToString());
                var result = await Task.Run(() => JsonObject.Parse(respContent).GetNamedObject("data").GetNamedString("url").ToString());
                linkBitly.Content = result;
                linkBitly.NavigateUri = new Uri(result);
                linkBitly.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                //The POST failed, so update the UI accordingly
                txtBlockResult.Text = "Error";
            }
        }

        public async Task MashapeWordCloudCall(string text)
        {
            // See https://gatheringpoint-word-cloud-maker.p.mashape.com for Mashape documentation
            var targeturi = "https://gatheringpoint-word-cloud-maker.p.mashape.com/index.php?height=200&width=300&textblock=" + text;

            var client = new System.Net.Http.HttpClient();

            HttpContent content = new StringContent("");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // Get your Mashape header value at https://www.mashape.com/docs/consume/rest
            // To see how this was computed, head over to https://www.mashape.com/docs/consume/csharp
            content.Headers.Add("X-Mashape-Authorization", MashapeHeader);

            var response = await client.PostAsync(targeturi, content);

            if (response.IsSuccessStatusCode)
            {
                //The POST succeeded, so update the UI accordingly
                string respContent = await response.Content.ReadAsStringAsync();
                //txtBlockResult1.Text = await Task.Run(() => JsonObject.Parse(respContent).GetNamedString("url").ToString());
                var result = await Task.Run(() => JsonObject.Parse(respContent).GetNamedString("url").ToString());
                linkWordCloud.Content = result;
                linkWordCloud.NavigateUri = new Uri(result);
                linkWordCloud.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                //The POST failed, so update the UI accordingly
                txtBlockResult1.Text = "Error";
            }
        }

    }
}
