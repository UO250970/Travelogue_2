using System;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
	public class JourneyModel 
	{
		public int Id { get; set; }

		public State JourneyState { get; set; }

		public string Name { get; set; }

		public ImageSource Image { get; set; }

		public DateTime IniDate { get; set; }

		public DateTime EndDate { get; set; }

        public JourneyModel()
		{
			Id = -1;
		}
	}
}
