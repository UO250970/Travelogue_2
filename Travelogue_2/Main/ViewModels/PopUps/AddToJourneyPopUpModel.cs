using System;
using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
	[QueryProperty(nameof(JourneyId), nameof(JourneyId))]
	[QueryProperty(nameof(DaySelectedNum), nameof(DaySelectedNum))]
	public class AddToJourneyPopUpModel : PhotoRendererModel
	{
		public string journeyId;
		public string JourneyId
		{
			get
			{
				return journeyId;
			}
			set
			{
				journeyId = value;
				LoadData();
			}
		}

		public string daySelectedNum;
		public string DaySelectedNum
		{
			get
			{
				return daySelectedNum;
			}
			set
			{
				daySelectedNum = value;
				LoadData();
			}
		}

		public Command CreateEventCommand { get; }
		public Command CreateReservationCommand { get; }
		public Command CreateEntryCommand { get; }
		public Command CreateToEntryCommand { get; }
		public Command CancelCommand { get; }

		public AddToJourneyPopUpModel()
		{
			CreateEventCommand = new Command(() => CreateEventC());
			CreateReservationCommand = new Command(() => CreateReservationC());
			CreateEntryCommand = new Command(() => CreateEntryC());
			CreateToEntryCommand = new Command(() => CreateToEntryC());

			CancelCommand = new Command(() => CancelC());
		}

		public override void LoadData()
		{
			if (JourneyId != null && DaySelectedNum != null)
			{
				// TODO - Pilla el journey de BBDD y pilla el list de dias o whatever
				ObservableCollection<DateTime> Days = new ObservableCollection<DateTime>
				{
					new DateTime(2021, 02, 02),
					new DateTime(2021, 02, 03),
					new DateTime(2021, 02, 04)
				};

				DaySelected = Days[int.Parse(DaySelectedNum)];
				MinDaySelected = Days.First();
				MaxDaySelected = Days.Last();
			}
		}

		#region Title

		public string Title { get; set; } = "";

		#endregion

		#region DaySelected

		private DateTime daySelected = DateTime.Today;
		public DateTime DaySelected
		{
			get => daySelected;
			set
			{
				SetProperty(ref daySelected, value);
			}
		}

		#endregion

		#region IniDaySelected

		private DateTime iniDaySelected = DateTime.Today;
		public DateTime IniDaySelected
		{
			get => iniDaySelected;
			set
			{
				SetProperty(ref iniDaySelected, value);
			}
		}

		#endregion

		#region EndDaySelected

		private DateTime endDaySelected = DateTime.Today;
		public DateTime EndDaySelected
		{
			get => endDaySelected;
			set
			{
				SetProperty(ref endDaySelected, value);
			}
		}

		#endregion

		#region MinDaySelected

		private DateTime minDaySelected = DateTime.Today;
		public DateTime MinDaySelected
		{
			get => minDaySelected;
			set
			{
				SetProperty(ref minDaySelected, value);
			}
		}

		#endregion

		#region MaxDaySelected

		private DateTime maxDaySelected = DateTime.Today;
		public DateTime MaxDaySelected
		{
			get => maxDaySelected;
			set
			{
				SetProperty(ref maxDaySelected, value);
			}
		}

		#endregion

		#region Location

		private string location = "";
		public string Location
		{
			get => location;
			set
			{
				SetProperty(ref location, value);
			}
		}

		#endregion
		
		async internal void CreateEventC()
		{ // TO-DO Aqui mas alante podría pasarle el ID del Day y buscarlo en BBDD....
		  //await Shell.Current.GoToAsync($"{nameof(CreateEventView)}?{nameof(CreateEventViewModel.DaySelected)}={JourneyDays.IndexOf(DaySelected)}&{nameof(CreateEventViewModel.JourneyId)}={JourneyId}");
		  // TO-DO Chekear que todo no null y crear. 

			if (DaySelected == null)
			{
				await Alerter.AlertNoDaySelected();
			}
			else if (Title == "")
			{
				await Alerter.AlertNoTitleInEvent();
			}
			else
			{
				await Alerter.AlertEventCreated();
				await Shell.Current.GoToAsync("..");
			}
		}

		async internal void CreateReservationC()
		{

			if (IniDaySelected == null && EndDaySelected == null)
			{
				await Alerter.AlertNoDaySelected();
			}
			else if (Title == "")
			{
				await Alerter.AlertNoTitleInReservation();
			}
			else
			{
				await Alerter.AlertReservationCreated();
				await Shell.Current.GoToAsync("..");
			}
		}

		async internal void CreateEntryC()
		{
			if (DaySelected == null)
			{
				await Alerter.AlertNoDaySelected();
			} 
			else if (Title == "")
			{
				await Alerter.AlertNoTitleInEntry();
			} else
			{
				await Alerter.AlertEntryCreated();
				await Shell.Current.GoToAsync("..");
			}
		}

		async internal void CreateToEntryC()
		{
			/*AddEntryPU = new AddEntryPopUp
			{
				BindingContext = this
			};*/
		}

		async internal void CancelC()
		{
			await Shell.Current.GoToAsync("..");
		}
	}
}
