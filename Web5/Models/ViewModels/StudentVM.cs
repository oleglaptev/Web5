using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web5.Models.ViewModels
{
    public class StudentVM
    {
        [Key]
        public System.Guid ID_студента { get; set; }

        [Required]
        [DisplayName("Фамилия")]
        [StringLength(100, MinimumLength = 2)]
        public string Фамилия { get; set; }

        [Required]
        [DisplayName("Имя")]
        [StringLength(100, MinimumLength = 2)]
        public string Имя { get; set; }

        [DisplayName("Отчество")]
        public string Отчество { get; set; }

        [Required]
        [DisplayName("Пол")]
        public bool Пол { get; set; }

        [Required]
        [DisplayName("Адрес")]
        public string Адрес_проживания { get; set; }

        [Required]
        [DisplayName("Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Дата_рождения { get; set; }

        [Required]
        [DisplayName("Владение ИЯ")]
        public string Уровень_владения_ИЯ { get; set; }
    }
}