using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockOutAPI2.Models
{
    public class CarrerasViewModel
    {
      
        public int crrCarreraId { get; set; }
        public string crrDescripcion { get; set; }
        public string crrObservacion { get; set; }
        public bool crrAnulado { get; set; }
        public Nullable<System.DateTime> crrFechaCreacion { get; set; }
        public int empEmpleadoIdCreador { get; set; }
        public string EmpleadoNombre { get; set; }
        public System.DateTime crrFechaDesde { get; set; }
        public Nullable<System.DateTime> crrFechaHasta { get; set; }
    
        public virtual Empleados Empleados { get; set; }
    
    }
}