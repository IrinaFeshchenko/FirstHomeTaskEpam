using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Neibour.Models;
using Neibour.ViewModels;

namespace Neibour.Controllers
{
    public class ChatController : Controller
    {
        private UserContext db;
        public ChatController(UserContext db)
        {
            this.db = db;
        }
        public IActionResult Chat()
        {
            var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
            int Id = 0;
            IQueryable<Dialogs_of_user> Dialogs = null;
            foreach (User user in aDDs)
                Id = user.Id_user;

            Dialogs = from user in db.Users
                      from dialog in db.Dialogs
                      where ((user.Id_user == dialog.User1 && user.Id_user != Id && dialog.User2 == Id) || (user.Id_user == dialog.User2 && user.Id_user != Id && dialog.User1 == Id))
                      select new Dialogs_of_user
                      {
                          Id_dialog = dialog.Id_dialog,
                          User1 = dialog.User1,
                          User2 = dialog.User2,
                          User2_Last_name = user.Last_name,
                          User2_name = user.First_name
                      };

            var groups = Dialogs;
            ViewBag.datad = groups;

            return View();
        }
        [HttpGet]
        public IActionResult GetComments(int id)
        {
            var messages = from sms in db.Messages
                           from user in db.Users
                           where sms.id_dialog == id && sms.id_u == user.Id_user
                           select new MessagesModel
                           {
                               id_dialog = sms.id_dialog,
                               id_u = sms.id_u,
                               Message = sms.Message,
                               First_name = user.First_name,
                               Last_name = user.Last_name,
                               WhenD = sms.When
                           };
            ViewBag.datad = messages;
            ViewData["Dialog"] = id;
            return PartialView("~/Views/Shared/_Message.cshtml", messages);

        }




        [HttpGet]
        public PartialViewResult _AddMessage(int id_dialog)
        {
            int id_Dialog = 0;
            int id_user = 0;
            var Profile = db.Users.Where(p => p.Email == @User.Identity.Name);
            foreach (User user in Profile)
                id_user = user.Id_user;

            var ads = db.Messages.Where(p => p.id_u == id_user && p.id_dialog == id_dialog);
            foreach (var ad in ads)
            {
                id_Dialog = ad.id_dialog;
            }
            ViewBag.Dialog = id_dialog;
            return PartialView("~/Views/Shared/_AddMessage.cshtml");
        }

        [HttpPost]
        public ActionResult AddComment(MessagesModel messages)
        {
            Messages add_msg = null;


            if (messages != null)
            {
                DateTime date1 = DateTime.Now;
                var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                int Id = 0;
                foreach (User user in aDDs)
                    Id = user.Id_user;
                add_msg = new Messages { Message = messages.Message, id_u = Id, When = date1, id_dialog = messages.id_dialog };
            }
            int i = add_msg.id_dialog;
            db.Messages.Add(add_msg);
            db.SaveChanges();

            return RedirectToAction("GetComments", "Chat", new { id = i });
        }

    }
}