using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormulaOneProject.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "A mező kitöltése kötelező!")]
        [DisplayName("Felhasználónév")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A jelszónak legalább {2} karakter hosszúságúnak kell lennie!", MinimumLength = 6)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; }
    }
}
