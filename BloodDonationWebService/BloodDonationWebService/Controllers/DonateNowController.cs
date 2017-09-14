using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodDonationWebService.Models;

namespace BloodDonationWebService.Controllers
{
    public class DonateNowController : Controller
    {
        private DonateNowDBContext db = new DonateNowDBContext();

        // GET: DonateNow
        public ActionResult Index()
        {
            return View(db.DonateNows.ToList());
        }

        // GET: DonateNow/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonateNow donateNow = db.DonateNows.Find(id);
            if (donateNow == null)
            {
                return HttpNotFound();
            }
            return View(donateNow);
        }

        // GET: DonateNow/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonateNow/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,City,Hospitals,BloodGroup")] DonateNow donateNow)
        {
            if (ModelState.IsValid)
            {
                db.DonateNows.Add(donateNow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donateNow);
        }

        // GET: DonateNow/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonateNow donateNow = db.DonateNows.Find(id);
            if (donateNow == null)
            {
                return HttpNotFound();
            }
            return View(donateNow);
        }

        // POST: DonateNow/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,City,Hospitals,BloodGroup")] DonateNow donateNow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donateNow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donateNow);
        }

        // GET: DonateNow/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonateNow donateNow = db.DonateNows.Find(id);
            if (donateNow == null)
            {
                return HttpNotFound();
            }
            return View(donateNow);
        }

        // POST: DonateNow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonateNow donateNow = db.DonateNows.Find(id);
            db.DonateNows.Remove(donateNow);
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
