using System.Threading.Tasks;
using Xamarin.Forms;

namespace Travelogue_2.Main.Services
{
    static class Alerter
	{
		private static Page Page;

		public static void SetPage(Page page)
			=> Page = page;

		public static async Task NoImplementedYet()
			=> await Page?.DisplayAlert("Error", "Funcionalidad en desarrollo", App.LocResources["Ok"]);

        #region Dates

        public static async Task AlertNoDaySelected()
			=> await Page?.DisplayAlert(App.LocResources["AlertNoDaySelected"], App.LocResources["MessNoDaySelected"], App.LocResources["Ok"]);

		public static async Task AlertModifyDatesInClosedJourney()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessModifyDatesInClosedJourney"], App.LocResources["Ok"]);

		public static async Task AlertModifyIniDateOpenJourney()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessModifyIniDateInOpenJourney"], App.LocResources["Ok"]);

		public static async Task AlertDatesAlreadyInUse()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessDatesAlreadyInUse"], App.LocResources["Ok"]);

		/*
		public static async Task AlertDayOccupied()
		{
			await Page?.DisplayAlert(AppResources.AlertDiaInvalid, AppResources.MessDiaInvalid, AppResources.Ok);
		}

		internal static async Task<bool> AlertEntryWithNoData()
		{
			return await Page?.DisplayAlert(AppResources.Empty, AppResources.MessEntryEmpty, AppResources.Ok, AppResources.Cancel);
		}

		internal static async Task<bool> AlertJourneyCanBeOpened()
		{
			return await Page?.DisplayAlert(AppResources.Empty, AppResources.MessJourneyCanBeOpened, AppResources.Ok, AppResources.Cancel);
		}

		internal static async Task<bool> AlertDelayIniDate()
		{
			return await Page?.DisplayAlert(AppResources.Empty, AppResources.MessJourneyWillBeDelayed, AppResources.Ok, AppResources.Cancel);
		}

		internal static async Task<bool> AlertJourneyCanBeClosed()
		{
			return await Page?.DisplayAlert(AppResources.Empty, AppResources.MessJourneyCanBeClosed, AppResources.Ok, AppResources.Cancel);
		}

		internal static async Task<bool> AlertRemoveJourneyWithPhotos()
		{
			return await Page?.DisplayAlert(AppResources.Empty, AppResources.MessRemoveJourneyWithPhotos, AppResources.Yes, AppResources.No);
		}*/

		#endregion

		#region Created

		public static async Task AlertJourneyCreated()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessJourCreated"], App.LocResources["Ok"]);

		/*
		public static async Task AlertJourneyAlreadyCreated()
		{
			await Page?.DisplayAlert(AppResources.Empty, AppResources.MessJourAlreadyCreated, AppResources.Ok);
		}
		*/
		internal static async Task AlertEventCreated()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessEventCreated"], App.LocResources["Ok"]);

		internal static async Task AlertReservationCreated()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessReservationCreated"], App.LocResources["Ok"]);

		internal static async Task AlertEntryCreated()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessEntryCreated"], App.LocResources["Ok"]);
		
		internal static async Task AlertInfoAdded()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessInfoAdded"], App.LocResources["Ok"]);

		internal static async Task AlertTextAdded()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessTextAdded"], App.LocResources["Ok"]);

		internal static async Task AlertImageAdded()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessImageAdded"], App.LocResources["Ok"]);

		internal static async Task AlertCardCreated()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessCardCreated"], App.LocResources["Ok"]);

        #endregion

        #region NotName
        /*
		public static async Task AlertNoNameInNewCountry()
		{
			await Page?.DisplayAlert(AppResources.AlertNoNameInNewCountry, AppResources.MessNoNameInNewCountry, AppResources.Ok);
		}
		*/
        public static async Task AlertNoNameInJourney()
			=> await Page?.DisplayAlert(App.LocResources["AlertNoNameInJourney"], App.LocResources["MessNoNameInJourney"], App.LocResources["Ok"]);

		public static async Task AlertNoTitleInEvent()
			=> await Page?.DisplayAlert(App.LocResources["AlertNoTitleInEvent"], App.LocResources["MessNoTitleInEvent"], App.LocResources["Ok"]);

