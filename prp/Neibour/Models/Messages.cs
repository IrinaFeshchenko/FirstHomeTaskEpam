using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neibour.Models
{
    public class Messages
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id_message { get; set; }
        public string Message { get; set; }
        public int id_u { get; set; }
        public DateTime When { get; set; }
        public int id_dialog { get; set; } 
        public virtual Dialogs dialog { get; set; }
    }
}
