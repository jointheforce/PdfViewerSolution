using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
            MemoryStream stream = new MemoryStream();


            await Xamarin.Forms.DependencyService.Get<IPDFSaveAndOpen>().SaveAndView("301_21" + ".pdf", "application / pdf", stream, PDFOpenContext.InApp);
        }

        /// <summary>
        /// Save and open the PDF file with "choose app".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            MemoryStream stream = new MemoryStream();

            await Xamarin.Forms.DependencyService.Get<IPDFSaveAndOpen>().SaveAndView("301_21" + ".pdf", "application / pdf", stream, PDFOpenContext.ChooseApp);
        }
    }
}
