using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Neibour.Models;
using Neibour.ViewModels;

namespace Neibour.Controllers
{
    public class UserController : Controller
    {
        private UserContext db;

        public UserController(UserContext context)
        {
            db = context;
        }


        //Добавление нового обьявления ЗАКОНЧЕННО
        //ЗАКОНЧЕННО
        [HttpGet]
        public IActionResult Ads()
        {
            return View();
        }
        //ЗАКОНЧЕННО
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ads(AdModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime date1 = DateTime.Now;
                var Profile = db.Users.Where(p => p.Email == @User.Identity.Name);
                int id = 0;
                foreach (User user in Profile)
                    id = user.Id_user;
                Ads new_add = new Ads { id_user = id, Title = model.Title, Description = model.Description, Cost = model.Cost, Date = date1,Place=model.Place,People=model.People };
                if (model.Image1 != null)
                {
                    byte[] imageData = null;
                    byte[] imageData2 = null;
                    byte[] imageData3 = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(model.Image1.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Image1.Length);
                    }
                    if (model.Image2 != null)
                    {
                        using (var binaryReader1 = new BinaryReader(model.Image2.OpenReadStream()))
                        {
                            imageData2 = binaryReader1.ReadBytes((int)model.Image2.Length);
                        }
                    }
                    if (model.Image3 != null)
                    {
                        using (var binaryReader2 = new BinaryReader(model.Image3.OpenReadStream()))
                        {
                            imageData3 = binaryReader2.ReadBytes((int)model.Image3.Length);
                        }
                    }
                    // установка массива байтов
                    new_add.Image1 = imageData;
                    new_add.Image2 = imageData2;
                    new_add.Image3 = imageData3;
                }
                db.Ads.Add(new_add);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
            
        }

        //Отображение профиля ЗАКОНЧЕННО
        [HttpGet]
        public IActionResult Profile()
        {
            var Profile = db.Users.Where(p => p.Email == @User.Identity.Name);
            ViewBag.data = Profile;
            return View();

        }

        //Вывод всех своих обьявлений ЗАКОНЧЕННО
        [HttpGet]
        public IActionResult ADS_with_search()
        {
            var Profile = db.Users.Where(p => p.Email == @User.Identity.Name);
            int id = 0;
            foreach (User user in Profile)
                id = user.Id_user;
            var ads_of_user = from n in db.Ads
                         where n.id_user == id
                         select new Ads
                         {
                             Id_ad = n.Id_ad,
                             Title = n.Title,
                             Description = n.Description,
                             Cost = n.Cost,
                             Date = n.Date,
                             Image1 = n.Image1
                         };
            ViewBag.data = ads_of_user;
            return View();
        }
        //ЗАКОНЧЕННО ПРОСМОТР

        [HttpGet]
        public IActionResult Watch(int id)
        {
            var ad_of_user = from n in db.Ads
                              where n.Id_ad == id
                              select new Ads
                              {
                                  Id_ad = n.Id_ad,
                                  Title = n.Title,
                                  Description = n.Description,
                                  Cost = n.Cost,
                                  Date = n.Date,
                                  Image1 = n.Image1,
                                  Image2=n.Image2,
                                  Image3=n.Image3,
                                  People =n.People,
                                  Place=n.Place
                              };
            ViewBag.data = ad_of_user;
            return View();
        }









