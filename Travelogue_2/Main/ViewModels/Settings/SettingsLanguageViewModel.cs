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

        ObservableCollection<LanguageLabel> Languages { get; }
        public Command LoadLanguagesCommand { get; }
        public Command<String> LanguageTapped { get; }

        public SettingsLanguageViewModel()
        {
            //ChangeLanguageCommand = new Command(() => ChangeLanguageC());
            Languages = new ObservableCollection<LanguageLabel>();

            ExecuteLoadLanguagesCommand();
            LanguageTapped = new Command<String>(OnLanguageSelected);
        }

        void ExecuteLoadLanguagesCommand()
        {
            IsBusy = true;

            try
            {
                Languages.Clear();
                foreach (string lang in CommonVariables.AvailableLocations)
				{
                    LanguageLabel temp = new LanguageLabel();
                    temp.language = lang;
                    temp.image = CommonVariables.GetFlag(lang);

                    Languages.Add(temp);
                }
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

        public String SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                SetProperty(ref selectedLanguage, value);
                OnLanguageSelected(value);
            }
        }

        #region INotifyPropertyChanged
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
        #endregion
    }

    public class LanguageLabel
	{
        public string language { get; set; }
        public ImageSource image { get; set; }
	}
}
