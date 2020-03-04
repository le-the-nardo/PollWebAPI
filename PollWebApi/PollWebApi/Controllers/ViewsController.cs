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
    public class ViewsController : ApiController
    {
        public ViewsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        private PollDataBaseEntities db = new PollDataBaseEntities();

        // GET: api/Views
        public IQueryable<Views> GetViews()
        {
            return db.Views;
        }

        // GET: api/Views/5
        [ResponseType(typeof(Views))]
        public IHttpActionResult GetViews(int id)
        {
            Views views = db.Views.Find(id);
            if (views == null)
            {
                return NotFound();
            }

            return Ok(views);
        }

        // PUT: api/Views/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutViews(int id, Views views)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != views.View_Id)
            {
                return BadRequest();
            }

            db.Entry(views).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViewsExists(id))
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

        // POST: api/Views
        [ResponseType(typeof(Views))]
        public IHttpActionResult PostViews(Views views)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Views.Add(views);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ViewsExists(views.View_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = views.View_Id }, views);
        }

        // DELETE: api/Views/5
        [ResponseType(typeof(Views))]
        public IHttpActionResult DeleteViews(int id)
        {
            Views views = db.Views.Find(id);
            if (views == null)
            {
                return NotFound();
            }

            db.Views.Remove(views);
            db.SaveChanges();

            return Ok(views);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ViewsExists(int id)
        {
            return db.Views.Count(e => e.View_Id == id) > 0;
        }
    }
}