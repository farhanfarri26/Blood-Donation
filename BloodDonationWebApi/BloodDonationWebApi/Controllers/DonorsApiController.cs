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
using System.Web.UI.WebControls;
using BloodDonationWebApi.Models;

namespace BloodDonationWebApi.Controllers
{
    public class DonorsApiController : ApiController
    {
        private DonorsDBContext db = new DonorsDBContext();

        // GET: api/DonorsApi
        public IQueryable<Donors> GetDonors()
        {
            return db.Donors.OrderByDescending(m=>m.Id);
        }

        //public IQueryable<Donors> GetDonors(string city, string area, string bloodgroup)
        //{
        //    var a = db.Donors.Where(x => x.City == city && x.Area == area && x.BloodGroup == bloodgroup);
        //    return a;
        //}



        // GET: api/DonorsApi/5
        [ResponseType(typeof(Donors))]
        public IHttpActionResult GetDonors(int id)
        {
            Donors donors = db.Donors.Find(id);
            if (donors == null)
            {
                return NotFound();
            }

            return Ok(donors);
        }

        // PUT: api/DonorsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDonors(int id, Donors donors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != donors.Id)
            {
                return BadRequest();
            }

            db.Entry(donors).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonorsExists(id))
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

        // POST: api/DonorsApi
        [ResponseType(typeof(Donors))]
        public IHttpActionResult PostDonors(Donors donors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Donors.Add(donors);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = donors.Id }, donors);
        }

        // DELETE: api/DonorsApi/5
        [ResponseType(typeof(Donors))]
        public IHttpActionResult DeleteDonors(int id)
        {
            Donors donors = db.Donors.Find(id);
            if (donors == null)
            {
                return NotFound();
            }

            db.Donors.Remove(donors);
            db.SaveChanges();

            return Ok(donors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DonorsExists(int id)
        {
            return db.Donors.Count(e => e.Id == id) > 0;
        }
    }
}