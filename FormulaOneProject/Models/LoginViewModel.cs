using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormulaOneProject.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "A mező kitöltése kötelező!")]
        [DisplayName("Felhasználónév")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A mező kitöltése kötelező!")]
        [DisplayName("Jelszó")]
        public string Password { get; set; }
    }
}
