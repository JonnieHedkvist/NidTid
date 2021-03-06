﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using NidTid.WebUI.Models;
using System.Web.Services;
using System.Web.Security;


namespace NidTid.WebUI.Controllers
{
    //[Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private IUserRepository repository;

        public UserController(IUserRepository userRepository) {
            this.repository = userRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.RedirectFromLoginPage(model.UserName, model.RememberMe);
                }

                ModelState.AddModelError("", "Felaktikt användarnamn eller lösenord");
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User", null);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Index(int? id)
        {
            User selectedUser = new User();
            if (id != null) {
                selectedUser = repository.Users.FirstOrDefault(u => u.Id == id);
            }
            return View(selectedUser);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create() { 
            User user = new User();
            return PartialView(user);
        }

        [HttpPost]
        public ActionResult SaveUser(User currentUser)
        {
            var currentId = 0;
            if (ModelState.IsValid)
            {
                currentId = repository.SaveUser(currentUser);
            }
            else
            {
                //FIXA FELMEDDELANDE
            }
            return RedirectToAction(actionName: "Index", routeValues: new { id = currentId });
        }

        [HttpPost]
        [Authorize]
        public String DeleteUser(int id)
        {
            string msg = "";
            User user = repository.Users.FirstOrDefault(u => u.Id == id);
            if (user.MeterPost.Count() > 0 || user.Project.Count() > 0 || user.Report.Count() > 0 )
            {
                msg = "Användaren är knuten till andra object i databasen (projekt, körjournaler eller timrapporter), och kan därför inte raderas!";
            }
            else
            {
                repository.DeleteUser(user);
                msg = "Användaren har raderats ur databasen";
            }
            return msg;
        }

        public ActionResult FilteredUsers(string term)
        {
            var filteredUsers = repository.Users.Where(c => c.Name.StartsWith(term)).Select(c => new
            {
                label = c.Name,
                value = c.Id
            });
            return Json(filteredUsers, JsonRequestBehavior.AllowGet);
        }

    }
}