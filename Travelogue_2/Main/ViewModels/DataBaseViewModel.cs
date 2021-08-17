using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
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

		public IDataStoreNU<Item> DataStore => DependencyService.Get<IDataStoreNU<Item>>();

        public void ExecuteLoadDataCommand()
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

        public virtual void OnAppearing()
            => IsBusy = true;



        async virtual internal void Back() => await Shell.Current.GoToAsync("..");

    }
}
