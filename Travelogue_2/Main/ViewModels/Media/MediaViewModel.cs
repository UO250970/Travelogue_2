using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
		public CalendarEventCollection CalendarJourneis { get; }

		public int localizationFirstDay;
		public Command SearchJourneyCommand { get; }
		public ObservableDictionary<string, List<EntryImageModel>> ImagesOrdered { get; set; }
		public ObservableCollection<EntryImageModel> Images { get; }
		public ObservableCollection<EntryImageModel> ImagesSearched { get; set; }

		public MediaViewModel()
		{
			CalendarJourneis = new CalendarEventCollection();

			localization = Resources.CurrentCultureI();
			localizationFirstDay = (int)Resources.CurrentCultureI().DateTimeFormat.FirstDayOfWeek;

			SearchJourneyCommand = new Command(() => SearchJourneyC());

			ImagesOrdered = new ObservableDictionary<string, List<EntryImageModel>>();
			Images = new ObservableCollection<EntryImageModel>();
			ImagesSearched = new ObservableCollection<EntryImageModel>();

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			CalendarInlineEvent jour = new CalendarInlineEvent();
			jour.AutomationId = "1";
			jour.StartTime = DateTime.Today;
			jour.EndTime = DateTime.Today.AddDays(4);
			jour.Subject = "Prueba 5";
			jour.Color = Color.FromHex("#3D6D9B");

			CalendarJourneis.Add(jour);

			Images.Clear();
			EntryImageModel image = new EntryImageModel();
			Images.Add(image);

			List<string> viajes = new List<string>();
			viajes.Add(App.LocResources["NoJourneyAssociated"]);

			foreach (string s in viajes)
			{
				var tempList = Images.Where(x => x.JourneyName.ToUpper().Equals(s.ToUpper())).ToList();
				ImagesOrdered.Add(s, tempList);
			}
		}

		#region calendar
		private CultureInfo localization;

		public CultureInfo Localization
		{
			get { return localization; }
			set { SetProperty(ref localization, value); }// SetProperty(ref _localization, Resources.CurrentCultureI()); }
		}
		#endregion

		#region gallery
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
				var temp = Images.Where(x => x.JourneyName.ToUpper().Contains(searchText.ToUpper()) == true);
				if (temp.Count() != ImagesSearched.Count())
				{
					ImagesSearched.Clear();
					foreach (var card in temp)
					{
						ImagesSearched.Add(card);
					}

					ImagesOrdered.Clear();

					List<string> viajes = new List<string>();
					viajes.Add(App.LocResources["NoJourneyAssociated"]);

					foreach (string s in viajes)
					{
						var tempList = Images.Where(x => x.JourneyName.ToUpper().Equals(s.ToUpper())).ToList();
						ImagesOrdered.Add(s, tempList);
					}
				}
			}
		}
		#endregion

		internal Position GetPosition()
		{
			return GeolocalizationUtil.GetPosition();
		}

		protected void OnResourcesChanged(object s, PropertyChangedEventArgs e)
		{
			//SetProperty(ref localization, Resources.CurrentCultureI(), "localization");
		}

	}
}
