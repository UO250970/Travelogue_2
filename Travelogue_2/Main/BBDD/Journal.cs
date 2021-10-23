using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using Travelogue_2.Main.Models;

namespace Travelogue_2.Main.BBDD
{
	[Table("Journal")]
	public class Journal
	{

		[PrimaryKey, AutoIncrement, Column("_id"), NotNull]
		public int Id { get; set; } = 0;

		[Column("State"), NotNull]
		public State JourneyState { get; set; } = State.CREATED;

		[Column("Name"), MaxLength(40), NotNull]
		public string Name { get; set; } = string.Empty;

		public Journal() { }

		[ForeignKey(typeof(Journey))]
		public int JourneyId { get; set; }

		[OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
		public Journey Journey { get; set; }

		//[Column("Path"), MaxLength(40), NotNull]
		//public string Path { get; set; } = string.Empty;

		[TextBlob("Pages")]
		public List<string> Pages { get; set; } = new List<string>();

		public Journal(Journey journey)
        {
			JourneyId = journey.Id;
			Journey = journey;
        }
	}
}
