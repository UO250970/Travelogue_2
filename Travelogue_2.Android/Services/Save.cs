using System;
using System.IO;
using Android.Content;
using Java.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android;
using Android.Content.PM;
using AndroidX.Core.Content;
using AndroidX.Core.App;
using Travelogue_2.Main.Utils;
using Android.App;

[assembly: Dependency(typeof(Travelogue_2.Droid.Services.Save))]
namespace Travelogue_2.Droid.Services
{
    public class Save : ISave
    {

        //Method to save document as a file in Android and view the saved document
        public async Task<string> SaveAndView(string fileName, string contentType, MemoryStream stream)
        {
            string root = null;

            if (ContextCompat.CheckSelfPermission(Forms.Context.ApplicationContext, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Activity) Forms.Context, new string[] { Manifest.Permission.WriteExternalStorage }, 1);
            }

            //Get the root path in android device.
            /*if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }*/
            root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Create directory and file 
            Java.IO.File myDir = new Java.IO.File(root + "/Travelogue");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            //Remove if the file exists
            if (file.Exists()) file.Delete();

            //Write the stream into the file
            FileOutputStream outs = new FileOutputStream(file);
            outs.Write(stream.ToArray());

            outs.Flush();
            outs.Close();

            //Invoke the created file for viewing
            if (file.Exists())
            {
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                //Android.Net.Uri path = FileProvider.GetUriForFile(Forms.Context, Android.App.Application.Context.PackageName + ".fileprovider", file); //Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                Android.Net.Uri uri = FileProvider.GetUriForFile(Android.App.Application.Context, Android.App.Application.Context.PackageName + ".fileprovider", file);
                //Android.Net.Uri uri = FileProvider.GetUriForFile(Forms.Context.ApplicationContext, Android.App.Application.Context.PackageName + ".provider", file);
                //Android.Net.Uri uri = FileProvider.GetUriForFile(Forms.Context.ApplicationContext, "com.company.app.fileprovider", file);
                intent.SetDataAndType(uri, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
            }

            return file.Path;
        }
    }

}