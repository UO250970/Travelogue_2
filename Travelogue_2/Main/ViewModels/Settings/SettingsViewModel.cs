using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Settings;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public ImageSource FlagRoute;

        public Command LanguagesSettingsCommand { get; }

        public SettingsViewModel() 
        {
            LanguagesSettingsCommand = new Command(() => LanguagesSettingsC());
        }

        async internal void LanguagesSettingsC()
            => await Shell.Current.GoToAsync(nameof(SettingsLanguageView));


        async internal void SettingsLanguageViewC()
            => await Shell.Current.GoToAsync($"{ nameof(SettingsLanguageView)}");

    }
}
