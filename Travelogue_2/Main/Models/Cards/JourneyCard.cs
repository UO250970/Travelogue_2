using System;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
	public class JourneyCard 
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public ImageSource Image { get; set; }

		public DateTime IniDate { get; set; }

		public DateTime EndDate { get; set; }

	}
}
