using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
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

        public bool Reserv { get; set; } = false;

        [Column("Address")]
        public string Address { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [ManyToMany(typeof(DayEvent), CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
        public List<Day> Days { get; set; } = new List<Day>();

        public Event() { }


        public override bool Equals(object obj)
        {
            try
            {
                Event temp = (Event) obj;
                bool Title = this.Title.Equals(temp.Title);
                bool Time = this.Title.Equals(temp.Time);
                bool Address = this.Title.Equals(temp.Address);
                bool Reserva = this.Reserv.Equals(temp.Reserv);
                bool PhoneNumber = this.Title.Equals(temp.PhoneNumber);

                return Title && Time && Address && Reserva && PhoneNumber;
            } catch
            {
                return false;
            }
            
        }
    }
}
