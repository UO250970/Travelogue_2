using System;
using Travelogue_2.Main.ViewModels.Journey;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Journey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JourneySettingsView : ContentPage
	{
		public JourneySettingsViewModel model;

		public JourneySettingsView()
		{
			InitializeComponent();

			BindingContext = model = new JourneySettingsViewModel();

			Shell.SetNavBarIsVisible(this, true);
		}

		public void CheckNewIniDate(object sender, EventArgs e)
		{
			model.CheckNewIniDate(IniDatePicker, EndDatePicker);
		}

		public void CheckNewEndDate(object sender, EventArgs e)
		{
			model.CheckNewEndDate(IniDatePicker, EndDatePicker);
		}
	} // TO-DO Sacar a estilo el color del enlaces
}