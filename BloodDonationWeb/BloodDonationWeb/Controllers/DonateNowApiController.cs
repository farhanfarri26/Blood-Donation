using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BloodDonationWeb.Models;

namespace BloodDonationWeb.Controllers
{
    public class DonateNowApiController : ApiController
    {
        private DonateNowDBContext db = new DonateNowDBContext();

        // GET: api/DonateNowApi
        public IQueryable<DonateNow> GetDonateNows()
        {
            return db.DonateNows;
        }

        // GET: api/DonateNowApi/5
        [ResponseType(typeof(DonateNow))]
        public IHttpActionResult GetDonateNow(int id)
        {
            DonateNow donateNow = db.DonateNows.Find(id);
            if (donateNow == null)
            {
                return NotFound();
            }

            return Ok(donateNow);
        }

        // PUT: api/DonateNowApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDonateNow(int id, DonateNow donateNow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != donateNow.Id)
            {
                return BadRequest();
            }

            db.Entry(donateNow).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonateNowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DonateNowApi
        [ResponseType(typeof(DonateNow))]
        public IHttpActionResult PostDonateNow(DonateNow donateNow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DonateNows.Add(donateNow);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = donateNow.Id }, donateNow);
        }

        // DELETE: api/DonateNowApi/5
        [ResponseType(typeof(DonateNow))]
        public IHttpActionResult DeleteDonateNow(int id)
        {
            DonateNow donateNow = db.DonateNows.Find(id);
            if (donateNow == null)
            {
                return NotFound();
            }

            db.DonateNows.Remove(donateNow);
            db.SaveChanges();

            return Ok(donateNow);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DonateNowExists(int id)
        {
            return db.DonateNows.Count(e => e.Id == id) > 0;
        }
    }
}