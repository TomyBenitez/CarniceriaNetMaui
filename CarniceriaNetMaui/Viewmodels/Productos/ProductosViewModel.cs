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

namespace CarniceriaNetMaui.Viewmodels.Productos
{
    public class ProductosViewModel : ObjectNotification
    {
        ProductosRepository productosRepository = new ProductosRepository();

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

        private Producto productoSeleccionado;

        public Producto ProductoSeleccionado
        {
            get { return productoSeleccionado; }
            set
            {
                productoSeleccionado = value;
                OnPropertyChanged();
                EliminarProductoCommand.ChangeCanExecute();
                EditarProductoCommand.ChangeCanExecute();
                AgregarProductoCommand.ChangeCanExecute();

            }
        }
        private ObservableCollection<Producto> productos;

        public ObservableCollection<Producto> Productos
        {
            get { return productos; }
            set
            {
                productos = value;
                OnPropertyChanged();
            }
        }

        public Command ObtenerProductoCommand { get; }
        public Command EditarProductoCommand { get; }
        public Command EliminarProductoCommand { get; }
        public Command AgregarProductoCommand { get; }




        public ProductosViewModel()
        {
            Productos = new ObservableCollection<Producto>();
            ObtenerProductoCommand = new Command(ObtenerProductos);
            EditarProductoCommand = new Command(EditarProducto, PermitirEditar);
            EliminarProductoCommand = new Command(EliminarProducto, PermitirEliminar);
            AgregarProductoCommand = new Command(AgregarProducto);
        }



        private bool PermitirEditar(object arg)
        {
            return productoSeleccionado != null;
        }

        private bool PermitirEliminar(object arg)
        {
            return productoSeleccionado != null;
        }

        private void AgregarProducto(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("AbrirNuevoEditarProducto"));
        }

        private async void EliminarProducto(object obj)
        {
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar un Producto", $"¿Está seguro que desea eliminar el Producto {productoSeleccionado.Nombre}?", "Si", "No");
            if (respuesta)
            {
                ActividadRealizandose = true;
                await productosRepository.RemoveAsync(productoSeleccionado.Id);
                ObtenerProductos(this);
                ActividadRealizandose = false;
                productoSeleccionado = null;
            }
        }

        private void EditarProducto(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("EditarProducto") { ProductoAEditar = ProductoSeleccionado });
        }

        public async void ObtenerProductos(object obj)
        {
            ActividadRealizandose = true;
            Productos.Clear();
            var productos = await productosRepository.GetAllAsync();
            foreach (var producto in productos)
            {
                Productos.Add(producto);
            }
            ActividadRealizandose = false;
        }
    }
}
