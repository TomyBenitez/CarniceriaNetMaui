using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        public string Apellido_Nombre { get; set; }
        public string Dirección { get; set; } = string.Empty;
        public string Teléfono { get; set; } = string.Empty;
        public override string ToString()
        {
            return Apellido_Nombre;
        }
    }
}
