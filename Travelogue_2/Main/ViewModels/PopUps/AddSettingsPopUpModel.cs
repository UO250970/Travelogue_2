﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
	public class AddSettingsPopUpModel : PhotoRendererModel
	{
		public ObservableCollection<ImageCard> CardImages { get; }

		public Command AddImageCommand { get; }
		public Command CreateCardCommand { get; }
		public Command CreateDestinyCommand { get; }
		public Command CancelCommand { get; }
		public Command<ImageCard> ImageTapped { get; }

		public AddSettingsPopUpModel()
		{
			AddImageCommand = new Command(() => AddImageC());
			CreateCardCommand = new Command(() => CreateCardCAsync());
			CreateDestinyCommand = new Command(() => CreateDestinyC());
			CancelCommand = new Command(() => CancelCAsync());


			CardImages = new ObservableCollection<ImageCard>();

			ImageTapped = new Command<ImageCard>(OnImageSelected);
		}

		#region Title

		public string Name { get; set; } = string.Empty;

		#endregion

		public override void LoadData()
		{
			throw new NotImplementedException();
		}

		async internal void AddImageC()
		{
			ImageCard success = await CameraUtil.Photo(this);
			if (success != null)
			{
				CardImages.Add(success);
			}
		}

		public async Task CreateCardCAsync()
		{
			if (Name == string.Empty)
			{
				await Alerter.AlertNoNameInCard();
			} 
			else if (CardImages.Count == 0)
			{
				await Alerter.AlertNoImagesInCard();
			}
			else
			{
				await Alerter.AlertCardCreated();
				await Shell.Current.GoToAsync("..");
			}
		}

		public void CreateDestinyC()
		{

		}

		public async Task CancelCAsync()
		{
			await Shell.Current.GoToAsync("..");
		}

		async void OnImageSelected(ImageCard image)
		{
			if (image == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			//await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
		}

	}
}
