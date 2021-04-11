using System;
using System.Collections.ObjectModel;
using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
	[QueryProperty(nameof(JourneyId), nameof(JourneyId))]
	[QueryProperty(nameof(EntryId), nameof(EntryId))]
	public class EditOrDeleteToEntryPopUpModel : DataBaseViewModel
	{
		public string journeyId;
		public string JourneyId
		{
			get => journeyId;
			set
			{
				journeyId = value;
				LoadData();
			}
		}

		public string entryId;
		public string EntryId
		{
			get => entryId;
			set => entryId = value;
		}
		
		public Command SaveCommand { get; }
		public Command DeleteCommand { get; }
		public Command CancelCommand { get; }

		public EditOrDeleteToEntryPopUpModel()
		{
			SaveCommand = new Command(() => SaveyC());
			DeleteCommand = new Command(() => DeleteC());

			CancelCommand = new Command(() => CancelC());
		}

		public override void LoadData()
		{
			if (JourneyId != null)
			{
			}

			ObservableCollection<DateTime> Days = new ObservableCollection<DateTime>
			{
				new DateTime(2021, 02, 02),
				new DateTime(2021, 02, 03),
				new DateTime(2021, 02, 04)
			};

		}

		async internal void SaveyC()
		{

		}

		async internal void DeleteC()
		{

		}

		async internal void CancelC()
		{
			if ( false )
			{
				bool result = await Alerter.AlertInfoWillBeLost();

				if (result) await Shell.Current.GoToAsync("..");
			}
			else
			{
				await Shell.Current.GoToAsync("..");
			}
		}
	}
}
