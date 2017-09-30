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
    public class FindDonorsApiController : ApiController
    {
        private FindDonorsDBContext db = new FindDonorsDBContext();

        // GET: api/FindDonorsApi
        public IQueryable<FindDonor> GetFindDonors()
        {
            return db.FindDonors;
        }

        // GET: api/FindDonorsApi/5
        [ResponseType(typeof(FindDonor))]
        public IHttpActionResult GetFindDonor(int id)
        {
            FindDonor findDonor = db.FindDonors.Find(id);
            if (findDonor == null)
            {
                return NotFound();
            }

            return Ok(findDonor);
        }

        // PUT: api/FindDonorsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFindDonor(int id, FindDonor findDonor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != findDonor.Id)
            {
                return BadRequest();
            }

            db.Entry(findDonor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FindDonorExists(id))
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

        // POST: api/FindDonorsApi
        [ResponseType(typeof(FindDonor))]
        public IHttpActionResult PostFindDonor(FindDonor findDonor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FindDonors.Add(findDonor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = findDonor.Id }, findDonor);
        }

        // DELETE: api/FindDonorsApi/5
        [ResponseType(typeof(FindDonor))]
        public IHttpActionResult DeleteFindDonor(int id)
        {
            FindDonor findDonor = db.FindDonors.Find(id);
            if (findDonor == null)
            {
                return NotFound();
            }

            db.FindDonors.Remove(findDonor);
            db.SaveChanges();

            return Ok(findDonor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FindDonorExists(int id)
        {
            return db.FindDonors.Count(e => e.Id == id) > 0;
        }
    }
}