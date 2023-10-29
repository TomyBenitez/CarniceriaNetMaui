using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Viewmodels.Clientes;

namespace CarniceriaNetMaui.Views.Clientes;

public partial class NuevoEditarClienteView : ContentPage
{
    NuevoEditarClienteViewModel NuevoEditarClienteViewModel { get; set; }
    public NuevoEditarClienteView()
	{
		InitializeComponent();
	}

    public NuevoEditarClienteView(Cliente clienteAEditar)
    {
        InitializeComponent();
        NuevoEditarClienteViewModel = this.BindingContext as NuevoEditarClienteViewModel;
        NuevoEditarClienteViewModel.ClienteEditado = clienteAEditar;
        NuevoEditarClienteViewModel.CargarDatosEnPantalla();
    }
}