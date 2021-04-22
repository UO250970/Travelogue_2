using Travelogue_2.Main.Views.Settings;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public ImageSource FlagRoute;

        public Command LanguagesSettingsCommand { get; }
        public Command CardHolderSettingsCommand { get; }
        public Command DestiniesSettingsCommand { get; }
        public Command StyleSettingsCommand { get; }

        public SettingsViewModel() 
        {
            LanguagesSettingsCommand = new Command(() => LanguagesSettingsC());
            CardHolderSettingsCommand = new Command(() => CardHolderSettingsC());
            DestiniesSettingsCommand = new Command(() => DestiniesSettingsC());
            StyleSettingsCommand = new Command(() => StyleSettingsC());
        }

        async internal void LanguagesSettingsC()
            => await Shell.Current.GoToAsync(nameof(SettingsLanguageView));

        async internal void CardHolderSettingsC()
            => await Shell.Current.GoToAsync(nameof(SettingsCardHolderView));

        async internal void DestiniesSettingsC()
            => await Shell.Current.GoToAsync(nameof(SettingsDestiniesView));

        async internal void StyleSettingsC()
            => await Shell.Current.GoToAsync(nameof(SettingsStyleView));
    }
}
