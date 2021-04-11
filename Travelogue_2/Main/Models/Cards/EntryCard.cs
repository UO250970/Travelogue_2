
using System.Collections.ObjectModel;

namespace Travelogue_2.Main.Models.Entries
{
	public class EntryCard : IEntry
	{
		public string Title { get; set; } = string.Empty;

		public ObservableCollection<IEntry> Content { get; set; } = new ObservableCollection<IEntry>();
	}
}
