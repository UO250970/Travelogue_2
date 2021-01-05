using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Settings
{
    public class SettingsLanguageViewModel : BaseViewModel
    {

        private String selectedLanguage;
        //public Command ChangeLanguageCommand { get; }

        public ObservableCollection<String> Languages { get; }
        public Command LoadLanguagesCommand { get; }
        public Command<String> LanguageTapped { get; }

        public SettingsLanguageViewModel()
        {
            //ChangeLanguageCommand = new Command(() => ChangeLanguageC());
            Languages = new ObservableCollection<String>();
            LoadLanguagesCommand = new Command(() => ExecuteLoadLanguagesCommand());

            LanguageTapped = new Command<String>(OnLanguageSelected);
        }

        void ExecuteLoadLanguagesCommand()
        {
            IsBusy = true;

            try
            {
                Languages.Clear();
                Languages.Add("ES");
                Languages.Add("FR");
                Languages.Add("EN");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedLanguage = null;
        }

        public String SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                SetProperty(ref selectedLanguage, value);
                OnLanguageSelected(value);
            }
        }

        async void OnLanguageSelected(String language)
        {
            if (language == null)
                   return;
            App.CurrentLanguage = language;
            MessagingCenter.Send<object, CultureChangedMessage>(this,
                    String.Empty, new CultureChangedMessage(language));

            await Alerter.AlertLanguageChanged();
            await Shell.Current.GoToAsync("..");
        }
    }
}
