using System;
using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddToEntryPopUp : ContentPage
	{
		public AddToJourneyPopUpModel model;

		public AddToEntryPopUp()
		{
			InitializeComponent();

			BindingContext = model = new AddToJourneyPopUpModel();
		}

		private void CreateText(object sender, EventArgs e)
		{
			GridImage.IsVisible = false;
			GridText.IsVisible = true;
		}

		private void CreateImage(object sender, EventArgs e)
		{
			GridText.IsVisible = false;
			GridImage.IsVisible = true;
		}
	}
}