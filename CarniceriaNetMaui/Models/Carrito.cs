using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        [Required]
        public int ProductosId { get; set; }
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
