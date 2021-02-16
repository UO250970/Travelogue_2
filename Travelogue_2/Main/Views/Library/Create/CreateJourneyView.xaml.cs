using System;
using Travelogue_2.Main.ViewModels.Library.Create;
using Travelogue_2.Main.Views.Fragments;
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

		public void CheckNewIniDate(object sender, EventArgs e)
		{
			model.CheckNewIniDate(IniDatePicker, EndDatePicker);
		}

		public void CheckNewEndDate(object sender, EventArgs e)
		{
			model.CheckNewEndDate(IniDatePicker, EndDatePicker);
		}

		public void AddDestiny(object sender, EventArgs e)
		{
			//DestiniesCollection.HeightRequest = model.DestiniesSelected.Count * 125;
			/*+AbsoluteLayout absoluteLayout = new AbsoluteLayout();
			DestinyCardView destinyCard = new DestinyCardView();
			Button button = new Button()
			{
				Style = App.circleButtonDeleteStyle,
				HorizontalOptions = StartAndExpand,
				Text = "&#xea0f;", 
				FontFamily = "{StaticResource Icon},
				Command = model.DestinyTappedDelete,
				CommandParameter = "{Binding .}"
			};
			StackLayout destinyStack = new CreateDestinyStack(this, destiny);
			Destinies.Children.Add(destinyStack);*/
		}

		
	}
}