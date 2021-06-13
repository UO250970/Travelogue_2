using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Travelogue_2.Main.Models
{
	[Table("Embassy")]
	public class Embassy
	{
		[PrimaryKey, AutoIncrement, Column("_id"), NotNull]
		public int Id { get; set; } = 0;

		[ForeignKey(typeof(Destiny))]
		public string DestinyId { get; set; }

		[ManyToOne]
		public Destiny Destiny { get; set; }

		[Column("Country")]
		public string Country { get; set; } = string.Empty;

		[Column("City")]
		public string City { get; set; } = string.Empty;

		[Column("PhoneNumber")]
		public string PhoneNumber { get; set; } = string.Empty;
		
		public Embassy() { }
	}
}
