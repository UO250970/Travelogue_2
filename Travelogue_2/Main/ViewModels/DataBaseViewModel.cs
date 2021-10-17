using System;
using System.Diagnostics;
using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels
{
	public abstract class DataBaseViewModel : BaseViewModel
	{
        public string CurrentJourneyId
		{
			get => DayTracker.CurrentJourneyId;
			set
			{
                DayTracker.CurrentJourneyId = value;
            }
        }

        public void ExecuteLoadDataCommand()
        {

            try
            {
                LoadData();
                /**if (CurrentJourneyId != string.Empty)
                {
                }*/
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public virtual void OnAppearing() { }

        public abstract void LoadData();

        async virtual internal void Back() => await Shell.Current.GoToAsync("..");

    }
}
