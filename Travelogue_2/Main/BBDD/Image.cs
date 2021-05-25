using SQLite;
using System;

namespace Travelogue_2.Main.BBDD
{
    [Table("Image")]
    public class Image
    {
        [PrimaryKey, AutoIncrement, Column("_id"), NotNull]
        public int Id { get; set; }

        [Column("Path")]
        public string Path { get; set; } = string.Empty;

        [Column("Name")]
        public string Name { get; set; } = string.Empty;

        [Column("Caption")]
        public string Caption { get; set; } = string.Empty;

        [Column("Date")]
        public DateTime Date { get; set; }

        /** Constructor público sin parámetros necesario para la base de datos */
        public Image() : this("", "") { }

        public Image(string path, string name)
        {
            Path = path;
            Name = name;
            Date = DateTime.Now;
        }

    }
}
