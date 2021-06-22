using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Travelogue_2.Main.BBDD
{
    [Table("DayEvent")]
    public class DayEvent
    {
        [ForeignKey(typeof(Day))]
        public int Day { get; set; }

        [ForeignKey(typeof(Event))]
        public int Event { get; set; }
    }
}
