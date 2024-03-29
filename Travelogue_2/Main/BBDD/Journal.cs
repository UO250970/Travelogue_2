﻿using SQLite;
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
        public State JournalState { get; set; } = State.CREATED;

        [Column("Name"), NotNull]
        public string Name { get; set; } = string.Empty;

        public Journal() { }

        [ForeignKey(typeof(Journey))]
        public int JourneyId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Journey Journey { get; set; }

        [Column("Path")]
        public string Path { get; set; } = string.Empty;

        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
        public List<Image> Pages { get; set; } = new List<Image>();

        public Journal(Journey journey)
        {
            JourneyId = journey.Id;
            Journey = journey;
        }
    }
}
