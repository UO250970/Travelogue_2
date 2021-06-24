using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Travelogue_2.Main.BBDD
{
    [Table("Day")]
    public class Day
    {
		[PrimaryKey, AutoIncrement, Column("_id"), NotNull]
		public int Id { get; set; } = 0;

		[NotNull, Unique]
		public DateTime Date { get; set; } = DateTime.Today;

		[ForeignKey(typeof(Journey))]
		public int JourneyId { get; set; }

		[ManyToOne]
		public Journey Journey { get; set; }

		[ManyToMany(typeof(DayEvent), CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
		public List<Event> Events { get; set; } = new List<Event>();

		/**[ManyToMany(typeof(DayReser), CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
		public List<Reser_Info> ReserList { get; set; } = new List<Reser_Info>();*/

		[OneToMany(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
		public List<Entry> Entries { get; set; } = new List<Entry>();

		public Day() { }

		/** Constructor público con un parámetro
         * Date - Fecha asociada dentro del viaje */
		public Day(DateTime date) : base()
		{
			Date = date;
			//Entries = new List<Entry>();
		}
	}
}
