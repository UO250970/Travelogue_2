using Syncfusion.SfCalendar.XForms;
using System.Globalization;
using Map = Xamarin.Forms.Maps.Map;

namespace Travelogue_2.Main.ViewModels.Media
{
    public class MediaViewModel : BaseViewModel
    {

		public CultureInfo localization => Resources.CurrentCultureI();

		public int localizationFirstDay => (int) Resources.CurrentCultureI().DateTimeFormat.FirstDayOfWeek;

		public void Refresh(SfCalendar calendar, Map map)
		{
			calendar.Refresh();
			//map = new Map(MapSpan.FromCenterAndRadius(GetPosition(), Distance.FromMiles(10)));
		}

	}
}
