﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //var user = User.Identity;
                //ViewBag.Name = user.Name;

                //ViewBag.displayMenu = "No";

                //if (isAdminUser())
                //{
                //    ViewBag.displayMenu = "Yes";
                //}
                //if (isEmployeeUser())
                //{
                //    RedirectToAction("Index", "Employees");
                //}
                if (isCustomerUser())
                {
                    RedirectToAction("CustomerDetails");
                }
                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }

            return View();
        }

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool isEmployeeUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Employee")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool isCustomerUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Customer")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public ActionResult CustomerDetails()
        {
            string id = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser customer = db.Users.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult PickupDay()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Customer")
                {
                    var model = context.Users.Find(User.Identity.GetUserId());
                    model.UserName = model.Email;
                    return View(model);
                }
                else
                {
                    return View("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult PickupDay(ApplicationUser model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var userInDB = db.Users.AsNoTracking().Where(m => m.Id == model.Id).First();
            model.UserName = userInDB.UserName;
            model.Email = userInDB.Email;
            model.Address = userInDB.Address;
            model.City = userInDB.City;
            model.State = userInDB.State;
            model.ZipCode = userInDB.ZipCode;
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch(DbEntityValidationException e)
                {
                    foreach (var entityValidationErrors in e.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Debug.WriteLine(validationError.ErrorMessage);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}