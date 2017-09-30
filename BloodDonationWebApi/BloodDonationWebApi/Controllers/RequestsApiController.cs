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
using BloodDonationWebApi.Models;

namespace BloodDonationWebApi.Controllers
{
    public class RequestsApiController : ApiController
    {
        private RequestsDBContext db = new RequestsDBContext();

        // GET: api/RequestsApi
        public IQueryable<Requests> GetRequests()
        {
            return db.Requests;
        }

        // GET: api/RequestsApi/5
        [ResponseType(typeof(Requests))]
        public IHttpActionResult GetRequests(int id)
        {
            Requests requests = db.Requests.Find(id);
            if (requests == null)
            {
                return NotFound();
            }

            return Ok(requests);
        }

        // PUT: api/RequestsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequests(int id, Requests requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requests.Id)
            {
                return BadRequest();
            }

            db.Entry(requests).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestsExists(id))
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

        // POST: api/RequestsApi
        [ResponseType(typeof(Requests))]
        public IHttpActionResult PostRequests(Requests requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Requests.Add(requests);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = requests.Id }, requests);
        }

        // DELETE: api/RequestsApi/5
        [ResponseType(typeof(Requests))]
        public IHttpActionResult DeleteRequests(int id)
        {
            Requests requests = db.Requests.Find(id);
            if (requests == null)
            {
                return NotFound();
            }

            db.Requests.Remove(requests);
            db.SaveChanges();

            return Ok(requests);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestsExists(int id)
        {
            return db.Requests.Count(e => e.Id == id) > 0;
        }
    }
}