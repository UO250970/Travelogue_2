using System.Collections.ObjectModel;
using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation.Modelate
{
    public class BackgroundSelectorViewModel : PhotoRendererModel
    {
        public ObservableCollection<ImageSource> Backgrounds { get; set; }

        public override void LoadData()
        {
            Backgrounds = CommonVariables.GetBackgrounds();
        }
    }
}
