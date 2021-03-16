using Syncfusion.SfCalendar.XForms;
using System;
using System.ComponentModel;
using System.Globalization;
using Travelogue_2.Main.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Travelogue_2.Main.ViewModels.Media
{
	public class MediaViewModel : DataBaseViewModel
	{
		public CalendarEventCollection CalendarJourneis { get; }

		public int localizationFirstDay;
		
		public MediaViewModel()
		{
			CalendarJourneis = new CalendarEventCollection();

			localization = Resources.CurrentCultureI();
			localizationFirstDay = (int)Resources.CurrentCultureI().DateTimeFormat.FirstDayOfWeek;

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
		}

		private CultureInfo localization;

		public CultureInfo Localization
		{
			get { return localization; }
			set { SetProperty(ref localization, value); }// SetProperty(ref _localization, Resources.CurrentCultureI()); }
		}

		internal Position GetPosition()
		{
			string loca = Resources.CurrentCulture();
			if (CommonVariables.AvailableLanguagesLocations.ContainsKey(loca.ToUpper()))
			{
				var temp = CommonVariables.AvailableLanguagesLocations[loca.ToUpper()];
				return new Position(temp.Latitude, temp.Longitude);
			} else
			{
				return new Position();
			}
		}

		protected void OnResourcesChanged(object s, PropertyChangedEventArgs e)
		{
			//SetProperty(ref localization, Resources.CurrentCultureI(), "localization");
		}

	}
}
