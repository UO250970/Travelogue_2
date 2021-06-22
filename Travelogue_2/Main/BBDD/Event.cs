using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Travelogue_2.Main.BBDD
{
    [Table("Event")]
    public class Event
    {
        [PrimaryKey, AutoIncrement, Column("_id"), NotNull]
        public int Id { get; set; } = 0;

        [Column("Title"), MaxLength(40), NotNull]
        public string Title { get; set; } = string.Empty;

        [MaxLength(40), NotNull]
        public string Time { get; set; } = string.Empty;

        [Column("Address")]
        public string Address { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [ManyToMany(typeof(DayEvent), CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
        public List<Day> Days { get; set; } = new List<Day>();

        public Event() { }
    }
}
