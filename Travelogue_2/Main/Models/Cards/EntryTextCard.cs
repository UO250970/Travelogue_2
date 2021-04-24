
namespace Travelogue_2.Main.Models.Entries
{
	public class EntryTextCard : IEntry
	{
		public int Id { get; set; }
		public string Time { get; set; }

		public EntryTextCard(int id)
		{
			Id = id;
			Time = "00:00";
		}

		public string Text { get; set; } = string.Empty;
	}
}
