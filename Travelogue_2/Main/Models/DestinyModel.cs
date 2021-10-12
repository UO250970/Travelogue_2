using System.Collections.Generic;
using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
	public class DestinyModel
	{
		public string Code { get; set; } = "";

		public string Destiny { get; set; } = "";

		public string Currency { get; set; } = "";

		public ImageSource Flag { get; set; }

		public string MoreInfoCountry 
		{ 
			get => CommonVariables.CountryWebSite + Code.ToLower(); 
		}

		public Dictionary<string, string> Embassies { get; set; } = new Dictionary<string, string>();

		public List<string> EmbassiesCities { get; set; } = new List<string>();
	}
}
