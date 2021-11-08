using Android.Graphics;

using Travelogue_2.Main.Services;
using System.IO;
using System;
using Path = System.IO.Path;
using Travelogue_2.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DependencyImplementation))]
namespace Travelogue_2.Droid.Services
{
    public class DependencyImplementation : IDependency
    {

        public static readonly int REQUEST_CODE_OPEN_DIRECTORY = 1;
        Stream str = null;

        public string Save(Stream stream, string name)
        {
            str = stream;
            str.Position = 0;
            var fileBytes = ReadFully(str);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(fileBytes, 0, fileBytes.Length);
            return ExportBitmapAsJPG(bitmap, name);
        }


        string ExportBitmapAsJPG(Bitmap bitmap, string name)
        {
            string sdCardPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string directoryname = Path.Combine(sdCardPath, "Travelogue_2");
            string filePath = Path.Combine(directoryname, name);
            var stream = new FileStream(filePath, FileMode.Create);
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            stream.Close();
            return filePath;
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