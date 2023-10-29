using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Models
{
    public class Venta
    {
        public int Id { get; set; }
        [Required]
        public int CobradorId { get; set; }
        public Cobrador? Cobrador { get; set; }
        [Required]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        [Required]
        public int CarritoId { get; set; }
        public Carrito? Carrito { get; set; }
        public DateTime Fecha { get; set; }
    }
}
