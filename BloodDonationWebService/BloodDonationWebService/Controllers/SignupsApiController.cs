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
    public class SignupsApiController : ApiController
    {
        private SignupDBContext db = new SignupDBContext();

        // GET: api/SignupsApi
        public IQueryable<Signup> GetSignups()
        {
            return db.Signups;
        }

        // GET: api/SignupsApi/5
        [ResponseType(typeof(Signup))]
        public IHttpActionResult GetSignup(int id)
        {
            Signup signup = db.Signups.Find(id);
            if (signup == null)
            {
                return NotFound();
            }

            return Ok(signup);
        }

        // PUT: api/SignupsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSignup(int id, Signup signup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != signup.Id)
            {
                return BadRequest();
            }

            db.Entry(signup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SignupExists(id))
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

        // POST: api/SignupsApi
        [ResponseType(typeof(Signup))]
        public IHttpActionResult PostSignup(Signup signup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Signups.Add(signup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = signup.Id }, signup);
        }

        // DELETE: api/SignupsApi/5
        [ResponseType(typeof(Signup))]
        public IHttpActionResult DeleteSignup(int id)
        {
            Signup signup = db.Signups.Find(id);
            if (signup == null)
            {
                return NotFound();
            }

            db.Signups.Remove(signup);
            db.SaveChanges();

            return Ok(signup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SignupExists(int id)
        {
            return db.Signups.Count(e => e.Id == id) > 0;
        }
    }
}