using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation.Modelate
{
    public class JournalModelationViewModel : PhotoRendererModel
    {
        private ObservableCollection<ImageModel> pages;
        public ObservableCollection<ImageModel> Pages
        {
            get => pages;
            set => SetProperty(ref pages, value);
        }
        public Command CreatePageCommand { get; set; }
        public Command EndJournalCommand { get; set; }
        public Command<ImageModel> PageTapped { get; set; }
        public Command<ImageModel> PageTappedDelete { get; set; }

        public JournalModelationViewModel()
        {
            Pages = new ObservableCollection<ImageModel>();

            CreatePageCommand = new Command(x => CreatePageC());
            EndJournalCommand = new Command(x => EndJournalC());

            PageTapped = new Command<ImageModel>(OnPageSelected);
            PageTappedDelete = new Command<ImageModel>(OnPageDelete);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            if (CurrentJourneyId != "0")
            {
                JournalModel journal = DataBaseUtil.GetJournalById(int.Parse(CurrentJourneyId));
                JourneyName = journal.Name;

                Pages.Clear();
                journal.Pages.ForEach(x => Pages.Add(x));

                PagesCount = string.Format(Resources["PagesCountMess"], Pages.Count);

                _ = int.Parse(PagesCount) > 0 ? CreateJournalIsEnabled = true : CreateJournalIsEnabled = false;
            }
        }

        #region JourneyName
        public string journeyName;

        public string JourneyName
        {
            get => journeyName;
            set => SetProperty(ref journeyName, value);
        }
        #endregion

        #region PagesCount
        public string pagesCount = "";
        public string PagesCount
        {
            get => pagesCount;
            set => SetProperty(ref pagesCount, value);
        }
        #endregion

        #region CreateJournalIsEnabled
        private bool createJournalIsEnabled = false;
        public bool CreateJournalIsEnabled
        {
            get => createJournalIsEnabled;
            set => SetProperty(ref createJournalIsEnabled, value);
        }
        #endregion

        #region Commands
        public async void CreatePageC()
        {
            await Shell.Current.GoToAsync($"{nameof(BackgroundSelectorView)}?{nameof(BackgroundSelectorViewModel.PageNum)}={Pages.Count}");
        }

        public void EndJournalC()
        {
            DataBaseUtil.CloseJournal(int.Parse(CurrentJourneyId), GetName());
        }
        #endregion

        async void OnPageSelected(ImageModel page)
        {
            if (page == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(PageModelationView)}?{nameof(PageModelationViewModel.BackgroundPath)}={page.Path}&" +
                                                                    $"{nameof(PageModelationViewModel.PageNum)}={page.Page}&" +
                                                                    $"{nameof(PageModelationViewModel.NewPage)}={CommonVariables.False}");
        }

        void OnPageDelete(ImageModel page)
        {
            if (page == null)
                return;

            DataBaseUtil.DeleteImageById(page.ImageId);
        }

        public string GetName()
        {
            return JourneyName + " Journal" + CommonVariables.JournalExtension;
        }

    }
}
