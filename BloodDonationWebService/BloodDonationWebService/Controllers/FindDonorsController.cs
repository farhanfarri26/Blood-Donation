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
    public class FindDonorsController : Controller
    {
        private FindDonorDBContext db = new FindDonorDBContext();

        // GET: FindDonors
        public ActionResult Index()
        {
            return View(db.FindDonors.ToList());
        }

        // GET: FindDonors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FindDonor findDonor = db.FindDonors.Find(id);
            if (findDonor == null)
            {
                return HttpNotFound();
            }
            return View(findDonor);
        }

        // GET: FindDonors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FindDonors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,City,Area,BloodGroup")] FindDonor findDonor)
        {
            if (ModelState.IsValid)
            {
                db.FindDonors.Add(findDonor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(findDonor);
        }

        // GET: FindDonors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FindDonor findDonor = db.FindDonors.Find(id);
            if (findDonor == null)
            {
                return HttpNotFound();
            }
            return View(findDonor);
        }

        // POST: FindDonors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,City,Area,BloodGroup")] FindDonor findDonor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(findDonor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(findDonor);
        }

        // GET: FindDonors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FindDonor findDonor = db.FindDonors.Find(id);
            if (findDonor == null)
            {
                return HttpNotFound();
            }
            return View(findDonor);
        }

        // POST: FindDonors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FindDonor findDonor = db.FindDonors.Find(id);
            db.FindDonors.Remove(findDonor);
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
