using System.ComponentModel;
using System.Globalization;

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

		protected void OnResourcesChanged(object s, PropertyChangedEventArgs e)
		{
			//SetProperty(ref localization, Resources.CurrentCultureI(), "localization");
		}

	}
}
