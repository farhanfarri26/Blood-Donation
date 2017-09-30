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
    public class BloodBanksApiController : ApiController
    {
        private BloodBanksDBContext db = new BloodBanksDBContext();

        // GET: api/BloodBanksApi
        public IQueryable<BloodBank> GetBloodBanks()
        {
            return db.BloodBanks;
        }

        // GET: api/BloodBanksApi/5
        [ResponseType(typeof(BloodBank))]
        public IHttpActionResult GetBloodBank(int id)
        {
            BloodBank bloodBank = db.BloodBanks.Find(id);
            if (bloodBank == null)
            {
                return NotFound();
            }

            return Ok(bloodBank);
        }

        // PUT: api/BloodBanksApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBloodBank(int id, BloodBank bloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloodBank.Id)
            {
                return BadRequest();
            }

            db.Entry(bloodBank).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodBankExists(id))
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
        [ResponseType(typeof(BloodBank))]
        public IHttpActionResult PostBloodBank(BloodBank bloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BloodBanks.Add(bloodBank);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bloodBank.Id }, bloodBank);
        }

        // DELETE: api/BloodBanksApi/5
        [ResponseType(typeof(BloodBank))]
        public IHttpActionResult DeleteBloodBank(int id)
        {
            BloodBank bloodBank = db.BloodBanks.Find(id);
            if (bloodBank == null)
            {
                return NotFound();
            }

            db.BloodBanks.Remove(bloodBank);
            db.SaveChanges();

            return Ok(bloodBank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BloodBankExists(int id)
        {
            return db.BloodBanks.Count(e => e.Id == id) > 0;
        }
    }
}