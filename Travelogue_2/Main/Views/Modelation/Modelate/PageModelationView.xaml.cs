using Syncfusion.SfImageEditor.XForms;
using System.IO;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.ViewModels.Modelation.Modelate;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Modelation.Modelate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageModelationView : ContentPage
    {
        public PageModelationViewModel model;

        public PageModelationView()
        {
            InitializeComponent();

            BindingContext = model = new PageModelationViewModel();

            CustomEditor.Model = model;
            //Content = new CustomImageEditor( int.Parse(model.CurrentJourneyId) );
            //CustomEditor.Source = model.ImageSelectedSource;
        }

        private void ImageSaving(object sender, ImageSavingEventArgs args)
        {
            if (args.FileName is null || args.FileName == string.Empty)
            {
                args.FileName = model.GetName();
            }

            model.Stream = args.Stream;
        }

    }
}