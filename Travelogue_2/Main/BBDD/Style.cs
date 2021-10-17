using SQLite;

namespace Travelogue_2.Main.BBDD
{
    [Table("Style")]
    public class Style
    {
        [PrimaryKey, AutoIncrement, Column("Name"), NotNull]
        public string Name { get; set; }

        [Column("Primary")]
        public string Primary { get; set; } // #3D6D9B - azul, #D075BF - rosa, #8DD075 - verde

        [Column("PrimaryFaded")]
        public string PrimaryFaded { get; set; } // #B2D4F4

        [Column("Secondary")]
        public string Secondary { get; set; } // #247c7c

    }
}
