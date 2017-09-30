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
    public class AddRequestsApiController : ApiController
    {
        private AddRequestDBContext db = new AddRequestDBContext();

        // GET: api/AddRequestsApi
        public IQueryable<AddRequest> GetAddRequests()
        {
            return db.AddRequests;
        }

        // GET: api/AddRequestsApi/5
        [ResponseType(typeof(AddRequest))]
        public IHttpActionResult GetAddRequest(int id)
        {
            AddRequest addRequest = db.AddRequests.Find(id);
            if (addRequest == null)
            {
                return NotFound();
            }

            return Ok(addRequest);
        }

        // PUT: api/AddRequestsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddRequest(int id, AddRequest addRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addRequest.Id)
            {
                return BadRequest();
            }

            db.Entry(addRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddRequestExists(id))
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

        // POST: api/AddRequestsApi
        [ResponseType(typeof(AddRequest))]
        public IHttpActionResult PostAddRequest(AddRequest addRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AddRequests.Add(addRequest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = addRequest.Id }, addRequest);
        }

        // DELETE: api/AddRequestsApi/5
        [ResponseType(typeof(AddRequest))]
        public IHttpActionResult DeleteAddRequest(int id)
        {
            AddRequest addRequest = db.AddRequests.Find(id);
            if (addRequest == null)
            {
                return NotFound();
            }

            db.AddRequests.Remove(addRequest);
            db.SaveChanges();

            return Ok(addRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddRequestExists(int id)
        {
            return db.AddRequests.Count(e => e.Id == id) > 0;
        }
    }
}