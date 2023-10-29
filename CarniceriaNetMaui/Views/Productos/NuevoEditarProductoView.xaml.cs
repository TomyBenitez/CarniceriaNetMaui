using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Viewmodels.Productos;

namespace CarniceriaNetMaui.Views.Productos;

public partial class NuevoEditarProductoView : ContentPage
{
    NuevoEditarProductoViewModel NuevoEditarProductoViewModel { get; set; }
    public NuevoEditarProductoView()
	{
		InitializeComponent();
	}
    public NuevoEditarProductoView(Producto productoAEditar)
    {
        InitializeComponent();
        NuevoEditarProductoViewModel = this.BindingContext as NuevoEditarProductoViewModel;
        NuevoEditarProductoViewModel.ProductoEditado = productoAEditar;
        NuevoEditarProductoViewModel.CargarDatosEnPantalla();
    }
}