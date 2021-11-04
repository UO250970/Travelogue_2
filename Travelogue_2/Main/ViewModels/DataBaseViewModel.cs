using System;
using System.Diagnostics;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels
{
    public abstract class DataBaseViewModel : BaseViewModel
    {

        public string CurrentJourneyId
        {
            get => DataBaseUtil.GetCurrentJourneyId();
            set
            {
                DataBaseUtil.SetCurrentJourneyId(value);
            }
        }

        public void ExecuteLoadDataCommand()
        {

            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public virtual void OnAppearing()
        {
            ExecuteLoadDataCommand();
        }

        public abstract void LoadData();

        internal virtual async void Back() => await Shell.Current.GoToAsync("..");

    }
}
