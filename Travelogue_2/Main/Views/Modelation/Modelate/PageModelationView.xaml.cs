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

            Content = new CustomImageEditor(model.JourneyId)
            {
                Source = model.imageSelectedSource
            };
        }

    }
}