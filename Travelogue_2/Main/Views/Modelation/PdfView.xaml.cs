using System.IO;
using System.Reflection;
using Travelogue_2.Main.ViewModels.Modelation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Modelation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PdfView : ContentPage
    {
        PdfViewModel model;
        public PdfView()
        {
            BindingContext = model = new PdfViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Stream  fileStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream(model.JournalPath);
            //Load the PDF
            pdfViewerControl.LoadDocument(fileStream);
            //pdfViewerControl.
        }
    }
}