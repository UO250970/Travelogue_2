using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
    public class ContinueModelationViewModel : DataBaseViewModel
    {

        public Command<JournalModel> JourneyTapped { get; }
        public ObservableCollection<JournalModel> ContinueJournals { get; set; }

        public ContinueModelationViewModel()
        {
            ContinueJournals = new ObservableCollection<JournalModel>();

            JourneyTapped = new Command<JournalModel>(OnJournalSelected);

            ExecuteLoadDataCommand();
        }
        public override void LoadData()
        {
            ContinueJournals.Clear();
            DataBaseUtil.GetJournalsToContinue()?.ForEach(x => ContinueJournals.Add(x));
        }

        async void OnJournalSelected(JournalModel journal)
        {
            if (journal == null)
                return;

            CurrentJourneyId = journal.Id.ToString();
            await Shell.Current.GoToAsync($"{nameof(JournalModelationView)}");
        }

    }
}
