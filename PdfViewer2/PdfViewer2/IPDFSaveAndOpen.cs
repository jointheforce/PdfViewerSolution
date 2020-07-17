using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PdfViewer2
{
    public interface IPDFSaveAndOpen
    {
        Task SaveAndView(string fileName, String contentType, byte[] bytesOfFile, PDFOpenContext context);
    }

    /// <summary>
    /// Where should the PDF file open. In the app or out of the app.
    /// </summary>
    public enum PDFOpenContext
    {
        InApp,
        ChooseApp
    }
}
