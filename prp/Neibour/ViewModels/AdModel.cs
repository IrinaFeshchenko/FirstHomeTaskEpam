using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Neibour.ViewModels
{
    public class AdModel
    {
        public int Id_ad { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        public string Title { get; set; }
        public int id_user { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string Cost { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public DateTime Date { get; set; }
        public IFormFile Image1 { get; set; }
        public IFormFile Image2 { get; set; }
        public IFormFile Image3 { get; set; }
        public string Place { get; set; }
        public int People { get; set; }
    }
}
