using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Neibour.ViewModels
{
    public class RegisterModel
    {
        
        [Required(ErrorMessage = "Не указана фамилия")]
        public string Last_name { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string First_name { get; set; }
        [Required(ErrorMessage = "Не указано отчество")]
        public string Middle_name { get; set; }
        [Required(ErrorMessage = "Не указан пасспорт")]
        public string Passport { get; set; }
        [Required(ErrorMessage = "Не указана дата рождения")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Не указана цель")]
        public string Purpose { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
        public DateTime Date_of_registration { get; set; }
        public string Gender { get; set; }
    }
}
