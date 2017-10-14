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
using Microsoft.AspNet.Identity;

namespace BloodDonationWebApi.Controllers
{
    public class DonorsApiController : ApiController
    {
        private DonorsDBContext db = new DonorsDBContext();

        //GET: api/AddDonorsApi
        public IQueryable<Donors> GetAddDonors()
        {
            return db.Donors.OrderByDescending(m => m.Id);
        }

        //GET: api/DonorsApi?donor=
        [ResponseType(typeof(Donors))]
        public IHttpActionResult GetDonors(string donor)
        {
            if (String.IsNullOrEmpty(donor))
            {
                return NotFound();
            }

            return Ok(db.Donors.Where(x => x.BloodGroup == donor).OrderByDescending(m => m.Id));
        }


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
        //[Authorize]
        public IHttpActionResult PostDonors(Donors donors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //string userId = User.Identity.GetUserId();
            //donors.UserId = userId;

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