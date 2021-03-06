﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neibour.Models;
using Neibour.ViewModels;

namespace Neibour.Controllers
{
    public class AccountController : Controller
    {
        private UserContext db;
        public AccountController(UserContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password  && u.Passport=="+");
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            DateTime date1 = DateTime.Now;
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    if (Convert.ToInt32(model.Passport) >= 24)
                    {
                        // добавляем пользователя в бд
                        db.Users.Add(new User
                        {
                            First_name = model.First_name,
                            Last_name = model.Last_name,
                            Middle_name = model.Middle_name,
                            Passport = "+",
                            Birthday = model.Birthday,
                            Purpose = model.Purpose,
                            Email = model.Email,
                            Password = model.Password,
                            Profile = null,
                            Date_of_registration = date1,
                            Gender = model.Gender
                        });
                        await db.SaveChangesAsync();

                        await Authenticate(model.Email); // аутентификация

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        db.Users.Add(new User
                        {
                            First_name = model.First_name,
                            Last_name = model.Last_name,
                            Middle_name = model.Middle_name,
                            Passport = "-",
                            Birthday = model.Birthday,
                            Purpose = model.Purpose,
                            Email = model.Email,
                            Password = model.Password,
                            Profile = null,
                            Date_of_registration = date1
                        });
                        await db.SaveChangesAsync();

                        //await Authenticate(model.Email); // аутентификация

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> ReturnAsync(string First_name,string Last_name,string email)
        {
            DateTime date1 = DateTime.Now;
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Passport == "+");
                if (user != null)
                {
                    await Authenticate(email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    db.Users.Add(new User
                    {
                        First_name = Last_name,
                        Last_name = First_name,
                        Middle_name = null,
                        Passport = "+",
                        Birthday = date1,
                        Purpose = null,
                        Email = email,
                        Password = "google",
                        Profile = null,
                        Date_of_registration = date1
                    });
                    await db.SaveChangesAsync();

                    await Authenticate(email); // аутентификация

                    //return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}