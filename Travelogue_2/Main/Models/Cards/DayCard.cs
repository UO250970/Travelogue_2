using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
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

	}

}
