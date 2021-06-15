using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.ViewModels.Journal;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journey
{
    public class JourneyOngoingViewModel : DataBaseViewModel
    {
        private bool journeyOnGoing = false;
        public bool JourneyOnGoing
		{
            get => journeyOnGoing;
            set
            {
                SetProperty(ref journeyOnGoing, value);
                SetProperty(ref journeyNotOnGoing, !value);
            }
        }

        private bool journeyNotOnGoing = true;
        public bool JourneyNotOnGoing
        {
            get => journeyNotOnGoing;
        }

        public JourneyOngoingViewModel()
		{
            ExecuteLoadDataCommand();
		}

        public override async void LoadData() 
        {
            JourneyModel journey = DataBaseUtil.GetJourneyOnGoing();

            if (journey.Id >= 0)
			{
                JourneyOnGoing = true;

                //JourneyTemplateViewModel model = new JourneyTemplateViewModel();
                //model.JourneyId = journey.Id;

                //await Shell.Current.GoToAsync("PAGE_URI", model);
                await Shell.Current.GoToAsync($"..?{nameof(JourneyTemplateViewModel.JourneyId)}={journey.Id.ToString()}");
            }

        }

    }
}
