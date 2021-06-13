﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace Travelogue_2.Main.BBDD
{
    [Table("Day")]
    public class Day
    {
		[PrimaryKey, AutoIncrement, Column("_id"), NotNull]
		public int Id { get; set; }

		[NotNull]
		public DateTime Date { get; set; } = DateTime.Today;

		[ForeignKey(typeof(Journey))]
		public int JourneyId { get; set; }

		[ManyToOne]
		public Journey Journey { get; set; }

		/**[OneToMany(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
		public List<Event_Info> EventList { get; set; } = new List<Event_Info>();

		[ManyToMany(typeof(DayReser), CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
		public List<Reser_Info> ReserList { get; set; } = new List<Reser_Info>();

		//[OneToMany(CascadeOperations = CascadeOperation.All)]
		[OneToMany(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
		public List<Entry> Entries { get; set; } = new List<Entry>();*/

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
