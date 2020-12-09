using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Team_1_Project.DAL;
using Team_1_Project.Models;

namespace Team_1_Project.Controllers
{
    public class coreValuesRecognitionsController : Controller
    {
        private Team1ProjectContext db = new Team1ProjectContext();
        private Guid id;
        private object recList;


        // GET: coreValuesRecognitions
        [Authorize]
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
        [Authorize]
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
        public ActionResult Create([Bind(Include = "ID,award,recognizorID,recognizedID,recognizationDate,customRecognition")] coreValuesRecognition coreValuesRecognition)
        {
            if (ModelState.IsValid)
            {
                db.coreValuesRecognitions.Add(coreValuesRecognition);
                db.SaveChanges();
                var personRecognizee = db.userData.Find(coreValuesRecognition.recognizedID);
                var email = personRecognizee.email;
                var fullName = personRecognizee.fullName;
                var personRecognizor = db.userData.Find(coreValuesRecognition.recognizorID);
                var firstName = personRecognizor.firstName;
                var lastName = personRecognizor.lastName;
                var date = coreValuesRecognition.recognizationDate;
                var customRecognition = coreValuesRecognition.customRecognition;

                var msg = "Hi " + fullName + " , We wanted to congradulate you for receiving a recognition from " + firstName + " " + lastName + " on " + date + ".";
                msg += "Have a great day ! ";

                MailMessage myMessage = new MailMessage();
                MailAddress from = new MailAddress("kw355016@gmail.com", "CentricResponseTeam");
                myMessage.From = from;
                myMessage.To.Add(email);
                myMessage.Subject = "Recognition Earned";
                myMessage.Body = msg;

                try
                {
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("Team1mis4200@gmail.com", "Testing123!");
                    smtp.EnableSsl = true;
                    // smtp.Send(myMessage);
                    TempData["msg"] = msg;
                    TempData["mailError"] = "";
                    return View("mailError");
                }
                catch (Exception ex)
                {
                    TempData["mailError"] = ex.Message;
                    return View("mailError");
                }
               


            }

            ViewBag.recognizedID = new SelectList(db.userData, "ID", "lastName", coreValuesRecognition.recognizedID);
            ViewBag.recognizorID = new SelectList(db.userData, "ID", "lastName", coreValuesRecognition.recognizorID);
            return View(coreValuesRecognition);
        }

        // GET: coreValuesRecognitions/Edit/5
        [Authorize]
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
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (coreValuesRecognition.recognizorID == memberID)
            {
                return View(coreValuesRecognition);
            }
            else
            {
                return View("NotAbleToEdit");
            }
            
        }

        // POST: coreValuesRecognitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,award,recognizorID,recognizedID,recognizationDate,customRecognition")] coreValuesRecognition coreValuesRecognition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coreValuesRecognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recognizedID = new SelectList(db.userData, "ID", "fullName", coreValuesRecognition.recognizedID);
            ViewBag.recognizorID = new SelectList(db.userData, "ID", "fullName", coreValuesRecognition.recognizorID);
            return View(coreValuesRecognition);
        }

        // GET: coreValuesRecognitions/Delete/5
        [Authorize]
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
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (coreValuesRecognition.recognizorID == memberID)
            {
                return View(coreValuesRecognition);
            }
            else
            {
                return View("NotAbleToEdit");
            }
            
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
        public ActionResult SendEmail()
        {

            return View();



        }
        //var rec = db.coreValuesRecognitions.Where(r => r.recognizedID == id);
           // ViewBag.rec = recList.ToList();

           // var totalcnt = recList.Count();
           // ViewBag.total = totalcnt;

           // @Html.Partial("recognitioncount", coreValuesRecognition);
           // var total = ViewBag.total;
           //<h2>Total recognitions received = @total</h2>

    }
}
