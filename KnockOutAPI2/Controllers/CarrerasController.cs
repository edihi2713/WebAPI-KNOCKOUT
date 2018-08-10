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
using KnockOutAPI2;
using KnockOutAPI2.Models;
using System.Threading;

namespace KnockOutAPI2.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "http://localhost:59326", headers: "*", methods: "*")]
    public class CarrerasController : ApiController
    {
        private Meucci3Entities db = new Meucci3Entities();

        // GET api/Carreras
        public IEnumerable<CarrerasViewModel> GetCarreras()
        {

            Thread.Sleep(3000);
            var result = db.Carreras.AsEnumerable().Select(c => new CarrerasViewModel
            {
                crrCarreraId = c.crrCarreraId,
                crrDescripcion = c.crrDescripcion,
                crrObservacion  = c.crrObservacion,
                crrAnulado  = c.crrAnulado,
                crrFechaCreacion  = c.crrFechaCreacion,
                empEmpleadoIdCreador  = c.empEmpleadoIdCreador,
                EmpleadoNombre = c.Empleados.empNombre + ' ' + c.Empleados.empApellido,
                crrFechaDesde = c.crrFechaDesde ,
                crrFechaHasta = c.crrFechaHasta 
 
      
            });

            return  result;

           
        }

        // GET api/Carreras/5
        [ResponseType(typeof(Carreras))]
        public IHttpActionResult GetCarreras(int id)
        {
            Carreras carreras = db.Carreras.Find(id);
            if (carreras == null)
            {
                return NotFound();
            }

            return Ok(carreras);
        }

        // PUT api/Carreras/5
        public IHttpActionResult PutCarreras(int id, Carreras carreras)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carreras.crrCarreraId)
            {
                return BadRequest();
            }

            db.Entry(carreras).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrerasExists(id))
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

        // POST api/Carreras
        [ResponseType(typeof(Carreras))]
        public IHttpActionResult PostCarreras(Carreras carreras)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Carreras.Add(carreras);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = carreras.crrCarreraId }, carreras);
        }

        // DELETE api/Carreras/5
        [ResponseType(typeof(Carreras))]
        public IHttpActionResult DeleteCarreras(int id)
        {
            Carreras carreras = db.Carreras.Find(id);
            if (carreras == null)
            {
                return NotFound();
            }

            db.Carreras.Remove(carreras);
            db.SaveChanges();

            return Ok(carreras);
        }


        [HttpGet]
        [Route("api/Carreras/getCareerbyEmployee")]
        public HttpResponseMessage getCareerbyEmployee()
        {

     

            List<CareerData> result = new List<CareerData>();


            List<int> idEmpleados = new List<int>();

            idEmpleados = (from car in db.Carreras select car.empEmpleadoIdCreador).Distinct().ToList();




            foreach (int item in idEmpleados)
            {

                int cant = 0;
                string nombreEmpleado = "";

                nombreEmpleado = db.Carreras.Where(c => c.empEmpleadoIdCreador == item).Select(c => c.Empleados.empNombre).FirstOrDefault();
                cant = db.Carreras.Where(c => c.empEmpleadoIdCreador == item).Count();

                if (cant > 0)
                {
                    result.Add(new CareerData()
                    {
                        NombreEmpleado = nombreEmpleado,
                        Cantidad = cant
                    });
                }
            }

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }


            return Request.CreateResponse(HttpStatusCode.BadRequest, "La lista para le gráfico está vacío");


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarrerasExists(int id)
        {
            return db.Carreras.Count(e => e.crrCarreraId == id) > 0;
        }
    }
}