﻿using System.Threading.Tasks;
using Travelogue_2.Resources.Localization;
using Xamarin.Forms;

namespace Travelogue_2.Main.Utils
{
    static class Alerter
	{
		private static Page Page;

		public static void SetPage(Page page)
		{
			Page = page;
		}

		public static async Task NoImplementedYet()
		{
			await Page?.DisplayAlert("Error", "Funcionalidad en desarrollo", App.LocResources["Ok"]);
		}
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
		}


		/** Created */
		/*
		public static async Task AlertJourneyCreated()
		{
			await Page?.DisplayAlert(AppResources.Empty, AppResources.MessJourCreated, AppResources.Ok);
		}

		public static async Task AlertJourneyAlreadyCreated()
		{
			await Page?.DisplayAlert(AppResources.Empty, AppResources.MessJourAlreadyCreated, AppResources.Ok);
		}

		internal static async Task AlertEntryCreated()
		{
			await Page?.DisplayAlert(AppResources.Empty, AppResources.MessEventCreated, AppResources.Ok);
		}

		internal static async Task AlertTextCreated()
		{
			await Page?.DisplayAlert(AppResources.Empty, AppResources.MessTextCreated, AppResources.Ok);
		}

		internal static async Task AlertImageCreated()
		{
			await Page?.DisplayAlert(AppResources.Empty, AppResources.MessImageCreated, AppResources.Ok);
		}

		internal static async Task AlertReservationCreated()
		{
			await Page?.DisplayAlert(AppResources.Empty, AppResources.MessReservationCreated, AppResources.Ok);
		}

		public static async Task AlertNoText()
		{
			await Page?.DisplayAlert(AppResources.AlertNoText, AppResources.MessNoText, AppResources.Ok);
		}

		/** No Name */
		/*
		public static async Task AlertNoNameInNewCountry()
		{
			await Page?.DisplayAlert(AppResources.AlertNoNameInNewCountry, AppResources.MessNoNameInNewCountry, AppResources.Ok);
		}

		public static async Task AlertNoNameInJourney()
		{
			await Page?.DisplayAlert(AppResources.AlertNoNameInJourney, AppResources.MessNoNameInJourney, AppResources.Ok);
		}

		public static async Task AlertNoNameInEntry()
		{
			await Page?.DisplayAlert(AppResources.AlertNoNameInEntry, AppResources.MessNoNameInEntry, AppResources.Ok);
		}

		public static async Task AlertNoNameInEvent()
		{
			await Page?.DisplayAlert(AppResources.AlertNoNameInEvent, AppResources.MessNoNameInEvent, AppResources.Ok);
		}

		internal static async Task AlertNoNameInReservation()
		{
			await Page?.DisplayAlert(AppResources.AlertNoNameInReservation, AppResources.MessNoNameInReservation, AppResources.Ok);
		}

		internal static async Task AlertNoImageSelected()
		{
			await Page?.DisplayAlert(AppResources.AlertNoImageSelected, AppResources.MessNoImageSelected, AppResources.Ok);
		}

		/** Too Many */
		/*
		internal static async Task AlertTooanyCountriesInJourney()
		{
			await Page?.DisplayAlert(AppResources.Empty, AppResources.MessTooManyCountries, AppResources.Ok);
		}

		internal static async Task AlertTooManyEntriesInDay()
		{
			await Page?.DisplayAlert(AppResources.Empty, AppResources.MessTooManyEntries, AppResources.Ok);
		}

		/** Does not exists */
		/*
		internal static async Task<bool> AlertCountryDoesNotExist()
		{
			return await Page?.DisplayAlert(AppResources.Empty, AppResources.MessCountryDoesNotExists, AppResources.Ok, AppResources.Cancel);
		}

		/** Already exists */

		internal static async Task<bool> AlertCountryAlreadySelected()
		{
			return await Page?.DisplayAlert(App.LocResources["Empty"], "El destino ya está incluido", App.LocResources["Ok"], App.LocResources["Cancel"]);
		}

		/** Photo */
		/*
		// Gochada -> True es take, false es pick
		internal static async Task<string> AlertPhoto()
		{
			return await Page?.DisplayActionSheet(AppResources.Empty, AppResources.Empty, AppResources.Empty, AppResources.TakePhoto, AppResources.PickPhoto);
		}

		/** Servicios */

		internal static async Task<bool> AlertEnableLocation()
		{
			return await Page?.DisplayAlert(App.LocResources["Empty"], "Habilite la geolocalización para poder usarla en la aplicación", App.LocResources["Ok"], App.LocResources["Cancel"]);
		}

		/** Ajustes */

		internal static async Task<bool> AlertLanguageChanged()
		{
			return await Page?.DisplayAlert(App.LocResources["Empty"], App.LocResources["MessLanguageChanged"], App.LocResources["Ok"], App.LocResources["Cancel"]);
		}
	}
}
