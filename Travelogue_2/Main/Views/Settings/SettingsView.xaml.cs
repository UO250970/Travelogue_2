using Travelogue_2.Main.ViewModels.Settings;
using Xamarin.Forms;

namespace Travelogue_2.Main.Views.Settings
{
    public partial class SettingsView : ContentPage
    {
        readonly SettingsViewModel model;

        public SettingsView()
        {
            InitializeComponent();

            BindingContext = model = new SettingsViewModel();

            Shell.SetNavBarIsVisible(this, false);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            int count = MainGrid.ColumnDefinitions.Count;
            double rowHeight = width / count;
            foreach (RowDefinition r in MainGrid.RowDefinitions)
            {
                r.Height = rowHeight;
            }

            base.OnSizeAllocated(width, height);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}