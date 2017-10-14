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
    public class AddRequestsController : Controller
    {
        private AddRequestDBContext db = new AddRequestDBContext();

        // GET: AddRequests
        public ActionResult Index()
        {
            return View(db.AddRequests.ToList());
        }

        // GET: AddRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddRequest addRequest = db.AddRequests.Find(id);
            if (addRequest == null)
            {
                return HttpNotFound();
            }
            return View(addRequest);
        }

        // GET: AddRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,CellNumber,City,Hospitals,BloodGroup")] AddRequest addRequest)
        {
            if (ModelState.IsValid)
            {
                db.AddRequests.Add(addRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(addRequest);
        }

        // GET: AddRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddRequest addRequest = db.AddRequests.Find(id);
            if (addRequest == null)
            {
                return HttpNotFound();
            }
            return View(addRequest);
        }

        // POST: AddRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,CellNumber,City,Hospitals,BloodGroup")] AddRequest addRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addRequest);
        }

        // GET: AddRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddRequest addRequest = db.AddRequests.Find(id);
            if (addRequest == null)
            {
                return HttpNotFound();
            }
            return View(addRequest);
        }

        // POST: AddRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddRequest addRequest = db.AddRequests.Find(id);
            db.AddRequests.Remove(addRequest);
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
