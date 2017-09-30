using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodDonationWeb.Models;

namespace BloodDonationWeb.Controllers
{
    public class BloodBanksController : Controller
    {
        private BloodBanksDBContext db = new BloodBanksDBContext();

        // GET: BloodBanks
        public ActionResult Index()
        {
            return View(db.BloodBanks.ToList());
        }

        // GET: BloodBanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodBank bloodBank = db.BloodBanks.Find(id);
            if (bloodBank == null)
            {
                return HttpNotFound();
            }
            return View(bloodBank);
        }

        // GET: BloodBanks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BloodBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HospitalName,PhoneNumber")] BloodBank bloodBank)
        {
            if (ModelState.IsValid)
            {
                db.BloodBanks.Add(bloodBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bloodBank);
        }

        // GET: BloodBanks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodBank bloodBank = db.BloodBanks.Find(id);
            if (bloodBank == null)
            {
                return HttpNotFound();
            }
            return View(bloodBank);
        }

        // POST: BloodBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HospitalName,PhoneNumber")] BloodBank bloodBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloodBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bloodBank);
        }

        // GET: BloodBanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodBank bloodBank = db.BloodBanks.Find(id);
            if (bloodBank == null)
            {
                return HttpNotFound();
            }
            return View(bloodBank);
        }

        // POST: BloodBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BloodBank bloodBank = db.BloodBanks.Find(id);
            db.BloodBanks.Remove(bloodBank);
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
