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
    public class BloodUsersApiController : ApiController
    {
        private BloodUsersDBContext db = new BloodUsersDBContext();

        // GET: api/BloodUsersApi
        public IQueryable<BloodUsers> GetBloodUsers()
        {
            return db.BloodUsers;
        }

        //GET: api/BloodUsersApi?cellnumber=Value
        [ResponseType(typeof(BloodUsers))]
        public IHttpActionResult GetBloodUsers(string cellnumber)
        {
            if (String.IsNullOrEmpty(cellnumber))
            {
                return NotFound();
            }
            return Ok(db.BloodUsers.Where(x => x.CellNumber == cellnumber));
        }

        //GET: api/BloodUsersApi?cellnumber=Value&&password=Value
        [ResponseType(typeof(BloodUsers))]
        public IHttpActionResult GetBloodUsers(string cellnumber, string password)
        {
            if (String.IsNullOrEmpty(cellnumber) || string.IsNullOrEmpty(password))
            {
                return NotFound();
            }
            return Ok(db.BloodUsers.Where(x => x.CellNumber == cellnumber && x.Password == password));
        }

        // GET: api/BloodUsersApi/5
        [ResponseType(typeof(BloodUsers))]
        public IHttpActionResult GetBloodUsers(int id)
        {
            BloodUsers bloodUsers = db.BloodUsers.Find(id);
            if (bloodUsers == null)
            {
                return NotFound();
            }

            return Ok(bloodUsers);
        }

        // PUT: api/BloodUsersApi?password=Value
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBloodUsers(string password, BloodUsers bloodUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (password != bloodUsers.Password)
            {
                return BadRequest();
            }

            db.Entry(bloodUsers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (password != bloodUsers.Password)
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

        // PUT: api/BloodUsersApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBloodUsers(int id, BloodUsers bloodUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloodUsers.Id)
            {
                return BadRequest();
            }

            db.Entry(bloodUsers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodUsersExists(id))
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

        // POST: api/BloodUsersApi
        [ResponseType(typeof(BloodUsers))]
        public IHttpActionResult PostBloodUsers(BloodUsers bloodUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BloodUsers.Add(bloodUsers);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bloodUsers.Id }, bloodUsers);
        }

        // DELETE: api/BloodUsersApi/5
        [ResponseType(typeof(BloodUsers))]
        public IHttpActionResult DeleteBloodUsers(int id)
        {
            BloodUsers bloodUsers = db.BloodUsers.Find(id);
            if (bloodUsers == null)
            {
                return NotFound();
            }

            db.BloodUsers.Remove(bloodUsers);
            db.SaveChanges();

            return Ok(bloodUsers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BloodUsersExists(int id)
        {
            return db.BloodUsers.Count(e => e.Id == id) > 0;
        }
    }
}