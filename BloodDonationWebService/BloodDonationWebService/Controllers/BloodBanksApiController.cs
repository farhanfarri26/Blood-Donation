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
using BloodDonationWebService.Models;

namespace BloodDonationWebService.Controllers
{
    public class BloodBanksApiController : ApiController
    {
        private BloodBanksDBContext db = new BloodBanksDBContext();

        // GET: api/BloodBanksApi
        public IQueryable<BloodBanks> GetBloodBanks()
        {
            return db.BloodBanks;
        }

        // GET: api/BloodBanksApi/5
        [ResponseType(typeof(BloodBanks))]
        public IHttpActionResult GetBloodBanks(int id)
        {
            BloodBanks bloodBanks = db.BloodBanks.Find(id);
            if (bloodBanks == null)
            {
                return NotFound();
            }

            return Ok(bloodBanks);
        }

        // PUT: api/BloodBanksApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBloodBanks(int id, BloodBanks bloodBanks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloodBanks.Id)
            {
                return BadRequest();
            }

            db.Entry(bloodBanks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodBanksExists(id))
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

        // POST: api/BloodBanksApi
        [ResponseType(typeof(BloodBanks))]
        public IHttpActionResult PostBloodBanks(BloodBanks bloodBanks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BloodBanks.Add(bloodBanks);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bloodBanks.Id }, bloodBanks);
        }

        // DELETE: api/BloodBanksApi/5
        [ResponseType(typeof(BloodBanks))]
        public IHttpActionResult DeleteBloodBanks(int id)
        {
            BloodBanks bloodBanks = db.BloodBanks.Find(id);
            if (bloodBanks == null)
            {
                return NotFound();
            }

            db.BloodBanks.Remove(bloodBanks);
            db.SaveChanges();

            return Ok(bloodBanks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BloodBanksExists(int id)
        {
            return db.BloodBanks.Count(e => e.Id == id) > 0;
        }
    }
}