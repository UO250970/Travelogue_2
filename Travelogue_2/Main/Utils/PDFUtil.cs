using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Travelogue_2.Main.Models;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Travelogue_2.Main.Utils
{
    public static class PDFUtil
    {

        public static JournalModel CreatePdfFromJournal(JournalModel journal, string name)
        {
            //Create a new PDF document.
            PdfDocument doc = new PdfDocument();

            foreach(ImageModel iPage in journal.Pages)
            {
                PdfPage page = doc.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                FileStream temp = null;

                using (StreamReader reader = new StreamReader( iPage.Path))
                {
                    temp = new FileStream(iPage.Path, FileMode.Open, FileAccess.Read);
                    //read from file here
                }

                PdfBitmap image = new PdfBitmap(temp);

                graphics.DrawImage(image, 0, 0);
            }

            ////Save the document to the stream
            MemoryStream stream = new MemoryStream();
            //Save the document.
            doc.Save(stream);
            //Close the document.
            doc.Close(true);


            //Save the stream as a file in the device and invoke it for viewing
            journal.JournalPath = DependencyService.Get<ISave>().SaveAndView(name, "application/pdf", stream).Result;

            return journal;
        }
    }

    public interface ISave
    {
        //Method to save document as a file and view the saved document
        Task<string> SaveAndView(string filename, string contentType, MemoryStream stream);
    }
}
