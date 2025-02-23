using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web5.Models.ViewModels
{
    public class UserVM
    {
        [Required]
        [DisplayName("Логин")]
        public string Login { get; set; }
        [Required]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}