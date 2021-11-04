namespace Travelogue_2.Main.ViewModels.Journey
{
    public class JourneyViewModel : JourneyTemplateViewModel
    {
        public override void LoadData()
        {
            if (CurrentJourneyId != "0")
            {
                base.LoadData();
            }
            else
            {
                Back();
            }
        }
    }
}
