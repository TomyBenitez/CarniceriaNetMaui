using CarniceriaNetMaui.Viewmodels.Clientes;
using CarniceriaNetMaui.Views.Clientes;
using CommunityToolkit.Mvvm.Messaging;

namespace CarniceriaNetMaui.Views;

public partial class ClientesView : ContentPage
{
	public ClientesView()
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<MiMensaje>(this, (r, m) =>
        {
            AlRecibirMensaje(m);
        });
    }
    private async void AlRecibirMensaje(MiMensaje m)
    {
        if (m.Value == "AbrirNuevoEditarCliente")
        {
            await Navigation.PushAsync(new NuevoEditarClienteView());
        }
        if (m.Value == "EditarCliente")
        {
            await Navigation.PushAsync(new NuevoEditarClienteView(m.ClienteAEditar));
        }
        if (m.Value == "VolverAClienteView")
        {
            await Navigation.PopAsync();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewmodel = this.BindingContext as ClientesViewModel;
        viewmodel.ObtenerClientes(this);
        viewmodel.ClienteSeleccionado = null;
    }
}