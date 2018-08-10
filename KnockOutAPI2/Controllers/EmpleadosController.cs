using KnockOutAPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KnockOutAPI2.Controllers
{
    public class EmpleadosController : ApiController
    {

        private Meucci3Entities db = new Meucci3Entities();

        // GET api/empleados
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]  
        [Route("api/Empleados/EmpleadoAutoComplete")]
        public IEnumerable<EmpleadoData> EmpleadoAutoComplete(string query)
        {

            string[] prueba = null;

            prueba = query.Split().ToArray();

            query = prueba[0];

            var empleados = db.Empleados.Take(1000);


            var result = empleados.Where(e => e.empNombre.ToLower().Contains(query.ToLower())).AsEnumerable()
                .Select(c => new EmpleadoData {
                    idEmpleado = c.empEmpleadoId,
                    NombreEmpleado = c.empNombre + " " + c.empApellido
                });


            //var result = (from e in empleados
            //                 where e.empNombre.ToLower().StartsWith(query.ToLower())
            //                 select new EmpleadoData
            //                 {
            //                     idEmpleado = e.empEmpleadoId,
            //                     NombreEmpleado = e.empNombre + " " + e.empApellido

            //                 });



            return result;
           


        }
        // GET api/empleados/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/empleados
        public void Post([FromBody]string value)
        {
        }

        // PUT api/empleados/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/empleados/5
        public void Delete(int id)
        {
        }
    }
}
