
using System.Collections.ObjectModel;

namespace Travelogue_2.Main.Models.Entries
{
	public class EntryCard : IEntry
	{
		public string Title { get; set; } = "";

		public ObservableCollection<EntryTextCard> Content { get; set; } = new ObservableCollection<EntryTextCard>();
	}
}
