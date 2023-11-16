using CarniceriaNetMaui.Viewmodels.Ventas;
using CarniceriaNetMaui.Views.Ventas;
using CommunityToolkit.Mvvm.Messaging;

namespace CarniceriaNetMaui.Views;

public partial class VentasView : ContentPage
{
	public VentasView()
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<MiMensaje>(this, (r, m) =>
        {
            AlRecibirMensaje(m);
        });
    }

    private async void AlRecibirMensaje(MiMensaje m)
    {
        if (m.Value == "AbrirNuevoEditarVenta")
        {
            await Navigation.PushAsync(new NuevoEditarVentaView());
        }
        if (m.Value == "EditarVenta")
        {
            await Navigation.PushAsync(new NuevoEditarVentaView(m.VentaAEditar));
        }
        if (m.Value == "VolverAVentaView")
        {
            await Navigation.PopAsync();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewmodel = this.BindingContext as VentasViewModel;
        viewmodel.ObtenerVentas(this);
        viewmodel.VentaSeleccionado = null;
    }
}