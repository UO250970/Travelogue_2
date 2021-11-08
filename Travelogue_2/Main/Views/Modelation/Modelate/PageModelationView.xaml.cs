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
        }

        protected override bool OnBackButtonPressed()
        {
            model.Back();
            return true;
        }

        private void ImageSaving(object sender, ImageSavingEventArgs args)
        {
            args.Cancel = true; // To avoid the image saved into pictures library
            model.Save(args);
        }

    }
}