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
    public class RequestApiController : ApiController
    {
        private RequestDBContext db = new RequestDBContext();
        // GET: api/RequestApi
        public IQueryable<Request> GetRequests()
        {
            return db.Request;
        }

        //GET: api/RequestApi?cellnumber=Value
        [ResponseType(typeof(Request))]
        public IHttpActionResult GetRequest(string cellnumber)
        {
            if (String.IsNullOrEmpty(cellnumber))
            {
                return NotFound();
            }

            return Ok(db.Request.Where(x => x.AddedBy == cellnumber).OrderByDescending(m => m.Id));
        }

        //GET: api/RequestApi?blood=Value&&city=Value&&hospitals=Value
        [ResponseType(typeof(Request))]
        public IHttpActionResult GetRequest(string blood, string city, string hospitals)
        {
            if (String.IsNullOrEmpty(blood) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(hospitals))
            {
                return NotFound();
            }

            return Ok(db.Request.Where(x => x.BloodGroup == blood && x.City == city && x.Hospitals == hospitals).OrderByDescending(m => m.Id));
        }

        // GET: api/RequestApi/5
        [ResponseType(typeof(Request))]
        public IHttpActionResult GetRequest(int id)
        {
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        // PUT: api/RequestApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequest(int id, Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            db.Entry(request).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/RequestApi
        [ResponseType(typeof(Request))]
        public IHttpActionResult PostRequest(Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Request.Add(request);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = request.Id }, request);
        }

        // DELETE: api/RequestApi/5
        [ResponseType(typeof(Request))]
        public IHttpActionResult DeleteRequest(int id)
        {
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            db.Request.Remove(request);
            db.SaveChanges();

            return Ok(request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestExists(int id)
        {
            return db.Request.Count(e => e.Id == id) > 0;
        }
    }
}