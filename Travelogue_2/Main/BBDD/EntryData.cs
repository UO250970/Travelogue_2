using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Travelogue_2.Main.BBDD
{
	[Table("EntryData")]
	public class EntryData
	{
		[PrimaryKey, AutoIncrement, Column("_id"), NotNull]
		public int Id { get; set; } = 0;

		[MaxLength(40), NotNull]
		public string Time { get; set; } = string.Empty;

		[ForeignKey(typeof(Entry))]
		public int EntryId { get; set; }

		[ManyToOne]
		public Entry Entry { get; set; }

		[Column("Text")]
		public string Text { get; set; } = string.Empty;

		[ForeignKey(typeof(Image))]
		public int ImageId { get; set; }

		[OneToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
		public Image Image { get; set; }

		public EntryData() { }
	}
}
