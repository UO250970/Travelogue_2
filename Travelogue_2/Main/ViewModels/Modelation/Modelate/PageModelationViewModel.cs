using Plugin.Permissions.Abstractions;
using Syncfusion.SfImageEditor.XForms;
using System;
using System.Collections.Generic;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.PopUps;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation.Modelate
{
    [QueryProperty("BackgroundPath", "BackgroundPath")]
    [QueryProperty("NewPage", "NewPage")]
    [QueryProperty("PageNum", "PageNum")]
    public class PageModelationViewModel : ModelationSelectorViewModel
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

        private string newPage = string.Empty;
        public string NewPage
        {
            get => newPage;
            set
            {
                newPage = value;
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

        public override void LoadData()
        {
            if (BackgroundPath != null && NewPage != string.Empty)
            {
                if (NewPage.Equals(CommonVariables.True))
                {
                    ImageSelectedSource = ImageSource.FromResource(BackgroundPath);
                }
                else
                {
                    ImageSelectedSource = ImageSource.FromFile(BackgroundPath);
                }
                
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

        public async void SelectPhotos()
        {
            Shell.Current.GoToAsync($"{nameof(SelectImagePopUp)}");
        }

        public async void SelectText()
        {

        }


        public async void Save(ImageSavingEventArgs args)
        {
            DataBaseUtil.SavePage(args, GetName(), journalId);
        }

        public string GetName()
        {
            string journeyName = Journey.Name.Replace(" ", "");
            return journeyName + "_journal_" + PageNum;
        }

        internal override async void Back()
        {
            if (newPage.Equals(CommonVariables.True))
            {
                await Shell.Current.GoToAsync("../..");
            }
            else
            {
                base.Back();
            }
        }
    }
}
