using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.PopUps;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journal
{
	public class JourneyOngoingViewModel : PhotoRendererModel
	{
		public Command AddImageCommand { get; }
		public Command<ImageCard> ImageTapped { get; }
		public Command<DayCard> DayTapped { get; }
		public ObservableCollection<ImageCard> JourneyImages { get; }
		public ObservableCollection<DayCard> JourneyDays { get; }
		public ObservableCollection<EventCard> JourneyEvents { get; }
		public ObservableCollection<EntryCard> JourneyEntries { get; }

		/** Pop Ups */
		public Command AddToJourneyCommand { get; }
		/** */

		public JourneyOngoingViewModel()
		{
			AddImageCommand = new Command(() => AddImageC());
			AddToJourneyCommand = new Command(() => AddToJourneyC());

			JourneyImages = new ObservableCollection<ImageCard>();
			JourneyDays = new ObservableCollection<DayCard>();
			JourneyEvents = new ObservableCollection<EventCard>();
			JourneyEntries = new ObservableCollection<EntryCard>();

			ImageTapped = new Command<ImageCard>(OnImageSelected);
			DayTapped = new Command<DayCard>(OnDaySelected);

			ExecuteLoadDataCommand();
		}

		async Task ExecuteLoadDataCommand()
		{
			IsBusy = true;

			try
			{
				var temp = new DayCard();
				temp.Day = "2";
				temp.Month = "2";
				JourneyDays.Add(temp);

				var temp2 = new DayCard();
				temp2.Day = "3";
				temp2.Month = "2";
				var etemp1 = new EntryCard();
				etemp1.Title = "Prueba titulo";
				temp2.JourneyEntries.Add(etemp1);
				temp2.Entries = 1;
				JourneyDays.Add(temp2);

				var temp3 = new DayCard();
				temp3.Day = "4";
				temp3.Month = "2";
				JourneyDays.Add(temp3);
				//JourneyImages.Add(new ImageCard());

				daySelected = JourneyDays[0];
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		public void OnAppearing()
			=> IsBusy = true;

		#region Photos

		public ImageCard blanckImage = new ImageCard();
		public ImageCard BlanckImage
		{
			get => blanckImage;
			set
			{
				SetProperty(ref blanckImage, value);
			}
		}
		public int CardImagesHeight { get => CommonVariables.ImageCardMaxHeight; }

		#endregion

		#region Cover

		private ImageCard coverImage = new ImageCard()
		{
			ImageSour = CommonVariables.GetImage()
		};

		public ImageCard CoverImage
		{
			get => coverImage;
			set
			{
				SetProperty(ref coverImage, value);
			}
		}

		public int CoverImageHeight { get => CommonVariables.ImageMaxHeight; }

		#endregion

		#region DaySelected

		private DayCard daySelected = new DayCard();

		public DayCard DaySelected
		{
			get => daySelected;
			set
			{
				SetProperty(ref daySelected, value);
				EventsHeight = 40 * value.Events;
				EntriesHeight = 40 * value.Entries;
			}
		}

		#endregion

		private double eventsHeight = 0;

		public double EventsHeight
		{
			get => eventsHeight;
			set => SetProperty(ref eventsHeight, value);
		}


		private double entriesHeight = 0;

		public double EntriesHeight
		{
			get => entriesHeight;
			set => SetProperty(ref entriesHeight, value);
		}

		async internal void AddImageC()
		{
			ImageCard success = await CameraUtil.Photo(this);
			if (success != null)
			{
				JourneyImages.Add(success);
			}
		}

		async internal void AddToJourneyC()
		{
			AddToJourneyPopUp popup = new AddToJourneyPopUp();
			//popup.model.journey = JourneyCard;
		}

		#region OnAction

		async void OnImageSelected(ImageCard image)
		{
			if (image == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			//await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
		}

		async void OnDaySelected(DayCard day)
		{
			if (day == null)
				return;

			DaySelected = day;
			// This will push the ItemDetailPage onto the navigation stack
			//await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
		}

		#endregion

	}
}
