using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
    public class EndedModelationViewModel : DataBaseViewModel
    {

        public Command<JournalModel> JournalTapped { get; }
        public ObservableCollection<JournalModel> ClosedJournals { get; set; }

        public EndedModelationViewModel()
        {
            ClosedJournals = new ObservableCollection<JournalModel>();

            JournalTapped = new Command<JournalModel>(OnJourneySelected);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            ClosedJournals.Clear();
            DataBaseUtil.GetJournalsClosed()?.ForEach(x => ClosedJournals.Add(x));
        }

        async void OnJourneySelected(JournalModel journal)
        {
            if (journal == null)
                return;

            CurrentJourneyId = journal.Id.ToString();
            await Shell.Current.GoToAsync($"{nameof(JournalModelationView)}");
        }
    }
}