		public static async Task AlertNoTitleInReservation()
			=> await Page?.DisplayAlert(App.LocResources["AlertNoTitleInReservation"], App.LocResources["MessNoTitleInReservation"], App.LocResources["Ok"]);

		public static async Task AlertNoTitleInEntry()
			=> await Page?.DisplayAlert(App.LocResources["AlertNoTitleInEntry"], App.LocResources["MessNoTitleInEntry"], App.LocResources["Ok"]);
		
		public static async Task AlertEmptyData()
			=> await Page?.DisplayAlert(App.LocResources["AlertEmptyData"], App.LocResources["MessEmptyData"], App.LocResources["Ok"]);

		public static async Task AlertNoText()
			=> await Page?.DisplayAlert(App.LocResources["AlertNoText"], App.LocResources["MessNoText"], App.LocResources["Ok"]);

		public static async Task AlertNoImageSelected()
			=> await Page?.DisplayAlert(App.LocResources["AlertNoImageSelected"], App.LocResources["MessNoImageSelected"], App.LocResources["Ok"]);

		public static async Task AlertNoNameInCard()
			=> await Page?.DisplayAlert(App.LocResources["AlertNoNameInCard"], App.LocResources["MessNoNameInCard"], App.LocResources["Ok"]);

		public static async Task AlertNoImagesInCard()
			=> await Page?.DisplayAlert(App.LocResources["AlertNoImagesInCard"], App.LocResources["MessNoImagesInCard"], App.LocResources["Ok"]);

        #endregion

        #region TooMany

        internal static async Task AlertTooManyDestiniesInJourney()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessTooManyDestinies"], App.LocResources["Ok"]);

		internal static async Task AlertTooManyEventsInDay()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessTooManyEvents"], App.LocResources["Ok"]);

		/*
		internal static async Task AlertTooManyEntriesInDay()
		{
			await Page?.DisplayAlert(AppResources.Empty, AppResources.MessTooManyEntries, AppResources.Ok);
		}*/

		#endregion

		#region Save

		public static async Task AlertEventSaved()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessEventSaved"], App.LocResources["Ok"]);
		
		public static async Task AlertEntrySaved()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessEntrySaved"], App.LocResources["Ok"]);

		#endregion

		/** Does not exists */
		/*
		internal static async Task<bool> AlertCountryDoesNotExist()
		{
			return await Page?.DisplayAlert(AppResources.Empty, AppResources.MessCountryDoesNotExists, AppResources.Ok, AppResources.Cancel);
		}

		/** Already exists */

		internal static async Task AlertDestinyAlreadySelected()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessDestinyAlreadySelected"], App.LocResources["Ok"]);

		internal static async Task AlertJourneyNameInUse() 
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessJourneyNameAlreadUsed"], App.LocResources["Ok"]);

		#region Delete

		internal static async Task<bool> AlertDeleteJourney()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessDeleteJourney"], App.LocResources["Ok"], App.LocResources["Cancel"]);

		internal static async Task<bool> AlertInfoWillBeLost()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessInfoWillBeLost"], App.LocResources["Ok"], App.LocResources["Cancel"]);

		internal static async Task AlertJourneyDeleted()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessJourneyDeleted"], App.LocResources["Ok"]);

		internal static async Task<bool> AlertDayInfoWillBeLost() 
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessInfoWillBeLost"], App.LocResources["Ok"], App.LocResources["Cancel"]);

		internal static async Task AlertEventDeleted()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessEventDeleted"], App.LocResources["Ok"]);

		internal static async Task AlertEntryDeleted()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessEntryDeleted"], App.LocResources["Ok"]);
		#endregion

		/** Photo */

		// Gochada -> True es take, false es pick
		internal static async Task<string> AlertPhoto()
			=> await Page?.DisplayActionSheet(App.LocResources["Empty"], App.LocResources["Empty"], App.LocResources["Empty"], App.LocResources["TakePhoto"], App.LocResources["PickPhoto"]);

		/** Servicios */

		internal static async Task<bool> AlertEnableLocation()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], "Habilite la geolocalización para poder usarla en la aplicación", App.LocResources["Ok"], App.LocResources["Cancel"]);

		/** Ajustes */

		internal static async Task<bool> AlertLanguageChanged()
			=> await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessLanguageChanged"], App.LocResources["Ok"], App.LocResources["Cancel"]);

	}
}
