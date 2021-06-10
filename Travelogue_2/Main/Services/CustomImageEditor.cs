using Syncfusion.SfImageEditor.XForms;
using System;
using System.Collections.Generic;
using Travelogue_2.Main.Utils;

namespace Travelogue_2.Main.Services
{
	public class CustomImageEditor : SfImageEditor
	{
		public int JourneyId { get; set; }
		private List<string> HeaderButtonsNames { get; set; }
		private Dictionary<string, Delegate> HeaderButtons { get; set; }
		private List<string> FooterButtonsNames { get; set; }
		private Dictionary<string, Delegate> FooterButtons { get; set; }

		public CustomImageEditor(int JourneyId) : base()
		{
			this.JourneyId = JourneyId;

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

			DataBaseUtil.GetJourneyForJournal(JourneyId);

			FooterButtonsNames = new List<string>() { "Text", "Photo", "Shape" };
			FooterButtons = new Dictionary<string, Delegate>
			{
				{ FooterButtonsNames[0], new Action(TextButton) },
				{ FooterButtonsNames[1], new Action(PhotoButton) },
				{ FooterButtonsNames[2], new Action(ShapeButton) }
			};
		}

		private void StyleButtons()
		{
			ToolbarSettings.ToolbarItems.Clear();

			ToolbarSettings.ToolbarItems.Add(
				new HeaderToolbarItem() { Text = HeaderButtonsNames[0] });
			ToolbarSettings.ToolbarItems.Add(
				new HeaderToolbarItem() { Text = HeaderButtonsNames[1] });
			ToolbarSettings.ToolbarItems.Add(
				new HeaderToolbarItem() { Text = HeaderButtonsNames[3] });
			ToolbarSettings.ToolbarItems.Add(
				new HeaderToolbarItem() { Text = HeaderButtonsNames[4] });

			//ToolbarSettings.ToolbarItems.Add(
				//new FooterToolbarItem() { Icon = BitmapFactory.DecodeResource(Resources, Resource.Drawable.text) });
			ToolbarSettings.ToolbarItems.Add(
				new FooterToolbarItem() { Text = "More" });
		}

		private void ToolbarSettings_ToolbarItemSelected(object sender, ToolbarItemSelectedEventArgs e)
		{
			string Name = e.ToolbarItem.Text;

			if (HeaderButtonsNames.Contains(Name))
			{
				HeaderButtons[Name].DynamicInvoke(null);
			} else if (FooterButtonsNames.Contains(Name))
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

		public void SaveButton()
		{

		}

		public void TextButton()
		{

		}

		public void PhotoButton()
		{

		}

		public void ShapeButton()
		{

		}

		#endregion

	}
}
