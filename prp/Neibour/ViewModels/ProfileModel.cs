using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neibour.ViewModels
{
    public class ProfileModel
    {
        public int id_user { get; set; }
        public string Last_name { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Passport { get; set; }
        public DateTime Birthday { get; set; }
        public string Purpose { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Profile { get; set; }
        public DateTime Date_of_registration { get; set; }
        public string Gender { get; set; }
    }
}
