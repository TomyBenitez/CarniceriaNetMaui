using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Repositories;
using CarniceriaNetMaui.Utils;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Viewmodels.Ventas
{
    public class NuevoEditarVentasViewModel : ObjectNotification
    {
        VentasRepository ventasRepository = new VentasRepository();
        ProductosRepository productosRepository = new ProductosRepository();
        ClientesRepository clientesRepository = new ClientesRepository();
        public Venta VentaEditado { get; set; }

        private int cobradorId;

        public int CobradorId
        {
            get { return cobradorId; }
            set
            {
                cobradorId = value;
                OnPropertyChanged();
            }
        }
        private int clienteId;

        public int ClienteId
        {
            get { return clienteId; }
            set
            {
                clienteId = value;
                OnPropertyChanged();
            }
        }

        private int productoId;
        public int ProductoId
        {
            get { return productoId; }
            set
            {
                productoId = value;
                OnPropertyChanged();
            }
        }

        private int cantidad;
        public int Cantidad
        {
            get { return cantidad; }
            set
            {
                cantidad = value;
                OnPropertyChanged();
            }
        }

        public List<Cliente> listaCliente;
        public List<Cliente> ListaCliente
        {
            get { return listaCliente; }
            set 
            { 
                listaCliente = value;
                OnPropertyChanged();
            }
        }
        public List<Producto> listaProducto;
        public List<Producto> ListaProducto
        {
            get { return listaProducto; }
            set
            {
                listaProducto = value;
                OnPropertyChanged();
            }
        }
        public Command GuardarCommand { get; }
        public Command CancelarCommand { get; }

        public NuevoEditarVentasViewModel()
        {
            GuardarCommand = new Command(Guardar);
            CancelarCommand = new Command(Cancelar);
            CargarPickers();
        }

        private async Task CargarPickers()
        {
           var clientes = await clientesRepository.GetAllAsync();
           var productos = await productosRepository.GetAllAsync();
           ListaCliente = clientes.ToList();
           OnPropertyChanged(nameof(ListaCliente));
           listaProducto = productos.ToList();
           OnPropertyChanged(nameof(ListaProducto));
        }

        private void Cancelar(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("VolverAVentaView"));
        }

        private async void Guardar(object obj)
        {
            cobradorId = Helper.LoginCobradorId;
            if (VentaEditado == null)
            {
                Venta venta = await ventasRepository.AddAsync(clienteId, cobradorId, productoId, cantidad);
            }
            else
            {
                await ventasRepository.UpdateAsync(VentaEditado.Id, clienteId, cobradorId, productoId, cantidad);
            }
            WeakReferenceMessenger.Default.Send(new MiMensaje("VolverAVentaView"));
        }

        public void CargarDatosEnPantalla()
        {
            ClienteId = VentaEditado.ClienteId;
            CobradorId = VentaEditado.CobradorId;
            ProductoId = VentaEditado.ProductoId;
            Cantidad = VentaEditado.Cantidad;
        }
    }
}
