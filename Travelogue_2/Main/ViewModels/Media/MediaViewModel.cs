using System;
using System.ComponentModel;
using System.Globalization;
using Travelogue_2.Main.Services;
using Xamarin.Forms.Maps;

namespace Travelogue_2.Main.ViewModels.Media
{
    public class MediaViewModel : BaseViewModel
    {

		private CultureInfo _localization;

		public CultureInfo localization
		{
			get { return _localization; }
			set { _localization = value; OnPropertyChanged("localization"); }// SetProperty(ref _localization, Resources.CurrentCultureI()); }
		}

		public int localizationFirstDay;
		
		public MediaViewModel()
		{
			localization = Resources.CurrentCultureI();
			localizationFirstDay = (int)Resources.CurrentCultureI().DateTimeFormat.FirstDayOfWeek;
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
