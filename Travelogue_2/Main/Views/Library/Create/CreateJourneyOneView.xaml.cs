using System;
using Travelogue_2.Main.ViewModels.Library.Create;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Library.Create
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateJourneyOneView : ContentPage
	{

		readonly CreateJourneyOneViewModel model;

		public CreateJourneyOneView()
		{
			InitializeComponent();

			BindingContext = model = new CreateJourneyOneViewModel();

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
	}
}