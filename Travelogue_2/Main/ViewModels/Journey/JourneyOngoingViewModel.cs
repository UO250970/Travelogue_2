namespace Travelogue_2.Main.ViewModels.Journey
{
    public class JourneyOngoingViewModel : DataBaseViewModel
    {

        private bool journeyOngoing = false;

        public bool JourneyOngoing
        {
            get => journeyOngoing;
            set => SetProperty(ref journeyOngoing, value);
        }

        public override void LoadData() { }

    }
}
