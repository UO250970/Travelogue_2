namespace Travelogue_2.Main.Models
{
	public class EntryImageModel : ImageModel, IEntry
	{
		public int Id { get; set; }

		public string Time { get; set; } = "00:00";
	}
}
