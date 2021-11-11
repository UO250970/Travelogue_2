using Travelogue_2.Main.Utils;

namespace Travelogue_2.Main.ViewModels
{
    public abstract class ModelationSelectorViewModel : DataBaseViewModel
    {

        public string TextSelected
        {
            get => DataBaseUtil.GetTextSelectedId();
            set
            {
                DataBaseUtil.SetTextSelectedId(value);
            }
        }

        public string ImageSelectedPath
        {
            get => DataBaseUtil.GetImageSelectedPath();
            set
            {
                DataBaseUtil.SetImageSelectedPath(value);
            }
        }
    }
}
