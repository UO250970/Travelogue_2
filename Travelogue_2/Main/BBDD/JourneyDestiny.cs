using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Travelogue_2.Main.BBDD
{
    [Table("JourneyDestiny")]
    public class JourneyDestiny
    {
        [ForeignKey(typeof(Journey))]
        public int Journey { get; set; }

        [ForeignKey(typeof(Destiny))]
        public string Destiny { get; set; }
    }
}
