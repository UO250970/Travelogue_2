using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Travelogue_2.Main.Utils
{
    public static class ShareUtil
    {

        public static async Task ShareImage(string path)
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                File = new ShareFile(path)
            });
        }
    }
}
