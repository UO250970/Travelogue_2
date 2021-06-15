using System.Collections.ObjectModel;
using Travelogue_2.Main.Models.Entries;

namespace Travelogue_2.Main.Models
{
	public class EntryModel : IEntry
	{
		public int Id { get; set; }
		public string Time { get; set; }

		public string Title { get; set; } = string.Empty;

		public ObservableCollection<IEntry> Content { get; set; } = new ObservableCollection<IEntry>();
	}
}
