using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journal
{
	public class JourneyOngoingViewModel : PhotoRendererModel
	{
		public ObservableCollection<ImageCard> JourneyImages { get; }

		public JourneyOngoingViewModel()
		{
			JourneyImages = new ObservableCollection<ImageCard>();

			ExecuteLoadDataCommand();
		}

		async Task ExecuteLoadDataCommand()
		{
			IsBusy = true;

			try
			{
				JourneyImages.Add(new ImageCard());
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


		public ImageSource CoverImage 
		{
			get { return ImageSource.FromResource(CommonVariables.GenericImage); } 
		}


	}
}
