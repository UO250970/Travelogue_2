using System;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
	public class AddSettingsPopUpModel : DataBaseViewModel
	{
		public Command CreateCardCommand { get; }
		public Command CreateDestinyCommand { get; }
		public Command CancelCommand { get; }

		public AddSettingsPopUpModel()
		{
			CreateCardCommand = new Command(() => CreateCardC());
			CreateDestinyCommand = new Command(() => CreateDestinyC());
			CancelCommand = new Command(() => CancelC());
		}

		public override void LoadData()
		{
			throw new NotImplementedException();
		}

		public void CreateCardC()
		{

		}

		public void CreateDestinyC()
		{

		}

		public void CancelC()
		{

		}

	}
}
