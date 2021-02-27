using System;
using Travelogue_2.Main.ViewModels.Library.Create;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Library.Create
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateJourneyView : ContentPage
	{

		readonly CreateJourneyViewModel model;

		public CreateJourneyView()
		{
			InitializeComponent();

			BindingContext = model = new CreateJourneyViewModel();
			DestiniesCollection.HeightRequest = 0;

			Shell.SetNavBarIsVisible(this, false);
		}
		protected override void OnAppearing()
		{
			model.OnAppearing();
			base.OnAppearing();
		}

		public void CheckNewIniDate(object sender, EventArgs e)
		{
			model.CheckNewIniDate(IniDatePicker, EndDatePicker);
		}

		public void CheckNewEndDate(object sender, EventArgs e)
		{
			model.CheckNewEndDate(IniDatePicker, EndDatePicker);
		}

	}
}