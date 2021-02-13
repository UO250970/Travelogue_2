namespace Travelogue_2.Main.Models
{
	public class DayCard
	{
		public int Id { get; set; }

		public string Day { get; set; }

		private string month;
		public string Month
		{
			get { return month; }
			set => month = App.LocResources["MonthShort_" + value];
		}

		public int Entries { get; set; }

		public string EntriesText 
		{
			get
			{
				return Entries + " " + App.LocResources["Entries"];
			} 
		}

		public int Events { get; set; }

		public string EventsText
		{
			get
			{
				return Events + " " + App.LocResources["Events"];
			}
		}

	}

}
