using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Travelogue_2.Main.BBDD
{
	[Table("Entry")]
	public class Entry
	{
		[PrimaryKey, AutoIncrement, Column("_id"), NotNull]
		public int Id { get; set; } = 0;

		[Column("Title"), MaxLength(40), NotNull]
		public string Title { get; set; } = string.Empty;

		[MaxLength(40), NotNull]
		public string Time { get; set; } = string.Empty;

		[ForeignKey(typeof(Day))]
		public int DayId { get; set; }

		[ManyToOne]
		public Day Day { get; set; }

		//[OneToMany(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
		//public List<EntryText> TextList { get; set; } = new List<EntryText>();

		[OneToMany(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
		public List<Image> ImageList { get; set; } = new List<Image>();

		public Entry() { }
	}
}
