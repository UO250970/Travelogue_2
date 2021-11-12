using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;

namespace Travelogue_2.Main.ViewModels.Modelation
{
    public class PdfViewModel : DataBaseViewModel
    {
        private string journalPath = string.Empty;
        public string JournalPath
        {
            get => journalPath;
            set => SetProperty(ref journalPath, value);
        }

        public override void LoadData()
        {
            JournalModel temp = DataBaseUtil.GetJournalById(int.Parse(CurrentJourneyId));
            JournalPath = temp.JournalPath;
            JournalName = temp.Name;
        }

        private string journalName = string.Empty;
        public string JournalName
        {
            get => journalName;
            set => SetProperty(ref journalName, value);
        }
    }
}
