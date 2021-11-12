using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
    public class CardModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Top { get; set; }

        public string Back { get; set; }


        public ImageSource TopImageSour
        {
            get
            {
                return ImageSource.FromFile(Top);
            }
        }

        public ImageSource BackImageSour
        {
            get
            {
                return ImageSource.FromFile(Back);
            }
        }


    }
}
