﻿using SQLite;
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

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
        public Journey Journey { get; set; }

        [ManyToMany(typeof(DayEvent), CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
        public List<Event> Events { get; set; } = new List<Event>();

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

        public override bool Equals(object obj)
        {
            DateTime temp = ((Day)obj).Date;
            return (temp.Year == this.Date.Year) && (temp.Month == this.Date.Month) && (temp.Day == this.Date.Day);
        }
    }
}
