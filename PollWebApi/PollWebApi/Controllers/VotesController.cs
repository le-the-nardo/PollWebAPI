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
using PollWebApi.Entities;

namespace PollWebApi.Controllers
{
    public class VotesController : ApiController
    {
        public VotesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        private PollDataBaseEntities db = new PollDataBaseEntities();

        // GET: api/Votes
        public IQueryable<Votes> GetVotes()
        {
            return db.Votes;
        }

        // GET: api/Votes/5
        [ResponseType(typeof(Votes))]
        public IHttpActionResult GetVotes(int id)
        {
            Votes votes = db.Votes.Find(id);
            if (votes == null)
            {
                return NotFound();
            }

            return Ok(votes);
        }
        
        // POST: api/Votes
        [ResponseType(typeof(Votes))]
        public IHttpActionResult PostVotes(Votes votes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Votes.Add(votes);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VotesExists(votes.Option_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = votes.Option_Id }, votes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VotesExists(int id)
        {
            return db.Votes.Count(e => e.Option_Id == id) > 0;
        }
    }
}