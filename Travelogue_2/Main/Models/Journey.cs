using SQLite;
using System;
using Travelogue_2.Main.Models;

namespace Travelogue_2.Main.Domain
{
    [Table("Journey")]
    public class Journey
    {
        [PrimaryKey, AutoIncrement, Column("_id"), NotNull]
        public int Id { get; set; } = 0;

        [Column("State"), NotNull]
        public State JourneyState { get; set; } = State.CREATED;

        [MaxLength(40), NotNull]
        public string Name { get; set; } = string.Empty;

        [Column("Start_date"), NotNull]
        public DateTime Date_ini { get; set; } = DateTime.Today;

        [Column("End_date"), NotNull]
        public DateTime Date_end { get; set; } = DateTime.Today;
    }
}
