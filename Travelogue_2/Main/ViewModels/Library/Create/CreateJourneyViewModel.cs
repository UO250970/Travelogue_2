using System;
using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library.Create
{
    public class CreateJourneyViewModel : PhotoRendererModel
    {
        public Command AddCoverCommand { get; }
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }

        public ObservableCollection<DayModel> DaysSelected { get; }

        public CreateJourneyViewModel()
        {
            AddCoverCommand = new Command(() => AddCoverC());

            CancelCommand = new Command(() => Back());
            SaveCommand = new Command(() => SaveC());

            ExecuteLoadDataCommand();
        }

        #region CoverImage
        public ImageModel coverImage = new ImageModel();
        public ImageModel CoverImage
        {
            get => coverImage;
            set
            {
                SetProperty(ref coverImage, value);
            }
        }
        #endregion

        #region Title
        private string title = string.Empty;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
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
        private DateTime iniDate;
        public DateTime IniDate
        {
            get => iniDate;
            set => SetProperty(ref iniDate, value);
        }
        #endregion

        #region EndDate
        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set => SetProperty(ref endDate, value);
        }
        #endregion

        #region ImageVisible
        private bool imageVisible = false;
        public bool ImageVisible
        {
            get => imageVisible;
            set => SetProperty(ref imageVisible, value);
        }
        #endregion

        #region LabelVisible
        private bool labelVisible = true;
        public bool LabelVisible
        {
            get => labelVisible;
            set => SetProperty(ref labelVisible, value);
        }
        #endregion

        public override void LoadData()
        {
            IniDate = DataBaseUtil.GetNextDayAvailable();
            EndDate = IniDate;
        }

        async internal void AddCoverC()
        {
            ImageModel success = await DataBaseUtil.Photo(this, true);
            if (success != null)
            {
                CoverImage = success;
            }
        }

        async internal void SaveC()
        {
            if (title.Equals(string.Empty))
            {
                await Alerter.AlertNoNameInJourney();
            }
            else
            {
                JourneyModel temp = new JourneyModel()
                {
                    Id = int.Parse(CurrentJourneyId),
                    Name = Title,
                    IniDate = IniDate,
                    EndDate = EndDate,
                    CoverId = CoverImage.ImageId
                };
                if (await DataBaseUtil.SaveJourney(temp))
                    base.Back();
            }
        }

        internal void CheckNewIniDate(DatePicker iniDatePicker, DatePicker endDatePicker)
        {
            if (iniDatePicker.Date.CompareTo(EndDate.Date) > 0) endDatePicker.Date = iniDatePicker.Date;
        }

        internal void CheckNewEndDate(DatePicker iniDatePicker, DatePicker endDatePicker)
        {
            if (iniDatePicker.Date.CompareTo(EndDate.Date) > 0) iniDatePicker.Date = endDatePicker.Date;
        }

        internal override void Back()
        {
            DataBaseUtil.DeleteJourney(int.Parse(CurrentJourneyId));
            base.Back();
        }
    }
}
