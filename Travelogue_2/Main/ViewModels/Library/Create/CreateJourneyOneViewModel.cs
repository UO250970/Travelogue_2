using System;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library.Create
{
	public class CreateJourneyOneViewModel : BaseViewModel
	{
		private string title;

		public Command CancelCommand { get; }
		public Command SaveCommand { get; }

		public CreateJourneyOneViewModel()
		{

			CancelCommand = new Command(() => CancelC());
			SaveCommand = new Command(() => SaveC());

		}
		public string Title
		{
			get => title;
			set => SetProperty(ref title, value);
		}

		async internal void CancelC()
		{ }

		internal void CheckNewIniDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			throw new NotImplementedException();
		}

		internal void CheckNewEndDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			throw new NotImplementedException();
		}

		//=> await Shell.Current.GoToAsync(nameof(CreatedJourneysView));


		async internal void SaveC()
		{ }
		//=> await Shell.Current.GoToAsync(nameof(ClosedJourneysView));

	}
}
