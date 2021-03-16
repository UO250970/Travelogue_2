using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels
{
	public abstract class DataBaseViewModel : BaseViewModel
	{

		bool isBusy = false;
		public bool IsBusy
		{
			get => isBusy;
			set => SetProperty(ref isBusy, value);
		}

		public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public async Task ExecuteLoadDataCommand()
        {
            IsBusy = true;

            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

		public abstract void LoadData();

        public void OnAppearing()
            => IsBusy = true;

	}
}
