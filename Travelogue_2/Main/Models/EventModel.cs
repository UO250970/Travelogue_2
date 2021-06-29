using System;

namespace Travelogue_2.Main.Models
{
	public class EventModel
	{
		public int Id { get; set; } = 0;

		public string Time { get; set; } = string.Empty;

		public string Title { get; set; } = string.Empty;

		public string Address { get; set; } = string.Empty;

		public string PhoneNumber { get; set; } = string.Empty;

		public bool Reservation { get; set; } = false;

		public DateTime IniDay { get; set; }

		public DateTime EndDay { get; set; }

	}
}
