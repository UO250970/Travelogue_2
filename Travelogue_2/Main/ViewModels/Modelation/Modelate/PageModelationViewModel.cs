using System.Collections.Generic;
using System.IO;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation.Modelate
{
    [QueryProperty("BackgroundPath", "BackgroundPath")]
    [QueryProperty("PageNum", "PageNum")]
    public class PageModelationViewModel : DataBaseViewModel
    {
        private string backgroundPath;
        public string BackgroundPath
        {
            get => backgroundPath;
            set
            {
                backgroundPath = value;
                LoadData();
            }
        }

        private string pageNum;
        public string PageNum
        {
            get => pageNum;
            set => pageNum = value;
        }

        private string journalId;
        public string JournalId
        {
            get => journalId;
            set
            {
                journalId = value;
                Journey = DataBaseUtil.GetJourneyForJournal(int.Parse(JournalId));
            }
        }
        public JourneyModel Journey { get; set; }

        private ImageSource imageSelectedSource;
        public ImageSource ImageSelectedSource
        {
            get => imageSelectedSource;
            set => SetProperty(ref imageSelectedSource, value);
        }

        private Stream stream;
        public Stream Stream
        {
            get => stream;
            set => stream = value;
        }

        public override void LoadData()
        {
            if (BackgroundPath != null)
            {
                ImageSelectedSource = ImageSource.FromResource(BackgroundPath);
                JournalId = CurrentJourneyId;
            }
        }

        public List<EventModel> GetEvents()
        {
            return DataBaseUtil.GetEventsFromJourney(Journey.Id, false);
        }

        public List<EventModel> GetReservs()
        {
            return DataBaseUtil.GetEventsFromJourney(Journey.Id, true);
        }

        public List<EntryModel> GetEntries()
        {
            return DataBaseUtil.GetEntriesFromJourney(Journey.Id);
        }

        public string GetName()
        {
            CameraUtil.CheckPermissions();
            return Journey.Name + "_journal_" + PageNum;
        }

        internal override async void Back()
        {
            await Shell.Current.GoToAsync($"{nameof(JournalModelationView)}");
        }
    }
}
