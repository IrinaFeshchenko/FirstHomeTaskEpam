using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neibour.Models
{
    public class User
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id_user { get; set; }
        public string Last_name { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Passport { get; set; }
        public DateTime Birthday { get; set; }
        public string Purpose { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Profile { get; set; }
        public DateTime Date_of_registration { get; set; }
        public string Gender { get; set; }

        public List<Ads> add { get; set; }
        public IEnumerable<Dialogs> dialogs { get; set; }
        public User()
        {
            add = new List<Ads>();
            dialogs = new List<Dialogs>();

        }
    }
}
