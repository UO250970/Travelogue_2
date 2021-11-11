using Syncfusion.SfCalendar.XForms;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Journey;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;

namespace Travelogue_2.Main.ViewModels.Media
{
    public class MediaViewModel : PhotoRendererModel
    {
        public CalendarEventCollection CalendarJourneis { get; set; }

        public int localizationFirstDay;
        public Command SearchJourneyCommand { get; }
        public Command<string> JourneyTapped { get; }

        public ObservableDictionary<string, List<ImageModel>> ImagesOrdered { get; set; }
        private ObservableDictionary<string, List<ImageModel>> imagesSearched;
        public ObservableDictionary<string, List<ImageModel>> ImagesSearched
        {
            get => imagesSearched;
            set => SetProperty(ref imagesSearched, value);
        }

        public MediaViewModel()
        {
            CalendarJourneis = new CalendarEventCollection();

            localization = Resources.CurrentCultureI();
            localizationFirstDay = (int)Resources.CurrentCultureI().DateTimeFormat.FirstDayOfWeek;

            SearchJourneyCommand = new Command(() => SearchJourneyC());

            ImagesOrdered = new ObservableDictionary<string, List<ImageModel>>();
            ImagesSearched = new ObservableDictionary<string, List<ImageModel>>();

            JourneyTapped = new Command<string>(OnJourneySelected);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            CalendarJourneis.Clear();
            DataBaseUtil.GetCalendarJourneis().ForEach(x => CalendarJourneis.Add(x));

            ImagesOrdered.Clear();
            ImagesSearched.Clear();
            ObservableDictionary<string, List<ImageModel>> temp = DataBaseUtil.GetJourneisWithImages();

            temp.Keys.ForEach(x =>
            {
                ImagesOrdered.Add(x, temp[x]);
                ImagesSearched.Add(x, temp[x]);
            });
        }

        #region calendar
        private CultureInfo localization;

        public CultureInfo Localization
        {
            get { return localization; }
            set { SetProperty(ref localization, value); }// SetProperty(ref _localization, Resources.CurrentCultureI()); }
        }
        #endregion

        #region Gallery
        async internal void SearchJourneyC()
        {
            SearchText = string.Empty;
            SearchVisible = !SearchVisible;
        }

        private bool searchVisible = false;

        public bool SearchVisible
        {
            get => searchVisible;
            set => SetProperty(ref searchVisible, value);
        }

        private string searchText = string.Empty;
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                if (ImagesOrdered.Count != 0)
                {
                    ImagesSearched = ImagesOrdered;
                    List<string> keysToRemove = ImagesOrdered.Keys.Where(x => !x.ToUpper().Contains(searchText.ToUpper())).ToList();
                    foreach (string key in keysToRemove)
                    {
                        ImagesSearched.Remove(key);
                    }
                }
            }
        }
        #endregion

        #region Localization
        public int LocalizationFirstDay
        {
            get => int.Parse(Resources["LocalizationFirstDay"]);
        }
        #endregion

        internal Position GetPosition()
        {
            Shell.Current.GoToAsync("..");
            return DataBaseUtil.GetPosition();
        }

        internal List<CustomPin> GetMapPins()
        {
            return DataBaseUtil.GetMapPins();
        }

        async void OnJourneySelected(string journeyId)
        {
            if (journeyId == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            CurrentJourneyId = journeyId;
            await Shell.Current.GoToAsync($"{nameof(JourneyView)}");
        }

        public void OnAppearing()
        {
            LoadData();
        }

    }
}
