using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Settings
{
    public class SettingsLanguageViewModel : BaseViewModel
    {

        private string selectedLanguage;
        public Command SearchLanguageCommand { get; }
        public Command<string> LanguageTapped { get; }
        public ObservableCollection<string> Letters { get; }
        public ObservableCollection<LanguageLabel> Languages { get; }
        public ObservableCollection<LanguageLabel> LanguagesSearched { get; set; }

        public SettingsLanguageViewModel()
        {
            SearchLanguageCommand = new Command(() => SearchLanguageC());

            Languages = new ObservableCollection<LanguageLabel>();
            LanguagesSearched = new ObservableCollection<LanguageLabel>();

            LanguageTapped = new Command<string>(OnLanguageSelected);

            ExecuteLoadLanguagesCommand();
        }

        void ExecuteLoadLanguagesCommand()
        {
            IsBusy = true;

            try
            {
                Languages.Clear();
                foreach (string lang in CommonVariables.AvailableLanguages)
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

        public void OnAppearing()
            => IsBusy = true;

        async internal void SearchLanguageC()
        {
            SearchText = "";
            SearchVisible = !SearchVisible;
        }

        private bool searchVisible = false;

        public bool SearchVisible
        {
            get => searchVisible;
            set => SetProperty(ref searchVisible, value);
        }

        private string searchText = "";
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                var temp = Languages.Where(x => x.language.ToUpper().Contains(searchText.ToUpper()) == true);
                if (temp.Count() != LanguagesSearched.Count())
                {
                    LanguagesSearched.Clear();
                    foreach (var card in temp)
                    {
                        LanguagesSearched.Add(card);
                    }
                }
            }
        }

        public string SelectedLanguage
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
