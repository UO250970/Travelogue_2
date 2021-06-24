using Travelogue_2.Main.Models;
using Xamarin.Forms;

namespace Travelogue_2.Main.Services.TemplateSelectors
{
	public class EventTemplateSelector : DataTemplateSelector
	{
		private DataTemplate reservTemplate;

		public DataTemplate ReservTemplate
		{
			get => reservTemplate;
			set => reservTemplate = value;
		}

		private DataTemplate eventTemplate;

		public DataTemplate EventTemplate
		{
			get => eventTemplate;
			set => eventTemplate = value;
		}

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			EventModel evento = (EventModel) item;

			if (evento.PhoneNumber != "" || evento.PhoneNumber != null)
			{
				return ReservTemplate;
			}
			else
			{
				return EventTemplate;
			}
		}
	}
}
