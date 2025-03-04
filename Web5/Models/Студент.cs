
namespace Web5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;


    public partial class Студент
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Студент()
        {
            this.Студенты_в_группах = new HashSet<Студенты_в_группах>();
        }

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

        [Key]
        public System.Guid ID_студента { get; set; }

        [Required]
        [DisplayName("Адрес")]
        public string Адрес_проживания { get; set; }

        [Required]
        [DisplayName("Дата рождения")]
        public System.DateTime Дата_рождения { get; set; }

        [Required]
        [DisplayName("Владение ИЯ")]
        public string Уровень_владения_ИЯ { get; set; }
        public Nullable<System.Guid> ID_пользователя { get; set; }
    
        public virtual Пользователь Пользователи { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Студенты_в_группах> Студенты_в_группах { get; set; }
    }
}
