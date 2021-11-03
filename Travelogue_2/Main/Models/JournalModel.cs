
using System.Collections.Generic;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
    public class JournalModel
    {
        public int Id { get; set; }

        public State JournalState { get; set; }

        public string Name { get; set; }

        public int JourneyId { get; set; }

        public int CoverId { get; set; }

        public ImageSource CoverSource { get; set; }

        public List<ImageModel> Pages { get; set; }
    }
}
