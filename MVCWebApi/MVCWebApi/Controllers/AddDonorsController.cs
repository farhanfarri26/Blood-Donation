using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCWebApi.Models;

namespace MVCWebApi.Controllers
{
    public class AddDonorsController : Controller
    {
        private AddDonorDBContext db = new AddDonorDBContext();

        // GET: AddDonors
        public ActionResult Index()
        {
            return View(db.AddDonors.ToList());
        }

        // GET: AddDonors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDonor addDonor = db.AddDonors.Find(id);
            if (addDonor == null)
            {
                return HttpNotFound();
            }
            return View(addDonor);
        }

        // GET: AddDonors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddDonors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,CellNumber,City,Area,BloodGroup")] AddDonor addDonor)
        {
            if (ModelState.IsValid)
            {
                db.AddDonors.Add(addDonor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(addDonor);
        }

        // GET: AddDonors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDonor addDonor = db.AddDonors.Find(id);
            if (addDonor == null)
            {
                return HttpNotFound();
            }
            return View(addDonor);
        }

        // POST: AddDonors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,CellNumber,City,Area,BloodGroup")] AddDonor addDonor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addDonor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addDonor);
        }

        // GET: AddDonors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDonor addDonor = db.AddDonors.Find(id);
            if (addDonor == null)
            {
                return HttpNotFound();
            }
            return View(addDonor);
        }

        // POST: AddDonors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddDonor addDonor = db.AddDonors.Find(id);
            db.AddDonors.Remove(addDonor);
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
