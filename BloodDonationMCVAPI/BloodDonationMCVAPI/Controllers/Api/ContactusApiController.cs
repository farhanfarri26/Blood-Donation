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
using BloodDonationMCVAPI.Models;

namespace BloodDonationMCVAPI.Controllers.Api
{
    public class ContactusApiController : ApiController
    {
        private ContactusDBContext db = new ContactusDBContext();

        // GET: api/ContactusApi
        public IQueryable<Contactus> GetContactus()
        {
            return db.Contactus;
        }

        // GET: api/ContactusApi/5
        [ResponseType(typeof(Contactus))]
        public IHttpActionResult GetContactus(int id)
        {
            Contactus contactus = db.Contactus.Find(id);
            if (contactus == null)
            {
                return NotFound();
            }

            return Ok(contactus);
        }

        // PUT: api/ContactusApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactus(int id, Contactus contactus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactus.Id)
            {
                return BadRequest();
            }

            db.Entry(contactus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactusExists(id))
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

        // POST: api/ContactusApi
        [ResponseType(typeof(Contactus))]
        public IHttpActionResult PostContactus(Contactus contactus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contactus.Add(contactus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contactus.Id }, contactus);
        }

        // DELETE: api/ContactusApi/5
        [ResponseType(typeof(Contactus))]
        public IHttpActionResult DeleteContactus(int id)
        {
            Contactus contactus = db.Contactus.Find(id);
            if (contactus == null)
            {
                return NotFound();
            }

            db.Contactus.Remove(contactus);
            db.SaveChanges();

            return Ok(contactus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactusExists(int id)
        {
            return db.Contactus.Count(e => e.Id == id) > 0;
        }
    }
}