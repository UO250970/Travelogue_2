using System.Collections.Generic;

namespace Travelogue_2.Main.Models
{
	public class Destiny
	{
		public string Code { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;

		public string Flag { get; set; } = string.Empty;

		public string Currency { get; set; } = string.Empty;

		public bool Original { get; set; } = true;

		public List<Embassy> Embassies { get; set; }
	}
}
