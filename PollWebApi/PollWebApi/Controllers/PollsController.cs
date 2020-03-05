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

        // GET: api/Polls/5
        [ResponseType(typeof(Poll))]
        [Route("poll/{id}")]
        public IHttpActionResult GetPoll(int id)
        {
            //Poll poll = db.Poll.Find(id);
            //Options op = db.Options.Join();

            Poll poll = db.Poll.Join(db.Options, desc => desc.Options, new { firstname = "teste"});
            
                //Find(poll.Poll_Id);

            if (poll == null)
            {
                return NotFound();
            }

            //return Ok(poll.Description);
            return Json(new { poll_id = poll.Poll_Id, poll_description = poll.Description, options = op });
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