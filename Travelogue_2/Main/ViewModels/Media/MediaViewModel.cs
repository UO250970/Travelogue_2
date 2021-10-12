using Syncfusion.SfCalendar.XForms;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Travelogue_2.Main.ViewModels.Media
{
	public class MediaViewModel : PhotoRendererModel
	{
		public CalendarEventCollection CalendarJourneis { get; set; }

		public int localizationFirstDay;
		public Command SearchJourneyCommand { get; }
		public ObservableDictionary<string, List<ImageModel>> ImagesOrdered { get; set; }
		public ObservableDictionary<string, List<ImageModel>> ImagesSearched { get; set; }

		public MediaViewModel()
		{
			CalendarJourneis = new CalendarEventCollection();

			localization = Resources.CurrentCultureI();
			localizationFirstDay = (int)Resources.CurrentCultureI().DateTimeFormat.FirstDayOfWeek;

			SearchJourneyCommand = new Command(() => SearchJourneyC());

			ImagesOrdered = new ObservableDictionary<string, List<ImageModel>>();
			ImagesSearched = new ObservableDictionary<string, List<ImageModel>>();

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			CalendarJourneis.Clear();
			CalendarJourneis = CalendarUtil.GetJourneis();
			            
			ImagesOrdered.Clear();
			ImagesOrdered = DataBaseUtil.GetJourneisWithImages();
			ImagesSearched = ImagesOrdered;
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
					foreach ( string key in keysToRemove)
					{
						ImagesSearched.Remove(key);
					}
				}
			}
		}
		#endregion

		internal Position GetPosition()
		{
			return GeolocalizationUtil.GetPosition();
		}

		/*public void OnAppearing()
		{
			LoadData();
		}*/

	}
}
