namespace Travelogue_2.Main.Models
{
    public class EntryTextModel : IEntry
    {
        public int Id { get; set; }

        public string Time { get; set; }

        public string Text { get; set; } = string.Empty;
    }
}
