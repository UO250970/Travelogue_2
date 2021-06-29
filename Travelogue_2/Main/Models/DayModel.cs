using Java.Lang;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
	public class DayModel
	{
		public int Id { get; set; }

		public DateTime Date
        {
			get => new DateTime(Integer.ParseInt(year), Integer.ParseInt(MonthNum), Integer.ParseInt(Day));
        } 

		public string Day { get; set; }

		private string month;
		public string Month
		{
			get => month;
			set
			{
				month = App.LocResources["MonthShort_" + value];
				MonthNum = value;
			}
		}

		public string MonthNum { get; set; }

		private string year;
		public string Year
		{
			get => year;
			set => year = value;
		}

		public Color background = (Color) App.Current.Resources["PrimaryFaded"];
		public Color Background 
		{ 
			get => background;
			set => background = value;
		}

		public ObservableCollection<EntryModel> JourneyEntries { get; set; } = new ObservableCollection<EntryModel>();

		public ObservableCollection<EventModel> JourneyEvents { get; set; } = new ObservableCollection<EventModel>();

		public DateTime ToDateTime()
		{
			return new DateTime(int.Parse(Year), int.Parse(MonthNum), int.Parse(Day));
		}

		public void Select()
        {
			Background = (Color)Application.Current.Resources["Primary"];
		}

		public void Unselect()
        {
			Background = (Color)Application.Current.Resources["PrimaryFaded"];
		}

	}
}
