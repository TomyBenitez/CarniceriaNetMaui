using CarniceriaNetMaui.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Models
{
    public class Cobrador
    {
        public int Id { get; set; }
        [Required]
        public string Apellido_Nombre { get; set; }
        public string Dirección { get; set; } = string.Empty;
        public string Teléfono { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public TipoCobradorEnum Tipocobrador { get; set; }
        public override string ToString()
        {
            return Apellido_Nombre;
        }
    }
}
