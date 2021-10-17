using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Fragments
{
	public class DestiniesCollectionViewModel : PhotoRendererModel
	{
		public Command AddDestinyCommand { get; }
		public Command MoreInfoCommand { get; }

		public ObservableCollection<DestinyModel> DestiniesSelected { get; }
		public ObservableCollection<string> DestiniesList { get; }
		public Command<DestinyModel> DestinyTappedDelete { get; }

		public DestiniesCollectionViewModel()
		{
			AddDestinyCommand = new Command(() => AddDestinyC());
			MoreInfoCommand = new Command<string>((x) => MoreInfoC(x));

			DestiniesSelected = new ObservableCollection<DestinyModel>();
			DestiniesList = new ObservableCollection<string>();

			DestinyTappedDelete = new Command<DestinyModel>(OnDestinySelectedDelete);

			ExecuteLoadDataCommand();
		}


		#region CorrectDestinyText
		private bool correctDestinyText;
		public bool CorrectDestinyText
		{
			get => correctDestinyText;
			set => SetProperty(ref correctDestinyText, value);
		}
		#endregion

		#region DestinyText
		private string destinyText;
		public string DestinyText
		{
			get => destinyText;
			set
			{
				if (DestiniesList.Contains(value))
				{
					CorrectDestinyText = true;
				}
				else
				{
					CorrectDestinyText = false;
				}
				SetProperty(ref destinyText, value);
			}
		}
		#endregion

		async internal void AddDestinyC()
		{
			if (DestiniesSelected.Count <= CommonVariables.DestiniesInJourney)
			{
				DestinyModel destiny = DataBaseUtil.GetDestinyByName(DestinyText);

				if (!DestiniesSelected.Contains(destiny))
				{
					DestiniesSelected.Add(destiny);
				}
				else
				{
					await Alerter.AlertDestinyAlreadySelected();
				}
			}
			else
			{
				await Alerter.AlertTooManyDestiniesInJourney();
			}
			DestinyText = string.Empty;
		}

		async internal void MoreInfoC(string path)
		{
			await Browser.OpenAsync(path);
		}

		void OnDestinySelectedDelete(DestinyModel destiny)
		{
			if (destiny == null)
				return;

			DestiniesSelected.Remove(destiny);
		}

		public override void LoadData()
		{
		}
	}
}
