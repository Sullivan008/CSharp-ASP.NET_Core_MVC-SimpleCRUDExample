using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleCRUDExample.Models.Team
{
    public class TeamBaseViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [DisplayName("Unique Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Year of Foundation is required.")]
        [DisplayName("Year of Foundation")]
        public int YearOfFoundation { get; set; }

        [Required(ErrorMessage = "Number of won world Championships.")]
        [DisplayName("Number of won world Championships")]
        public int NumberOfWonWorldChampionships { get; set; }

        [Required(ErrorMessage = "Is paid entry fee is required.")]
        [DisplayName("Is paid entry fee?")]
        public bool IsPaidEntryFee { get; set; }
    }
}
