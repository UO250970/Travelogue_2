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

		public Color Background = (Color)Application.Current.Resources["PrimaryFaded"];
		
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

		public event PropertyChangedEventHandler PropertyChanged;


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
