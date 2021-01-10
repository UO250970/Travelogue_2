using System.Globalization;
using Travelogue_2.Resources.Localization;

namespace Travelogue_2.Main.ViewModels.Media
{
    public class MediaViewModel : BaseViewModel
    {

		public CultureInfo localization => App.LocResources.CurrentCultureInfo;

	}
}
