using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Neibour.Models
{
    public class Ads
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id_ad { get; set; }
        public int id_user { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }
        public DateTime Date { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public string Place { get; set; }
        public int People { get; set; }
        public virtual User user { get; set; }
    }
}
