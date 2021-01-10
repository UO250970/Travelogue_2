using System;
using System.Collections.Generic;

namespace Travelogue_2.Main.Utils
{
    class CommonVariables
    {
		public static int EntriesInDay { get; } = 50;

		public static int CountriesInJourney { get; } = 50;

		public static int DataInEntry { get; } = 50;

		public static int EventsInDay { get; } = 20;

		/** Common string */
		public static string AppName { get; } = "Travelogue_2";

		public static string CountryWebSite { get; } = "http://country.io/";

		public static string FlagImagesPath { get; } = "Travelogue_2.Resources.Imgs.Flags.";

		public static string FlagImagesExtension { get; } = "_Flag.png";

		public static string ImagesExtension { get; } = ".png";

		public static string GenericImage { get; } = "Travelogue_2.Resources.Imgs.default_image.png";

		public static string GenericFlag { get; } = "Travelogue_2.Resources.Imgs.Flags.DEFAULT_Flag.png";

		public static List<String> AvailableLocations { get; } = new List<string>() { "ES", "EN", "FR" };

	}
}
