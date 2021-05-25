using Travelogue_2.Main.Models.Entries;

namespace Travelogue_2.Main.Models
{
	public class EntryImageModel : ImageModel, IEntry
	{
		public int Id { get; set; }
		public string Time { get; set; }

		public EntryImageModel()
		{
			Time = "00:00";
		}

		public EntryImageModel(int id)
		{
			Id = id;
			Time = "00:00";
		}

		public string ImageFoot { get; set; } = string.Empty;

		public string JourneyName { get; set; } = App.LocResources["NoJourneyAssociated"];
	}
}
