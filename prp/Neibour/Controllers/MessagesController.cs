using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neibour.Models;
using Neibour.ViewModels;

namespace Neibour.Controllers
{
    [Route("chat/[controller]")]
    public class MessagesController : Controller
    {
        private UserContext db;
        public MessagesController(UserContext db)
        {
            this.db = db;
        }
        [HttpGet("{group_id}")]
        public IEnumerable<Messages> GetById(int group_id)
        {
            return db.Messages.Where(gb => gb.id_dialog == group_id);
        }
        [HttpPost]
        public IActionResult Create([FromBody] MessagesModel message)
        {
            var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
            int Id = 0;
            foreach (User user in aDDs)
                Id = user.Id_user;
            Messages new_message = new Messages { id_u = Id, Message = message.Message, id_dialog = message.id_dialog };

            db.Messages.Add(new_message);
            db.SaveChanges();

            return new ObjectResult(new { status = "success", data = new_message });
        }
    }
}