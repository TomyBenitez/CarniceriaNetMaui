using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Repositories;
using CarniceriaNetMaui.Utils;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Viewmodels.Productos
{
    public class NuevoEditarProductoViewModel : ObjectNotification
    {
        ProductosRepository productosRepository = new ProductosRepository();
        public Producto ProductoEditado { get; set; }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged();
            }
        }
        private decimal monto;

        public decimal Monto
        {
            get { return monto; }
            set
            {
                monto = value;
                OnPropertyChanged();
            }
        }
        public Command GuardarCommand { get; }
        public Command CancelarCommand { get; }

        public NuevoEditarProductoViewModel()
        {
            GuardarCommand = new Command(Guardar);
            CancelarCommand = new Command(Cancelar);
        }

        private void Cancelar(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("VolverAProductoView"));
        }

        private async void Guardar(object obj)
        {
            if (ProductoEditado == null)
            {
                Producto producto = await productosRepository.AddAsync(nombre, monto);
            }
            else
            {
                await productosRepository.UpdateAsync(ProductoEditado.Id, nombre, monto);
            }
            WeakReferenceMessenger.Default.Send(new MiMensaje("VolverAProductoView"));
        }

        public void CargarDatosEnPantalla()
        {
            Nombre = ProductoEditado.Nombre;
            Monto = ProductoEditado.Monto;
        }
    }
}
