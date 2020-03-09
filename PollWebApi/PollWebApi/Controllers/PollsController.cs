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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PollWebApi.Models.Services;
using PollWebApi.Models.Responses;

namespace PollWebApi.Controllers
{
    public class PollsController : ApiController
    {
        private PollDataBaseEntities db = new PollDataBaseEntities();
        private PollService _PollService;
        public PollsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            _PollService = new PollService(db);

        }

        // GET: poll/5
        [ResponseType(typeof(GetPollResponse))]
        [Route("poll/{id}")]
        public IHttpActionResult GetPoll(int id)
        {
            var poll = _PollService.GetPollByID(id).ToList();

            if (poll == null)
            {
                return NotFound();
            }
            
            return Json(new { poll});            
        }
        
        // POST: poll
        [ResponseType(typeof(PostPollResponse))]
        [Route("poll")]
        public IHttpActionResult PostPoll(PostPollResponse poll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var p = _PollService.AddPoll(poll);        
            
            return Json(new { poll_id = p.Poll_Id });
        }

        // POST: poll/5/vote
        [ResponseType(typeof(PostVoteResponse))]
        [Route("poll/{id:int}/vote")]
        public IHttpActionResult PostVote(int id)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            var v = _PollService.AddVote(id);

            return Json(new { option_id = id });
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