﻿using System;
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
    public class coreValuesRecognitionsController : Controller
    {
        private Team1ProjectContext db = new Team1ProjectContext();

        // GET: coreValuesRecognitions
        public ActionResult Index()
        {
            var coreValuesRecognitions = db.coreValuesRecognitions.Include(c => c.recognized).Include(c => c.recognizor);
            return View(coreValuesRecognitions.ToList());
        }

        // GET: coreValuesRecognitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coreValuesRecognition coreValuesRecognition = db.coreValuesRecognitions.Find(id);
            if (coreValuesRecognition == null)
            {
                return HttpNotFound();
            }
            return View(coreValuesRecognition);
        }

        // GET: coreValuesRecognitions/Create
        public ActionResult Create()
        {
            ViewBag.recognizedID = new SelectList(db.userData, "ID", "fullName");
            ViewBag.recognizorID = new SelectList(db.userData, "ID", "fullName");
            return View();
        }

        // POST: coreValuesRecognitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,award,recognizorID,recognizedID,recognizationDate")] coreValuesRecognition coreValuesRecognition)
        {
            if (ModelState.IsValid)
            {
                db.coreValuesRecognitions.Add(coreValuesRecognition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.recognizedID = new SelectList(db.userData, "ID", "fullName", coreValuesRecognition.recognizedID);
            ViewBag.recognizorID = new SelectList(db.userData, "ID", "fullName", coreValuesRecognition.recognizorID);
            return View(coreValuesRecognition);
        }

        // GET: coreValuesRecognitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coreValuesRecognition coreValuesRecognition = db.coreValuesRecognitions.Find(id);
            if (coreValuesRecognition == null)
            {
                return HttpNotFound();
            }
            ViewBag.recognizedID = new SelectList(db.userData, "ID", "fullName", coreValuesRecognition.recognizedID);
            ViewBag.recognizorID = new SelectList(db.userData, "ID", "fullName", coreValuesRecognition.recognizorID);
            return View(coreValuesRecognition);
        }

        // POST: coreValuesRecognitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,award,recognizorID,recognizedID,recognizationDate")] coreValuesRecognition coreValuesRecognition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coreValuesRecognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recognizedID = new SelectList(db.userData, "ID", "", coreValuesRecognition.recognizedID);
            ViewBag.recognizorID = new SelectList(db.userData, "ID", "lastName", coreValuesRecognition.recognizorID);
            return View(coreValuesRecognition);
        }

        // GET: coreValuesRecognitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coreValuesRecognition coreValuesRecognition = db.coreValuesRecognitions.Find(id);
            if (coreValuesRecognition == null)
            {
                return HttpNotFound();
            }
            return View(coreValuesRecognition);
        }

        // POST: coreValuesRecognitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            coreValuesRecognition coreValuesRecognition = db.coreValuesRecognitions.Find(id);
            db.coreValuesRecognitions.Remove(coreValuesRecognition);
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
