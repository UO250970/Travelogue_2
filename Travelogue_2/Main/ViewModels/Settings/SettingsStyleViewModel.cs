using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Settings
{
	public class SettingsStyleViewModel : DataBaseViewModel
	{
        public Command<StyleModel> StyleTapped { get;}
        public ObservableCollection<StyleModel> Styles { get; }

        public SettingsStyleViewModel()
        {
            Styles = new ObservableCollection<StyleModel>();
            StyleTapped = new Command<StyleModel>(OnStyleSelected);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            Styles.Clear();

            DataBaseUtil.GetStyles().ForEach(x => Styles.Add(x));
        }

        void OnStyleSelected(StyleModel style)
        {
            if (style == null)
                return;

            Application.Current.Resources["Primary"] = Color.FromHex(style.Primary);
            Application.Current.Resources["PrimaryFaded"] = Color.FromHex(style.PrimaryFaded);
            Application.Current.Resources["Secondary"] = Color.FromHex(style.Secondary);
        }
    }

}
