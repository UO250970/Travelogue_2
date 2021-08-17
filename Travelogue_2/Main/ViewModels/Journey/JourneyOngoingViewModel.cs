﻿using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;

namespace Travelogue_2.Main.ViewModels.Journey
{
    public class JourneyOngoingViewModel : JourneyTemplateViewModel
    {
        public new string JourneyId
        {
            get => journeyId;
            set => journeyId = value;
        }

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

        public JourneyOngoingViewModel() : base ()
		{
            ExecuteLoadDataCommand();
		}

        public override void LoadData() 
        {
            JourneyModel journey = DataBaseUtil.GetJourneyOnGoing();

            if (journey.Id >= 0)
			{
                JourneyId = journey.Id.ToString();
                base.LoadData();
                JourneyOnGoing = true;
            }

        }

    }
}
