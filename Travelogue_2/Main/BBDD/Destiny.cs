using SQLite;
using System.Collections.Generic;

namespace Travelogue_2.Main.Models
{
	[Table("Destiny")]
	public class Destiny
	{
		[PrimaryKey, Column("_iso2"), NotNull, MaxLength(5)]
		public string Code { get; set; } = string.Empty;

		[Column("Name"), NotNull, MaxLength(30)]
		public string Name { get; set; } = string.Empty;

		[Column("Flag")]
		public string Flag { get; set; } = string.Empty;

		[Column("Currency"), MaxLength(15)]
		public string Currency { get; set; } = string.Empty;

		public bool Original { get; set; } = true;

		public List<Embassy> Embassies { get; set; }
	}
}
