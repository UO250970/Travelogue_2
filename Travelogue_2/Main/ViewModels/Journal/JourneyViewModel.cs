using System;
using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journal
{
	public class JourneyViewModel : BaseViewModel
	{

		private string journeyId;

		public string Id { get; set; }

        public ImageSource CoverImage
        {
            get { return ImageSource.FromResource(CommonVariables.GenericImage); }
        }


        public string JourneyId
        {
            get
            {
                return journeyId;
            }
            set
            {
                journeyId = value;
                LoadJourneyId(value);
            }
        }

        public async void LoadJourneyId(string journeyId)
        {
            IsBusy = true;

            try
            {
                //var item = await DataStore.GetItemAsync(journeyId);
                //Id = item.Id;
                //Text = item.Text;
                //Description = item.Description;
            }
            catch (Exception)
            {
                //Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
            => IsBusy = true;


    }
}
