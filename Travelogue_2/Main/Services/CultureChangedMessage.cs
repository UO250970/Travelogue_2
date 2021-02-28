using System.Globalization;

namespace Travelogue_2.Main.Services
{
    public class CultureChangedMessage
    {
        public CultureInfo NewCultureInfo { get; private set; }

        public CultureChangedMessage(string lngName)
            : this(new CultureInfo(lngName))
        { }

        public CultureChangedMessage(CultureInfo newCultureInfo) 
            => NewCultureInfo = newCultureInfo;
    }
}
