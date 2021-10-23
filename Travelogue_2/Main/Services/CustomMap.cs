using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Travelogue_2.Main.Services
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }
    }

    public class CustomPin : Pin
    {
        public string Journey { get; set; }
        //public string Url { get; set; }
        public ImageSource Image { get; set; }
    }
}
