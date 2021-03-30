using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Settings
{
	public class SettingsStyleViewModel : DataBaseViewModel
	{
        public Command<Style> StyleTapped { get;}
        public ObservableCollection<Style> Styles { get; }

        public SettingsStyleViewModel()
        {
            Styles = new ObservableCollection<Style>();
            StyleTapped = new Command<Style>(OnStyleSelected);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            Styles.Clear();

            Style temp1 = new Style();
            temp1.Name = "Original";
            temp1.Primary = "#3D6D9B";
            temp1.PrimaryFaded = "#B2D4F4";
            temp1.Secondary = "#247c7c";

            Styles.Add(temp1);

            Style temp2 = new Style();
            temp2.Name = "Secundario";
            temp2.Primary = "#D075BF";
            temp2.PrimaryFaded = "#B2D4F4";
            temp2.Secondary = "#247c7c";

            Styles.Add(temp2);

            Style temp3 = new Style();
            temp3.Name = "Terciario";
            temp3.Primary = "#8DD075";
            temp3.PrimaryFaded = "#B2D4F4";
            temp3.Secondary = "#247c7c";

            Styles.Add(temp3);
        }

        async void OnStyleSelected(Style style)
        {
            if (style == null)
                return;

            App.Current.Resources["Primary"] = Color.FromHex(style.Primary);
            App.Current.Resources["PrimaryFaded"] = Color.FromHex(style.PrimaryFaded);
            App.Current.Resources["Secondary"] = Color.FromHex(style.Secondary);
        }
    }

    public class Style
    {
        public ImageSource Image { get; set; }
        public string Name { get; set; }
        public string Primary { get; set; } // #3D6D9B - azul, #D075BF - rosa, #8DD075 - verde
        public string PrimaryFaded { get; set; } // #B2D4F4
        public string Secondary { get; set; } // #247c7c

    }
}
