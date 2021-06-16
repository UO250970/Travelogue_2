using Travelogue_2.Main.Models.Entries;

namespace Travelogue_2.Main.Models
{
	public class EntryImageModel : ImageModel, IEntry
	{
		public int Id { get; set; }

		public string Time { get; set; } = "00:00";

		public string JourneyName { get; set; } = App.LocResources["NoJourneyAssociated"];
	}
}
