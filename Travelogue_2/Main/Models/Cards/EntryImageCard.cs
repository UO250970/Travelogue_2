using Travelogue_2.Main.Models.Entries;

namespace Travelogue_2.Main.Models.Cards
{
	public class EntryImageCard : ImageCard, IEntry
	{
		public int Id { get; set; }
		public string Time { get; set; }

		public EntryImageCard()
		{
			Time = "00:00";
		}

		public EntryImageCard(int id)
		{
			Id = id;
			Time = "00:00";
		}

		public string ImageFoot { get; set; } = string.Empty;

		public string JourneyName { get; set; } = App.LocResources["NoJourneyAssociated"];
	}
}
