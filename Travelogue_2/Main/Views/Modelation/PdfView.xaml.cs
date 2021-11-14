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

            Shell.SetNavBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Load the PDF
            Stream FileStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream(model.JournalPath);
            pdfViewerControl.LoadDocument(FileStream);
            
        }
    }
}