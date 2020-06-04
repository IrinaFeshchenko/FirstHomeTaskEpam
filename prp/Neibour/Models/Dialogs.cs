using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neibour.Models
{
    public class Dialogs
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id_dialog { get; set; }
        public int User1 { get; set; }
        public int User2 { get; set; }
        public DateTime Last_date { get; set; }
        public string Last_message { get; set; }
        public List<Messages> sms { get; set; }
        public Dialogs()
        {
            sms = new List<Messages>();
        }
        public virtual User dial { get; set; }
    }
}
