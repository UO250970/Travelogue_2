using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Travelogue_2.Main.Models.Entries;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models.Cards
{
	public class DayCard
	{
		public int Id { get; set; }

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
				//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Background"));
		}
		
		public int Entries { get; set; }

		public string EntriesText 
		{
			get
			{
				return Entries + " " + App.LocResources["Entries"];
			} 
		}

		public ObservableCollection<EntryCard> JourneyEntries { get; set; } = new ObservableCollection<EntryCard>();

		public int Events { get; set; }

		public string EventsText
		{
			get
			{
				return Events + " " + App.LocResources["Events"];
			}
		}

		public ObservableCollection<EventCard> JourneyEvents { get; set; } = new ObservableCollection<EventCard>();

		public event PropertyChangedEventHandler PropertyChanged;

		public DateTime ToDateTime()
		{
			return new DateTime(int.Parse(Year), int.Parse(MonthNum), int.Parse(Day));
		}

		public bool Equals(DayCard obj)
		{
			//Check for null and compare run-time types.
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				DayCard p = (DayCard)obj;
				return (Day == p.Day) && (Month == p.Month) && (Year == p.Year);
			}
		}

	}

}
