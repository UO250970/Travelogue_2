using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Fragments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DayCardView : ContentPage
	{
		public DayCardView()
		{
			InitializeComponent();
		}

		public DayCardView(DayCard card)
		{
			InitializeComponent();

			Day.Text = card.Day;
			Month.Text = card.Month;
		}
	}
}