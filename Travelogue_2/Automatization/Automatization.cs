using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;

namespace Travelogue_2.Automatization
{
	static class Automatization
	{

		public static void PrepareCountries()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "Travelogue_2.Resources.JSON.ISO2Country.txt";
			string result = "";

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}

			var iso2List = JsonConvert.DeserializeObject<List<Destiny>>(result);
			foreach (Destiny country in iso2List)
			{
				country.Flag = country.Code + "_Flag";
				country.Original = true;
				CommonVariables.AvailableDestinies.Add(country);
			}

			//DataBase.InsertCountries(iso2List);
		}

	}
}
