using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Syncfusion.SfImageEditor.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Travelogue_2.Main.ViewModels.Modelation.Modelate;
using Xamarin.Forms;
using ToolbarItem = Syncfusion.SfImageEditor.XForms.ToolbarItem;

namespace Travelogue_2.Main.Services
{
    public class CustomImageEditor : SfImageEditor
    {
        public PageModelationViewModel Model { get; set; }
        private List<string> HeaderButtonsNames { get; set; }
        private Dictionary<string, Delegate> HeaderButtons { get; set; }
        private List<string> FooterButtonsNames { get; set; }
        private Dictionary<string, Delegate> FooterButtons { get; set; }

        private List<string> SubFooterButtonsNames { get; set; }
        private Dictionary<string, Delegate> SubFooterButtons { get; set; }

        private Stream Edits;

        public CustomImageEditor() 
        {
            PrepareButtons();
            StyleButtons();

            ToolbarSettings.ToolbarItemSelected += ToolbarSettings_ToolbarItemSelected;

            ItemSelected += Editor_ItemSelected;
            ItemUnselected += Editor_ItemUnselected;
        }

        private void PrepareButtons()
        {
            HeaderButtonsNames = new List<string>() { "Reset", "Undo", "Redo", "Save" };
            HeaderButtons = new Dictionary<string, Delegate>
            {
                { HeaderButtonsNames[0], new Action(ResetButton) },
                { HeaderButtonsNames[1], new Action(UndoButton) },
                { HeaderButtonsNames[2], new Action(RedoButton) },
                { HeaderButtonsNames[3], new Action(SaveButton) }
            };

            FooterButtonsNames = new List<string>() { "Text", "Photo", "Shape" };
            FooterButtons = new Dictionary<string, Delegate>
            {
                { FooterButtonsNames[0], new Action(TextButtonSelector) },
                { FooterButtonsNames[1], new Action(PhotoButtonSelector) },
                { FooterButtonsNames[2], new Action(ShapeButton) }
            };

            SubFooterButtonsNames = new List<string>() { "Event", "Reserv", "Entry" };
            SubFooterButtons = new Dictionary<string, Delegate>
            {
                { SubFooterButtonsNames[0], new Action(EventButton) },
                { SubFooterButtonsNames[1], new Action(ReservButton) },
                { SubFooterButtonsNames[2], new Action(EntryButton) }
            };

            ImageLoaded += TextButton;
            ImageLoaded += PhotoButton;

            RotatableElements = ImageEditorElements.Text | ImageEditorElements.CustomView;
        }

        private void StyleButtons()
        {
            ToolbarSettings.ToolbarItems.Clear();

            ToolbarSettings.ToolbarItems.Add(
                new HeaderToolbarItem() { Text = HeaderButtonsNames[0] });
            ToolbarSettings.ToolbarItems.Add(
                new HeaderToolbarItem() { Text = HeaderButtonsNames[1] });
            ToolbarSettings.ToolbarItems.Add(
                new HeaderToolbarItem() { Text = HeaderButtonsNames[2] });
            ToolbarSettings.ToolbarItems.Add(
                new HeaderToolbarItem() { Text = HeaderButtonsNames[3] });

            ToolbarSettings.ToolbarItems.Add(
                new FooterToolbarItem() 
                { 
                    Text = FooterButtonsNames[0],
                    SubItems = new ObservableCollection<ToolbarItem>
                    {
                        new ToolbarItem() { Text = SubFooterButtonsNames[0] },
                        new ToolbarItem() { Text = SubFooterButtonsNames[1] },
                        new ToolbarItem() { Text = SubFooterButtonsNames[2] }
                    }
                });
            ToolbarSettings.ToolbarItems.Add(
                new FooterToolbarItem() { Text = FooterButtonsNames[1] });
            ToolbarSettings.ToolbarItems.Add(
                new FooterToolbarItem() { Text = FooterButtonsNames[2] });
        }

        private void ToolbarSettings_ToolbarItemSelected(object sender, ToolbarItemSelectedEventArgs e)
        {
            string Name = e.ToolbarItem.Text;

            if (HeaderButtonsNames.Contains(Name))
            {
                HeaderButtons[Name].DynamicInvoke(null);
            }
            else if (FooterButtonsNames.Contains(Name))
            {
                FooterButtons[Name].DynamicInvoke(null);
            }
        }

        private void Editor_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            var Settings = e.Settings;

            if (Settings is PenSettings)
            {
            }
            else
            {
            }
        }

        private void Editor_ItemUnselected(object sender, ItemUnselectedEventArgs e)
        {
            var settings = e.Settings;

            if (settings is PenSettings)
            {
            }
            else
            {
            }
        }

        #region Actions

        public void ResetButton() => Reset();

        public void UndoButton() => Undo();

        public void RedoButton() => Redo();

        public void SaveButton() => Save();

        public void TextButtonSelector()
        {
            Model.SelectText();
        }

        public void TextButton(Object sender, ImageLoadedEventArgs args)
        {
            if (!Model.TextSelected.Equals(string.Empty))
            {
                AddText(Model.TextSelected);
            }
            Model.TextSelected = string.Empty;
        }

        public void EventButton()
        {
            Model.GetEvents();
        }

        public void ReservButton()
        {
            Model.GetReservs();
        }

        public void EntryButton()
        {
            Model.GetEntries();
        }

        public void PhotoButtonSelector()
        {
            Model.SelectPhotos();
        }

        public void PhotoButton(Object sender, ImageLoadedEventArgs args)
        {
            if (!Model.ImageSelectedPath.Equals(string.Empty))
            {
                Image customImage = new Image() { HeightRequest = 200, WidthRequest = 200 };
                string path = Model.ImageSelectedPath;
                customImage.Source = ImageSource.FromFile(path);
                AddCustomView(customImage, new CustomViewSettings());

//                Model.ImageSelectedPath = string.Empty;
            }

        }

        public void ShapeButton()
        {

        }

        #endregion

        public static async Task<PermissionStatus> CheckPermissions()
        {
            PermissionStatus statusStorage = PermissionStatus.Unknown;
            try
            {
                statusStorage = CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>().Result;
                if (statusStorage != PermissionStatus.Granted)
                {
                    statusStorage = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                }

                Debug.WriteLine("Permision storage: " + statusStorage);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error permision storage: " + e.StackTrace);
            }

            return statusStorage;
        }
    }
}
