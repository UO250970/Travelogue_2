using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Settings;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public Command SettingsLanguageViewCommand { get; }
        public ImageSource FlagRoute { get; }

        public SettingsViewModel()
        {
            SettingsLanguageViewCommand = new Command(() => SettingsLanguageViewC());

            FlagRoute = ImageSource.FromResource(CommonVariables.FlagImagesPath + App.LocResources.CurrentCulture().ToUpper() + CommonVariables.FlagImagesExtension);
        }

        async internal void SettingsLanguageViewC()
            => await Shell.Current.GoToAsync($"{ nameof(SettingsLanguageView)}");

    }
}
