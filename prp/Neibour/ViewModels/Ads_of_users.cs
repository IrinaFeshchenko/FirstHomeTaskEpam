using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neibour.ViewModels
{
    public class Ads_of_users
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public int id_ad { get; set; }
        public int id_user { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }
        public DateTime date { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public string Place { get; set; }
        public int People { get; set; }
        public string Gender { get; set; }
    }
}
