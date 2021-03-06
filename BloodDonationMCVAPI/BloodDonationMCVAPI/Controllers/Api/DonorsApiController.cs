﻿using System;
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
    public class DonorsApiController : ApiController
    {
        private DonorsDBContext db = new DonorsDBContext();

        // GET: api/DonorsApi
        public IQueryable<Donors> GetDonors()
        {
            return db.Donors.OrderByDescending(m => m.Id);
        }

        //GET: api/DonorsApi?cellnumber=
        [ResponseType(typeof(Donors))]
        public IHttpActionResult GetDonors(string cellnumber)
        {
            if (String.IsNullOrEmpty(cellnumber))
            {
                return NotFound();
            }

            return Ok(db.Donors.Where(x => x.AddedBy == cellnumber).OrderByDescending(m => m.Id));
        }

        //GET: api/DonorsApi?blood=Value&&city=Value
        [ResponseType(typeof(Donors))]
        public IHttpActionResult GetDonors(string blood, string city)
        {
            if (String.IsNullOrEmpty(blood) || String.IsNullOrEmpty(city))
            {
                return NotFound();
            }

            return Ok(db.Donors.Where(x => x.BloodGroup == blood && x.City == city && x.FutureUse == "Available").OrderByDescending(m => m.Id));
        }

        //GET: api/DonorsApi?blood=Value&&city=Value
        //[ResponseType(typeof(Donors))]
        //public IHttpActionResult GetDonors(string blood, string city)
        //{
        //    if (String.IsNullOrEmpty(blood) || String.IsNullOrEmpty(city))
        //    {
        //        return NotFound();
        //    }

        //    return Ok(db.Donors.Where(x => x.BloodGroup == blood && x.City == city).OrderByDescending(m => m.Id));
        //}


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
        public IHttpActionResult PostDonors(Donors donors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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