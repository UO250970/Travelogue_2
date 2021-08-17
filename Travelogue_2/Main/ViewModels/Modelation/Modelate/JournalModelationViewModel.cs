using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation.Modelate
{
	public class JournalModelationViewModel : PhotoRendererModel
	{
		public string JourneyId; 
		public ObservableCollection<ImageModel> Pages { get; set; }
		public Command CreatePageCommand { get; set; }
		public Command<ImageModel> PageTapped { get; set;  }
		public Command<ImageModel> PageTappedDelete { get; set; }

		public JournalModelationViewModel()
		{
			Pages = new ObservableCollection<ImageModel>();

			CreatePageCommand = new Command(x => CreatePageC());

			PageTapped = new Command<ImageModel>(OnPageSelected);
			PageTappedDelete = new Command<ImageModel>(OnPageDelete);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			//
			
			JourneyName = "Prueba titulo";
			PagesCount = string.Format(Resources["PagesCountMess"], Pages.Count);

		}

		#region JourneyName
		public string journeyName;

		public string JourneyName
		{
			get => journeyName;
			set => SetProperty(ref journeyName, value);
		}
		#endregion

		#region ImageSelected
		public ImageSource imageSelected = CommonVariables.GetGenericImage();

		public ImageSource ImageSelected
		{
			get => imageSelected;
			set => SetProperty(ref imageSelected, value);
		}
		#endregion

		#region PagesCount
		public string pagesCount = "";
		public string PagesCount
		{
			get => pagesCount;
			set => SetProperty(ref pagesCount, value);
		}
		#endregion

		public async void CreatePageC()
        {
			await Shell.Current.GoToAsync($"{nameof(BackgroundSelectorView)}");
		}

		void OnPageSelected(ImageModel page)
		{
			if (page == null)
				return;

			ImageSelected = page.ImageSour;
			// This will push the ItemDetailPage onto the navigation stack
		}

		void OnPageDelete(ImageModel page)
        {
			if (page == null)
				return;
		}


	}
}
