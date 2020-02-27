using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.DataAccessLayer.Entities.Core;

namespace Data.DataAccessLayer.Entities
{
    public class Team : IEntity
    {
        [Key]
        public int TeamId { get; set; }

        [Column(TypeName = "NVARCHAR(250)")]
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
