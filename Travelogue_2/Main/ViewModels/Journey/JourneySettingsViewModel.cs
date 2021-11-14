using System;
using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journey
{
    public class JourneySettingsViewModel : PhotoRendererModel
    {
        public Command ModifyCoverCommand { get; }
        public Command MoreInfoCommand { get; }
        public Command PhoneNumberTappedCommand { get; }
        public Command DeleteJourneyCommand { get; }
        public ObservableCollection<DestinyModel> JourneyDestinies { get; set; }

        public JourneySettingsViewModel()
        {
            coverImage = new EntryImageModel();
            JourneyDestinies = new ObservableCollection<DestinyModel>();

            ModifyCoverCommand = new Command(x => ModifyCoverC());
            MoreInfoCommand = new Command<string>((x) => MoreInfoC(x));
            PhoneNumberTappedCommand = new Command<string>((x) => PhoneNumberTappedC(x));
            DeleteJourneyCommand = new Command(x => DeleteJourneyC());

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            Journey = DataBaseUtil.GetJourneyById(int.Parse(CurrentJourneyId));
            JourneyName = Journey.Name;
            State = Journey.JourneyState;

            if (Journey.CoverId >= 1)
            {
                ImageModel cover = DataBaseUtil.GetImageById(journey.CoverId);
                CoverImage = cover;
            }

            IniDate = Journey.IniDate;
            IniDateEnabled = !State.Equals(State.CLOSED) && !State.Equals(State.OPEN);
            EndDateEnabled = !State.Equals(State.CLOSED);
            EndDate = Journey.EndDate;
        }

        private JourneyModel journey;

        public JourneyModel Journey
        {
            get => journey;
            set => SetProperty(ref journey, value);
        }

        #region State
        private State State { get; set; }
        #endregion

        #region JourneyName
        public string journeyName;

        public string JourneyName
        {
            get => journeyName;
            set
            {
                SetProperty(ref journeyName, value);
                Journey.Name = JourneyName;
            }
        }
        #endregion

        #region CoverImage
        private ImageModel coverImage = new ImageModel();
        public ImageModel CoverImage
        {
            get => coverImage;
            set
            {
                SetProperty(ref coverImage, value);
                Journey.CoverId = coverImage.ImageId;
            }
        }
        #endregion

        #region MinimumDate
        private DateTime minimumDate = DateTime.Today;
        public DateTime MinimumDate
        {
            get => minimumDate;
            set => SetProperty(ref minimumDate, value);
        }
        #endregion

        #region IniDate
        private bool iniDateEnabled = true;
        public bool IniDateEnabled
        {
            get => iniDateEnabled;
            set => SetProperty(ref iniDateEnabled, value);
        }

        private DateTime iniDate = DateTime.Today;
        public DateTime IniDate
        {
            get => iniDate;
            set
            {
                if (value.CompareTo(minimumDate) < 0) MinimumDate = iniDate;
                SetProperty(ref iniDate, value);
                Journey.IniDate = IniDate;
            }
        }
        #endregion

        #region EndDate
        private bool endDateEnabled = true;
        public bool EndDateEnabled
        {
            get => endDateEnabled;
            set => SetProperty(ref endDateEnabled, value);
        }

        private DateTime endDate = DateTime.Today;
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                SetProperty(ref endDate, value);
                Journey.EndDate = EndDate;
            }
        }
        #endregion

        async internal void ModifyCoverC()
        {
            ImageModel success = await DataBaseUtil.Photo(this);
            if (success != null)
            {
                CoverImage = success;
            }
        }

        async internal void MoreInfoC(string path)
        {
            await Browser.OpenAsync(path);
        }

        internal void PhoneNumberTappedC(string number)
        {
            PhoneDialer.Open(number);
        }

        internal void DeleteJourneyC()
        {
            DataBaseUtil.DeleteJourney(int.Parse(CurrentJourneyId), true);
            base.Back();
        }

        internal void CheckNewIniDate(DatePicker iniDatePicker, DatePicker endDatePicker)
        {
            if (iniDatePicker.Date.CompareTo(endDatePicker.Date) > 0)
                endDatePicker.Date = iniDatePicker.Date;
        }

        internal void CheckNewEndDate(DatePicker iniDatePicker, DatePicker endDatePicker)
        {
            if (endDatePicker.Date.CompareTo(iniDatePicker.Date) < 0)
                iniDatePicker.Date = endDatePicker.Date;
        }

        internal override async void Back()
        {
            if (await DataBaseUtil.SaveJourney(Journey) && DataBaseUtil.SaveImage(CoverImage)) base.Back();
        }

    }
}
