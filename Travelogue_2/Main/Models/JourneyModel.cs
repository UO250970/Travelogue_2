using System;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
	public class JourneyModel 
	{
		public int Id { get; set; }

		public State JourneyState { get; set; }

		public string Name { get; set; }

		public int CoverId { get; set; }

		public ImageSource CoverSource { get; set; }

		public DateTime IniDate { get; set; }

		public DateTime EndDate { get; set; }

        public JourneyModel()
		{
			Id = -1;
		}

		public override bool Equals(object obj)
		{
			//Check for null and compare run-time types.
			if ((obj == null) || !GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				JourneyModel p = (JourneyModel)obj;
				return Id == p.Id;
			}
		}
	}
}
