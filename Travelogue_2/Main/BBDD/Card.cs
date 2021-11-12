using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Travelogue_2.Main.BBDD
{
    [Table("Cards")]
    public class Card
    {
        [PrimaryKey, AutoIncrement, Column("_id"), NotNull]
        public int Id { get; set; } = 0;

        [Column("Title"), NotNull]
        public string Name { get; set; } = string.Empty;

        [ForeignKey(typeof(Image))]
        public int Top { get; set; }

        [ForeignKey(typeof(Image))]
        public int Back { get; set; }
    }
}
