using Android.Graphics;

using Android.Content;
using Android.Widget;
using Travelogue_2.Droid.Services;
using Travelogue_2.Main.Services;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DependencyImplementation))]
namespace Travelogue_2.Droid.Services
{
    public class DependencyImplementation : IDependency
    {
        public static readonly int REQUEST_CODE_OPEN_DIRECTORY = 1;
        Stream str = null;

        public void Save(Stream stream, string name)
        {
            str = stream;
            str.Position = 0;
            var fileBytes = ReadFully(str);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(fileBytes, 0, fileBytes.Length);
            ExportBitmapAsJPG(bitmap, name);
        }


        void ExportBitmapAsJPG(Bitmap bitmap, string name)
        {
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var filePath = System.IO.Path.Combine(sdCardPath, name);
            var stream = new FileStream(filePath, FileMode.Create);
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            stream.Close();
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}