using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.ViewModels.Modelation.Modelate;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
    public class StartModelationViewModel : DataBaseViewModel
    {

        public Command<JournalModel> JournalTapped { get; }
        public ObservableCollection<JournalModel> StartJournals { get; set; }

        public StartModelationViewModel()
        {
            StartJournals = new ObservableCollection<JournalModel>();

            JournalTapped = new Command<JournalModel>(OnJournalSelected);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            StartJournals.Clear();

            DataBaseUtil.GetJournalsToStart()?.ForEach(x => StartJournals.Add(x));
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
