using System.Collections.Generic;
using System.Collections.ObjectModel;
using Travelogue_2.Main.BBDD;
using Travelogue_2.Main.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelogue_2.Main.Services
{
    class CommonVariables
    {
		/** Common values */
		public static int EntriesInDay { get => 50; }

		public static int DestiniesInJourney { get => 50; } 

		public static int DataInEntry { get => 50; }

		public static int EventsInDay { get => 20; }

		public static int ImageMaxHeight { get => 180; }

		public static int ImageCardMaxHeight { get => 85; }

		public static int JourneyNameMaxLenght { get => 50; }

		/** Common string */

		public static string Blank { get => "Blank"; }

		public static string AppName { get => "Travelogue_2"; }

		public static string ResourcesMonth { get => "Month_"; }

		public static string Background { get => "Background_"; }

		public static string CountryWebSite { get => "http://country.io/"; }

		public static string ImagesPath { get => AppName + ".Resources.Imgs"; }

		public static string FlagImagesPath { get => ImagesPath + ".Flags."; }

		public static string BackgroundImagesPath { get => ImagesPath + ".Backgrounds."; }

		public static string FlagImagesExtension { get => "_Flag" + ImagesExtension; } 

		public static string ImagesExtension { get => ".png"; }

		public static string SavedImagesExtension { get => ".jpg"; }

		public static string GenericImage { get => ImagesPath + ".default_image" + ImagesExtension; }

		public static string GenericImageImage { get => ImagesPath + ".default_image_image" + ImagesExtension; }

		public static string GenericFlag { get => FlagImagesPath + "DEFAULT" + FlagImagesExtension; }  

		public static List<string> AvailableLanguages { get => new List<string>() { "ES", "FR", "EN" }; }
		public static List<Destiny> AvailableDestinies { get; set; } = new List<Destiny>();

		public static Dictionary<string, Location> AvailableLanguagesLocations 
		{
			get => new Dictionary<string, Location>() 
			{ 
				{ "ES", new Location(40.41966158373195, -3.690064616210935) }
			}; 
		}

		public static List<string> AvailableFlags { get => new List<string>() { "AD", "AF", "AT", "AU", "CU", "DE", "ES", "FR" }; }


		public static List<string> Alphabet { get => new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" }; }

		public static ImageSource GetFlag(string name)
		{
			if ( AvailableFlags.Contains(name) )
			{
				string tempString = (FlagImagesPath + name.ToUpper() + FlagImagesExtension);
				return ImageSource.FromResource(tempString);
			} else
			{
				return ImageSource.FromResource(GenericFlag);
			}
		}

		public static ImageSource GetGenericImage()
		{
			return ImageSource.FromResource(GenericImage);
		}

		public static ImageSource GetGenericImageImage()
        {
			return ImageSource.FromResource(GenericImageImage);
		}

		public static List<string> AvailableBackgrounds { get => new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }; }

		public static ObservableCollection<ImageModel> GetBackgrounds() 
		{
			ObservableCollection<ImageModel> temp = new ObservableCollection<ImageModel>();
			foreach ( string back in AvailableBackgrounds)
			{
				ImageModel tempI = new ImageModel();
				string tempString = (BackgroundImagesPath + Background + back + ImagesExtension);
				tempI.Path = tempString;
				temp.Add(tempI);
			}
			return temp;
		}

		public static ImageSource GetBackground(string name)
		{
			string tempString = (BackgroundImagesPath + name + ImagesExtension);
			return ImageSource.FromResource(tempString);
		}

	}
}
