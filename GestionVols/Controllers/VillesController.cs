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
using DAL;

namespace GestionVols.Controllers
{
    public class VillesController : ApiController
    {
        private aeroportEntities db = new aeroportEntities();

        // GET: api/Villes
        public IQueryable<Ville> GetVilles()
        {
            return db.Villes;
        }

        // GET: api/Villes/5
        [ResponseType(typeof(Ville))]
        public IHttpActionResult GetVille(int id)
        {
            Ville ville = db.Villes.Find(id);
            if (ville == null)
            {
                return NotFound();
            }

            return Ok(ville);
        }

        // PUT: api/Villes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVille(int id, Ville ville)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ville.Id_Ville)
            {
                return BadRequest();
            }

            db.Entry(ville).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VilleExists(id))
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

        // POST: api/Villes
        [ResponseType(typeof(Ville))]
        public IHttpActionResult PostVille(Ville ville)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Villes.Add(ville);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ville.Id_Ville }, ville);
        }

        // DELETE: api/Villes/5
        [ResponseType(typeof(Ville))]
        public IHttpActionResult DeleteVille(int id)
        {
            Ville ville = db.Villes.Find(id);
            if (ville == null)
            {
                return NotFound();
            }

            db.Villes.Remove(ville);
            db.SaveChanges();

            return Ok(ville);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VilleExists(int id)
        {
            return db.Villes.Count(e => e.Id_Ville == id) > 0;
        }
    }
}