

using System.ComponentModel.DataAnnotations;

namespace FilmsStorage.Models.ViewModels
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string LoginName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordRepeat { get; set; }
    }
}