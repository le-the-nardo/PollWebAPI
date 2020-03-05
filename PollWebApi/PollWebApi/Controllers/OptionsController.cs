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
    public class OptionsController : ApiController
    {
        public OptionsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        private PollDataBaseEntities db = new PollDataBaseEntities();

        // GET: api/Options
        public IQueryable<Options> GetOptions()
        {
            return db.Options;
        }

        // GET: api/Options/5
        [ResponseType(typeof(Options))]
        public IHttpActionResult GetOptions(int id)
        {
            Options options = db.Options.Find(id);
            if (options == null)
            {
                return NotFound();
            }

            return Ok(options);
        }

        // POST: api/Options
        [ResponseType(typeof(Options))]
        public IHttpActionResult PostOptions(Options options)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Options.Add(options);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = options.Option_Id }, options);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OptionsExists(int id)
        {
            return db.Options.Count(e => e.Option_Id == id) > 0;
        }
    }
}