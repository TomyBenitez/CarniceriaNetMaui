using CarniceriaNetMaui.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui
{
    public class MiMensaje : ValueChangedMessage<string>
    {
        public Cobrador CobradorAEditar { get; set; }
        public Cliente ClienteAEditar { get; set; }
        public Producto ProductoAEditar { get; set; }
        public Venta VentaAEditar { get; set; }

        public MiMensaje(string value) : base(value)
        {

        }

    }
}
