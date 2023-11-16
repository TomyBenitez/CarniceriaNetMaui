using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Repositories;
using CarniceriaNetMaui.Utils;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Viewmodels.Ventas
{
    public class VentasViewModel : ObjectNotification
    {
        VentasRepository ventasRepository = new VentasRepository();
        ProductosRepository productosRepository = new ProductosRepository();
        ClientesRepository clientesRepository = new ClientesRepository();

        private bool actividadRealizandose = false;

        public bool ActividadRealizandose
        {
            get { return actividadRealizandose; }
            set
            {
                actividadRealizandose = value;
                OnPropertyChanged();
            }
        }

        private Venta ventaSeleccionado;

        public Venta VentaSeleccionado
        {
            get { return ventaSeleccionado; }
            set
            {
                ventaSeleccionado = value;
                OnPropertyChanged();
                EliminarVentaCommand.ChangeCanExecute();
                EditarVentaCommand.ChangeCanExecute();
                AgregarVentaCommand.ChangeCanExecute();

            }
        }
        private ObservableCollection<Venta> ventas;

        public ObservableCollection<Venta> Ventas
        {
            get { return ventas; }
            set
            {
                ventas = value;
                OnPropertyChanged();
            }
        }

        public Command ObtenerVentasCommand { get; }
        public Command EditarVentaCommand { get; }
        public Command EliminarVentaCommand { get; }
        public Command AgregarVentaCommand { get; }




        public VentasViewModel()
        {
            Ventas = new ObservableCollection<Venta>();
            ObtenerVentasCommand = new Command(ObtenerVentas);
            EditarVentaCommand = new Command(EditarVenta, PermitirEditar);
            EliminarVentaCommand = new Command(EliminarVenta, PermitirEliminar);
            AgregarVentaCommand = new Command(AgregarVenta);
        }



        private bool PermitirEditar(object arg)
        {
            return ventaSeleccionado != null;
        }

        private bool PermitirEliminar(object arg)
        {
            return ventaSeleccionado != null;
        }

        private void AgregarVenta(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("AbrirNuevoEditarVenta"));
        }

        private async void EliminarVenta(object obj)
        {
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar una Venta", $"¿Está seguro que desea eliminar la venta {ventaSeleccionado.ProductoId}?", "Si", "No");
            if (respuesta)
            {
                ActividadRealizandose = true;
                await ventasRepository.RemoveAsync(ventaSeleccionado.Id);
                ObtenerVentas(this);
                ActividadRealizandose = false;
                ventaSeleccionado = null;
            }
        }

        private void EditarVenta(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("EditarVenta") { VentaAEditar = VentaSeleccionado });
        }

        public async void ObtenerVentas(object obj)
        {
            ActividadRealizandose = true;
            Ventas.Clear();
            var ventas = await ventasRepository.GetAllAsync();
            var productos = await productosRepository.GetAllAsync();
            var clientes = await clientesRepository.GetAllAsync();
            foreach (var venta in ventas)
            {
                var producto = productos.FirstOrDefault(p => p.Id == venta.ProductoId);
                if (producto != null)
                {
                    var nombreProducto = producto.Nombre;
                    venta.NombreProducto = nombreProducto;
                }
                var cliente = clientes.FirstOrDefault(c => c.Id == venta.ClienteId);
                if (cliente != null)
                {
                    var nombreCliente = cliente.Apellido_Nombre;
                    venta.NombreCliente = nombreCliente;
                }
                Ventas.Add(venta);
            }
            ActividadRealizandose = false;
        }
    }
}
