using ExifLib;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.ViewModels;
using Travelogue_2.Main.ViewModels.Media;
using Travelogue_2.Main.Views.Media;
using Xamarin.Forms;

namespace Travelogue_2.Main.Services
{
    public abstract class PhotoRendererModel : DataBaseViewModel
    {
        public Command<ImageModel> ViewImageCommand { get; set; }

        public PhotoRendererModel()
        {
            ViewImageCommand = new Command<ImageModel>(x => ViewImageC(x));
        }

        public ImageModel ImageSelected { get; set; }

        public ImageModel AddImage(MediaFile file, bool temporal)
        {
            try
            {
                if (file != null)
                {
                    var temp = ExifReader.ReadJpeg(file.GetStream());

                    string latitud = DecodeLatitude(temp).ToString();
                    string longitud = DecodeLongitude(temp).ToString();

                    string name = "";
                    if (CurrentJourneyId != null)
                    {
                        name = DataBaseUtil.GetNameFromJourney(CurrentJourneyId);
                    }
                    else
                    {
                        name = App.LocResources[CommonVariables.BlankName];
                    }

                    ImageModel image;
                    if (temporal)
                    {
                        image = new ImageModel()
                        {
                            Path = file.Path,
                            Journey = name,
                            Latitud = latitud,
                            Longitud = longitud
                        };
                    }
                    else
                    {
                        image = DataBaseUtil.CreateImage(file.Path, "", name, latitud, longitud);
                    }
                    return image;
                }
                return null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        private static double DecodeLatitude(JpegInfo info)
        {
            double degrees = ToDegrees(info.GpsLatitude);
            return info.GpsLatitudeRef == ExifGpsLatitudeRef.North ? degrees : -degrees;
        }
        private static double DecodeLongitude(JpegInfo info)
        {
            double degrees = ToDegrees(info.GpsLongitude);
            return info.GpsLongitudeRef == ExifGpsLongitudeRef.East ? degrees : -degrees;
        }
        private static double ToDegrees(double[] coord)
        {
            return coord[0] + coord[1] / 60.0 + coord[2] / (60.0 * 60.0);
        }

        public int CoverImageHeight { get => CommonVariables.ImageMaxHeight; }

        public int CardImagesHeight { get => CommonVariables.ImageCardMaxHeight; }


        public EntryImageModel blankImage = new EntryImageModel()
        {
            Path = CommonVariables.Blank
        };
        public EntryImageModel BlankImage
        {
            get => blankImage;
            set
            {
                SetProperty(ref blankImage, value);
            }
        }

        public async void ViewImageC(ImageModel image)
        {
            ImageSelected = image;
            await Shell.Current.GoToAsync($"{nameof(ImageView)}?{ nameof(ImageViewModel.ImageId)}={ image.ImageId}");
        }
    }

    public interface IDependency
    {
        string Save(Stream stream, string name); // Select Directory
    }
}