        //Редактироание своего выбранного обявения 
       [HttpGet]
        public async Task<IActionResult> Edit_my_ad(int? id)
        {
            int id_user = 0;
            var Profile = db.Users.Where(p => p.Email == @User.Identity.Name);
            foreach (User user in Profile)
                id_user = user.Id_user;

            Ads ads = await db.Ads.FirstOrDefaultAsync(p => p.Id_ad == id && p.id_user==id_user);
                return View(ads);
        }
        [HttpPost]
        public async Task<IActionResult> Edit_my_ad(AdModel ads)
        {
            var Profile = db.Users.Where(p => p.Email == @User.Identity.Name);
            int id = 0;
            foreach (User user in Profile)
                id = user.Id_user;
            Ads new_add = new Ads {  Id_ad = ads.Id_ad, id_user = id, Title = ads.Title, Description = ads.Description, Cost = ads.Cost, Date = ads.Date,People=ads.People,Place=ads.Place };
            if (ads.Image1!=null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(ads.Image1.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)ads.Image1.Length);
                }
                new_add.Image1 = imageData;
            }
            if(ads.Image2!=null)
            {
                byte[] imageData2 = null;
                using (var binaryReader = new BinaryReader(ads.Image2.OpenReadStream()))
                {
                    imageData2 = binaryReader.ReadBytes((int)ads.Image2.Length);
                }
                new_add.Image2 = imageData2;
            }
            if (ads.Image3 != null)
            {
                byte[] imageData3 = null;
                using (var binaryReader = new BinaryReader(ads.Image3.OpenReadStream()))
                {
                    imageData3 = binaryReader.ReadBytes((int)ads.Image3.Length);
                }
                new_add.Image3 = imageData3;
            }
            var ad_of_user = from n in db.Ads
                             where n.Id_ad == ads.Id_ad
                             select new Ads
                             {
                                 Image1 = n.Image1,
                                 Image2 = n.Image2,
                                 Image3 = n.Image3
                             };
            foreach(var add in ad_of_user)
            {
                if (ads.Image1 == null && add.Image1 != null)
                {
                    //byte[] a = null;
                    //a = add.Image1;
                    //new_add.Image1 = a;
                    new_add.Image1 = null;
                }
                if(ads.Image1 != null && add.Image1 != null)
                {
                    byte[] a = null;
                    a = add.Image1;
                    new_add.Image1 = a;
                }
                if (ads.Image2 == null && add.Image2 != null)
                {
                    //byte[] a2 = null;
                    //a2 = add.Image2;
                    //new_add.Image2 = a2;
                    new_add.Image2 = null;
                }
                if (ads.Image2 != null && add.Image2 != null)
                {
                    byte[] a2 = null;
                    a2 = add.Image2;
                    new_add.Image2 = a2;
                    //new_add.Image2 = null;
                }
                if (ads.Image3 == null && add.Image3 != null)
                {
                    //byte[] a3 = null;
                    //a3 = add.Image3;
                    //new_add.Image3 = a3;
                    new_add.Image3 = null;
                }
                if (ads.Image3 != null && add.Image3 != null)
                {
                    byte[] a3 = null;
                    a3 = add.Image3;
                    new_add.Image3 = a3;
                    //new_add.Image3 = null;
                }
                if (ads.Image1==null && add.Image1==null)
                {
                    new_add.Image1 = null;
                }
                if (ads.Image2 == null && add.Image2 == null)
                {
                    new_add.Image2 = null;
                }
                if (ads.Image3 == null && add.Image3 == null)
                {
                    new_add.Image3 = null;
                }
                if (ads.Image1 != null && add.Image1 == null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(ads.Image1.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)ads.Image1.Length);
                    }
                    new_add.Image1 = imageData;
                }
                if (ads.Image2 != null && add.Image2 == null)
                {
                    byte[] imageData2 = null;
                    using (var binaryReader = new BinaryReader(ads.Image2.OpenReadStream()))
                    {
                        imageData2 = binaryReader.ReadBytes((int)ads.Image2.Length);
                    }
                    new_add.Image2 = imageData2;
                }
                if (ads.Image3 != null && add.Image3 == null)
                {
                    byte[] imageData3 = null;
                    using (var binaryReader = new BinaryReader(ads.Image3.OpenReadStream()))
                    {
                        imageData3 = binaryReader.ReadBytes((int)ads.Image3.Length);
                    }
                    new_add.Image3 = imageData3;
                }
            }
            db.Ads.Update(new_add);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }



        //ЗАКОНЧЕННО
        //Удаление своего выбранного обьявления ЗАКОНЧЕННО
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            int id_user = 0;
            var Profile = db.Users.Where(p => p.Email == @User.Identity.Name);
            foreach (User user in Profile)
            id_user = user.Id_user;
            Ads add_for_delete = await db.Ads.FirstOrDefaultAsync(p => p.Id_ad == id && p.id_user==id_user);

            var ad_of_user = from n in db.Ads
                             where n.Id_ad == id
                             select new Ads
                             {
                                 Image1 = n.Image1,
                                 Image2 = n.Image2,
                                 Image3 = n.Image3
                             };
            ViewBag.data = ad_of_user;
            return View(add_for_delete);
        }
        //ЗАКОНЧЕННО
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            int id_user = 0;
            var Profile = db.Users.Where(p => p.Email == @User.Identity.Name);
            foreach (User user in Profile)
            id_user = user.Id_user;
            Ads delete = await db.Ads.FirstOrDefaultAsync(p => p.Id_ad == id && p.id_user==id_user);
            db.Ads.Remove(delete);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }






        //ЗАКОНЧЕННО

        [HttpGet]
        public IActionResult All_ads_of_site()
        {
            if (User.Identity.IsAuthenticated)
            {
                var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                int Id = 0;
                foreach(User user in aDDs)
                    Id = user.Id_user;
                var phones = from user in db.Users
                             join ads in db.Ads on user.Id_user equals ads.id_user where user.Id_user!=Id
                             select new Ads_of_users
                             {
                                 First_name = user.First_name,
                                 Last_name = user.Last_name,
                                 id_ad = ads.Id_ad,
                                 id_user = ads.id_user,
                                 Title = ads.Title,
                                 Description = ads.Description,
                                 Cost = ads.Cost,
                                 date = ads.Date,
                                 Image1 = ads.Image1,
                                 Image2 = ads.Image2,
                                 Image3=ads.Image3
                             };
                ViewBag.data = phones;
                return View();
            }
            else
            { 

                var phones = from user in db.Users
                                      join ads in db.Ads on user.Id_user equals ads.id_user
                                      select new Ads_of_users
                                      {
                First_name = user.First_name,
                Last_name = user.Last_name,
                id_ad = ads.Id_ad,
                id_user = ads.id_user,
                Title = ads.Title,
                Description = ads.Description,
                Cost = ads.Cost,
                date = ads.Date,
                Image1=ads.Image1,
                Image2=ads.Image2,
                Image3=ads.Image3
            };
                ViewBag.data = phones;
                return View();
            }
        }


        [HttpGet]
        public IActionResult _AdsFilter(string company, int people,string gender)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (company != "All" && people != 0 && gender != "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where user.Id_user != Id && ads.Place == company && ads.People == people && user.Gender == gender
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (company != "All" && people != 0 && gender == "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where user.Id_user != Id && ads.Place == company && ads.People == people
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (company != "All" && gender != "All" && people == 0)
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where user.Id_user != Id && ads.Place == company && user.Gender == gender
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (people != 0 && gender != "All" && company == "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where user.Id_user != Id && ads.People == people && user.Gender == gender
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (company != "All" && people == 0 && gender == "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where user.Id_user != Id && ads.Place == company
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (company == "All" && people != 0 && gender == "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where user.Id_user != Id && ads.People == people
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (company == "All" && people == 0 && gender != "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where user.Id_user != Id && user.Gender == gender
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where user.Id_user != Id
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
            }
            else
            {
                if (company != "All" && people != 0 && gender != "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where ads.Place == company && ads.People == people && user.Gender == gender
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (company != "All" && people != 0 && gender == "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where ads.Place == company && ads.People == people
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (company != "All" && gender != "All" && people == 0)
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where ads.Place == company && user.Gender == gender
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (people != 0 && gender != "All" && company == "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where ads.People == people && user.Gender == gender
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (company != "All" && people == 0 && gender == "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where ads.Place == company
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (company == "All" && people != 0 && gender == "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where ads.People == people
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else if (company == "All" && people == 0 && gender != "All")
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 where user.Gender == gender
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
                else
                {
                    var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
                    int Id = 0;
                    foreach (User user in aDDs)
                        Id = user.Id_user;
                    var phones = from user in db.Users
                                 join ads in db.Ads on user.Id_user equals ads.id_user
                                 select new Ads_of_users
                                 {
                                     First_name = user.First_name,
                                     Last_name = user.Last_name,
                                     id_ad = ads.Id_ad,
                                     id_user = ads.id_user,
                                     Title = ads.Title,
                                     Description = ads.Description,
                                     Cost = ads.Cost,
                                     date = ads.Date,
                                     Image1 = ads.Image1,
                                     Image2 = ads.Image2,
                                     Image3 = ads.Image3
                                 };
                    ViewBag.data = phones;
                }
            }
            return View();
        }


















        //ЗАКОНЧЕННО

        [HttpGet]
        public IActionResult Watch_add(int id)
        {
            var add = from ads in db.Ads
                      join user in db.Users on ads.id_user equals user.Id_user
                      where ads.Id_ad == id
                      select new Ads_of_users
                      {
                          id_user = user.Id_user,
                          id_ad=ads.Id_ad,
                          First_name = user.First_name,
                          Last_name = user.Last_name,
                          Title = ads.Title,
                          Description = ads.Description,
                          Cost = ads.Cost,
                          date = ads.Date,
                          Image1 = ads.Image1,
                          Image2 = ads.Image2,
                          Image3 = ads.Image3,
                          Place = ads.Place,
                          People = ads.People
                      };
            ViewBag.my_ad = add;
            return View();
        }







        [HttpGet]
        public async Task<IActionResult> Edit_profile(int? id)
        {

            User profile = await db.Users.FirstOrDefaultAsync(p => p.Email == User.Identity.Name && p.Id_user==id);
            return View(profile);
        }
        [HttpPost]
        public async Task<IActionResult> Edit_profile(ProfileModel _profile_model)
        {
            User new_add = new User { Id_user = _profile_model.id_user, Last_name = _profile_model.Last_name, First_name = _profile_model.First_name, Middle_name = _profile_model.Middle_name,Passport=_profile_model.Passport, Birthday = _profile_model.Birthday, Purpose = _profile_model.Purpose,Email = _profile_model.Email,Password=_profile_model.Password,Date_of_registration = _profile_model.Date_of_registration,Gender = _profile_model.Gender };
            if (_profile_model.Profile != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(_profile_model.Profile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)_profile_model.Profile.Length);
                }
                new_add.Profile = imageData;
            }
            db.Users.Update(new_add);
            await db.SaveChangesAsync();
            return RedirectToAction("Profile", "User");
        }


        [HttpGet]
        public PartialViewResult _deletePhoto(int id_add)
        {
            var ads = db.Ads.Where(p => p.Id_ad == id_add);
            ViewBag.data = id_add;
            ViewBag.addphoto = ads;
            return PartialView("~/Views/Shared/_deletePhoto.cshtml");
        }
        [HttpGet]
        public IActionResult Profile_from_add(int id)
        {
            var Profile = db.Users.Where(p => p.Id_user == id);
            ViewBag.data = Profile;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChatAsync(int ID)
        {
            var aDDs = db.Users.Where(p => p.Email == @User.Identity.Name);
            int Id = 0;
            IQueryable<Dialogs_of_user> Dialogs = null;
            foreach (User user in aDDs)
                Id = user.Id_user;
            Dialogs dial = await db.Dialogs.FirstOrDefaultAsync(u => (u.User1 == ID && u.User2==Id) ||(u.User1==Id && u.User2==ID));
            if (dial==null)
            {
                Dialogs new_dialog = new Dialogs { User1 = Id, User2 = ID};
                db.Dialogs.Add(new_dialog);
                db.SaveChanges();
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
            }
            else
            {
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
            }
           

            return View();
        }

    }


}