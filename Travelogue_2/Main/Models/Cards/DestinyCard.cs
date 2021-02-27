using System.Collections.Generic;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
	public class DestinyCard
	{
		public string Code { get; set; }

		public string Destiny { get; set; }

		public string Currency { get; set; }

		public ImageSource Flag
		{
			get => CommonVariables.GetFlag(Destiny);
		}

		public string MoreInfoCountry { get => CommonVariables.CountryWebSite + Code.ToLower(); }

		public Dictionary<string, string> Embassies { get; set; }

		public List<string> EmbassiesCities { get; set; }
	}
}
