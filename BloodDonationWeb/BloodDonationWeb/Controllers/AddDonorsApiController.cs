using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using BloodDonationWeb.Models;

namespace BloodDonationWeb.Controllers
{
    public class AddDonorsApiController : ApiController
    {
        private AddDonorDBContext db = new AddDonorDBContext();

        // GET: api/AddDonorsApi
        public IQueryable<AddDonor> GetAddDonors()
        {
            return db.AddDonors.OrderByDescending(m=>m.Id);
        }

        // GET: api/AddDonorsApi/5
        [ResponseType(typeof(AddDonor))]
        public IHttpActionResult GetAddDonor(int id)
        {
            AddDonor addDonor = db.AddDonors.Find(id);
            if (addDonor == null)
            {
                return NotFound();
            }

            return Ok(addDonor);
        }

        // PUT: api/AddDonorsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddDonor(int id, AddDonor addDonor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addDonor.Id)
            {
                return BadRequest();
            }

            db.Entry(addDonor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddDonorExists(id))
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

        // POST: api/AddDonorsApi
        [ResponseType(typeof(AddDonor))]
        public IHttpActionResult PostAddDonor(AddDonor addDonor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AddDonors.Add(addDonor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = addDonor.Id }, addDonor);
        }

        // DELETE: api/AddDonorsApi/5
        [ResponseType(typeof(AddDonor))]
        public IHttpActionResult DeleteAddDonor(int id)
        {
            AddDonor addDonor = db.AddDonors.Find(id);
            if (addDonor == null)
            {
                return NotFound();
            }

            db.AddDonors.Remove(addDonor);
            db.SaveChanges();

            return Ok(addDonor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddDonorExists(int id)
        {
            return db.AddDonors.Count(e => e.Id == id) > 0;
        }
    }
}