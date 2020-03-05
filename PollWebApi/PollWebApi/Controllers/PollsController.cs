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
    public class PollsController : ApiController
    {
        public PollsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        private PollDataBaseEntities db = new PollDataBaseEntities();

        // GET: api/Polls
        public IQueryable<Poll> GetPoll()
        {
            return db.Poll;
        }

        // GET: api/Polls/5
        [ResponseType(typeof(Poll))]
        public IHttpActionResult GetPoll(int id)
        {
            Poll poll = db.Poll.Find(id);
            if (poll == null)
            {
                return NotFound();
            }

            return Ok(poll);
        }

        // POST: api/Polls
        [ResponseType(typeof(Poll))]
        public IHttpActionResult PostPoll(Poll poll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Poll.Add(poll);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = poll.Poll_Id }, poll);
        }
                
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PollExists(int id)
        {
            return db.Poll.Count(e => e.Poll_Id == id) > 0;
        }
    }
}