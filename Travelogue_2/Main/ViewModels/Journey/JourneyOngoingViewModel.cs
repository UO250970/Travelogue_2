using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;

namespace Travelogue_2.Main.ViewModels.Journey
{
    public class JourneyOngoingViewModel : JourneyTemplateViewModel
    {
        private bool journeyOnGoing = false;
        public bool JourneyOnGoing
		{
            get => journeyOnGoing;
            set
            {
                SetProperty(ref journeyOnGoing, value);
                JourneyNotOnGoing = !value;
            }
        }

        private bool journeyNotOnGoing = true;
        public bool JourneyNotOnGoing
        {
            get => journeyNotOnGoing;
            set
            {
                SetProperty(ref journeyNotOnGoing, value);
            }
        }

        public override void LoadData() 
        {
            JourneyModel journey = DataBaseUtil.GetJourneyOnGoing();

            if (journey?.Id >= 0)
			{
                CurrentJourneyId = journey.Id.ToString();
                JourneyOnGoing = true;
                base.LoadData();
            } else
            {
                JourneyOnGoing = false;
            }
        }

    }
}
