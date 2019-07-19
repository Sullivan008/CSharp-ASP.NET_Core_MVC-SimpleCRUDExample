using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormulaOneProject.Models
{
    public class TeamViewModel
    {
        [Key]
        public int TeamID { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező!")]
        [DisplayName("Név")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező!")]
        [DisplayName("Az alapítási év")]
        public int YearOfFoundation { get; set; }

        public List<Year> Years { get; set; } = new List<Year>();

        [Required]
        [DisplayName("Megnyert világbajnokságok száma")]
        public int NumberOfWinWorldChamp { get; set; }

        [Required]
        [DisplayName("Nevezési díjat befizette-e")]
        public bool IsPaidEntryFee { get; set; }
    }

    public partial class Year
    {
        public int YearID { get; set; }

        public string YearOfFoundation { get; set; }
    }
}
