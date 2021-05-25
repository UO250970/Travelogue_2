using Travelogue_2.Main.Models;
using Travelogue_2.Main.Models.Entries;
using Xamarin.Forms;

namespace Travelogue_2.Main.Services
{
	public class EntryTemplateSelector : DataTemplateSelector
	{

        private DataTemplate textTemplate;

        public DataTemplate TextTemplate
        {
            get => textTemplate;
            set => textTemplate = value;
        }

        private DataTemplate imageNoFootTemplate;

        public DataTemplate ImageNoFootTemplate
        {
            get => imageNoFootTemplate;
            set => imageNoFootTemplate = value;
        }

        private DataTemplate imageFootTemplate;

        public DataTemplate ImageFootTemplate
        {
            get => imageFootTemplate;
            set => imageFootTemplate = value;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			if (item is EntryTextModel e)
			{
                return TextTemplate;
			} else if ( ((EntryImageModel)item).ImageFoot == string.Empty )
			{
                return imageNoFootTemplate;
			}
			else
			{
                return ImageFootTemplate;
			}
		}
	}
}
