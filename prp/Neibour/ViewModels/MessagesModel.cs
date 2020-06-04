using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neibour.ViewModels
{
    public class MessagesModel
    {
        public int Id_message { get; set; }
        public string Message { get; set; }
        public int id_u { get; set; }
        public DateTime WhenD { get; set; }
        public int id_dialog { get; set; }
        public string Last_name { get; set; }
        public string First_name { get; set; }
    }
}
