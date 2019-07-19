using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Entities.DataBaseEntities
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Name { get; set; }

        [Required]
        public int YearOfFoundation { get; set; }

        [Required]
        public int NumberOfWinWorldChamp { get; set; }

        [Required]
        public bool IsPaidEntryFee { get; set; }
    }
}
