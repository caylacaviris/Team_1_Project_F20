﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team_1_Project.DAL;
using Team_1_Project.Models;

namespace Team_1_Project.Controllers
{
    public class userDataController : Controller
    {
        private Team1ProjectContext db = new Team1ProjectContext();
        private object id;

        // GET: userData
        public ActionResult Index(string searchString)
        {
            var testusers = from u in db.userData select u;
            if (!String.IsNullOrEmpty(searchString))
            {
                testusers = testusers.Where(u =>
               u.lastName.Contains(searchString)
               || u.firstName.Contains(searchString));
                //if here, users were found so view them
                return View(testusers.ToList());
            }
            return View(db.userData.ToList());
        }




        // GET: userData/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userData userData = db.userData.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // GET: userData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,lastName,firstName,phoneNumber,email,officeLocation,position,hireDate")] userData userData)
        {
            if (ModelState.IsValid)
            {
                Guid memberID;
                Guid.TryParse(User.Identity.GetUserId(), out memberID);
                userData.ID = memberID;
                //userData.ID = Guid.NewGuid();
                db.userData.Add(userData);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return View("duplicateUser");
                }
                return RedirectToAction("Index");
            }

            return View(userData);
        }

        // GET: userData/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userData userData = db.userData.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (userData.ID == memberID)
            {
                return View(userData);
            }
            else
            {
                return View("NotAuthenticated");
            }


        }

        // POST: userData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,lastName,firstName,phoneNumber,email,officeLocation,position,hireDate")] userData userData)
        {

            if (ModelState.IsValid)
            {
               db.Entry(userData).State = EntityState.Modified;
                db.SaveChanges();
               return RedirectToAction("Index");
            }
           return View(userData);

        }

        // GET: userData/Delete/5
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userData userData = db.userData.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (userData.ID == memberID)
            {
                return View(userData);
            }
            else
            {
                return View("NotAuthenticated");
            }
        }

        // POST: userData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            userData userData = db.userData.Find(id);
            db.userData.Remove(userData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


