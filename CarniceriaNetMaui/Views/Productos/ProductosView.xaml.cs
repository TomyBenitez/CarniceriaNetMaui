using CarniceriaNetMaui.Viewmodels.Productos;
using CarniceriaNetMaui.Views.Productos;
using CommunityToolkit.Mvvm.Messaging;

namespace CarniceriaNetMaui.Views;

public partial class ProductosView : ContentPage
{
	public ProductosView()
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<MiMensaje>(this, (r, m) =>
        {
            AlRecibirMensaje(m);
        });
    }

    private async void AlRecibirMensaje(MiMensaje m)
    {
        if (m.Value == "AbrirNuevoEditarProducto")
        {
            await Navigation.PushAsync(new NuevoEditarProductoView());
        }
        if (m.Value == "EditarProducto")
        {
            await Navigation.PushAsync(new NuevoEditarProductoView(m.ProductoAEditar));
        }
        if (m.Value == "VolverAProductoView")
        {
            await Navigation.PopAsync();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewmodel = this.BindingContext as ProductosViewModel;
        viewmodel.ObtenerProductos(this);
        viewmodel.ProductoSeleccionado = null;
    }
}