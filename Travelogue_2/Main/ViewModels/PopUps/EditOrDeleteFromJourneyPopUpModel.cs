using System;
using System.Collections.Generic;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
	[QueryProperty(nameof(DaySelectedNum), nameof(DaySelectedNum))]
	[QueryProperty(nameof(EventId), nameof(EventId))]
	[QueryProperty(nameof(EntryId), nameof(EntryId))]
	public class EditOrDeleteFromJourneyPopUpModel : DataBaseViewModel
	{
		public string eventId;
		public string EventId
		{
			get => eventId;
			set
			{
				eventId = value;
				LoadEvent();
			}
		}

		public string entryId;
		public string EntryId
		{
			get => entryId;
			set
			{
				entryId = value;
				LoadEntry();
			}
		}

		public string daySelectedNum;
		public string DaySelectedNum
		{
			get => daySelectedNum;
			set
			{
				daySelectedNum = value;
				LoadData();
			}
		}

		public Command SaveEventCommand { get; }
		public Command DeleteEventCommand { get; }

		public Command SaveEntryCommand { get; }
		public Command DeleteEntryCommand { get; }

		public Command SaveTextCommand { get; }
		public Command DeleteTextCommand { get; }

		public Command CancelCommand { get; }

		public EditOrDeleteFromJourneyPopUpModel()
		{
			SaveEventCommand = new Command(() => SaveEventC());
			DeleteEventCommand = new Command(() => DeleteEventC());

			SaveEntryCommand = new Command(() => SaveEntryC());
			DeleteEntryCommand = new Command(() => DeleteEntryC());

			//SaveTextCommand = new Command(() => SaveyTextC());
			//DeleteTextCommand = new Command(() => DeleteTextC());

			CancelCommand = new Command(() => CancelC());
		}

		public override void LoadData()
		{
			if (CurrentJourneyId != null && DaySelectedNum != null)
            {
				JourneyModel journey = DataBaseUtil.GetJourneyById( int.Parse(CurrentJourneyId) );
				List<DayModel> Days = DataBaseUtil.GetDaysFromJourney(journey);

				DaySelected = Days[int.Parse(DaySelectedNum)].Date;

				MaxDaySelected = Days.Last().Date;
				MinDaySelected = Days.First().Date;
			}
		}

		public void LoadEvent()
        {
			if (eventId != null)
            {
				Evento = DataBaseUtil.GetEventById( int.Parse(eventId) );

				if (Evento.Reservation) ReserVisible = true; 
				else EventVisible = true;

				Time = TimeSpan.Parse( Evento.Time );
			}
        }

		public void LoadEntry()
        {
			if (entryId != null)
			{
				EntryModel temp = DataBaseUtil.GetEntryById( int.Parse(entryId) );

				Entry = temp;
			}
			if (eventId != null)
            {
				EventModel temp = DataBaseUtil.GetEventById( int.Parse(eventId) );

				Evento = temp;
            }
		}

		private EventModel evento;

		public EventModel Evento
		{
			get => evento;
			set => SetProperty(ref evento, value);
		}

		private EntryModel entry;

		public EntryModel Entry
        {
			get => entry;
			set => SetProperty(ref entry, value);
        }

		#region IsVisible

		private bool eventVisible = false;
		public bool EventVisible
        {
			get => eventVisible;
			set => SetProperty(ref eventVisible, value);
        }

		private bool reserVisible = false;
		public bool ReserVisible
		{
			get => reserVisible;
			set => SetProperty(ref reserVisible, value);
		}

		#endregion

		#region DaySelected

		private DateTime daySelected = DateTime.Today;
		public DateTime DaySelected
		{
			get => daySelected;
			set => SetProperty(ref daySelected, value);
		}

		#endregion

		#region Time
		private TimeSpan time;
		public TimeSpan Time
        {
			get => time;
			set => SetProperty(ref time, value);
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

		internal void CheckNewIniDate(DatePicker iniDatePicker)
		{
			if (Evento != null && !Evento.Reservation)
			{
				Evento.EndDay = iniDatePicker.Date;
			}
		}

		internal void CheckNewIniDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			if (Evento != null && iniDatePicker.Date.CompareTo(Evento.EndDay.Date) > 0) endDatePicker.Date = iniDatePicker.Date;
		}

		internal void CheckNewEndDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			if (Evento != null && iniDatePicker.Date.CompareTo(Evento.EndDay.Date) > 0) iniDatePicker.Date = endDatePicker.Date;
		}

        internal void SaveEventC()
        {
            //Evento.Time = Time.Hours + ":" + Time.Minutes;
            DataBaseUtil.SaveEvent(Evento);
            Back();
        }

        internal void DeleteEventC()
		{
			DataBaseUtil.DeleteEvent(Evento);
			Back();
		}

		internal void SaveEntryC()
		{
			DataBaseUtil.SaveEntry(Entry, DaySelected);
			Back();
		}

		internal void DeleteEntryC()
		{
			DataBaseUtil.DeleteEntry(Entry);
			Back();
		}

		/*async internal void SaveyTextC()
		{
			DataBaseUtil.SaveText(Text);
			Back();
		}

		async internal void DeleteTextC()
		{
			DataBaseUtil.DeleteText(Text);
			Back();
		}*/

		async internal void CancelC()
		{
			if ( Evento?.Title != string.Empty || Entry?.Title != string.Empty )
			{
				bool result = await Alerter.AlertInfoWillBeLost();

				if (result) await Shell.Current.GoToAsync("..");
			}
			else
			{
				Back();
			}
		}

	}
}
