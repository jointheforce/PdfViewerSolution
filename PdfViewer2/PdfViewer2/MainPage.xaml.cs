using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace PdfViewer2
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // <summary>
        /// Save and open the PDF file in the app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            // get file from server
            var result = Request();
            _ = result.ContinueWith(async t =>
                 {
                     if (t.Result != null)
                     {
                         var byteArray = t.Result;
                         await Xamarin.Forms.DependencyService.Get<IPDFSaveAndOpen>().SaveAndView("genericFile" + ".pdf", "application/pdf", byteArray, PDFOpenContext.InApp);
                     }
                 });
           
        }
        private async Task<byte[]> Request()
        {
            try
            {
                var el = "";

                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                    {
                        // certificate ignored
                        return true;
                    };
                    using (var client = new HttpClient(clientHandler))
                    {
                        var u = new Uri("https://192.168.0.102:5001/api/file");
                        var payload = JsonConvert.SerializeObject(el);
                        HttpContent c = new StringContent(payload, Encoding.UTF8, "application/pdf");
                        //var r = await client.PostAsync(u, c, CancellationToken.None);
                        var r = await client.GetAsync(u, CancellationToken.None);
                        if (r.IsSuccessStatusCode)
                        {
                            var byteArray = r.Content.ReadAsByteArrayAsync().Result;
                            return byteArray;
                            //File.WriteAllBytes("doc.pdf", byteArray);
                        }
                        else
                        {
                            return null;
                        }
                        //var temp = r.StatusCode;
                        //if (r.StatusCode == System.Net.HttpStatusCode.OK)
                        //{
                        //    return true;
                        //}
                        //else
                        //{
                        //    return false;
                        //}
                        //var content = r.Content;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Save and open the PDF file with "choose app".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            MemoryStream stream = new MemoryStream();

            await Xamarin.Forms.DependencyService.Get<IPDFSaveAndOpen>().SaveAndView("301_21" + ".pdf", "application / pdf", null, PDFOpenContext.ChooseApp);
        }
    }
}
