using System.Collections.ObjectModel;

namespace Travelogue_2.Main.Models
{
	public class EntryModel 
	{
		public int Id { get; set; }

		public string Time { get; set; }

		public string Title { get; set; } = string.Empty;

		public ObservableCollection<IEntry> Content { get; set; } = new ObservableCollection<IEntry>();
	}
}
